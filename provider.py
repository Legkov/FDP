from qgis.PyQt.QtWidgets import QAction, QFileDialog, QDialog, QVBoxLayout, QListWidget, QDialogButtonBox
from qgis.PyQt.QtCore import QFileInfo, QVariant
from qgis.core import (
    QgsDataProvider,
    QgsVectorDataProvider,
    QgsFields,
    QgsField,
    QgsFeature,
    QgsGeometry,
    QgsPointXY,
    QgsCoordinateReferenceSystem,
    QgsRectangle,
    QgsVectorLayer,
    QgsProviderRegistry,
    QgsProviderMetadata,
    QgsDataProviderUri,
    QgsApplication,
    QgsProject,
    QgsWkbTypes,
    QgsDataItem,
    QgsLayerItem,
    QgsDataCollectionItem,
    QgsAbstractFeatureSource,
    QgsFeatureRequest,
    QgsFeatureIterator,
    QgsAbstractFeatureIterator,
    QgsExpressionContext,
    QgsExpression,
    QgsExpressionContextUtils
)
from qgis.gui import QgsDataItemProvider
import json
import os

class MultiLayerData:
    """Класс для чтения многослойных файлов"""
    @staticmethod
    def read_file(path):
        """Читает файл и возвращает словарь слоев"""
        layers = {}
        try:
            with open(path, 'r', encoding='utf-8') as f:
                data = json.load(f)
                
            for layer_name, layer_data in data.items():
                # Поля
                fields = QgsFields()
                for field_info in layer_data['fields']:
                    # Преобразование строкового типа в QVariant.Type
                    type_map = {
                        'int': QVariant.Int,
                        'double': QVariant.Double,
                        'string': QVariant.String,
                        'bool': QVariant.Bool,
                        'date': QVariant.Date,
                        'datetime': QVariant.DateTime
                    }
                    qtype = type_map.get(field_info['type'], QVariant.String)
                    fields.append(QgsField(field_info['name'], qtype))
                
                # Объекты
                features = []
                feature_dict = {}  # Словарь для быстрого доступа по ID
                for feature_data in layer_data['features']:
                    feat = QgsFeature()
                    fid = feature_data['id']
                    feat.setId(fid)
                    feat.setFields(fields)
                    
                    # Атрибуты
                    for i, attr in enumerate(feature_data['attributes']):
                        feat.setAttribute(i, attr)
                    
                    # Геометрия
                    geom_type = feature_data['geometry']['type']
                    coords = feature_data['geometry']['coordinates']
                    
                    if geom_type == 'Point':
                        geom = QgsGeometry.fromPointXY(QgsPointXY(coords[0], coords[1]))
                    elif geom_type == 'LineString':
                        points = [QgsPointXY(p[0], p[1]) for p in coords]
                        geom = QgsGeometry.fromPolylineXY(points)
                    elif geom_type == 'Polygon':
                        rings = []
                        for ring in coords:
                            points = [QgsPointXY(p[0], p[1]) for p in ring]
                            rings.append(points)
                        geom = QgsGeometry.fromPolygonXY(rings)
                    else:
                        continue
                    
                    feat.setGeometry(geom)
                    features.append(feat)
                    feature_dict[fid] = feat
                
                # Экстент
                extent = QgsRectangle()
                for feat in features:
                    if not feat.geometry().isNull():
                        feat_extent = feat.geometry().boundingBox()
                        if extent.isEmpty():
                            extent = feat_extent
                        else:
                            extent.combineExtentWith(feat_extent)
                
                layers[layer_name] = {
                    'fields': fields,
                    'features': features,
                    'feature_dict': feature_dict,  # Словарь для быстрого доступа
                    'extent': extent,
                    'crs': QgsCoordinateReferenceSystem(layer_data['crs'])
                }
                
            return layers
        except Exception as e:
            print(f"Ошибка чтения файла: {str(e)}")
            return {}

