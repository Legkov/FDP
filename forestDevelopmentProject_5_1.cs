namespace FDP
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Runtime.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.Globalization;
    using System.Xml;
    using System.IO;
    using System.Text;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;


    #region FractionDigitsAttribute
    public class FractionDigitsAttribute : ValidationAttribute
    {
        private readonly uint _decimalPrecision;

        public FractionDigitsAttribute(uint decimalPrecision)
        {
            _decimalPrecision = decimalPrecision;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            if (value is decimal)
                return HasPrecision(value, _decimalPrecision);

            if (value is float)
                return HasPrecision(value, _decimalPrecision);

            if (value is double)
                return HasPrecision(value, _decimalPrecision);

            return false;
        }

        private static bool HasPrecision(object value, uint precision)
        {
            string valueStr = String.Empty;
            if (value is decimal)
                valueStr = ((decimal)value).ToString(CultureInfo.InvariantCulture);

            if (value is float)
                valueStr = ((float)value).ToString(CultureInfo.InvariantCulture);

            if (value is double)
                valueStr = ((double)value).ToString(CultureInfo.InvariantCulture);

            int indexOfDot = valueStr.IndexOf('.');
            if (indexOfDot == -1)
            {
                return true;
            }
            return valueStr.Length - indexOfDot - 1 <= precision;
        }
    }
    #endregion

    #region MaxDigitsAttribute
    public class MaxDigitsAttribute : ValidationAttribute
    {
        private readonly int _max;
        private readonly int _min;

        public MaxDigitsAttribute(int max, int min = 0)
        {
            _max = max;
            _min = min;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!IsValid(value))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return null;
        }

        public override bool IsValid(object value)
        {
            // you could do any custom validation you like if (value is int) {
            var stringValue = "" + (int)value;
            var length = stringValue.Length;
            if (length >= _min && length <= _max)
                return true;

            return false;
        }
    }
    #endregion

    /// <summary>
    /// Описание проекта освоения лесов
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    [XmlRootAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1", IsNullable = false)]
    public class forestDevelopmentProject
    {
        private static XmlSerializer _serializerXml;

        public serviceInfo serviceInfo { get; set; }
        /// <summary>
        /// Преамбула - титульный лист
        /// </summary>
        public forestDevelopmentProjectPreamble preamble { get; set; }
        /// <summary>
        /// I. Общие сведения
        /// </summary>
        public forestDevelopmentProjectGeneral general { get; set; }
        /// <summary>
        /// II. Организация использования лесов (по виду разрешенного использования лесов)
        /// </summary>
        public forestDevelopmentProjectForestUse forestUse { get; set; }
        [XmlArrayItemAttribute(IsNullable = false)]
        public List<file> attachments { get; set; }

        /// <summary>
        /// forestDevelopmentProject class constructor
        /// </summary>
        public forestDevelopmentProject()
        {
            attachments = new List<file>();
            general = new forestDevelopmentProjectGeneral();
            preamble = new forestDevelopmentProjectPreamble();
            serviceInfo = new serviceInfo();
        }

        private static XmlSerializer SerializerXml
        {
            get
            {
                if ((_serializerXml == null))
                {
                    _serializerXml = new XmlSerializerFactory().CreateSerializer(typeof(forestDevelopmentProject));
                }
                return _serializerXml;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serialize forestDevelopmentProject object
        /// </summary>
        /// <returns>XML value</returns>
        public virtual string Serialize()
        {
            StreamReader streamReader = null;
            MemoryStream memoryStream = null;
            try
            {
                memoryStream = new MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                SerializerXml.Serialize(xmlWriter, this);
                memoryStream.Seek(0, SeekOrigin.Begin);
                streamReader = new StreamReader(memoryStream);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes forestDevelopmentProject object
        /// </summary>
        /// <param name="input">string to deserialize</param>
        /// <param name="obj">Output forestDevelopmentProject object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string input, out forestDevelopmentProject obj, out Exception exception)
        {
            exception = null;
            obj = default(forestDevelopmentProject);
            try
            {
                obj = Deserialize(input);
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string input, out forestDevelopmentProject obj)
        {
            Exception exception = null;
            return Deserialize(input, out obj, out exception);
        }

        public static forestDevelopmentProject Deserialize(string input)
        {
            StringReader stringReader = null;
            try
            {
                stringReader = new StringReader(input);
                return ((forestDevelopmentProject)(SerializerXml.Deserialize(XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static forestDevelopmentProject Deserialize(Stream s)
        {
            return ((forestDevelopmentProject)(SerializerXml.Deserialize(s)));
        }
        #endregion

        /// <summary>
        /// Serializes current forestDevelopmentProject object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, out Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName);
                return true;
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual void SaveToFile(string fileName)
        {
            StreamWriter streamWriter = null;
            try
            {
                string dataString = Serialize();
                FileInfo outputFile = new FileInfo(fileName);
                streamWriter = outputFile.CreateText();
                streamWriter.WriteLine(dataString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an forestDevelopmentProject object
        /// </summary>
        /// <param name="fileName">File to load and deserialize</param>
        /// <param name="obj">Output forestDevelopmentProject object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out forestDevelopmentProject obj, out Exception exception)
        {
            exception = null;
            obj = default(forestDevelopmentProject);
            try
            {
                obj = LoadFromFile(fileName);
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out forestDevelopmentProject obj)
        {
            Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static forestDevelopmentProject LoadFromFile(string fileName)
        {
            FileStream file = null;
            StreamReader sr = null;
            try
            {
                file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(file);
                string dataString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(dataString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.3")]
    public class serviceInfo
    {
        public string provider { get; set; }
        public string version { get; set; }
        public string name { get; set; }
        [DefaultValue("51a51362-192a-45c6-8947-33e97f420dbf")]
        public string guid { get; set; }

        /// <summary>
        /// serviceInfo class constructor
        /// </summary>
        public serviceInfo()
        {
            version = "3.0";
            guid = "51a51362-192a-45c6-8947-33e97f420dbf";
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class surveyWorkRow
    {
        public characteristicsWorkRow characteristics { get; set; }
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }

        /// <summary>
        /// surveyWorkRow class constructor
        /// </summary>
        public surveyWorkRow()
        {
            location = new location();
            characteristics = new characteristicsWorkRow();
        }
    }

    /// <summary>
    /// Характеристика и обоснование проектируемых видов и объемов работ
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class characteristicsWorkRow
    {
        /// <summary>
        /// Вид проектируемых работ
        /// </summary>
        public string typeWork { get; set; }
        /// <summary>
        /// Характеристика и обоснование проектируемых работ
        /// </summary>
        public string characteristic { get; set; }
        /// <summary>
        /// Объем проектируемых работ
        /// </summary>
        public string volume { get; set; }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.3")]
    public class location
    {
        [XmlElement("cadastralNumber", typeof(string))]
        [XmlElement("cuttingArea", typeof(string))]
        [XmlElement("forestry", typeof(reference))]
        [XmlElement("quarter", typeof(string))]
        [XmlElement("subforestry", typeof(reference))]
        [XmlElement("taxationUnit", typeof(string))]
        [XmlElement("tract", typeof(reference))]
        [XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items { get; set; }
        [XmlElement("ItemsElementName")]
        [XmlIgnore]
        public ItemsChoiceType[] ItemsElementName { get; set; }
    }

    /// <summary>
    /// Ссылка на справочник (НСИ)
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.3")]
    public class reference
    {
        /// <summary>
        /// Идентификатор НСИ
        /// </summary>
        [XmlAttribute]
        public string id { get; set; }
        /// <summary>
        /// Наименование НСИ
        /// </summary>
        [XmlAttribute]
        public string name { get; set; }
        /// <summary>
        /// Описание элемента (заполняется опционально)
        /// </summary>
        [XmlAttribute]
        public string description { get; set; }
    }

    
    [Serializable]
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.3", IncludeInSchema = false)]
    public enum ItemsChoiceType
    {
        cadastralNumber,
        cuttingArea,
        forestry,
        quarter,
        subforestry,
        taxationUnit,
        tract,
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class surveyWork
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<surveyWorkRow> table { get; set; }

        /// <summary>
        /// surveyWork class constructor
        /// </summary>
        public surveyWork()
        {
            table = new List<surveyWorkRow>();
            content = new contentType();
        }
    }

    /// <summary>
    /// Текстовое и графическое содержимое раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/commonType/4.2")]
    public class contentType
    {
        public descriptionType description { get; set; }
        /// <summary>
        /// Содержимое перед таблицей
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<contentRow> contentBeforeTable { get; set; }
        /// <summary>
        /// Содержимое после таблицы
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<contentRow> contentAfterTable { get; set; }

        /// <summary>
        /// contentType class constructor
        /// </summary>
        public contentType()
        {
            contentAfterTable = new List<contentRow>();
            contentBeforeTable = new List<contentRow>();
            description = new descriptionType();
        }
    }

    /// <summary>
    /// Описание раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/commonType/4.2")]
    public class descriptionType
    {
        /// <summary>
        /// Номер раздела
        /// </summary>
        public string number { get; set; }
        /// <summary>
        /// Название раздела
        /// </summary>
        public string name { get; set; }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/commonType/4.2")]
    public class contentRow
    {
        [XmlElement("image", typeof(file))]
        [XmlElement("text", typeof(string))]
        public object Item { get; set; }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.3")]
    public class file
    {
        /// <summary>
        /// Уникальный идентификатор файла в архиве, может быть:
        /// - guid;
        /// - набор символов уникальный внутри всего xml-документа;
        /// - любой hash файла, например md5.
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// Оригинальное наименование файла (имя.расширение)
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Расширение оригинального файла
        /// </summary>
        public string extension { get; set; }
        /// <summary>
        /// ЭЦП в формате PKCS#7 detached.
        /// </summary>
        [XmlElement(DataType = "base64Binary")]
        public byte[] signature { get; set; }
    }

    /// <summary>
    /// Строка  местоположение проектируемых мероприятий
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class locationMeasureRow
    {
        /// <summary>
        /// Вид мероприятия
        /// </summary>
        public reference measure { get; set; }
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Год проведения
        /// </summary>
        public int year { get; set; }

        /// <summary>
        /// locationMeasureRow class constructor
        /// </summary>
        public locationMeasureRow()
        {
            location = new location();
            measure = new reference();
        }
    }

    /// <summary>
    /// местоположение проектируемых мероприятий
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class locationMeasure
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationMeasureRow> table { get; set; }

        /// <summary>
        /// locationMeasure class constructor
        /// </summary>
        public locationMeasure()
        {
            table = new List<locationMeasureRow>();
            content = new contentType();
        }
    }

    /// <summary>
    /// Местоположение работ
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class locationWorkRow
    {
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Вид проектируемых работ/технология
        /// </summary>
        public string typeWork { get; set; }

        /// <summary>
        /// locationWorkRow class constructor
        /// </summary>
        public locationWorkRow()
        {
            location = new location();
        }
    }

    /// <summary>
    /// Местоположение работ
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class locationWork
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationWorkRow> table { get; set; }

        /// <summary>
        /// locationWork class constructor
        /// </summary>
        public locationWork()
        {
            table = new List<locationWorkRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class treeRegisterRow
    {
        /// <summary>
        /// N дерева
        /// </summary>
        public string number { get; set; }
        /// <summary>
        /// Порода
        /// </summary>
        public reference tree { get; set; }
        /// <summary>
        /// Возраст, лет
        /// </summary>
        public int age { get; set; }
        /// <summary>
        /// Состояние
        /// </summary>
        public string condition { get; set; }
        /// <summary>
        /// Диаметр, см
        /// </summary>
        public float diameter { get; set; }
        /// <summary>
        /// Высота, м
        /// </summary>
        public float height { get; set; }

        /// <summary>
        /// treeRegisterRow class constructor
        /// </summary>
        public treeRegisterRow()
        {
            tree = new reference();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class treeRegister
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<treeRegisterRow> table { get; set; }

        /// <summary>
        /// treeRegister class constructor
        /// </summary>
        public treeRegister()
        {
            table = new List<treeRegisterRow>();
            content = new contentType();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class characteristicsWork
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<characteristicsWorkRow> table { get; set; }

        /// <summary>
        /// characteristicsWork class constructor
        /// </summary>
        public characteristicsWork()
        {
            table = new List<characteristicsWorkRow>();
            content = new contentType();
        }
    }

    /// <summary>
    /// объем проектируемых мероприятий
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class volumeMeasureRow
    {
        /// <summary>
        /// Вид мероприятия
        /// </summary>
        public reference measure { get; set; }
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public reference unitType { get; set; }
        /// <summary>
        /// Объем
        /// </summary>
        [MaxDigitsAttribute(15)]
        [FractionDigitsAttribute(4)]
        [RangeAttribute(0D, 9999999999.9999D)]
        public decimal volume { get; set; }

        /// <summary>
        /// volumeMeasureRow class constructor
        /// </summary>
        public volumeMeasureRow()
        {
            unitType = new reference();
            location = new location();
            measure = new reference();
        }
    }

    /// <summary>
    /// местоположение проектируемых мероприятий
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class volumeMeasure
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<volumeMeasureRow> table { get; set; }

        /// <summary>
        /// volumeMeasure class constructor
        /// </summary>
        public volumeMeasure()
        {
            table = new List<volumeMeasureRow>();
            content = new contentType();
        }
    }

    /// <summary>
    /// Строка местоположения заготовки лесных ресурсов
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class locationResourceRow
    {
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Вид ресурса
        /// </summary>
        public reference resource { get; set; }
        /// <summary>
        /// Способ заготовки
        /// </summary>
        public string typeHarvesting { get; set; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public reference unitType { get; set; }
        /// <summary>
        /// Объем заготовки
        /// </summary>
        [MaxDigitsAttribute(15)]
        [FractionDigitsAttribute(4)]
        [RangeAttribute(0D, 9999999999.9999D)]
        public decimal volume { get; set; }

        /// <summary>
        /// locationResourceRow class constructor
        /// </summary>
        public locationResourceRow()
        {
            unitType = new reference();
            resource = new reference();
            location = new location();
        }
    }

    /// <summary>
    /// местоположения заготовки лесных ресурсов
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class locationResource
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationResourceRow> table { get; set; }

        /// <summary>
        /// locationResource class constructor
        /// </summary>
        public locationResource()
        {
            table = new List<locationResourceRow>();
            content = new contentType();
        }
    }

    /// <summary>
    /// Строка объема заготовки лесных ресурсов
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class volumeResourceRow
    {
        /// <summary>
        /// Вид ресурса (ягоды, грибы)
        /// </summary>
        public reference resource { get; set; }
        /// <summary>
        /// Детальное описание вида ресурса (Виды грибов, ягод, орехов и т.п.)
        /// </summary>
        public string typeResource { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public reference unitType { get; set; }
        /// <summary>
        /// Фонд всего (ежегодный допустимый объем/норматив)
        /// </summary>
        [MaxDigitsAttribute(15)]
        [FractionDigitsAttribute(4)]
        [RangeAttribute(0D, 9999999999.9999D)]
        public decimal fund { get; set; }
        /// <summary>
        /// Проектируемые ежегодные объемы заготовки
        /// </summary>
        [MaxDigitsAttribute(15)]
        [FractionDigitsAttribute(4)]
        [RangeAttribute(0D, 9999999999.9999D)]
        public decimal fundAnnual { get; set; }

        /// <summary>
        /// volumeResourceRow class constructor
        /// </summary>
        public volumeResourceRow()
        {
            unitType = new reference();
            resource = new reference();
        }
    }

    /// <summary>
    /// объема заготовки лесных ресурсов
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class volumeResource
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<volumeResourceRow> table { get; set; }

        /// <summary>
        /// volumeResource class constructor
        /// </summary>
        public volumeResource()
        {
            table = new List<volumeResourceRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class locationResourceResinRow
    {
        public location location { get; set; }
        /// <summary>
        /// Преобладающая порода
        /// </summary>
        public reference tree { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Способ подсочки
        /// </summary>
        public string typeHarvesting { get; set; }
        /// <summary>
        /// Выход живицы, кг/га
        /// </summary>
        [MaxDigitsAttribute(15)]
        [FractionDigitsAttribute(4)]
        [RangeAttribute(0D, 9999999999.9999D)]
        public decimal resinOutlet { get; set; }

        /// <summary>
        /// locationResourceResinRow class constructor
        /// </summary>
        public locationResourceResinRow()
        {
            tree = new reference();
            location = new location();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class locationResourceResin
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationResourceResinRow> table { get; set; }

        /// <summary>
        /// locationResourceResin class constructor
        /// </summary>
        public locationResourceResin()
        {
            table = new List<locationResourceResinRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class volumeResourceResinRow
    {
        /// <summary>
        /// 1. Всего спелых и перестойных насаждений, пригодных для подсочки
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal useful { get; set; }
        /// <summary>
        /// 1.1. Из них не вовлечены в подсочку:
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal completion { get; set; }
        /// <summary>
        /// в том числе нерентабельные
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal unprofitable { get; set; }
        /// <summary>
        /// 2. Может ежегодно находиться в подсочке
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal maybe { get; set; }
        /// <summary>
        /// 3. Фактически находится в подсочке
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal used { get; set; }
        /// <summary>
        /// 4. Вышедшие из подсочки, всего
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal finished { get; set; }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class volumeResourceResin
    {
        public contentType content { get; set; }
        public volumeResourceResinTable table { get; set; }

        /// <summary>
        /// volumeResourceResin class constructor
        /// </summary>
        public volumeResourceResin()
        {
            table = new volumeResourceResinTable();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class volumeResourceResinTable
    {
        public volumeResourceResinRow row { get; set; }
    }

    /// <summary>
    /// Ведомость лесотаксационных выделов, в которых проектируется заготовка древесины
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(TypeName = "locationInfoRow", Namespace = "http://rosleshoz.gov.ru/xmlns/locationCuttingsWoodType/4.2")]
    public class locationInfoRow2
    {
        public location location { get; set; }
        /// <summary>
        /// Преобладающая порода
        /// </summary>
        public reference tree { get; set; }
        /// <summary>
        /// Форма рубки
        /// </summary>
        public formCutting formCutting { get; set; }
        /// <summary>
        /// Указывается элемент справочника "Виды рубок"
        /// </summary>
        public typeCuttingType typeCutting { get; set; }
        /// <summary>
        /// Указывается элемент справочника "Виды рубок"
        /// </summary>
        public string descriptionCutting { get; set; }
        /// <summary>
        /// Планируемый способ лесовосстановления
        /// </summary>
        public string reforestationMethod { get; set; }
        /// <summary>
        /// Всего на лесном участке
        /// </summary>
        public resourcesType7 resources { get; set; }

        /// <summary>
        /// locationInfoRow2 class constructor
        /// </summary>
        public locationInfoRow2()
        {
            resources = new resourcesType7();
            tree = new reference();
            location = new location();
        }
    }

    /// <summary>
    /// Формы рубок
    /// </summary>
    
    [Serializable]
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/sTypes/4.3")]
    public enum formCutting
    {
        [XmlEnumAttribute("Сплошная рубка")]
        Сплошнаярубка,
        [XmlEnumAttribute("Выборочная рубка")]
        Выборочнаярубка,
    }

    /// <summary>
    /// Виды рубок
    /// </summary>
    
    [Serializable]
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/sTypes/4.3")]
    public enum typeCuttingType
    {
        [XmlEnumAttribute("Рубка спелых и перестойных лесных насаждений")]
        Рубкаспелыхиперестойныхлесныхнасаждений,
        [XmlEnumAttribute("Рубка при уходе за лесом")]
        Рубкаприуходезалесом,
        [XmlEnumAttribute("Вырубка погибших и поврежденных лесных насаждений")]
        Вырубкапогибшихиповрежденныхлесныхнасаждений,
        [XmlEnumAttribute("Рубка лесных насаждений на лесных участках, предназначенных для строительства, ре" +
            "конструкции и эксплуатации объектов лесной, лесоперерабатывающей инфраструктуры " +
            "и объектов, не связанных с созданием лесной инфраструктуры")]
        Рубкалесныхнасажденийналесныхучасткахпредназначенныхдлястроительствареконструкциииэксплуатацииобъектовлеснойлесоперерабатывающейинфраструктурыиобъектовнесвязанныхссозданиемлеснойинфраструктуры,
    }

    /// <summary>
    /// Ведомость лесотаксационных выделов, в которых проектируется заготовка древесины
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(TypeName = "resourcesType", Namespace = "http://rosleshoz.gov.ru/xmlns/locationCuttingsWoodType/4.2")]
    public class resourcesType7
    {
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Запас средний на 1 га, м3
        /// </summary>
        public float averageVolume { get; set; }
        /// <summary>
        /// Запас на выделе, м3
        /// </summary>
        public float volume { get; set; }
        /// <summary>
        /// % выборки (для выборочных рубок спелых и перестойных лесных насаждений)
        /// </summary>
        [FractionDigitsAttribute(2)]
        [MaxDigitsAttribute(5)]
        [RangeAttribute(0D, 100D)]
        public decimal samplePercentage { get; set; }
    }

    /// <summary>
    /// Ведомость лесотаксационных выделов, в которых проектируется заготовка древесины
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(TypeName = "specialPurposeInfoRow", Namespace = "http://rosleshoz.gov.ru/xmlns/locationCuttingsWoodType/4.2")]
    public class specialPurposeInfoRow4
    {
        /// <summary>
        /// Всего на лесном участке
        /// </summary>
        public resourcesType7 resources { get; set; }
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public specialPurposeType specialPurpose { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationInfoRow2> locationInfo { get; set; }

        /// <summary>
        /// specialPurposeInfoRow4 class constructor
        /// </summary>
        public specialPurposeInfoRow4()
        {
            locationInfo = new List<locationInfoRow2>();
            resources = new resourcesType7();
        }
    }

    /// <summary>
    /// Виды целевого назначения лесов
    /// </summary>
    
    [Serializable]
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/sTypes/4.3")]
    public enum specialPurposeType
    {
        [XmlEnumAttribute("Защитные леса")]
        Защитныелеса,
        [XmlEnumAttribute("Эксплуатационные леса")]
        Эксплуатационныелеса,
        [XmlEnumAttribute("Резервные леса")]
        Резервныелеса,
    }

    /// <summary>
    /// Ведомость лесотаксационных выделов, в которых проектируется заготовка древесины
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/locationCuttingsWoodType/4.2")]
    public class locationCuttingsWoodType
    {
        /// <summary>
        /// Всего на лесном участке
        /// </summary>
        public resourcesType7 resources { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<specialPurposeInfoRow4> specialPurposeInfo { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationInfoRow2> locationInfoOnAgriculturalLands { get; set; }

        /// <summary>
        /// locationCuttingsWoodType class constructor
        /// </summary>
        public locationCuttingsWoodType()
        {
            resources = new resourcesType7();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class locationCuttingsWood
    {
        public contentType content { get; set; }
        public locationCuttingsWoodType table { get; set; }

        /// <summary>
        /// locationCuttingsWood class constructor
        /// </summary>
        public locationCuttingsWood()
        {
            table = new locationCuttingsWoodType();
            content = new contentType();
        }
    }

    /// <summary>
    /// Средняя характеристика по хозяйству
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(TypeName = "farmInfoRow", Namespace = "http://rosleshoz.gov.ru/xmlns/volumeCuttingsWoodType/4.2")]
    public class farmInfoRow2
    {
        /// <summary>
        /// Хозяйство
        /// </summary>
        public farm farm { get; set; }
        public resourcesType6 resources { get; set; }

        /// <summary>
        /// farmInfoRow2 class constructor
        /// </summary>
        public farmInfoRow2()
        {
            resources = new resourcesType6();
        }
    }

    /// <summary>
    /// Справочник "Хозяйства"
    /// </summary>
    
    [Serializable]
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/sTypes/4.3")]
    public enum farm
    {
        Мягколиственное,
        Твердолиственное,
        Хвойное,
    }

    /// <summary>
    /// Установленный объем заготовки древесины на лесном участке
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(TypeName = "resourcesType", Namespace = "http://rosleshoz.gov.ru/xmlns/volumeCuttingsWoodType/4.2")]
    public class resourcesType6
    {
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// запас (корневой), тыс. м3
        /// </summary>
        public float rootVolume { get; set; }
        /// <summary>
        /// Текстовый комментарий к проекту для вывода.
        /// </summary>
        public float liquidVolume { get; set; }
    }

    /// <summary>
    /// Средняя характеристика по хозяйству
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/volumeCuttingsWoodType/4.2")]
    public class typeCuttingInfoRow
    {
        /// <summary>
        /// Указывается элемент справочника "Виды рубок"
        /// </summary>
        public typeCuttingType typeCutting { get; set; }
        /// <summary>
        /// Форма рубки
        /// </summary>
        public formCutting formCutting { get; set; }
        /// <summary>
        /// Указывается элемент справочника "Виды рубок"
        /// </summary>
        public string descriptionCutting { get; set; }
        public resourcesType6 resources { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<farmInfoRow2> farmInfo { get; set; }

        /// <summary>
        /// typeCuttingInfoRow class constructor
        /// </summary>
        public typeCuttingInfoRow()
        {
            farmInfo = new List<farmInfoRow2>();
            resources = new resourcesType6();
        }
    }

    /// <summary>
    /// Установленный объем заготовки древесины на лесном участке
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(TypeName = "specialPurposeInfoRow", Namespace = "http://rosleshoz.gov.ru/xmlns/volumeCuttingsWoodType/4.2")]
    public class specialPurposeInfoRow3
    {
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public specialPurposeType specialPurpose { get; set; }
        /// <summary>
        /// Всего на лесном участке
        /// </summary>
        public resourcesType6 resources { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<typeCuttingInfoRow> typeCuttingInfo { get; set; }

        /// <summary>
        /// specialPurposeInfoRow3 class constructor
        /// </summary>
        public specialPurposeInfoRow3()
        {
            typeCuttingInfo = new List<typeCuttingInfoRow>();
            resources = new resourcesType6();
        }
    }

    /// <summary>
    /// Установленный объем заготовки древесины на лесном участке
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/volumeCuttingsWoodType/4.2")]
    public class volumeCuttingsWoodType
    {
        /// <summary>
        /// Всего на лесном участке
        /// </summary>
        public resourcesType6 resources { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<specialPurposeInfoRow3> specialPurposeInfo { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<typeCuttingInfoRow> typeCuttingInfo { get; set; }

        /// <summary>
        /// volumeCuttingsWoodType class constructor
        /// </summary>
        public volumeCuttingsWoodType()
        {
            typeCuttingInfo = new List<typeCuttingInfoRow>();
            resources = new resourcesType6();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class volumeCuttingsWood
    {
        public contentType content { get; set; }
        public volumeCuttingsWoodType table { get; set; }

        /// <summary>
        /// volumeCuttingsWood class constructor
        /// </summary>
        public volumeCuttingsWood()
        {
            table = new volumeCuttingsWoodType();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class listProtectionRow
    {
        /// <summary>
        /// Наименование объекта, строения, сооружения
        /// </summary>
        public string objectProtection { get; set; }
        /// <summary>
        /// Проектируемые мероприятия
        /// </summary>
        public string measureProtection { get; set; }
        /// <summary>
        /// Место расположения
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public reference unitType { get; set; }
        /// <summary>
        /// Объем
        /// </summary>
        public float volume { get; set; }

        /// <summary>
        /// listProtectionRow class constructor
        /// </summary>
        public listProtectionRow()
        {
            unitType = new reference();
            location = new location();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class listProtection
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<listProtectionRow> table { get; set; }

        /// <summary>
        /// listProtection class constructor
        /// </summary>
        public listProtection()
        {
            table = new List<listProtectionRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/areaForestCareType/4.2")]
    public class treeRow
    {
        /// <summary>
        /// Порода
        /// </summary>
        public reference tree { get; set; }
        /// <summary>
        /// Вид ухода
        /// </summary>
        public string typeCare { get; set; }
        public resourcesType5 resources { get; set; }

        /// <summary>
        /// treeRow class constructor
        /// </summary>
        public treeRow()
        {
            resources = new resourcesType5();
            tree = new reference();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(TypeName = "resourcesType", Namespace = "http://rosleshoz.gov.ru/xmlns/areaForestCareType/4.2")]
    public class resourcesType5
    {
        /// <summary>
        /// Площадь лесов, нуждающихся в уходе за лесами га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Ежегодная площадь ухода за лесами, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal annualArea { get; set; }
    }

    /// <summary>
    /// Строка целевого назначения
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/areaForestCareType/4.2")]
    public class farmRow
    {
        /// <summary>
        /// Всего
        /// </summary>
        public resourcesType5 resources { get; set; }
        /// <summary>
        /// Хозяйство
        /// </summary>
        public farm farm { get; set; }
        /// <summary>
        /// Категория защитных лесов (только для защитных лесов)
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<treeRow> treeInfo { get; set; }

        /// <summary>
        /// farmRow class constructor
        /// </summary>
        public farmRow()
        {
            treeInfo = new List<treeRow>();
            resources = new resourcesType5();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/areaForestCareType/4.2")]
    public class areaForestCareType
    {
        /// <summary>
        /// Всего
        /// </summary>
        public resourcesType5 resources { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<farmRow> farmInfo { get; set; }

        /// <summary>
        /// areaForestCareType class constructor
        /// </summary>
        public areaForestCareType()
        {
            farmInfo = new List<farmRow>();
            resources = new resourcesType5();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class areaForestCare
    {
        public contentType content { get; set; }
        public areaForestCareType table { get; set; }

        /// <summary>
        /// areaForestCare class constructor
        /// </summary>
        public areaForestCare()
        {
            table = new areaForestCareType();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class listForestCareRow
    {
        /// <summary>
        /// Вид ухода
        /// </summary>
        public string typeCare { get; set; }
        /// <summary>
        /// Место расположения
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Целевая порода
        /// </summary>
        public reference tree { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }

        /// <summary>
        /// listForestCareRow class constructor
        /// </summary>
        public listForestCareRow()
        {
            tree = new reference();
            location = new location();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class listForestCare
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<listForestCareRow> table { get; set; }

        /// <summary>
        /// listForestCare class constructor
        /// </summary>
        public listForestCare()
        {
            table = new List<listForestCareRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class listReforestationRow
    {
        /// <summary>
        /// Категория земель
        /// </summary>
        public reference landCategory { get; set; }
        /// <summary>
        /// Место расположения
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Планируемый способ лесовосстановления
        /// </summary>
        public string conditionsProjectedMethod { get; set; }

        /// <summary>
        /// listReforestationRow class constructor
        /// </summary>
        public listReforestationRow()
        {
            location = new location();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class listReforestation
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<listReforestationRow> table { get; set; }

        /// <summary>
        /// listReforestation class constructor
        /// </summary>
        public listReforestation()
        {
            table = new List<listReforestationRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(TypeName = "resourcesType", Namespace = "http://rosleshoz.gov.ru/xmlns/reforestationPlanType/4.2")]
    public class resourcesType4
    {
        /// <summary>
        /// Итого исскусственное
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal synthetic { get; set; }
        /// <summary>
        /// в т.ч. Посев
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal sowing { get; set; }
        /// <summary>
        /// в т.ч. Посадка
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal landing { get; set; }
        /// <summary>
        /// Комбинированное лесовосстановление
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal combined { get; set; }
        /// <summary>
        /// Естественное лесовосстановление
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal natural { get; set; }
        /// <summary>
        /// Всего
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal total { get; set; }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/reforestationPlanType/4.2")]
    public class landCategoryRow
    {
        /// <summary>
        /// Категория земель
        /// </summary>
        public reference landCategory { get; set; }
        public resourcesType4 resources { get; set; }

        /// <summary>
        /// landCategoryRow class constructor
        /// </summary>
        public landCategoryRow()
        {
            resources = new resourcesType4();
            landCategory = new reference();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/reforestationPlanType/4.2")]
    public class reforestationPlanType
    {
        /// <summary>
        /// Информация по категориям земель
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<landCategoryRow> landCategoryInfo { get; set; }
        /// <summary>
        /// Итого
        /// </summary>
        public resourcesType4 volumes { get; set; }
        /// <summary>
        /// Средние ежегодные объемы лесовосстановления
        /// </summary>
        public resourcesType4 averageAnnualVolumes { get; set; }

        /// <summary>
        /// reforestationPlanType class constructor
        /// </summary>
        public reforestationPlanType()
        {
            averageAnnualVolumes = new resourcesType4();
            volumes = new resourcesType4();
            landCategoryInfo = new List<landCategoryRow>();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class reforestationPlan
    {
        public contentType content { get; set; }
        public reforestationPlanType table { get; set; }

        /// <summary>
        /// reforestationPlan class constructor
        /// </summary>
        public reforestationPlan()
        {
            table = new reforestationPlanType();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class areaNeedReforestationRow
    {
        /// <summary>
        /// Категория земель
        /// </summary>
        public reference landCategory { get; set; }
        /// <summary>
        /// Место расположения
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }

        /// <summary>
        /// areaNeedReforestationRow class constructor
        /// </summary>
        public areaNeedReforestationRow()
        {
            location = new location();
            landCategory = new reference();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class areaNeedReforestation
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<areaNeedReforestationRow> table { get; set; }

        /// <summary>
        /// areaNeedReforestation class constructor
        /// </summary>
        public areaNeedReforestation()
        {
            table = new List<areaNeedReforestationRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class listTaxationUnitRadioactiveZoneRow
    {
        /// <summary>
        /// Место расположения
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Зона загрязнения (слабая, средняя, сильная)
        /// </summary>
        public string contaminatedZone { get; set; }
        /// <summary>
        /// Ограничение использования лесного участка
        /// </summary>
        public string usageRestriction { get; set; }

        /// <summary>
        /// listTaxationUnitRadioactiveZoneRow class constructor
        /// </summary>
        public listTaxationUnitRadioactiveZoneRow()
        {
            location = new location();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class listTaxationUnitRadioactiveZone
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<listTaxationUnitRadioactiveZoneRow> table { get; set; }

        /// <summary>
        /// listTaxationUnitRadioactiveZone class constructor
        /// </summary>
        public listTaxationUnitRadioactiveZone()
        {
            table = new List<listTaxationUnitRadioactiveZoneRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class zonesRadioactiveContaminationRow
    {
        /// <summary>
        /// Место расположения
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Зона загрязнения (слабая, средняя, сильная)
        /// </summary>
        public string contaminatedZone { get; set; }
        /// <summary>
        /// Установка предупреждающих аншлагов, шт.
        /// </summary>
        public float installationWarningBoards { get; set; }
        /// <summary>
        /// Радиационный контроль лесных ресурсов (по видам)
        /// </summary>
        public string radiationControl { get; set; }
        /// <summary>
        /// Дозиметрический контроль при проведении лесохозяйственных работ
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal dosimetricControl { get; set; }
        /// <summary>
        /// Прочие защитные мероприятия
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal otherEvents { get; set; }

        /// <summary>
        /// zonesRadioactiveContaminationRow class constructor
        /// </summary>
        public zonesRadioactiveContaminationRow()
        {
            location = new location();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class zonesRadioactiveContamination
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<zonesRadioactiveContaminationRow> table { get; set; }

        /// <summary>
        /// zonesRadioactiveContamination class constructor
        /// </summary>
        public zonesRadioactiveContamination()
        {
            table = new List<zonesRadioactiveContaminationRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/sanitaryRecreationalMeasuresType/4.2")]
    public class sanritaryRecreationalMeasureRow
    {
        /// <summary>
        /// Вид санитарно-оздоровительного мероприятия
        /// </summary>
        public reference sanritaryRecreationalMeasure { get; set; }
        /// <summary>
        /// Место расположения
        /// </summary>
        public location location { get; set; }
        public resourcesType3 resources { get; set; }
        /// <summary>
        /// Год проведения
        /// </summary>
        public string year { get; set; }
        /// <summary>
        /// Обоснование
        /// </summary>
        public string rationale { get; set; }

        /// <summary>
        /// sanritaryRecreationalMeasureRow class constructor
        /// </summary>
        public sanritaryRecreationalMeasureRow()
        {
            resources = new resourcesType3();
            location = new location();
            sanritaryRecreationalMeasure = new reference();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(TypeName = "resourcesType", Namespace = "http://rosleshoz.gov.ru/xmlns/sanitaryRecreationalMeasuresType/4.2")]
    public class resourcesType3
    {
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Вырубаемый запас (общий), м3
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal volume { get; set; }
        /// <summary>
        /// Вырубаемый запас (ликвидный), м3
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal liquidVolume { get; set; }
        /// <summary>
        /// Вырубаемый запас (деловой), м3
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal businessVolume { get; set; }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(TypeName = "specialPurposeRow", Namespace = "http://rosleshoz.gov.ru/xmlns/sanitaryRecreationalMeasuresType/4.2")]
    public class specialPurposeRow2
    {
        /// <summary>
        /// Всего
        /// </summary>
        public resourcesType3 resources { get; set; }
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public specialPurposeType specialPurpose { get; set; }
        /// <summary>
        /// Категория защитных лесов (только для защитных лесов)
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<sanritaryRecreationalMeasureRow> sanritaryRecreationalMeasureInfo { get; set; }

        /// <summary>
        /// specialPurposeRow2 class constructor
        /// </summary>
        public specialPurposeRow2()
        {
            sanritaryRecreationalMeasureInfo = new List<sanritaryRecreationalMeasureRow>();
            resources = new resourcesType3();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/sanitaryRecreationalMeasuresType/4.2")]
    public class sanitaryRecreationalMeasuresType
    {
        /// <summary>
        /// Всего
        /// </summary>
        public resourcesType3 resources { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<specialPurposeRow2> specialPurposeInfo { get; set; }

        /// <summary>
        /// sanitaryRecreationalMeasuresType class constructor
        /// </summary>
        public sanitaryRecreationalMeasuresType()
        {
            specialPurposeInfo = new List<specialPurposeRow2>();
            resources = new resourcesType3();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class sanitaryRecreationalMeasures
    {
        public contentType content { get; set; }
        public sanitaryRecreationalMeasuresType table { get; set; }

        /// <summary>
        /// sanitaryRecreationalMeasures class constructor
        /// </summary>
        public sanitaryRecreationalMeasures()
        {
            table = new sanitaryRecreationalMeasuresType();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class damageDeathRow
    {
        /// <summary>
        /// Наименование причин повреждения и гибели лесов
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public specialPurposeType specialPurpose { get; set; }
        /// <summary>
        /// Место расположения
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Площадь поврежденных и погибших насаждений нарастающим итогом, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal areaDamage { get; set; }
        /// <summary>
        /// Площадь погибших насаждений нарастающим итогом, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal areaDeath { get; set; }

        /// <summary>
        /// damageDeathRow class constructor
        /// </summary>
        public damageDeathRow()
        {
            location = new location();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class damageDeath
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<damageDeathRow> table { get; set; }

        /// <summary>
        /// damageDeath class constructor
        /// </summary>
        public damageDeath()
        {
            table = new List<damageDeathRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class fociHarmfulOrganismsRow
    {
        /// <summary>
        /// Наименование очагов вредных организмов
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public specialPurposeType specialPurpose { get; set; }
        /// <summary>
        /// Место расположения
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Мероприятия, необходимые для ликвидации очагов вредных организмов
        /// </summary>
        public string measure { get; set; }
        /// <summary>
        /// Общий запас
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal rootVolume { get; set; }
        /// <summary>
        /// Ликвидный запас
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal liquidVolume { get; set; }
        /// <summary>
        /// Деловой запас
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal commodityVolume { get; set; }
        /// <summary>
        /// Год проведения
        /// </summary>
        public int year { get; set; }

        /// <summary>
        /// fociHarmfulOrganismsRow class constructor
        /// </summary>
        public fociHarmfulOrganismsRow()
        {
            location = new location();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class fociHarmfulOrganisms
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<fociHarmfulOrganismsRow> table { get; set; }

        /// <summary>
        /// fociHarmfulOrganisms class constructor
        /// </summary>
        public fociHarmfulOrganisms()
        {
            table = new List<fociHarmfulOrganismsRow>();
            content = new contentType();
        }
    }

    /// <summary>
    /// Строка целевого назначения
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/preventionType/4.2")]
    public class аgriculturalLandsRow
    {
        /// <summary>
        /// Всего
        /// </summary>
        public resourcesType2 resources { get; set; }
        /// <summary>
        /// Категория защитных лесов (только для защитных лесов)
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationRow> locationInfo { get; set; }

        /// <summary>
        /// аgriculturalLandsRow class constructor
        /// </summary>
        public аgriculturalLandsRow()
        {
            locationInfo = new List<locationRow>();
            resources = new resourcesType2();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(TypeName = "resourcesType", Namespace = "http://rosleshoz.gov.ru/xmlns/preventionType/4.2")]
    public class resourcesType2
    {
        /// <summary>
        /// Лечение деревьев (всего), шт.
        /// </summary>
        public float treatmentTtrees { get; set; }
        /// <summary>
        /// Лечение деревьев (ежегодно), шт.
        /// </summary>
        public float treatmentTtreesAnnual { get; set; }
        /// <summary>
        /// Применение пестицидов для предотвращения появления очагов вредных организмов (всего), га
        /// </summary>
        public float pesticideApplication { get; set; }
        /// <summary>
        /// Применение пестицидов для предотвращения появления очагов вредных организмов (ежегодно), га
        /// </summary>
        public float pesticideApplicationAnnual { get; set; }
        /// <summary>
        /// Использование удобрений (всего), га
        /// </summary>
        public float fertilizerUse { get; set; }
        /// <summary>
        /// Использование удобрений (ежегодно), га
        /// </summary>
        public float fertilizerUseAnnual { get; set; }
        /// <summary>
        /// Улучшение условий (всего), шт
        /// </summary>
        public float improvingConditions { get; set; }
        /// <summary>
        /// Улучшение условий (ежегодно), шт
        /// </summary>
        public float improvingConditionsAnnual { get; set; }
        /// <summary>
        /// Охрана местообитаний (всего), га
        /// </summary>
        public float habitatProtection { get; set; }
        /// <summary>
        /// Охрана местообитаний (ежегодно), га
        /// </summary>
        public float habitatProtectionAnnual { get; set; }
        /// <summary>
        /// Посев травянистых нектароносных растений (всего), га
        /// </summary>
        public float sowing { get; set; }
        /// <summary>
        /// Посев травянистых нектароносных растений (ежегодно), га
        /// </summary>
        public float sowingAnnual { get; set; }
        /// <summary>
        /// Использование феромонов (всего), шт.
        /// </summary>
        public float usePheromones { get; set; }
        /// <summary>
        /// Использование феромонов (ежегодно), шт.
        /// </summary>
        public float usePheromonesAnnual { get; set; }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/preventionType/4.2")]
    public class locationRow
    {
        /// <summary>
        /// Место расположения
        /// </summary>
        public location location { get; set; }
        public resourcesType2 resources { get; set; }

        /// <summary>
        /// locationRow class constructor
        /// </summary>
        public locationRow()
        {
            resources = new resourcesType2();
            location = new location();
        }
    }

    /// <summary>
    /// Строка целевого назначения
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/preventionType/4.2")]
    public class specialPurposeRow
    {
        /// <summary>
        /// Всего
        /// </summary>
        public resourcesType2 resources { get; set; }
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public specialPurposeType specialPurpose { get; set; }
        /// <summary>
        /// Категория защитных лесов (только для защитных лесов)
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationRow> locationInfo { get; set; }

        /// <summary>
        /// specialPurposeRow class constructor
        /// </summary>
        public specialPurposeRow()
        {
            locationInfo = new List<locationRow>();
            resources = new resourcesType2();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/preventionType/4.2")]
    public class preventionType
    {
        /// <summary>
        /// Всего
        /// </summary>
        public resourcesType2 resources { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<specialPurposeRow> specialPurposeInfo { get; set; }
        /// <summary>
        /// На землях сельскохозяйственного назначения
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<аgriculturalLandsRow> onAgriculturalLands { get; set; }

        /// <summary>
        /// preventionType class constructor
        /// </summary>
        public preventionType()
        {
            onAgriculturalLands = new List<аgriculturalLandsRow>();
            specialPurposeInfo = new List<specialPurposeRow>();
            resources = new resourcesType2();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class prevention
    {
        public contentType content { get; set; }
        public preventionType table { get; set; }

        /// <summary>
        /// prevention class constructor
        /// </summary>
        public prevention()
        {
            table = new preventionType();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class fireEquipmentRow
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public reference unitType { get; set; }
        /// <summary>
        /// В соответствии с действующими нормативами
        /// </summary>
        public string regulations { get; set; }
        /// <summary>
        /// Имеется в наличии
        /// </summary>
        public float availability { get; set; }
        /// <summary>
        /// Проектируется приобретение, аренда, изготовление
        /// </summary>
        public float need { get; set; }
        /// <summary>
        /// Место расположения
        /// </summary>
        public string position { get; set; }

        /// <summary>
        /// fireEquipmentRow class constructor
        /// </summary>
        public fireEquipmentRow()
        {
            unitType = new reference();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class fireEquipment
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<fireEquipmentRow> table { get; set; }

        /// <summary>
        /// fireEquipment class constructor
        /// </summary>
        public fireEquipment()
        {
            table = new List<fireEquipmentRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class designedFirePreventionRow
    {
        /// <summary>
        /// Объект противопожарного обустройства
        /// </summary>
        public string firePreventionObject { get; set; }
        /// <summary>
        /// Вид мероприятия
        /// </summary>
        public string typeEvent { get; set; }
        /// <summary>
        /// Описание местоположения места складирования пожарного инвентаря
        /// </summary>
        public string position { get; set; }
        public location location { get; set; }
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public specialPurposeType specialPurpose { get; set; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public reference unitType { get; set; }
        /// <summary>
        /// Потребность
        /// </summary>
        public string need { get; set; }
        /// <summary>
        /// В наличие
        /// </summary>
        public string availability { get; set; }
        /// <summary>
        /// Проектируемый общий объем
        /// </summary>
        public string designedTotalVolume { get; set; }
        /// <summary>
        /// Проектируемый ежегодный объем
        /// </summary>
        public string designedAnnualVolume { get; set; }
        /// <summary>
        /// Сроки выполнения мероприятий
        /// </summary>
        public string deadlines { get; set; }

        /// <summary>
        /// designedFirePreventionRow class constructor
        /// </summary>
        public designedFirePreventionRow()
        {
            unitType = new reference();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class designedFirePrevention
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<designedFirePreventionRow> table { get; set; }

        /// <summary>
        /// designedFirePrevention class constructor
        /// </summary>
        public designedFirePrevention()
        {
            table = new List<designedFirePreventionRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class characteristicWaterObjectsRow
    {
        /// <summary>
        /// Водный объект
        /// </summary>
        public string waterObject { get; set; }
        /// <summary>
        /// Расположение ООПТ
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }

        /// <summary>
        /// characteristicWaterObjectsRow class constructor
        /// </summary>
        public characteristicWaterObjectsRow()
        {
            location = new location();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class characteristicWaterObjects
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<characteristicWaterObjectsRow> table { get; set; }

        /// <summary>
        /// characteristicWaterObjects class constructor
        /// </summary>
        public characteristicWaterObjects()
        {
            table = new List<characteristicWaterObjectsRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/characteristicFireClassesType/4.2")]
    public class percentType
    {
        /// <summary>
        /// Площадь I класса пожарной опасности
        /// </summary>
        [FractionDigitsAttribute(2)]
        [MaxDigitsAttribute(5)]
        [RangeAttribute(0D, 100D)]
        public decimal areaFireClasses_I { get; set; }
        /// <summary>
        /// Площадь II класса пожарной опасности
        /// </summary>
        [FractionDigitsAttribute(2)]
        [MaxDigitsAttribute(5)]
        [RangeAttribute(0D, 100D)]
        public decimal areaFireClasses_II { get; set; }
        /// <summary>
        /// Площадь III класса пожарной опасности
        /// </summary>
        [FractionDigitsAttribute(2)]
        [MaxDigitsAttribute(5)]
        [RangeAttribute(0D, 100D)]
        public decimal areaFireClasses_III { get; set; }
        /// <summary>
        /// Площадь IV класса пожарной опасности
        /// </summary>
        [FractionDigitsAttribute(2)]
        [MaxDigitsAttribute(5)]
        [RangeAttribute(0D, 100D)]
        public decimal areaFireClasses_IV { get; set; }
        /// <summary>
        /// Площадь V класса пожарной опасности
        /// </summary>
        [FractionDigitsAttribute(2)]
        [MaxDigitsAttribute(5)]
        [RangeAttribute(0D, 100D)]
        public decimal areaFireClasses_V { get; set; }
        /// <summary>
        /// Площадь общая
        /// </summary>
        [FractionDigitsAttribute(2)]
        [MaxDigitsAttribute(5)]
        [RangeAttribute(0D, 100D)]
        public decimal area { get; set; }

        /// <summary>
        /// percentType class constructor
        /// </summary>
        public percentType()
        {
            area = ((decimal)(100m));
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/characteristicFireClassesType/4.2")]
    public class amountType
    {
        /// <summary>
        /// Площадь I класса пожарной опасности
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal areaFireClasses_I { get; set; }
        /// <summary>
        /// Площадь II класса пожарной опасности
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal areaFireClasses_II { get; set; }
        /// <summary>
        /// Площадь III класса пожарной опасности
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal areaFireClasses_III { get; set; }
        /// <summary>
        /// Площадь IV класса пожарной опасности
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal areaFireClasses_IV { get; set; }
        /// <summary>
        /// Площадь V класса пожарной опасности
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal areaFireClasses_V { get; set; }
        /// <summary>
        /// Площадь общая
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Средний класс
        /// </summary>
        public string middleClass { get; set; }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(TypeName = "locationInfoRow", Namespace = "http://rosleshoz.gov.ru/xmlns/characteristicFireClassesType/4.2")]
    public class locationInfoRow1
    {
        public location location { get; set; }
        /// <summary>
        /// Всего
        /// </summary>
        public amountType amount { get; set; }

        /// <summary>
        /// locationInfoRow1 class constructor
        /// </summary>
        public locationInfoRow1()
        {
            amount = new amountType();
            location = new location();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/characteristicFireClassesType/4.2")]
    public class characteristicFireClassesType
    {
        /// <summary>
        /// местоположение
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationInfoRow1> locationInfo { get; set; }
        /// <summary>
        /// Всего
        /// </summary>
        public amountType amount { get; set; }
        /// <summary>
        /// %
        /// </summary>
        public percentType percent { get; set; }

        /// <summary>
        /// characteristicFireClassesType class constructor
        /// </summary>
        public characteristicFireClassesType()
        {
            percent = new percentType();
            amount = new amountType();
            locationInfo = new List<locationInfoRow1>();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/measuresType/4.2")]
    public class characteristicFireClasses
    {
        public contentType content { get; set; }
        public characteristicFireClassesType table { get; set; }

        /// <summary>
        /// characteristicFireClasses class constructor
        /// </summary>
        public characteristicFireClasses()
        {
            table = new characteristicFireClassesType();
            content = new contentType();
        }
    }

    /// <summary>
    /// Проектируемый объем и местоположение рубок лесных насаждений на лесном участке при использовании лесов не связанных с заготовкой древесины
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class volumeCuttingsRow
    {
        /// <summary>
        /// Проектируемый объект
        /// </summary>
        public reference @object { get; set; }
        public location location { get; set; }
        /// <summary>
        /// Площадь объекта, строения, сооружения
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Корневой запас
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal rootVolume { get; set; }
        /// <summary>
        /// Корневой запас хвойных насаждений
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal rootConiferVolume { get; set; }
        /// <summary>
        /// Ликвидный запас
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal liquidVolume { get; set; }
        /// <summary>
        /// Ликвидный запас хвойных насаждений
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal liquidConiferVolume { get; set; }
        /// <summary>
        /// Год проведения
        /// </summary>
        public int year { get; set; }

        /// <summary>
        /// volumeCuttingsRow class constructor
        /// </summary>
        public volumeCuttingsRow()
        {
            location = new location();
            @object = new reference();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class volumeCuttings
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<volumeCuttingsRow> table { get; set; }

        /// <summary>
        /// volumeCuttings class constructor
        /// </summary>
        public volumeCuttings()
        {
            table = new List<volumeCuttingsRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/objectsForestType/4.2")]
    public class locationInfoRow
    {
        public location location { get; set; }
        /// <summary>
        /// Площадь общая
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        public float length { get; set; }
        public reference measure { get; set; }
        public string characteristic { get; set; }
        public string year { get; set; }

        /// <summary>
        /// locationInfoRow class constructor
        /// </summary>
        public locationInfoRow()
        {
            measure = new reference();
            location = new location();
        }
    }

    /// <summary>
    /// Таблица описания проектируемого объекта в лесном участке
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/objectsForestType/4.2")]
    public class objectsInfoRow
    {
        /// <summary>
        /// Наименование объекта
        /// </summary>
        public reference @object { get; set; }
        /// <summary>
        /// Площадь объекта(га)
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal areaObject { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationInfoRow> locationInfo { get; set; }

        /// <summary>
        /// objectsInfoRow class constructor
        /// </summary>
        public objectsInfoRow()
        {
            locationInfo = new List<locationInfoRow>();
            @object = new reference();
        }
    }

    /// <summary>
    /// 7. Сведения о наличии зданий, сооружений, объектов, связанных с созданием лесной инфраструктуры и объектов, не связанных с созданием лесной инфраструктуры на проектируемом лесном участке
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/objectsForestType/4.2")]
    public class objectsForestType
    {
        /// <summary>
        /// Существующие объекты
        /// </summary>
        public objectsForestTypeExistingObjects existingObjects { get; set; }
        /// <summary>
        /// Проектируемые объекты
        /// </summary>
        public objectsForestTypeProjectedObjects projectedObjects { get; set; }

        /// <summary>
        /// objectsForestType class constructor
        /// </summary>
        public objectsForestType()
        {
            projectedObjects = new objectsForestTypeProjectedObjects();
            existingObjects = new objectsForestTypeExistingObjects();
        }
    }

    /// <summary>
    /// Существующие объекты
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/objectsForestType/4.2")]
    public class objectsForestTypeExistingObjects
    {
        /// <summary>
        /// Объекты лесной инфраструктуры
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<objectsInfoRow> forestInfrastructure { get; set; }
        /// <summary>
        /// Объекты не связанные с созданием лесной инфраструктуры
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<objectsInfoRow> nonforestInfrastructure { get; set; }

        /// <summary>
        /// objectsForestTypeExistingObjects class constructor
        /// </summary>
        public objectsForestTypeExistingObjects()
        {
            nonforestInfrastructure = new List<objectsInfoRow>();
            forestInfrastructure = new List<objectsInfoRow>();
        }
    }

    /// <summary>
    /// Проектируемые объекты
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/objectsForestType/4.2")]
    public class objectsForestTypeProjectedObjects
    {
        /// <summary>
        /// Объекты лесной инфраструктуры
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<objectsInfoRow> forestInfrastructure { get; set; }
        /// <summary>
        /// Объекты не связанные с созданием лесной инфраструктуры
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<objectsInfoRow> nonforestInfrastructure { get; set; }

        /// <summary>
        /// objectsForestTypeProjectedObjects class constructor
        /// </summary>
        public objectsForestTypeProjectedObjects()
        {
            nonforestInfrastructure = new List<objectsInfoRow>();
            forestInfrastructure = new List<objectsInfoRow>();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestUseType/4.2")]
    public class locationObject
    {
        public contentType content { get; set; }
        public objectsForestType table { get; set; }

        /// <summary>
        /// locationObject class constructor
        /// </summary>
        public locationObject()
        {
            table = new objectsForestType();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/infoType/4.2")]
    public class encumbrancesRow
    {
        /// <summary>
        /// Вид обременения
        /// </summary>
        public string typeEncumbrance { get; set; }
        /// <summary>
        /// реквизиты
        /// </summary>
        public string borderArea { get; set; }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/infoType/4.2")]
    public class encumbrances
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<encumbrancesRow> table { get; set; }

        /// <summary>
        /// encumbrances class constructor
        /// </summary>
        public encumbrances()
        {
            table = new List<encumbrancesRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/infoType/4.2")]
    public class surveysRow
    {
        /// <summary>
        /// Материалы изыскания или обследования
        /// </summary>
        public string surveyMaterials { get; set; }
        /// <summary>
        /// реквизиты
        /// </summary>
        public string requisites { get; set; }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/infoType/4.2")]
    public class surveys
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<surveysRow> table { get; set; }

        /// <summary>
        /// surveys class constructor
        /// </summary>
        public surveys()
        {
            table = new List<surveysRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/infoType/4.2")]
    public class rarePlantsAnimalsRow
    {
        /// <summary>
        /// Расположение ООПТ
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Вид, порода
        /// </summary>
        public string species { get; set; }
        /// <summary>
        /// Ограничения
        /// </summary>
        public string restrictions { get; set; }
        /// <summary>
        /// Основание для охраны
        /// </summary>
        public string basisProtection { get; set; }

        /// <summary>
        /// rarePlantsAnimalsRow class constructor
        /// </summary>
        public rarePlantsAnimalsRow()
        {
            location = new location();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/infoType/4.2")]
    public class rarePlantsAnimals
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<rarePlantsAnimalsRow> table { get; set; }

        /// <summary>
        /// rarePlantsAnimals class constructor
        /// </summary>
        public rarePlantsAnimals()
        {
            table = new List<rarePlantsAnimalsRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/infoType/4.2")]
    public class contaminationRow
    {
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Вид загрязнения
        /// </summary>
        public string typeContamination { get; set; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public reference unitType { get; set; }
        /// <summary>
        /// Количественные показатели загрязнения
        /// </summary>
        public string volume { get; set; }

        /// <summary>
        /// contaminationRow class constructor
        /// </summary>
        public contaminationRow()
        {
            unitType = new reference();
            location = new location();
        }
    }

    /// <summary>
    /// Тип описания раздела
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/infoType/4.2")]
    public class contamination
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<contaminationRow> table { get; set; }

        /// <summary>
        /// contamination class constructor
        /// </summary>
        public contamination()
        {
            table = new List<contaminationRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/infoType/4.2")]
    public class speciallyProtectedNaturalAreasRow
    {
        public string speciallyProtectedNaturalArea { get; set; }
        public string position { get; set; }
        public string securityMode { get; set; }
        public string @event { get; set; }
    }

    /// <summary>
    /// Характеристика имеющихся в границах лесного участка особо охраняемых природных территорий и объектов (границы и режим особой охраны), мероприятия по сохранению объектов биоразнообразия
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/infoType/4.2")]
    public class speciallyProtectedNaturalAreas
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<speciallyProtectedNaturalAreasRow> table { get; set; }

        /// <summary>
        /// speciallyProtectedNaturalAreas class constructor
        /// </summary>
        public speciallyProtectedNaturalAreas()
        {
            table = new List<speciallyProtectedNaturalAreasRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/infoType/4.2")]
    public class characteristicsForestPlantationsLandPlotRow
    {
        public farm farm { get; set; }
        public reference tree { get; set; }
        public string composition { get; set; }
        public float height { get; set; }
        public string forestVegetationCover { get; set; }
        public string crownDensityTreeTier { get; set; }
        public string crownDensitysShrubTier { get; set; }

        /// <summary>
        /// characteristicsForestPlantationsLandPlotRow class constructor
        /// </summary>
        public characteristicsForestPlantationsLandPlotRow()
        {
            tree = new reference();
        }
    }

    /// <summary>
    /// Сведения о качественных и количественных характеристиках лесных насаждений на земельном участке из земель сельскохозяйственного назначения
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/infoType/4.2")]
    public class characteristicsForestPlantationsLandPlot
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<characteristicsForestPlantationsLandPlotRow> table { get; set; }

        /// <summary>
        /// characteristicsForestPlantationsLandPlot class constructor
        /// </summary>
        public characteristicsForestPlantationsLandPlot()
        {
            table = new List<characteristicsForestPlantationsLandPlotRow>();
            content = new contentType();
        }
    }

    /// <summary>
    /// Средняя характеристика по преобладающим породам
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/averageTaxationRatesType/4.2")]
    public class treeInfoRow
    {
        /// <summary>
        /// Преобладающая порода
        /// </summary>
        public reference tree { get; set; }
        public resourcesType1 resources { get; set; }

        /// <summary>
        /// treeInfoRow class constructor
        /// </summary>
        public treeInfoRow()
        {
            resources = new resourcesType1();
            tree = new reference();
        }
    }

    /// <summary>
    /// Средняя характеристика по лесному участку
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(TypeName = "resourcesType", Namespace = "http://rosleshoz.gov.ru/xmlns/averageTaxationRatesType/4.2")]
    public class resourcesType1
    {
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        /// <summary>
        /// Возраст насаждения
        /// </summary>
        public int age { get; set; }
        /// <summary>
        /// Бонитет
        /// </summary>
        public reference bonitet { get; set; }
        /// <summary>
        /// Полнота насаждения
        /// </summary>
        public reference completeness { get; set; }
        /// <summary>
        /// Средний запас насаждений покрытых лесной растительностью на 1 га, м.куб
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal volume { get; set; }
        /// <summary>
        /// Средний запас спелых и перестойных насаждений на 1 га, м.куб
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal ripeAndOverripe { get; set; }
        /// <summary>
        /// Средний прирост по запасу на 1 га, м.куб
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal growth { get; set; }
        /// <summary>
        /// Лесной растительный покров
        /// </summary>
        public string composition { get; set; }

        /// <summary>
        /// resourcesType1 class constructor
        /// </summary>
        public resourcesType1()
        {
            completeness = new reference();
            bonitet = new reference();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/averageTaxationRatesType/4.2")]
    public class farmInfoRow
    {
        public farm farm { get; set; }
        public resourcesType1 resources { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<treeInfoRow> treeInfo { get; set; }

        /// <summary>
        /// farmInfoRow class constructor
        /// </summary>
        public farmInfoRow()
        {
            treeInfo = new List<treeInfoRow>();
            resources = new resourcesType1();
        }
    }

    /// <summary>
    /// Средняя характеристика по лесному участку
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(TypeName = "specialPurposeInfoRow", Namespace = "http://rosleshoz.gov.ru/xmlns/averageTaxationRatesType/4.2")]
    public class specialPurposeInfoRow2
    {
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public specialPurposeType specialPurpose { get; set; }
        /// <summary>
        /// Всего по лесному участку
        /// </summary>
        public resourcesType1 resources { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<farmInfoRow> farmInfo { get; set; }

        /// <summary>
        /// specialPurposeInfoRow2 class constructor
        /// </summary>
        public specialPurposeInfoRow2()
        {
            farmInfo = new List<farmInfoRow>();
            resources = new resourcesType1();
        }
    }

    /// <summary>
    /// Средняя характеристика по лесному участку
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/averageTaxationRatesType/4.2")]
    public class averageTaxationRatesType
    {
        /// <summary>
        /// Всего по лесному участку
        /// </summary>
        public resourcesType1 resources { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<specialPurposeInfoRow2> specialPurposeInfo { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<farmInfoRow> farmInfo { get; set; }

        /// <summary>
        /// averageTaxationRatesType class constructor
        /// </summary>
        public averageTaxationRatesType()
        {
            farmInfo = new List<farmInfoRow>();
            specialPurposeInfo = new List<specialPurposeInfoRow2>();
            resources = new resourcesType1();
        }
    }

    /// <summary>
    /// Таксационная характеристика лесных насаждений на лесном участке
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/infoType/4.2")]
    public class averageTaxationRates
    {
        public contentType content { get; set; }
        public averageTaxationRatesType table { get; set; }

        /// <summary>
        /// averageTaxationRates class constructor
        /// </summary>
        public averageTaxationRates()
        {
            table = new averageTaxationRatesType();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/distributionSpecialPurposeType/4.2")]
    public class protectionCategoryInfoRow
    {
        /// <summary>
        /// Категория защитности
        /// </summary>
        public reference protectionCategory { get; set; }
        public resourcesType resources { get; set; }

        /// <summary>
        /// protectionCategoryInfoRow class constructor
        /// </summary>
        public protectionCategoryInfoRow()
        {
            resources = new resourcesType();
            protectionCategory = new reference();
        }
    }

    /// <summary>
    /// Детальная строка
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/distributionSpecialPurposeType/4.2")]
    public class resourcesType
    {
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal area { get; set; }
        public decimal percent { get; set; }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/distributionSpecialPurposeType/4.2")]
    public class groupInfoRow
    {
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public groupType group { get; set; }
        /// <summary>
        /// Категория защитных лесов
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<protectionCategoryInfoRow> protectionCategoryInfo { get; set; }
        public resourcesType resources { get; set; }

        /// <summary>
        /// groupInfoRow class constructor
        /// </summary>
        public groupInfoRow()
        {
            resources = new resourcesType();
            protectionCategoryInfo = new List<protectionCategoryInfoRow>();
        }
    }

    /// <summary>
    /// Группы категорий защитности
    /// </summary>
    
    [Serializable]
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/distributionSpecialPurposeType/4.2")]
    public enum groupType
    {
        [XmlEnumAttribute("леса, расположенные на особо охраняемых природных территориях")]
        лесарасположенныенаособоохраняемыхприродныхтерриториях,
        [XmlEnumAttribute("леса, расположенные в водоохранных зонах")]
        лесарасположенныевводоохранныхзонах,
        [XmlEnumAttribute("леса, выполняющие функции защиты природных и иных объектов")]
        лесавыполняющиефункциизащитыприродныхииныхобъектов,
        [XmlEnumAttribute("ценные леса")]
        ценныелеса,
        [XmlEnumAttribute("городские леса")]
        городскиелеса,
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/distributionSpecialPurposeType/4.2")]
    public class specialPurposeInfoRow
    {
        public specialPurposeType specialPurpose { get; set; }
        public resourcesType resources { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<groupInfoRow> groupInfo { get; set; }

        /// <summary>
        /// specialPurposeInfoRow class constructor
        /// </summary>
        public specialPurposeInfoRow()
        {
            groupInfo = new List<groupInfoRow>();
            resources = new resourcesType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/distributionSpecialPurposeType/4.2")]
    public class distributionSpecialPurposeType
    {
        /// <summary>
        /// Защитные леса
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<specialPurposeInfoRow> specialPurposeInfo { get; set; }
        /// <summary>
        /// Всего
        /// </summary>
        public resourcesType resources { get; set; }

        /// <summary>
        /// distributionSpecialPurposeType class constructor
        /// </summary>
        public distributionSpecialPurposeType()
        {
            resources = new resourcesType();
            specialPurposeInfo = new List<specialPurposeInfoRow>();
        }
    }

    /// <summary>
    /// Распределение площади лесного участка по видам целевого назначения лесов на защитные (по их категориям), эксплуатационные и резервные леса
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/infoType/4.2")]
    public class distributionSpecialPurpose
    {
        public contentType content { get; set; }
        public distributionSpecialPurposeType table { get; set; }

        /// <summary>
        /// distributionSpecialPurpose class constructor
        /// </summary>
        public distributionSpecialPurpose()
        {
            table = new distributionSpecialPurposeType();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/infoType/4.2")]
    public class listLandPlotsRow
    {
        public location location { get; set; }
        public decimal area { get; set; }
        public decimal areaCoveredForest { get; set; }

        /// <summary>
        /// listLandPlotsRow class constructor
        /// </summary>
        public listLandPlotsRow()
        {
            location = new location();
        }
    }

    /// <summary>
    /// Перечень земельных участков из земель сельскохозяйственного назначения, на которых осуществляется использование, охрана, защита, воспроизводство лесов
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/infoType/4.2")]
    public class listLandPlots
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<listLandPlotsRow> table { get; set; }

        /// <summary>
        /// listLandPlots class constructor
        /// </summary>
        public listLandPlots()
        {
            table = new List<listLandPlotsRow>();
            content = new contentType();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/infoType/4.2")]
    public class listQuartersTaxationUnitRow
    {
        public location location { get; set; }
        public decimal area { get; set; }

        /// <summary>
        /// listQuartersTaxationUnitRow class constructor
        /// </summary>
        public listQuartersTaxationUnitRow()
        {
            location = new location();
        }
    }

    /// <summary>
    /// Перечень предоставленных в аренду, постоянное(бессрочное) пользование, в отношении которых установлен сервитут или публичный сервитут, лесных кварталов, лесотаксационных выделов или их частей
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/infoType/4.2")]
    public class listQuartersTaxationUnit
    {
        public contentType content { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<listQuartersTaxationUnitRow> table { get; set; }

        /// <summary>
        /// listQuartersTaxationUnit class constructor
        /// </summary>
        public listQuartersTaxationUnit()
        {
            table = new List<listQuartersTaxationUnitRow>();
            content = new contentType();
        }
    }

    /// <summary>
    /// Описание периода действия проекта
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/commonType/4.2")]
    public class usePeriodType
    {
        /// <summary>
        /// Бессрочный
        /// </summary>
        public bool perpetual { get; set; }
        /// <summary>
        /// Срок использования, лет
        /// </summary>
        [XmlElement(DataType = "integer")]
        public string amountYears { get; set; }
        /// <summary>
        /// Срок использования, месяцев
        /// </summary>
        [XmlElement(DataType = "integer")]
        public string amountMonths { get; set; }
        /// <summary>
        /// Срок использования, дней
        /// </summary>
        [XmlElement(DataType = "integer")]
        public string amountDays { get; set; }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/commonType/4.2")]
    public class partnerListRow
    {
        public physicalPerson partner { get; set; }
        public string usageType { get; set; }
        public contract contract { get; set; }
        public string cadastralNumber { get; set; }
        public string position { get; set; }

        /// <summary>
        /// partnerListRow class constructor
        /// </summary>
        public partnerListRow()
        {
            contract = new contract();
            partner = new physicalPerson();
        }
    }

    /// <summary>
    /// Сведения о лесопользователе
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.3")]
    public class physicalPerson
    {
        /// <summary>
        /// Имя
        /// </summary>
        [MinLengthAttribute(1)]
        public string first_name { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [MinLengthAttribute(1)]
        public string last_name { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string patronimic_name { get; set; }
        /// <summary>
        /// Документ, удостоверяющий личность
        /// </summary>
        public physicalPersonIdentity_document identity_document { get; set; }
        /// <summary>
        /// ИНН физического лица (12 цифр)
        /// </summary>
        [RegularExpressionAttribute("([0-9]{1}[1-9]{1}|[1-9]{1}[0-9]{1})[0-9]{10}")]
        public string inn { get; set; }
        /// <summary>
        /// Местонахождение
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// Контактная информация
        /// </summary>
        public string contactInformation { get; set; }

        /// <summary>
        /// physicalPerson class constructor
        /// </summary>
        public physicalPerson()
        {
            identity_document = new physicalPersonIdentity_document();
        }
    }

    /// <summary>
    /// Документ, удостоверяющий личность
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.3")]
    public class physicalPersonIdentity_document
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Серия
        /// </summary>
        [XmlElement(IsNullable = true)]
        public string series { get; set; }
        /// <summary>
        /// Номер
        /// </summary>
        public string number { get; set; }
    }

    /// <summary>
    /// Описание правоустанавливающего документа
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.3")]
    public class contract
    {
        public string type { get; set; }
        public string number { get; set; }
        [XmlElement(DataType = "date")]
        public System.DateTime date { get; set; }
        public string registrationNumber { get; set; }
        [XmlElement(DataType = "date")]
        public System.DateTime registrationDate { get; set; }

        /// <summary>
        /// contract class constructor
        /// </summary>
        public contract()
        {
            number = "б/н";
        }
    }

    /// <summary>
    /// Сотрудник
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.3")]
    public class employee
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string first_name { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string last_name { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string patronimic_name { get; set; }
        /// <summary>
        /// Должность
        /// </summary>
        public string post { get; set; }
        /// <summary>
        /// Основание для полномочий
        /// </summary>
        public string basisAuthority { get; set; }
        /// <summary>
        /// Номер документа основания полномочий
        /// </summary>
        public string number { get; set; }
        /// <summary>
        /// Дата документа основания полномочий
        /// </summary>
        [XmlElement(DataType = "date")]
        public System.DateTime date { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        public string phone { get; set; }
    }

    /// <summary>
    /// Данные подписи
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.3")]
    public class signerData
    {
        /// <summary>
        /// Данные сотрудника
        /// </summary>
        public employee employee { get; set; }
        /// <summary>
        /// Подписанты документа
        /// </summary>
        [XmlElement(DataType = "date")]
        [DefaultValue(typeof(System.DateTime), "2016-07-15")]
        public System.DateTime date { get; set; }
        /// <summary>
        /// Организация
        /// </summary>
        public string organization { get; set; }

        /// <summary>
        /// signerData class constructor
        /// </summary>
        public signerData()
        {
            employee = new employee();
            date = new System.DateTime(636041376000000000);
        }
    }

    /// <summary>
    /// Сведения о лесопользователе
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.3")]
    public class individualEntrepreneur
    {
        /// <summary>
        /// Имя
        /// </summary>
        [MinLengthAttribute(1)]
        public string first_name { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [MinLengthAttribute(1)]
        public string last_name { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string patronimic_name { get; set; }
        /// <summary>
        /// Документ, удостоверяющий личность
        /// </summary>
        public individualEntrepreneurIdentity_document identity_document { get; set; }
        /// <summary>
        /// Основной государственный регистрационный номер (13 или 15 цифр)
        /// </summary>
        [RegularExpressionAttribute("(1|5|2|3|4)([0-9]{12}|[0-9]{14})")]
        public string ogrn { get; set; }
        /// <summary>
        /// ИНН физического лица (12 цифр)
        /// </summary>
        [XmlElement(IsNullable = true)]
        [RegularExpressionAttribute("([0-9]{1}[1-9]{1}|[1-9]{1}[0-9]{1})[0-9]{10}")]
        public string inn { get; set; }
        /// <summary>
        /// Местонахождение
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// Контактная информация
        /// </summary>
        public string contactInformation { get; set; }

        /// <summary>
        /// individualEntrepreneur class constructor
        /// </summary>
        public individualEntrepreneur()
        {
            inn = "526317984689";
        }
    }

    /// <summary>
    /// Документ, удостоверяющий личность
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.3")]
    public class individualEntrepreneurIdentity_document
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Серия
        /// </summary>
        [XmlElement(IsNullable = true)]
        public string series { get; set; }
        /// <summary>
        /// Номер
        /// </summary>
        public string number { get; set; }
    }

    /// <summary>
    /// Сведения о лесопользователе
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.3")]
    public class juridicalPerson
    {
        /// <summary>
        /// Наименование юридического лица (включая организационно-правовую форму)
        /// </summary>
        [MinLengthAttribute(1)]
        public string name { get; set; }
        /// <summary>
        /// ИНН
        /// </summary>
        [RegularExpressionAttribute("([0-9]{1}[1-9]{1}|[1-9]{1}[0-9]{1})[0-9]{8}")]
        public string inn { get; set; }
        /// <summary>
        /// Основной государственный регистрационный номер (13 или 15 цифр)
        /// </summary>
        [RegularExpressionAttribute("(1|5|2|3|4)([0-9]{12}|[0-9]{14})")]
        public string ogrn { get; set; }
        /// <summary>
        /// Местонахождение
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// Контактная информация
        /// </summary>
        public string contactInformation { get; set; }

        /// <summary>
        /// juridicalPerson class constructor
        /// </summary>
        public juridicalPerson()
        {
            inn = "0256017902";
        }
    }

    /// <summary>
    /// Сведения о контрагенте
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.3")]
    public class partner
    {
        [XmlElement("individualEntrepreneur", typeof(individualEntrepreneur))]
        [XmlElement("juridicalPerson", typeof(juridicalPerson))]
        [XmlElement("physicalPerson", typeof(physicalPerson))]
        public object Item { get; set; }
    }

    /// <summary>
    /// Организация (физ. лицо, ИП)
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/commonType/4.2")]
    public class descriptionPartner
    {
        [XmlElement("partner", typeof(partner))]
        [XmlElement("partnerList", typeof(descriptionPartnerPartnerList))]
        [XmlElement("signerData", typeof(signerData))]
        public List<object> Items { get; set; }

        /// <summary>
        /// descriptionPartner class constructor
        /// </summary>
        public descriptionPartner()
        {
            Items = new List<object>();
        }
    }

    /// <summary>
    /// Перечень лесопользователей (Постановление Правительства РФ от 13 января 2017 г. N 5)
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/commonType/4.2")]
    public class descriptionPartnerPartnerList
    {
        [XmlElement("row")]
        [RequiredAttribute()]
        public List<partnerListRow> row { get; set; }

        /// <summary>
        /// descriptionPartnerPartnerList class constructor
        /// </summary>
        public descriptionPartnerPartnerList()
        {
            row = new List<partnerListRow>();
        }
    }

    /// <summary>
    /// Преамбула - титульный лист
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectPreamble
    {
        /// <summary>
        /// Описание местоположения лесного/земельного участка
        /// </summary>
        public string position { get; set; }
        /// <summary>
        /// Площадь лесного участка, га
        /// </summary>
        [FractionDigitsAttribute(8)]
        [MaxDigitsAttribute(20)]
        [RangeAttribute(0D, 100000000000D)]
        public decimal totalArea { get; set; }
        /// <summary>
        /// Основание для предоставления лесного участка
        /// </summary>
        public string grounds { get; set; }
        /// <summary>
        /// Описание видов использования лесов
        /// </summary>
        public string usageType { get; set; }
        /// <summary>
        /// Описание правоустанавливающего документа
        /// </summary>
        public contract contract { get; set; }
        /// <summary>
        /// Основание для внесение изменений в ПОЛ
        /// </summary>
        public string basisForChange { get; set; }
        /// <summary>
        /// Введение
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<contentRow> introduction { get; set; }
        /// <summary>
        /// Кадастровый номер лесного участка, номер учетной записи в государственном лесном реестре
        /// </summary>
        public string cadastralNumber { get; set; }

        /// <summary>
        /// forestDevelopmentProjectPreamble class constructor
        /// </summary>
        public forestDevelopmentProjectPreamble()
        {
            contract = new contract();
        }
    }

    /// <summary>
    /// I. Общие сведения
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectGeneral
    {
        /// <summary>
        /// Сведения о лице, использующем лесной участок
        /// </summary>
        public descriptionPartner forestUser { get; set; }
        /// <summary>
        /// Описание видов использования лесов
        /// </summary>
        public string usageType { get; set; }
        /// <summary>
        /// Признак проведения изыскательских работ
        /// </summary>
        public bool surveyWorkBool { get; set; }
        /// <summary>
        /// Описание правоустанавливающего документа
        /// </summary>
        public contract contract { get; set; }
        /// <summary>
        /// Срок использования, лет
        /// </summary>
        public usePeriodType usePeriod { get; set; }
        /// <summary>
        /// Сведения об органе государственной власти или органе местного самоуправления, предоставившем лесной участок в аренду или постоянное (бессрочное) пользование, установившем сервитут или предусмотренный статьей 39.37 Земельного кодекса Российской Федерации, публичный сервитут
        /// </summary>
        public reference executiveAuthority { get; set; }
        /// <summary>
        /// Перечень законодательных и иных нормативно-правовых актов, нормативно-технических, методических и проектных документов, на основе которых разработан проект освоения лесов
        /// </summary>
        public string regulations { get; set; }
        /// <summary>
        /// Срок действия проекта освоения лесов
        /// </summary>
        public string validityPeriod { get; set; }
        /// <summary>
        /// Разработчик проекта
        /// </summary>
        public descriptionPartner developer { get; set; }
        /// <summary>
        /// Год последнего лесоустройства
        /// </summary>
        public string yearForestManagement { get; set; }
        /// <summary>
        /// Комментарии к общим сведениям
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<contentRow> comments { get; set; }
        /// <summary>
        /// Сведения о лесном участке
        /// </summary>
        public forestDevelopmentProjectGeneralInfo info { get; set; }
        /// <summary>
        /// Создание и эксплуатация лесной инфраструктуры
        /// </summary>
        public forestDevelopmentProjectGeneralObjects objects { get; set; }
        /// <summary>
        /// Мероприятия по охране, защите и воспроизводству лесов
        /// </summary>
        public forestDevelopmentProjectGeneralMeasures measures { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneral class constructor
        /// </summary>
        public forestDevelopmentProjectGeneral()
        {
            measures = new forestDevelopmentProjectGeneralMeasures();
            objects = new forestDevelopmentProjectGeneralObjects();
            info = new forestDevelopmentProjectGeneralInfo();
            developer = new descriptionPartner();
            executiveAuthority = new reference();
            usePeriod = new usePeriodType();
            contract = new contract();
            forestUser = new descriptionPartner();
        }
    }

    /// <summary>
    /// Сведения о лесном участке
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectGeneralInfo
    {
        /// <summary>
        /// Перечень предоставленных в аренду, постоянное(бессрочное) пользование, в отношении которых установлен сервитут или публичный сервитут, лесных кварталов, лесотаксационных выделов или их частей
        /// </summary>
        public listQuartersTaxationUnit listQuartersTaxationUnit { get; set; }
        /// <summary>
        /// Перечень земельных участков из земель сельскохозяйственного назначения, на которых осуществляется использование, охрана, защита, воспроизводство лесов
        /// </summary>
        public listLandPlots listLandPlots { get; set; }
        /// <summary>
        /// Распределение площади лесного участка по видам целевого назначения лесов на защитные (по их категориям), эксплуатационные и резервные леса
        /// </summary>
        public distributionSpecialPurpose distributionSpecialPurpose { get; set; }
        /// <summary>
        /// Таксационная характеристика лесных насаждений на лесном участке
        /// </summary>
        public averageTaxationRates averageTaxationRates { get; set; }
        /// <summary>
        /// Сведения о качественных и количественных характеристиках лесных насаждений на земельном участке из земель сельскохозяйственного назначения
        /// </summary>
        public characteristicsForestPlantationsLandPlot characteristicsForestPlantationsLandPlot { get; set; }
        /// <summary>
        /// Характеристика имеющихся в границах лесного участка особо охраняемых природных территорий и объектов (границы и режим особой охраны), мероприятия по сохранению объектов биоразнообразия
        /// </summary>
        public speciallyProtectedNaturalAreas speciallyProtectedNaturalAreas { get; set; }
        /// <summary>
        /// Сведения о наличии загрязнения лесов (в том числе нефтяного, радиоактивного)
        /// </summary>
        public contamination contamination { get; set; }
        /// <summary>
        /// Сведения о наличии мест обитания редких и находящихся под угрозой исчезновения видов животных и мест произрастания редких и находящихся под угрозой исчезновения видов деревьев, кустарников, лиан и иных лесных растений
        /// </summary>
        public rarePlantsAnimals rarePlantsAnimals { get; set; }
        /// <summary>
        /// Сведения о материалах специальных изысканий, исследований или иных специальных обследований (при наличии)
        /// </summary>
        public surveys surveys { get; set; }
        /// <summary>
        /// Сведения об обременениях лесного участка
        /// </summary>
        public encumbrances encumbrances { get; set; }
    }

    /// <summary>
    /// I. Общие сведения
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectGeneralObjects
    {
        /// <summary>
        /// Характеристика существующих и проектируемых объектов лесной инфраструктуры на лесном участке
        /// </summary>
        public locationObject locationObject { get; set; }
        /// <summary>
        /// Проектируемый объем рубок лесных насаждений, при создании объектов лесной инфраструктуры
        /// </summary>
        public volumeCuttings volumeCuttings { get; set; }
    }

    /// <summary>
    /// I. Общие сведения
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectGeneralMeasures
    {
        /// <summary>
        /// Характеристика территории лесного участка по классам пожарной опасности
        /// </summary>
        public characteristicFireClasses characteristicFireClasses { get; set; }
        /// <summary>
        /// Характеристика водных объектов
        /// </summary>
        public characteristicWaterObjects characteristicWaterObjects { get; set; }
        /// <summary>
        /// Обоснование и характеристика проектируемых видов, объемов и сроков выполнения мероприятий по противопожарному обустройству лесов с учетом объектов, созданных при использовании лесов в соответствии с лесохозяйственным регламентом лесничества, и их территориальное размещение
        /// </summary>
        public designedFirePrevention designedFirePrevention { get; set; }
        /// <summary>
        /// Сведения о наличии и потребности в пожарной технике,оборудовании, снаряжении и инвентаре на лесном участке
        /// </summary>
        public fireEquipment fireEquipment { get; set; }
        /// <summary>
        /// Обоснование и характеристика видов и объемов планируемых профилактических мероприятий по защите лесов, с указанием мест проведения профилактических мероприятий
        /// </summary>
        public prevention prevention { get; set; }
        /// <summary>
        /// Сведения о наличии очагов вредных организмов на лесном участке, с указанием их местоположения и мероприятий,необходимых для ликвидации очагов вредных организмов
        /// </summary>
        public fociHarmfulOrganisms fociHarmfulOrganisms { get; set; }
        /// <summary>
        /// Сведения о повреждении и гибели лесов на начало действия проекта освоения лесов с указанием их местоположения
        /// </summary>
        public damageDeath damageDeath { get; set; }
        /// <summary>
        /// Обоснование и характеристика видов и объемов планируемых санитарно-оздоровительных мероприятий на лесном участке, с указанием мест проведения санитарно-оздоровительных мероприятий
        /// Для лесных насаждений, расположенных на землях сельскохозяйственного назначения - тег "specialPurpose" не заполняется
        /// </summary>
        public sanitaryRecreationalMeasures sanitaryRecreationalMeasures { get; set; }
        /// <summary>
        /// Обоснование и характеристика проектируемых видов и объемов защитных мероприятий в зонах радиоактивного загрязнения (если таковые имеются)
        /// </summary>
        public zonesRadioactiveContamination zonesRadioactiveContamination { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, входящих в зоны радиоактивного загрязнения (если таковые имеются), с указанием ограничений по видам использования лесных участков и заготовки лесных ресурсов
        /// </summary>
        public listTaxationUnitRadioactiveZone listTaxationUnitRadioactiveZone { get; set; }
        /// <summary>
        /// Площадь земель, нуждающихся в лесовосстановлении
        /// </summary>
        public areaNeedReforestation areaNeedReforestation { get; set; }
        /// <summary>
        /// Плановые способы и объемы лесовосстановления
        /// </summary>
        public reforestationPlan reforestationPlan { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых планируются мероприятия по лесовосстановлению
        /// </summary>
        public listReforestation listReforestation { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов (земельных участков из земель сельскохозяйственного назначения), в которых проектируются мероприятия по уходу за лесами
        /// </summary>
        public listForestCare listForestCare { get; set; }
        /// <summary>
        /// Площадь лесов, нуждающихся в уходе за лесами, проектируемые виды и ежегодные объемы ухода за лесами при воспроизводстве лесов, не связанные с заготовкой древесины
        /// </summary>
        public areaForestCare areaForestCare { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов (земельных участков из земель сельскохозяйственного назначения), в которых проектируются мероприятия по охране объектов животного и растительного мира, водных объектов
        /// </summary>
        public listProtection listProtection { get; set; }
    }

    /// <summary>
    /// Описание проекта освоения лесов
    /// </summary>
    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectForestUse
    {
        public forestDevelopmentProjectForestUseWoodHarvesting woodHarvesting { get; set; }
        public forestDevelopmentProjectForestUseResinHarvesting resinHarvesting { get; set; }
        public forestDevelopmentProjectForestUseNonTimberResourcesHarvesting nonTimberResourcesHarvesting { get; set; }
        public forestDevelopmentProjectForestUseFoodForestrResourcesHarvesting foodForestrResourcesHarvesting { get; set; }
        public forestDevelopmentProjectForestUseHunting hunting { get; set; }
        public forestDevelopmentProjectForestUseAgriculture agriculture { get; set; }
        public forestDevelopmentProjectForestUseFishing fishing { get; set; }
        public forestDevelopmentProjectForestUseResearchActivities researchActivities { get; set; }
        public forestDevelopmentProjectForestUseRecreation recreation { get; set; }
        public forestDevelopmentProjectForestUseForestPlantations forestPlantations { get; set; }
        public forestDevelopmentProjectForestUseForestNurseries forestNurseries { get; set; }
        public forestDevelopmentProjectForestUsePlantGrowing plantGrowing { get; set; }
        public forestDevelopmentProjectForestUseGeology geology { get; set; }
        public forestDevelopmentProjectForestUseHydrology hydrology { get; set; }
        public forestDevelopmentProjectForestUseLinearObjects linearObjects { get; set; }
        public forestDevelopmentProjectForestUseTimberProcessing timberProcessing { get; set; }
        public forestDevelopmentProjectForestUseSurveyWork surveyWork { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUse class constructor
        /// </summary>
        public forestDevelopmentProjectForestUse()
        {
            surveyWork = new forestDevelopmentProjectForestUseSurveyWork();
            timberProcessing = new forestDevelopmentProjectForestUseTimberProcessing();
            linearObjects = new forestDevelopmentProjectForestUseLinearObjects();
            hydrology = new forestDevelopmentProjectForestUseHydrology();
            geology = new forestDevelopmentProjectForestUseGeology();
            plantGrowing = new forestDevelopmentProjectForestUsePlantGrowing();
            forestNurseries = new forestDevelopmentProjectForestUseForestNurseries();
            forestPlantations = new forestDevelopmentProjectForestUseForestPlantations();
            recreation = new forestDevelopmentProjectForestUseRecreation();
            researchActivities = new forestDevelopmentProjectForestUseResearchActivities();
            fishing = new forestDevelopmentProjectForestUseFishing();
            agriculture = new forestDevelopmentProjectForestUseAgriculture();
            hunting = new forestDevelopmentProjectForestUseHunting();
            foodForestrResourcesHarvesting = new forestDevelopmentProjectForestUseFoodForestrResourcesHarvesting();
            nonTimberResourcesHarvesting = new forestDevelopmentProjectForestUseNonTimberResourcesHarvesting();
            resinHarvesting = new forestDevelopmentProjectForestUseResinHarvesting();
            woodHarvesting = new forestDevelopmentProjectForestUseWoodHarvesting();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectForestUseWoodHarvesting
    {
        public volumeCuttingsWood volumeCuttingsWood { get; set; }
        public volumeCuttingsWood volumeCuttingsWoodOnAgriculturalLands { get; set; }
        public locationCuttingsWood locationCuttingsWood { get; set; }
        public locationCuttingsWood locationCuttingsWoodOnAgriculturalLands { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseWoodHarvesting class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseWoodHarvesting()
        {
            locationCuttingsWoodOnAgriculturalLands = new locationCuttingsWood();
            locationCuttingsWood = new locationCuttingsWood();
            volumeCuttingsWoodOnAgriculturalLands = new volumeCuttingsWood();
            volumeCuttingsWood = new volumeCuttingsWood();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectForestUseResinHarvesting
    {
        public string technologies { get; set; }
        public volumeResourceResin volumeResourceResin { get; set; }
        public locationResourceResin locationResourceResin { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseResinHarvesting class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseResinHarvesting()
        {
            locationResourceResin = new locationResourceResin();
            volumeResourceResin = new volumeResourceResin();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectForestUseNonTimberResourcesHarvesting
    {
        public string technologies { get; set; }
        public volumeResource volumeResource { get; set; }
        public locationResource locationResource { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseNonTimberResourcesHarvesting class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseNonTimberResourcesHarvesting()
        {
            locationResource = new locationResource();
            volumeResource = new volumeResource();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectForestUseFoodForestrResourcesHarvesting
    {
        public string characteristics { get; set; }
        public string technologies { get; set; }
        public volumeResource volumeResource { get; set; }
        public locationResource locationResource { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseFoodForestrResourcesHarvesting class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseFoodForestrResourcesHarvesting()
        {
            locationResource = new locationResource();
            volumeResource = new volumeResource();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectForestUseHunting
    {
        public volumeCuttings volumeCuttings { get; set; }
        public volumeMeasure volumeMeasure { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseHunting class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseHunting()
        {
            volumeMeasure = new volumeMeasure();
            volumeCuttings = new volumeCuttings();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectForestUseAgriculture
    {
        public string technologies { get; set; }
        public volumeResource volumeResource { get; set; }
        public volumeMeasure volumeMeasure { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseAgriculture class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseAgriculture()
        {
            volumeMeasure = new volumeMeasure();
            volumeResource = new volumeResource();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectForestUseFishing
    {
        public characteristicsWork characteristicsWork { get; set; }
        public locationObject locationObject { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseFishing class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseFishing()
        {
            locationObject = new locationObject();
            characteristicsWork = new characteristicsWork();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectForestUseResearchActivities
    {
        public characteristicsWork characteristicsWork { get; set; }
        public volumeMeasure volumeMeasure { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseResearchActivities class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseResearchActivities()
        {
            volumeMeasure = new volumeMeasure();
            characteristicsWork = new characteristicsWork();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectForestUseRecreation
    {
        public treeRegister treeRegister { get; set; }
        public locationObject locationCapitalObject { get; set; }
        public locationObject locationNonCapitalObject { get; set; }
        public volumeCuttings volumeCuttings { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseRecreation class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseRecreation()
        {
            volumeCuttings = new volumeCuttings();
            locationNonCapitalObject = new locationObject();
            locationCapitalObject = new locationObject();
            treeRegister = new treeRegister();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectForestUseForestPlantations
    {
        public characteristicsWork characteristicsWork { get; set; }
        public locationWork locationWork { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseForestPlantations class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseForestPlantations()
        {
            locationWork = new locationWork();
            characteristicsWork = new characteristicsWork();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectForestUseForestNurseries
    {
        public characteristicsWork characteristicsWork { get; set; }
        public locationWork locationWork { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseForestNurseries class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseForestNurseries()
        {
            locationWork = new locationWork();
            characteristicsWork = new characteristicsWork();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectForestUsePlantGrowing
    {
        public characteristicsWork characteristicsWork { get; set; }
        public volumeMeasure volumeMeasure { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUsePlantGrowing class constructor
        /// </summary>
        public forestDevelopmentProjectForestUsePlantGrowing()
        {
            volumeMeasure = new volumeMeasure();
            characteristicsWork = new characteristicsWork();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectForestUseGeology
    {
        public characteristicsWork characteristicsWork { get; set; }
        public locationObject locationObject { get; set; }
        public volumeCuttings volumeCuttings { get; set; }
        public locationMeasure locationMeasure { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseGeology class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseGeology()
        {
            locationMeasure = new locationMeasure();
            volumeCuttings = new volumeCuttings();
            locationObject = new locationObject();
            characteristicsWork = new characteristicsWork();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectForestUseHydrology
    {
        public characteristicsWork characteristicsWork { get; set; }
        public locationObject locationObject { get; set; }
        public volumeCuttings volumeCuttings { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseHydrology class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseHydrology()
        {
            volumeCuttings = new volumeCuttings();
            locationObject = new locationObject();
            characteristicsWork = new characteristicsWork();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectForestUseLinearObjects
    {
        public characteristicsWork characteristicsWork { get; set; }
        public locationObject locationObject { get; set; }
        public volumeCuttings volumeCuttings { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseLinearObjects class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseLinearObjects()
        {
            volumeCuttings = new volumeCuttings();
            locationObject = new locationObject();
            characteristicsWork = new characteristicsWork();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectForestUseTimberProcessing
    {
        public characteristicsWork characteristicsWork { get; set; }
        public locationObject locationObject { get; set; }
        public volumeCuttings volumeCuttings { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseTimberProcessing class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseTimberProcessing()
        {
            volumeCuttings = new volumeCuttings();
            locationObject = new locationObject();
            characteristicsWork = new characteristicsWork();
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/5.1")]
    public class forestDevelopmentProjectForestUseSurveyWork
    {
        public surveyWork characteristicsWork { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseSurveyWork class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseSurveyWork()
        {
            characteristicsWork = new surveyWork();
        }
    }
}
