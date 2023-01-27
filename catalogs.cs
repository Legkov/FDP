#pragma warning disable
namespace FDP
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Runtime.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.Xml;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;
    
    
    /// <summary>
    /// Список справочников
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogs/4.1")]
    [XmlRootAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogs/4.1", IsNullable=false)]
    public class catalog
    {
        private static XmlSerializer _serializerXml;
        /// <summary>
        /// Справочник "Субъекты РФ"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<subject> subject { get; set; }
        /// <summary>
        /// Справочник "Субъекты РФ"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<landType> landType { get; set; }
        /// <summary>
        /// Справочник "Органы государственной власти, органы местного самоуправления"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<executiveAuthority> executiveAuthority { get; set; }
        /// <summary>
        /// Справочник "Лесничества"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<forestry> forestry { get; set; }
        /// <summary>
        /// Справочник "Участковые лесничества"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<subforestry> subforestry { get; set; }
        /// <summary>
        /// Справочник "Урочища"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<tract> tract { get; set; }
        /// <summary>
        /// Справочник "Древесные породы"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<tree> tree { get; set; }
        /// <summary>
        /// Справочник "Породы древесины"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<wood> wood { get; set; }
        /// <summary>
        /// Справочник "Сортименты"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<sortiment> sortiment { get; set; }
        /// <summary>
        /// Справочник "Типы рубок"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<typeCutting> typeCutting { get; set; }
        /// <summary>
        /// Справочник "Бонитеты" TODO:!!!!!!!!!!!!!!!!
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<bonitet> bonitet { get; set; }
        /// <summary>
        /// Справочник "Единицы измерения"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<unitType> unitType { get; set; }
        /// <summary>
        /// Справочник "Виды использования лесов"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<usageType> usageType { get; set; }
        /// <summary>
        /// Справочник "Вид заготавливаемых лесных ресурсов"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<resource> resource { get; set; }
        /// <summary>
        /// Справочник "Мероприятия"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<measure> measure { get; set; }
        /// <summary>
        /// Справочник "Объекты инфраструктуры"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<@object> @object { get; set; }
        /// <summary>
        /// Справочник "Целевые назначения лесов"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<specialPurpose> specialPurpose { get; set; }
        /// <summary>
        /// Справочник "Категории защитных лесов"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<protectionCategory> protectionCategory { get; set; }
        /// <summary>
        /// Справочник "Показатели отчетов"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<reportRate> reportRate { get; set; }
        /// <summary>
        /// Справочник "Группы показателей отчетов"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<reportRateType> reportRateType { get; set; }
        /// <summary>
        /// Справочник "Муниципальных образований"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<municipalDistrict> municipalDistrict { get; set; }
        /// <summary>
        /// Справочник "Причины повреждения"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<damageReason> damageReason { get; set; }
        /// <summary>
        /// Справочник "Признаки повреждения"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<damageSymptom> damageSymptom { get; set; }
        /// <summary>
        /// Справочник "Категории состояния деревьев"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<treeStateCategory> treeStateCategory { get; set; }
        /// <summary>
        /// Справочник "Мероприятия по защите лесов"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<@event> @event { get; set; }
        /// <summary>
        /// Справочник "Фазы развития очага вредных организмов"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<pestSourceGrowthPhase> pestSourceGrowthPhase { get; set; }
        /// <summary>
        /// Справочник "Лесные районы"
        /// </summary>
        [XmlArrayItemAttribute("element", IsNullable=false)]
        public List<forestArea> forestArea { get; set; }
        
        private static XmlSerializer SerializerXml
        {
            get
            {
                if ((_serializerXml == null))
                {
                    _serializerXml = new XmlSerializerFactory().CreateSerializer(typeof(catalog));
                }
                return _serializerXml;
            }
        }
        
        #region Serialize/Deserialize
        /// <summary>
        /// Serialize catalog object
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
                xmlWriterSettings.Indent = true;
                xmlWriterSettings.IndentChars = "  ";
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
        /// Deserializes catalog object
        /// </summary>
        /// <param name="input">string to deserialize</param>
        /// <param name="obj">Output catalog object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string input, out catalog obj, out Exception exception)
        {
            exception = null;
            obj = default(catalog);
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
        
        public static bool Deserialize(string input, out catalog obj)
        {
            Exception exception = null;
            return Deserialize(input, out obj, out exception);
        }
        
        public static catalog Deserialize(string input)
        {
            StringReader stringReader = null;
            try
            {
                stringReader = new StringReader(input);
                return ((catalog)(SerializerXml.Deserialize(XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }
        
        public static catalog Deserialize(Stream s)
        {
            return ((catalog)(SerializerXml.Deserialize(s)));
        }
        #endregion
        
        /// <summary>
        /// Serializes current catalog object into file
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
        /// Deserializes xml markup from file into an catalog object
        /// </summary>
        /// <param name="fileName">File to load and deserialize</param>
        /// <param name="obj">Output catalog object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out catalog obj, out Exception exception)
        {
            exception = null;
            obj = default(catalog);
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
        
        public static bool LoadFromFile(string fileName, out catalog obj)
        {
            Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }
        
        public static catalog LoadFromFile(string fileName)
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
    
    /// <summary>
    /// Справочник "Субъекты РФ"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class subject : reference
    {
    }
    
    /// <summary>
    /// Ссылка на справочник (НСИ)
    /// </summary>
    [XmlInclude(typeof(floor))]
    [XmlInclude(typeof(especiallyProtectiveArea))]
    [XmlInclude(typeof(pestSourceGrowthPhase))]
    [XmlInclude(typeof(@event))]
    [XmlInclude(typeof(treeStateCategory))]
    [XmlInclude(typeof(damageSymptom))]
    [XmlInclude(typeof(damageReason))]
    [XmlInclude(typeof(nameLand))]
    [XmlInclude(typeof(forestArea))]
    [XmlInclude(typeof(municipalDistrict))]
    [XmlInclude(typeof(reportRateType))]
    [XmlInclude(typeof(reportRate))]
    [XmlInclude(typeof(protectionCategory))]
    [XmlInclude(typeof(specialPurpose))]
    [XmlInclude(typeof(@object))]
    [XmlInclude(typeof(measure))]
    [XmlInclude(typeof(resource))]
    [XmlInclude(typeof(usageType))]
    [XmlInclude(typeof(unitType))]
    [XmlInclude(typeof(bonitet))]
    [XmlInclude(typeof(typeCutting))]
    [XmlInclude(typeof(sortiment))]
    [XmlInclude(typeof(wood))]
    [XmlInclude(typeof(tree))]
    [XmlInclude(typeof(tract))]
    [XmlInclude(typeof(subforestry))]
    [XmlInclude(typeof(forestry))]
    [XmlInclude(typeof(executiveAuthority))]
    [XmlInclude(typeof(landType))]
    [XmlInclude(typeof(subject))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
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
    
    /// <summary>
    /// Ярусы насаждений.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class floor : reference
    {
    }
    
    /// <summary>
    /// Особо-защитные участки (ОЗУ)
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class especiallyProtectiveArea : reference
    {
    }
    
    /// <summary>
    /// Справочник "Фазы развития очага вредных организмов"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class pestSourceGrowthPhase : reference
    {
    }
    
    /// <summary>
    /// Справочник "Мероприятия по защите лесов"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class @event : reference
    {
    }
    
    /// <summary>
    /// Справочник "Категории состояния деревьев"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class treeStateCategory : reference
    {
    }
    
    /// <summary>
    /// Справочник "Признаки повреждения"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class damageSymptom : reference
    {
        public reference parent { get; set; }
        
        /// <summary>
        /// damageSymptom class constructor
        /// </summary>
        public damageSymptom()
        {
            parent = new reference();
        }
    }
    
    /// <summary>
    /// Справочник "Причины повреждения"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class damageReason : reference
    {
        public string reduction { get; set; }
        public reference parent { get; set; }
        
        /// <summary>
        /// damageReason class constructor
        /// </summary>
        public damageReason()
        {
            parent = new reference();
        }
    }
    
    /// <summary>
    /// Наименования земель лесного фонда
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class nameLand : reference
    {
    }
    
    /// <summary>
    /// Справочник "Лесные районы"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class forestArea : reference
    {
    }
    
    /// <summary>
    /// Справочник "Муниципальных образований"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class municipalDistrict : reference
    {
        public reference parent { get; set; }
        public municipalDistrictType type { get; set; }
        
        /// <summary>
        /// municipalDistrict class constructor
        /// </summary>
        public municipalDistrict()
        {
            parent = new reference();
        }
    }
    
    /// <summary>
    /// Типы муниципальных образований
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/sTypes/4.1")]
    public enum municipalDistrictType
    {
        Регион,
        [XmlEnumAttribute("Район/Город")]
        РайонГород,
        Поселение,
        [XmlEnumAttribute("Населенный пункт")]
        Населенныйпункт,
    }
    
    /// <summary>
    /// Вид показателя
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class reportRateType : reference
    {
    }
    
    /// <summary>
    /// Справочник "Показатели отчетов"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class reportRate : reference
    {
        public reference parent { get; set; }
        public reference reportRateType { get; set; }
        public reference unitType { get; set; }
        public string codeLine { get; set; }
        
        /// <summary>
        /// reportRate class constructor
        /// </summary>
        public reportRate()
        {
            unitType = new reference();
            reportRateType = new reference();
            parent = new reference();
        }
    }
    
    /// <summary>
    /// Справочник "Категории защитных лесов"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class protectionCategory : reference
    {
        public reference parent { get; set; }
        
        /// <summary>
        /// protectionCategory class constructor
        /// </summary>
        public protectionCategory()
        {
            parent = new reference();
        }
    }
    
    /// <summary>
    /// Справочник "Целевые назначения лесов"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class specialPurpose : reference
    {
    }
    
    /// <summary>
    /// Справочник "Объекты инфраструктуры"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class @object : reference
    {
        public typeObject typeObject { get; set; }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/sTypes/4.1")]
    public enum typeObject
    {
        [XmlEnumAttribute("Объекты лесной инфраструктуры")]
        Объектылеснойинфраструктуры,
        [XmlEnumAttribute("Объекты лесного семеноводства")]
        Объектылесногосеменоводства,
        [XmlEnumAttribute("Объекты, не связанные с созданием лесной инфраструктуры")]
        Объектынесвязанныессозданиемлеснойинфраструктуры,
    }
    
    /// <summary>
    /// Справочник "Мероприятия"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class measure : reference
    {
    }
    
    /// <summary>
    /// Справочник "Вид заготавливаемых лесных ресурсов"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class resource : reference
    {
        public reference usageType { get; set; }
        
        /// <summary>
        /// resource class constructor
        /// </summary>
        public resource()
        {
            usageType = new reference();
        }
    }
    
    /// <summary>
    /// Справочник "Виды использования лесов"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class usageType : reference
    {
    }
    
    /// <summary>
    /// Справочник "Единицы измерения"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class unitType : reference
    {
        public string abbreviation { get; set; }
    }
    
    /// <summary>
    /// Справочник "Бонитеты" TODO:!!!!!!!!!!!!!!!!
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class bonitet : reference
    {
    }
    
    /// <summary>
    /// Справочник "Типы рубок"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class typeCutting : reference
    {
    }
    
    /// <summary>
    /// Справочник "Сортименты"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class sortiment : reference
    {
    }
    
    /// <summary>
    /// Справочник "Породы древесины"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class wood : reference
    {
        public string okpd { get; set; }
        public string tnved { get; set; }
    }
    
    /// <summary>
    /// Справочник "Древесные породы"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class tree : reference
    {
        public string abbreviation { get; set; }
    }
    
    /// <summary>
    /// Справочник "Урочища"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class tract : reference
    {
        public reference subject { get; set; }
        public reference subforestry { get; set; }
        
        /// <summary>
        /// tract class constructor
        /// </summary>
        public tract()
        {
            subforestry = new reference();
            subject = new reference();
        }
    }
    
    /// <summary>
    /// Справочник "Участковые лесничества"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class subforestry : reference
    {
        public reference subject { get; set; }
        public reference forestry { get; set; }
        
        /// <summary>
        /// subforestry class constructor
        /// </summary>
        public subforestry()
        {
            forestry = new reference();
            subject = new reference();
        }
    }
    
    /// <summary>
    /// Справочник "Лесничества"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class forestry : reference
    {
        public reference subject { get; set; }
        public reference landType { get; set; }
        
        /// <summary>
        /// forestry class constructor
        /// </summary>
        public forestry()
        {
            landType = new reference();
            subject = new reference();
        }
    }
    
    /// <summary>
    /// Справочник "Органы государственной власти, органы местного самоуправления"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class executiveAuthority : reference
    {
        public reference subject { get; set; }
        public reference landType { get; set; }
        
        /// <summary>
        /// executiveAuthority class constructor
        /// </summary>
        public executiveAuthority()
        {
            landType = new reference();
            subject = new reference();
        }
    }
    
    /// <summary>
    /// Справочник "Субъекты РФ"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class landType : reference
    {
    }
}
#pragma warning restore