class MyFeatureIterator(QgsAbstractFeatureIterator):
    """Кастомный итератор объектов с поддержкой пространственных запросов"""
    def __init__(self, source, request):
        super().__init__(request)
        self.source = source
        self.request = request
        self.index = 0
        self.closed = False
        self.filter_point = None
        
        # Если запрос содержит точку для идентификации, преобразуем ее в CRS слоя
        if request.filterType() == QgsFeatureRequest.FilterPoint:
            point = request.filterPoint()
            # Преобразуем точку в CRS слоя, если необходимо
            if point.crs().authid() != self.source.crs.authid():
                transform = QgsCoordinateTransform(point.crs(), self.source.crs, QgsProject.instance())
                self.filter_point = transform.transform(point)
            else:
                self.filter_point = point
        elif request.filterType() == QgsFeatureRequest.FilterRect:
            self.filter_rect = request.filterRect()

    def nextFeature(self, feature):
        """Возвращает следующий объект"""
        if self.closed:
            return False
            
        # Пропускаем объекты, которые не соответствуют фильтру
        while self.index < len(self.source.features):
            feat = self.source.features[self.index]
            self.index += 1
            
            # Проверяем пространственный фильтр (прямоугольник)
            if self.request.filterType() == QgsFeatureRequest.FilterRect:
                if not feat.geometry().intersects(self.filter_rect):
                    continue
                    
            # Проверяем фильтр по точке (для инструмента идентификации)
            elif self.request.filterType() == QgsFeatureRequest.FilterPoint:
                # Используем небольшой прямоугольник для учета допуска
                tolerance = 0.0001  # Допуск в градусах (для географических CRS) или в единицах проекции
                rect = QgsRectangle(
                    self.filter_point.x() - tolerance,
                    self.filter_point.y() - tolerance,
                    self.filter_point.x() + tolerance,
                    self.filter_point.y() + tolerance
                )
                if not feat.geometry().intersects(rect):
                    continue
                    
            # Проверяем атрибутивный фильтр (исправленная версия)
            elif self.request.filterType() == QgsFeatureRequest.FilterExpression:
                # Создаем контекст для вычисления выражения
                context = QgsExpressionContext()
                context.appendScope(QgsExpressionContextUtils.globalScope())
                context.appendScope(QgsExpressionContextUtils.projectScope(QgsProject.instance()))
                context.setFeature(feat)
                
                # Вычисляем выражение
                expression = self.request.filterExpression()
                try:
                    if not expression.evaluate(context).toBool():
                        continue
                except:
                    # В случае ошибки пропускаем объект
                    continue
            
            # Проверяем фильтр по идентификатору
            elif self.request.filterType() == QgsFeatureRequest.FilterFid:
                if feat.id() != self.request.filterFid():
                    continue
            
            # Применяем подстроку фильтра (subsetString), если она задана
            if self.source.provider().subsetString():
                # Создаем контекст для вычисления выражения
                context = QgsExpressionContext()
                context.appendScope(QgsExpressionContextUtils.globalScope())
                context.appendScope(QgsExpressionContextUtils.projectScope(QgsProject.instance()))
                context.setFeature(feat)
                
                # Вычисляем выражение подстроки фильтра
                subset_expression = QgsExpression(self.source.provider().subsetString())
                try:
                    if not subset_expression.evaluate(context).toBool():
                        continue
                except:
                    # В случае ошибки пропускаем объект
                    continue
            
            # Копируем объект
            feature.setFields(feat.fields())
            feature.setId(feat.id())
            feature.setAttributes(feat.attributes())
            feature.setGeometry(feat.geometry().clone())
            return True
            
        return False

    def rewind(self):
        """Перематывает итератор в начало"""
        if self.closed:
            return False
        self.index = 0
        return True

    def close(self):
        """Закрывает итератор"""
        self.closed = True
        return True

class MyFeatureSource(QgsAbstractFeatureSource):
    """Реализация источника объектов для провайдера"""
    def __init__(self, features, feature_dict, fields, extent, crs, provider):
        super().__init__()
        self.features = features
        self.feature_dict = feature_dict  # Словарь для быстрого доступа
        self.fields = fields
        self.extent = extent
        self.crs = crs
        self.provider = provider  # Ссылка на провайдер

    def getFeatures(self, request: QgsFeatureRequest) -> QgsFeatureIterator:
        """Возвращает итератор объектов"""
        return MyFeatureIterator(self, request)

    def fetchFeature(self, fid: int) -> QgsFeature:
        """Возвращает объект по идентификатору"""
        if fid in self.feature_dict:
            feat = self.feature_dict[fid]
            new_feature = QgsFeature(feat.fields())
            new_feature.setId(feat.id())
            new_feature.setAttributes(feat.attributes())
            if feat.hasGeometry():
                new_feature.setGeometry(feat.geometry().clone())
            return new_feature
        return QgsFeature()

    def extent(self) -> QgsRectangle:
        return self.extent

    def fields(self) -> QgsFields:
        return self.fields

    def uniqueValues(self, fieldIndex: int, limit: int = -1) -> set:
        """Возвращает уникальные значения для поля"""
        values = set()
        for feat in self.features:
            values.add(feat.attribute(fieldIndex))
            if limit > 0 and len(values) >= limit:
                break
        return values

class PythonVectorProvider(QgsVectorDataProvider):
    """Провайдер для чтения многослойных файлов"""
    
    def __init__(self, uri: str, options: QgsDataProvider.ProviderOptions):
        super().__init__(uri, options)
        self.uri = uri
        self.fields = QgsFields()
        self.features = []
        self.feature_dict = {}
        self.extent = QgsRectangle()
        self.crs = QgsCoordinateReferenceSystem("EPSG:4326")
        self.valid = False
        self.feature_source = None
        self.mSubsetString = ""  # Подстрока фильтра
        
        # Разбор URI: mydata:///path/to/file.json|layer=layer_name
        parts = uri.split('|')
        file_path = parts[0].replace("mydata://", "")
        layer_name = parts[1].split('=')[1] if len(parts) > 1 else None
        
        self.load_file(file_path, layer_name)
        
        if self.valid:
            self.feature_source = MyFeatureSource(
                self.features, 
                self.feature_dict,
                self.fields, 
                self.extent, 
                self.crs,
                self  # Передаем ссылку на провайдер
            )
    
    def load_file(self, path: str, layer_name: str = None):
        """Загружает данные из файла для указанного слоя"""
        layers = MultiLayerData.read_file(path)
        
        if not layers:
            return
            
        # Если слой не указан, берем первый
        if not layer_name:
            layer_name = list(layers.keys())[0]
            
        if layer_name not in layers:
            return
            
        layer_data = layers[layer_name]
        self.fields = layer_data['fields']
        self.features = layer_data['features']
        self.feature_dict = layer_data['feature_dict']
        self.extent = layer_data['extent']
        self.crs = layer_data['crs']
        self.valid = True
    
    def featureSource(self) -> QgsAbstractFeatureSource:
        """Реализация абстрактного метода"""
        return self.feature_source
    
    # --- Обязательные методы ---
    def name(self) -> str:
        return "python_vector_provider"
    
    def description(self) -> str:
        return "Python Multi-Layer Data Provider"
    
    def getFeatures(self, request: QgsFeatureRequest = QgsFeatureRequest()) -> QgsFeatureIterator:
        """Возвращает итератор объектов"""
        if self.feature_source:
            return self.feature_source.getFeatures(request)
        return QgsFeatureIterator()  # Пустой итератор
    
    def fetchFeature(self, fid: int) -> QgsFeature:
        """Возвращает объект по идентификатору"""
        if self.feature_source:
            return self.feature_source.fetchFeature(fid)
        return QgsFeature()
    
    def featureCount(self) -> int:
        return len(self.features) if self.valid else 0
    
    def fields(self) -> QgsFields:
        return self.fields
    
    def wkbType(self):
        if self.features:
            return self.features[0].geometry().wkbType()
        return QgsWkbTypes.Unknown
    
    def capabilities(self) -> int:
        # Добавляем поддержку подстроки фильтра
        return QgsVectorDataProvider.SelectAtId | QgsVectorDataProvider.SetSubsetString
    
    def extent(self) -> QgsRectangle:
        return self.extent
    
    def crs(self) -> QgsCoordinateReferenceSystem:
        return self.crs
    
    def isValid(self) -> bool:
        return self.valid
    
    # --- Поддержка подстроки фильтра ---
    def setSubsetString(self, subset: str) -> bool:
        """Устанавливает подстроку фильтра"""
        self.mSubsetString = subset
        # Сообщаем об изменении данных
        self.dataChanged.emit()
        return True
    
    def subsetString(self) -> str:
        """Возвращает текущую подстроку фильтра"""
        return self.mSubsetString

class PythonVectorProviderMetadata(QgsProviderMetadata):
    """Метаданные для регистрации провайдера"""
    
    def __init__(self):
        super().__init__(
            "python_vector_provider", 
            "Python Multi-Layer Provider", 
            "mydata://"
        )
    
    def createProvider(self, uri: str, options: QgsDataProvider.ProviderOptions, flags: QgsDataProvider.ReadFlags):
        return PythonVectorProvider(uri, options)
    
    def encodeUri(self, parts: dict) -> str:
        """Кодирует параметры в URI"""
        return f"mydata://{parts['path']}|layer={parts['layer']}"
    
    def decodeUri(self, uri: str) -> dict:
        """Декодирует URI в параметры"""
        parts = uri.split('|')
        file_path = parts[0].replace("mydata://", "")
        layer = parts[1].split('=')[1] if len(parts) > 1 else None
        return {'path': file_path, 'layer': layer}

# Остальные классы (LayerDialog, MyDataCollectionItem, MyDataItemProvider, MyPlugin) без изменений
