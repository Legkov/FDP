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
    using System.Globalization;
    using System.Xml;
    using System.IO;
    using System.Text;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    
    
    #region StringLengthListAttribute
    public class StringLengthListAttribute : StringLengthAttribute
    {
    public StringLengthListAttribute(int maximumLength)
        : base(maximumLength) { }

    public override bool IsValid(object value)
    {
        if (value==null)
           return true;

        foreach (var str in value as IEnumerable<string>)
        {
            if (str.Length > MaximumLength || str.Length < MinimumLength)
                return false;
        }

        return true;
    }


    }
    #endregion
    
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    [XmlRootAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2", IsNullable=false)]
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
        /// <summary>
        /// Описание структуры документа
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable=false)]
        public List<documentStructureRow> documentStructure { get; set; }
        [XmlArrayItemAttribute(IsNullable=false)]
        public List<file> attachments { get; set; }
        
        /// <summary>
        /// forestDevelopmentProject class constructor
        /// </summary>
        public forestDevelopmentProject()
        {
            attachments = new List<file>();
            documentStructure = new List<documentStructureRow>();
            forestUse = new forestDevelopmentProjectForestUse();
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
    public class file
    {
        public string id { get; set; }
        public string name { get; set; }
        public string extension { get; set; }
        [XmlElement(DataType="base64Binary")]
        public byte[] signature { get; set; }
    }
    
    /// <summary>
    /// Строка описание структуры документа. Может содержать:
    /// section - Наименование заголовка раздела (Ведение сельского хозяйства)
    /// text - Текстовое содержимое раздела (Произвольный текст)
    /// table- Номер таблицы из атрибута таблицы "number" ("25")
    /// image - имя графического файла в транспортном контейнере (изображение абриса и прочего)
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class documentStructureRow
    {
        [XmlElement("image", typeof(file))]
        [XmlElement("section", typeof(string))]
        [XmlElement("table", typeof(string))]
        [XmlElement("text", typeof(string))]
        [XmlChoiceIdentifierAttribute("ItemElementName")]
        public object Item { get; set; }
        [XmlIgnore]
        public ItemChoiceType ItemElementName { get; set; }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2", IncludeInSchema=false)]
    public enum ItemChoiceType
    {
        image,
        section,
        table,
        text,
    }
    
    /// <summary>
    /// Местоположение работ
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class locationWorkRow
    {
        /// <summary>
        /// Вид проектируемых работ
        /// </summary>
        public string typeWork { get; set; }
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal area { get; set; }
        
        /// <summary>
        /// locationWorkRow class constructor
        /// </summary>
        public locationWorkRow()
        {
            location = new location();
        }
    }
    
    /// <summary>
    /// Описание местоположения лесного участка
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
    public class location
    {
        public reference forestry { get; set; }
        public reference subforestry { get; set; }
        public reference tract { get; set; }
        public string quarter { get; set; }
        public string taxationUnit { get; set; }
        public string cuttingArea { get; set; }
        
        /// <summary>
        /// location class constructor
        /// </summary>
        public location()
        {
            tract = new reference();
            subforestry = new reference();
            forestry = new reference();
        }
    }
    
    /// <summary>
    /// Ссылка на справочник (НСИ)
    /// </summary>
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
        [Required]
        public string name { get; set; }
        /// <summary>
        /// Описание элемента (заполняется опционально)
        /// </summary>
        [XmlAttribute]
        public string description { get; set; }
    }
    
    /// <summary>
    /// Характеристика и обоснование проектируемых видов и объемов работ
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
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
    
    /// <summary>
    /// Строка  местоположение проектируемых мероприятий
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
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
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
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
    /// Строка местоположения заготовки лесных ресурсов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class locationResourceRow
    {
        public location location { get; set; }
        /// <summary>
        /// Вид недревесного лесного ресурса
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
    /// Строка объема заготовки лесных ресурсов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class volumeResourceRow
    {
        /// <summary>
        /// Вид ресурса
        /// </summary>
        public reference resource { get; set; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public reference unitType { get; set; }
        /// <summary>
        /// Фонд всего
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
    /// Проектируемый объем и местоположение рубок лесных насаждений на лесном участке при использовании лесов не связанных с заготовкой древесины
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
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
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
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
        /// volumeCuttingsRow class constructor
        /// </summary>
        public volumeCuttingsRow()
        {
            location = new location();
            @object = new reference();
        }
    }
    
    /// <summary>
    /// Строка характеристика и местоположение существующих и проектируемых объектов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class locationObjectRow
    {
        /// <summary>
        /// Наименование объекта, строения, сооружения
        /// </summary>
        public reference @object { get; set; }
        /// <summary>
        /// Проектируемые мероприятия
        /// </summary>
        public reference measure { get; set; }
        public location location { get; set; }
        /// <summary>
        /// Площадь объекта, строения, сооружения
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal area { get; set; }
        /// <summary>
        /// Протяженность объекта
        /// </summary>
        public float length { get; set; }
        /// <summary>
        /// Характеристика объекта
        /// </summary>
        public string characteristic { get; set; }
        /// <summary>
        /// Объем рубок(корневой запас), м3
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal rootVolume { get; set; }
        /// <summary>
        /// Объем рубок(в том числе хвойные корневой запас), м3
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal rootConiferVolume { get; set; }
        /// <summary>
        /// Объем рубок(ликвидный запас), м3
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal liquidVolume { get; set; }
        /// <summary>
        /// Объем рубок(в том числе хвойные ликвидный запас), м3
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal liquidConiferVolume { get; set; }
        
        /// <summary>
        /// locationObjectRow class constructor
        /// </summary>
        public locationObjectRow()
        {
            location = new location();
            @object = new reference();
        }
    }
    
    /// <summary>
    /// Данные сотрудника
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
    public class employee
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string patronimic_name { get; set; }
        public string post { get; set; }
        public string basisAuthority { get; set; }
        public string number { get; set; }
        [XmlElement(DataType="date")]
        public System.DateTime date { get; set; }
        public string phone { get; set; }
    }
    
    /// <summary>
    /// Данные подписи
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
    public class signerData
    {
        /// <summary>
        /// Данные сотрудника
        /// </summary>
        public employee employee { get; set; }
        /// <summary>
        /// Подписанты документа
        /// </summary>
        [XmlElement(DataType="date")]
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
    /// Индивидуальный предприниматель
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
    public class individualEntrepreneur
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string patronimic_name { get; set; }
        [XmlElement(IsNullable=true)]
        public string inn { get; set; }
        
        /// <summary>
        /// individualEntrepreneur class constructor
        /// </summary>
        public individualEntrepreneur()
        {
            inn = "526317984689";
        }
    }
    
    /// <summary>
    /// Физ. лицо
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
    public class physicalPerson
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string patronimic_name { get; set; }
        public physicalPersonIdentity_document identity_document { get; set; }
        
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
    public class physicalPersonIdentity_document
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Серия
        /// </summary>
        [XmlElement(IsNullable=true)]
        public string series { get; set; }
        /// <summary>
        /// Номер
        /// </summary>
        public string number { get; set; }
    }
    
    /// <summary>
    /// Юр.лицо
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
    public class juridicalPerson
    {
        public string name { get; set; }
        public string inn { get; set; }
        
        /// <summary>
        /// juridicalPerson class constructor
        /// </summary>
        public juridicalPerson()
        {
            inn = "0256017902";
        }
    }
    
    /// <summary>
    /// Сведения о лице, использующем лесной участок
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
    public class partner
    {
        [XmlElement("individualEntrepreneur", typeof(individualEntrepreneur))]
        [XmlElement("juridicalPerson", typeof(juridicalPerson))]
        [XmlElement("physicalPerson", typeof(physicalPerson))]
        public object Item { get; set; }
    }
    
    /// <summary>
    /// Описание правоустанавливающего документа
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
    public class contract
    {
        public string type { get; set; }
        public string number { get; set; }
        [XmlElement(DataType="date")]
        public System.DateTime date { get; set; }
        
        /// <summary>
        /// contract class constructor
        /// </summary>
        public contract()
        {
            number = "б/н";
        }
    }
    
    /// <summary>
    /// Преамбула - титульный лист
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectPreamble
    {
        /// <summary>
        /// Описание местоположения лесного участка
        /// </summary>
        public string location { get; set; }
        /// <summary>
        /// Площадь лесного участка, га
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneral
    {
        /// <summary>
        /// Сведения о лице, использующем лесной участок
        /// </summary>
        public partner partner { get; set; }
        /// <summary>
        /// Описание видов использования лесов
        /// </summary>
        public string usageType { get; set; }
        /// <summary>
        /// Адрес юридического или физического лица
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// Контактная информация
        /// </summary>
        public string contactInformation { get; set; }
        /// <summary>
        /// Описание правоустанавливающего документа
        /// </summary>
        public contract contract { get; set; }
        /// <summary>
        /// Срок использования, лет
        /// </summary>
        public float usePeriod { get; set; }
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
        [XmlElement(DataType="date")]
        public System.DateTime validityPeriod { get; set; }
        /// <summary>
        /// Разработчик проекта
        /// </summary>
        public signerData developer { get; set; }
        /// <summary>
        /// Год последнего лесоустройства
        /// </summary>
        public int yearForestManagement { get; set; }
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
            developer = new signerData();
            executiveAuthority = new reference();
            contract = new contract();
            partner = new partner();
        }
    }
    
    /// <summary>
    /// Сведения о лесном участке
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralInfo
    {
        /// <summary>
        /// Перечень предоставленных в аренду, постоянное(бессрочное) пользование, в отношении которых установлен сервитут или публичный сервитут, лесных кварталов, лесотаксационных выделов или их частей
        /// </summary>
        public forestDevelopmentProjectGeneralInfoListQuartersTaxationUnit listQuartersTaxationUnit { get; set; }
        /// <summary>
        /// Распределение площади лесного участка по видам целевого назначения лесов на защитные (по их категориям), эксплуатационные и резервные леса
        /// </summary>
        public forestDevelopmentProjectGeneralInfoDistributionSpecialPurpose distributionSpecialPurpose { get; set; }
        /// <summary>
        /// Таксационная характеристика лесных насажденийна лесном участке
        /// </summary>
        public forestDevelopmentProjectGeneralInfoAverageTaxationRates averageTaxationRates { get; set; }
        /// <summary>
        /// Характеристика имеющихся в границах лесного участка особо охраняемых природных территорий и объектов (границы и режим особой охраны), мероприятия по сохранению объектов биоразнообразия
        /// </summary>
        public forestDevelopmentProjectGeneralInfoSpeciallyProtectedNaturalAreas speciallyProtectedNaturalAreas { get; set; }
        /// <summary>
        /// Сведения о наличии загрязнения лесов (в том числе нефтяного, радиоактивного)
        /// </summary>
        public forestDevelopmentProjectGeneralInfoContamination contamination { get; set; }
        /// <summary>
        /// Сведения о наличии мест обитания редких и находящихся под угрозой исчезновения видов животных и мест произрастания редких и находящихся под угрозой исчезновения видов деревьев, кустарников, лиан и иных лесных растений
        /// </summary>
        public forestDevelopmentProjectGeneralInfoRarePlantsAnimals rarePlantsAnimals { get; set; }
        /// <summary>
        /// Сведения о материалах специальных изысканий, исследований или иных специальных обследований (при наличии)
        /// </summary>
        public forestDevelopmentProjectGeneralInfoSurveys surveys { get; set; }
        /// <summary>
        /// Сведения об обременениях лесного участка
        /// </summary>
        public forestDevelopmentProjectGeneralInfoEncumbrances encumbrances { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralInfo class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfo()
        {
            averageTaxationRates = new forestDevelopmentProjectGeneralInfoAverageTaxationRates();
            distributionSpecialPurpose = new forestDevelopmentProjectGeneralInfoDistributionSpecialPurpose();
            listQuartersTaxationUnit = new forestDevelopmentProjectGeneralInfoListQuartersTaxationUnit();
        }
    }
    
    /// <summary>
    /// Перечень предоставленных в аренду, постоянное(бессрочное) пользование, в отношении которых установлен сервитут или публичный сервитут, лесных кварталов, лесотаксационных выделов или их частей
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralInfoListQuartersTaxationUnit
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralInfoListQuartersTaxationUnitRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralInfoListQuartersTaxationUnit class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoListQuartersTaxationUnit()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralInfoListQuartersTaxationUnitRow
    {
        /// <summary>
        /// В поле taxationUnit может присутствовать только одно значение (без перечисления через запятую или тире
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Площадь эксплуатационная
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal area { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralInfoListQuartersTaxationUnitRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoListQuartersTaxationUnitRow()
        {
            location = new location();
        }
    }
    
    /// <summary>
    /// Распределение площади лесного участка по видам целевого назначения лесов на защитные (по их категориям), эксплуатационные и резервные леса
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralInfoDistributionSpecialPurpose
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralInfoDistributionSpecialPurposeRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralInfoDistributionSpecialPurpose class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoDistributionSpecialPurpose()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralInfoDistributionSpecialPurposeRow
    {
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public reference specialPurpose { get; set; }
        /// <summary>
        /// Категория защитных лесов
        /// </summary>
        public reference protectionCategory { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal area { get; set; }
        /// <summary>
        /// Процент
        /// </summary>
        [FractionDigitsAttribute(2)]
        [MaxDigitsAttribute(5)]
        [RangeAttribute(0D, 100D)]
        public decimal percent { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralInfoDistributionSpecialPurposeRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoDistributionSpecialPurposeRow()
        {
            specialPurpose = new reference();
        }
    }
    
    /// <summary>
    /// Таксационная характеристика лесных насажденийна лесном участке
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralInfoAverageTaxationRates
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralInfoAverageTaxationRatesRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralInfoAverageTaxationRates class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoAverageTaxationRates()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralInfoAverageTaxationRatesRow
    {
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public reference specialPurpose { get; set; }
        /// <summary>
        /// Справочник "Хозяйства"
        /// </summary>
        public farm farm { get; set; }
        /// <summary>
        /// Преобладающая порода (Указывается элемент справочника "Породы древесины")
        /// </summary>
        public reference tree { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
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
        public decimal completeness { get; set; }
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
        /// Состав насаждения
        /// </summary>
        public string composition { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralInfoAverageTaxationRatesRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoAverageTaxationRatesRow()
        {
            bonitet = new reference();
            tree = new reference();
            specialPurpose = new reference();
        }
    }
    
    /// <summary>
    /// Справочник "Хозяйства"
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/sTypes/4.1")]
    public enum farm
    {
        Мягколиственное,
        Твердолиственное,
        Хвойное,
    }
    
    /// <summary>
    /// Характеристика имеющихся в границах лесного участка особо охраняемых природных территорий и объектов (границы и режим особой охраны), мероприятия по сохранению объектов биоразнообразия
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralInfoSpeciallyProtectedNaturalAreas
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralInfoSpeciallyProtectedNaturalAreasRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralInfoSpeciallyProtectedNaturalAreas class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoSpeciallyProtectedNaturalAreas()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralInfoSpeciallyProtectedNaturalAreasRow
    {
        /// <summary>
        /// Особо охраняемая природная территория
        /// </summary>
        public string speciallyProtectedNaturalArea { get; set; }
        /// <summary>
        /// Режим охраны
        /// </summary>
        public string securityMode { get; set; }
        /// <summary>
        /// Мероприятия по сохранениию объектов
        /// </summary>
        public string @event { get; set; }
        /// <summary>
        /// Расположение ООПТ
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable=false)]
        public List<location> location { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralInfoSpeciallyProtectedNaturalAreasRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoSpeciallyProtectedNaturalAreasRow()
        {
            location = new List<location>();
        }
    }
    
    /// <summary>
    /// Сведения о наличии загрязнения лесов (в том числе нефтяного, радиоактивного)
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralInfoContamination
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralInfoContaminationRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralInfoContamination class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoContamination()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralInfoContaminationRow
    {
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
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
        public float volume { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralInfoContaminationRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoContaminationRow()
        {
            unitType = new reference();
            location = new location();
        }
    }
    
    /// <summary>
    /// Сведения о наличии мест обитания редких и находящихся под угрозой исчезновения видов животных и мест произрастания редких и находящихся под угрозой исчезновения видов деревьев, кустарников, лиан и иных лесных растений
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralInfoRarePlantsAnimals
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralInfoRarePlantsAnimalsRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralInfoRarePlantsAnimals class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoRarePlantsAnimals()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralInfoRarePlantsAnimalsRow
    {
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
        /// Расположение ООПТ
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable=false)]
        public List<location> location { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralInfoRarePlantsAnimalsRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoRarePlantsAnimalsRow()
        {
            location = new List<location>();
        }
    }
    
    /// <summary>
    /// Сведения о материалах специальных изысканий, исследований или иных специальных обследований (при наличии)
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralInfoSurveys
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralInfoSurveysRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralInfoSurveys class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoSurveys()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralInfoSurveysRow
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
    /// Сведения об обременениях лесного участка
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralInfoEncumbrances
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralInfoEncumbrancesRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralInfoEncumbrances class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoEncumbrances()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralInfoEncumbrancesRow
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
    /// Создание и эксплуатация лесной инфраструктуры
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralObjects
    {
        /// <summary>
        /// Характеристика существующих и проектируемых объектов лесной инфраструктуры на лесном участке
        /// </summary>
        public forestDevelopmentProjectGeneralObjectsLocationObject locationObject { get; set; }
        /// <summary>
        /// Проектируемый объем рубок лесных насаждений, при создании
        /// объектов лесной инфраструктуры
        /// </summary>
        public forestDevelopmentProjectGeneralObjectsVolumeCuttings volumeCuttings { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralObjects class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralObjects()
        {
            volumeCuttings = new forestDevelopmentProjectGeneralObjectsVolumeCuttings();
            locationObject = new forestDevelopmentProjectGeneralObjectsLocationObject();
        }
    }
    
    /// <summary>
    /// Характеристика существующих и проектируемых объектов лесной инфраструктуры на лесном участке
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralObjectsLocationObject
    {
        [XmlElement("row")]
        public List<locationObjectRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralObjectsLocationObject class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralObjectsLocationObject()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Проектируемый объем рубок лесных насаждений, при создании
    /// объектов лесной инфраструктуры
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralObjectsVolumeCuttings
    {
        [XmlElement("row")]
        public List<volumeCuttingsRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralObjectsVolumeCuttings class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralObjectsVolumeCuttings()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Мероприятия по охране, защите и воспроизводству лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasures
    {
        /// <summary>
        /// Характеристика территории лесного участка по классам пожарной опасности
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresCharacteristicFireClasses characteristicFireClasses { get; set; }
        /// <summary>
        /// Характеристика водных объектов
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresCharacteristicWaterObjects characteristicWaterObjects { get; set; }
        /// <summary>
        /// Обоснование и характеристика проектируемых видов, объемов и сроков выполнения мероприятий по противопожарному обустройству лесов с учетом объектов, созданных при использовании лесов в соответствии с лесохозяйственным регламентом лесничества, и их территориальное размещение
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresDesignedFirePrevention designedFirePrevention { get; set; }
        /// <summary>
        /// Сведения о наличии и потребности в пожарной технике,оборудовании, снаряжении и инвентаре на лесном участке
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresFireEquipment fireEquipment { get; set; }
        /// <summary>
        /// Обоснование и характеристика видов и объемов планируемых профилактических мероприятий по защите лесов, с указанием мест проведения профилактических мероприятий
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresPrevention prevention { get; set; }
        /// <summary>
        /// Сведения о наличии очагов вредных организмов на лесном участке, с указанием их местоположения и мероприятий,необходимых для ликвидации очагов вредных организмов
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresFociHarmfulOrganisms fociHarmfulOrganisms { get; set; }
        /// <summary>
        /// Сведения о повреждении и гибели лесов на начало действия проекта освоения лесов с указанием их местоположения
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresDamageDeath damageDeath { get; set; }
        /// <summary>
        /// Обоснование и характеристика видов и объемов планируемых санитарно-оздоровительных мероприятий на лесном участке, с указанием мест проведения санитарно-оздоровительных мероприятий
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresSanitaryRecreationalMeasures sanitaryRecreationalMeasures { get; set; }
        /// <summary>
        /// Обоснование и характеристика проектируемых видов и объемов защитных мероприятий в зонах радиоактивного загрязнения (если таковые имеются)
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresZonesRadioactiveContamination zonesRadioactiveContamination { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, входящих в зоны радиоактивного загрязнения (если таковые имеются), с указанием ограничений по видам использования лесных участков и заготовки лесных ресурсов
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresListTaxationUnitRadioactiveZone listTaxationUnitRadioactiveZone { get; set; }
        /// <summary>
        /// Площадь земель, нуждающихся в лесовосстановлении
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresAreaNeedReforestation areaNeedReforestation { get; set; }
        /// <summary>
        /// Плановые способы и объемы лесовосстановления
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresReforestationPlan reforestationPlan { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых планируются мероприятия по лесовосстановлению
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresListReforestation listReforestation { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых планируются мероприятия по лесовосстановлению
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresListForestCare listForestCare { get; set; }
        /// <summary>
        /// Площадь лесов, нуждающихся в уходе за лесами, проектируемые виды и ежегодные объемы ухода за лесами при воспроизводстве лесов, не связанные с заготовкой древесины
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresAreaForestCare areaForestCare { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируются мероприятия по охране объектов животного и растительного мира, водных объектов
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresListProtection listProtection { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasures class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasures()
        {
            listProtection = new forestDevelopmentProjectGeneralMeasuresListProtection();
            areaForestCare = new forestDevelopmentProjectGeneralMeasuresAreaForestCare();
            listForestCare = new forestDevelopmentProjectGeneralMeasuresListForestCare();
            listReforestation = new forestDevelopmentProjectGeneralMeasuresListReforestation();
            reforestationPlan = new forestDevelopmentProjectGeneralMeasuresReforestationPlan();
            areaNeedReforestation = new forestDevelopmentProjectGeneralMeasuresAreaNeedReforestation();
            listTaxationUnitRadioactiveZone = new forestDevelopmentProjectGeneralMeasuresListTaxationUnitRadioactiveZone();
            zonesRadioactiveContamination = new forestDevelopmentProjectGeneralMeasuresZonesRadioactiveContamination();
            sanitaryRecreationalMeasures = new forestDevelopmentProjectGeneralMeasuresSanitaryRecreationalMeasures();
            damageDeath = new forestDevelopmentProjectGeneralMeasuresDamageDeath();
            fociHarmfulOrganisms = new forestDevelopmentProjectGeneralMeasuresFociHarmfulOrganisms();
            prevention = new forestDevelopmentProjectGeneralMeasuresPrevention();
            fireEquipment = new forestDevelopmentProjectGeneralMeasuresFireEquipment();
            designedFirePrevention = new forestDevelopmentProjectGeneralMeasuresDesignedFirePrevention();
            characteristicWaterObjects = new forestDevelopmentProjectGeneralMeasuresCharacteristicWaterObjects();
            characteristicFireClasses = new forestDevelopmentProjectGeneralMeasuresCharacteristicFireClasses();
        }
    }
    
    /// <summary>
    /// Характеристика территории лесного участка по классам пожарной опасности
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresCharacteristicFireClasses
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralMeasuresCharacteristicFireClassesRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresCharacteristicFireClasses class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresCharacteristicFireClasses()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresCharacteristicFireClassesRow
    {
        [XmlElement(Order=0)]
        public location location { get; set; }
        /// <summary>
        /// Площадь I класса пожарной опасности
        /// </summary>
        [XmlElement(Order=1)]
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal areaFireClasses_I { get; set; }
        /// <summary>
        /// Площадь II класса пожарной опасности
        /// </summary>
        [XmlElement(Order=2)]
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal areaFireClasses_II { get; set; }
        /// <summary>
        /// Площадь III класса пожарной опасности
        /// </summary>
        [XmlElement(Order=3)]
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal areaFireClasses_III { get; set; }
        /// <summary>
        /// Площадь IV класса пожарной опасности
        /// </summary>
        [XmlElement(Order=4)]
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal areaFireClasses_IV { get; set; }
        /// <summary>
        /// Площадь V класса пожарной опасности
        /// </summary>
        [XmlElement(Order=5)]
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal areaFireClasses_V { get; set; }
        [XmlElement("areaFireClasses_V", Order=6)]
        public decimal areaFireClasses_V1 { get; set; }
        /// <summary>
        /// Площадь общая
        /// </summary>
        [XmlElement(Order=7)]
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal area { get; set; }
        /// <summary>
        /// Средний класс
        /// </summary>
        [XmlElement(Order=8)]
        public string middleClass { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresCharacteristicFireClassesRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresCharacteristicFireClassesRow()
        {
            location = new location();
        }
    }
    
    /// <summary>
    /// Характеристика водных объектов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresCharacteristicWaterObjects
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralMeasuresCharacteristicWaterObjectsRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresCharacteristicWaterObjects class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresCharacteristicWaterObjects()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresCharacteristicWaterObjectsRow
    {
        /// <summary>
        /// Водный объект
        /// </summary>
        public string waterObject { get; set; }
        /// <summary>
        /// Расположение ООПТ
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable=false)]
        public List<location> location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal area { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresCharacteristicWaterObjectsRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresCharacteristicWaterObjectsRow()
        {
            location = new List<location>();
        }
    }
    
    /// <summary>
    /// Обоснование и характеристика проектируемых видов, объемов и сроков выполнения мероприятий по противопожарному обустройству лесов с учетом объектов, созданных при использовании лесов в соответствии с лесохозяйственным регламентом лесничества, и их территориальное размещение
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresDesignedFirePrevention
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralMeasuresDesignedFirePreventionRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresDesignedFirePrevention class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresDesignedFirePrevention()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresDesignedFirePreventionRow
    {
        /// <summary>
        /// Объект противопожарного обустройства
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresDesignedFirePreventionRowFirePreventionObject firePreventionObject { get; set; }
        /// <summary>
        /// Вид мероприятия
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresDesignedFirePreventionRowTypeEvent typeEvent { get; set; }
        public location location { get; set; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public reference unitType { get; set; }
        /// <summary>
        /// Потребность
        /// </summary>
        public float need { get; set; }
        /// <summary>
        /// В наличие
        /// </summary>
        public float availability { get; set; }
        /// <summary>
        /// Проектируемый общий объем
        /// </summary>
        public float designedTotalVolume { get; set; }
        /// <summary>
        /// Проектируемый ежегодный объем
        /// </summary>
        public float designedAnnualVolume { get; set; }
        /// <summary>
        /// Сроки выполнения мероприятий
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresDesignedFirePreventionRowDeadlines deadlines { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresDesignedFirePreventionRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresDesignedFirePreventionRow()
        {
            unitType = new reference();
            location = new location();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public enum forestDevelopmentProjectGeneralMeasuresDesignedFirePreventionRowFirePreventionObject
    {
        I,
        II,
        IV,
        V,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public enum forestDevelopmentProjectGeneralMeasuresDesignedFirePreventionRowTypeEvent
    {
        I,
        II,
        IV,
        V,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public enum forestDevelopmentProjectGeneralMeasuresDesignedFirePreventionRowDeadlines
    {
        I,
        II,
        IV,
        V,
    }
    
    /// <summary>
    /// Сведения о наличии и потребности в пожарной технике,оборудовании, снаряжении и инвентаре на лесном участке
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresFireEquipment
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralMeasuresFireEquipmentRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresFireEquipment class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresFireEquipment()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresFireEquipmentRow
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresFireEquipmentRowName name { get; set; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public reference unitType { get; set; }
        /// <summary>
        /// В соответствии с действующими нормативами
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresFireEquipmentRowRegulations regulations { get; set; }
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
        public string location { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresFireEquipmentRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresFireEquipmentRow()
        {
            unitType = new reference();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public enum forestDevelopmentProjectGeneralMeasuresFireEquipmentRowName
    {
        I,
        II,
        IV,
        V,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public enum forestDevelopmentProjectGeneralMeasuresFireEquipmentRowRegulations
    {
        I,
        II,
        IV,
        V,
    }
    
    /// <summary>
    /// Обоснование и характеристика видов и объемов планируемых профилактических мероприятий по защите лесов, с указанием мест проведения профилактических мероприятий
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresPrevention
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralMeasuresPreventionRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresPrevention class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresPrevention()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresPreventionRow
    {
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public reference specialPurpose { get; set; }
        /// <summary>
        /// Место расположения
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Лечение деревьев, шт.
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresPreventionRowTreatmentTtrees treatmentTtrees { get; set; }
        /// <summary>
        /// Применение пестицидов для предотвращения появления очагов вредных организмов, га
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresPreventionRowPesticideApplication pesticideApplication { get; set; }
        /// <summary>
        /// Использование удобрений, га
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresPreventionRowFertilizerUse fertilizerUse { get; set; }
        /// <summary>
        /// Улучшение условий, шт
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresPreventionRowImprovingConditions improvingConditions { get; set; }
        /// <summary>
        /// Охрана местообитаний, га
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresPreventionRowHabitatProtection habitatProtection { get; set; }
        /// <summary>
        /// Посев травянистых нектароносных растений, га
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresPreventionRowSowing sowing { get; set; }
        /// <summary>
        /// Использование феромонов, шт.
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresPreventionRowUsePheromones usePheromones { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresPreventionRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresPreventionRow()
        {
            usePheromones = new forestDevelopmentProjectGeneralMeasuresPreventionRowUsePheromones();
            sowing = new forestDevelopmentProjectGeneralMeasuresPreventionRowSowing();
            habitatProtection = new forestDevelopmentProjectGeneralMeasuresPreventionRowHabitatProtection();
            improvingConditions = new forestDevelopmentProjectGeneralMeasuresPreventionRowImprovingConditions();
            fertilizerUse = new forestDevelopmentProjectGeneralMeasuresPreventionRowFertilizerUse();
            pesticideApplication = new forestDevelopmentProjectGeneralMeasuresPreventionRowPesticideApplication();
            treatmentTtrees = new forestDevelopmentProjectGeneralMeasuresPreventionRowTreatmentTtrees();
            location = new location();
            specialPurpose = new reference();
        }
    }
    
    /// <summary>
    /// Лечение деревьев, шт.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresPreventionRowTreatmentTtrees
    {
        /// <summary>
        /// Всего
        /// </summary>
        public float volume { get; set; }
        /// <summary>
        /// Ежегодный объем
        /// </summary>
        public float annualVolume { get; set; }
    }
    
    /// <summary>
    /// Применение пестицидов для предотвращения появления очагов вредных организмов, га
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresPreventionRowPesticideApplication
    {
        /// <summary>
        /// Всего
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal volume { get; set; }
        /// <summary>
        /// Ежегодный объем
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal annualVolume { get; set; }
    }
    
    /// <summary>
    /// Использование удобрений, га
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresPreventionRowFertilizerUse
    {
        /// <summary>
        /// Всего
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal volume { get; set; }
        /// <summary>
        /// Ежегодный объем
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal annualVolume { get; set; }
    }
    
    /// <summary>
    /// Улучшение условий, шт
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresPreventionRowImprovingConditions
    {
        /// <summary>
        /// Всего
        /// </summary>
        public float volume { get; set; }
        /// <summary>
        /// Ежегодный объем
        /// </summary>
        public float annualVolume { get; set; }
    }
    
    /// <summary>
    /// Охрана местообитаний, га
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresPreventionRowHabitatProtection
    {
        /// <summary>
        /// Всего
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal volume { get; set; }
        /// <summary>
        /// Ежегодный объем
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal annualVolume { get; set; }
    }
    
    /// <summary>
    /// Посев травянистых нектароносных растений, га
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresPreventionRowSowing
    {
        /// <summary>
        /// Всего
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal volume { get; set; }
        /// <summary>
        /// Ежегодный объем
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal annualVolume { get; set; }
    }
    
    /// <summary>
    /// Использование феромонов, шт.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresPreventionRowUsePheromones
    {
        /// <summary>
        /// Всего
        /// </summary>
        public float volume { get; set; }
        /// <summary>
        /// Ежегодный объем
        /// </summary>
        public float annualVolume { get; set; }
    }
    
    /// <summary>
    /// Сведения о наличии очагов вредных организмов на лесном участке, с указанием их местоположения и мероприятий,необходимых для ликвидации очагов вредных организмов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresFociHarmfulOrganisms
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralMeasuresFociHarmfulOrganismsRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresFociHarmfulOrganisms class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresFociHarmfulOrganisms()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresFociHarmfulOrganismsRow
    {
        /// <summary>
        /// Наименование очагов вредных организмов
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresFociHarmfulOrganismsRowName name { get; set; }
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public reference specialPurpose { get; set; }
        /// <summary>
        /// Место расположения
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal area { get; set; }
        /// <summary>
        /// Мероприятия, необходимые для ликвидации очагов вредных организмов
        /// </summary>
        public string measure { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresFociHarmfulOrganismsRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresFociHarmfulOrganismsRow()
        {
            location = new location();
            specialPurpose = new reference();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public enum forestDevelopmentProjectGeneralMeasuresFociHarmfulOrganismsRowName
    {
        I,
        II,
        IV,
        V,
    }
    
    /// <summary>
    /// Сведения о повреждении и гибели лесов на начало действия проекта освоения лесов с указанием их местоположения
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresDamageDeath
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralMeasuresDamageDeathRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresDamageDeath class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresDamageDeath()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresDamageDeathRow
    {
        /// <summary>
        /// Наименование причин повреждения и гибели лесов
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresDamageDeathRowName name { get; set; }
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public reference specialPurpose { get; set; }
        /// <summary>
        /// Место расположения
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Площадь поврежденных и погибших насаждений нарастающим итогом, га
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal areaDamage { get; set; }
        /// <summary>
        /// Площадь погибших насаждений нарастающим итогом, га
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal areaDeath { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresDamageDeathRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresDamageDeathRow()
        {
            location = new location();
            specialPurpose = new reference();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public enum forestDevelopmentProjectGeneralMeasuresDamageDeathRowName
    {
        I,
        II,
        IV,
        V,
    }
    
    /// <summary>
    /// Обоснование и характеристика видов и объемов планируемых санитарно-оздоровительных мероприятий на лесном участке, с указанием мест проведения санитарно-оздоровительных мероприятий
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresSanitaryRecreationalMeasures
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralMeasuresSanitaryRecreationalMeasuresRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresSanitaryRecreationalMeasures class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresSanitaryRecreationalMeasures()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresSanitaryRecreationalMeasuresRow
    {
        /// <summary>
        /// Вид санитарно-оздоровительного мероприятия
        /// </summary>
        public reference sanritaryRecreationalMeasure { get; set; }
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public reference specialPurpose { get; set; }
        /// <summary>
        /// Место расположения
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
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
        /// <summary>
        /// Год проведения
        /// </summary>
        public int year { get; set; }
        /// <summary>
        /// Обоснование
        /// </summary>
        public string rationale { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresSanitaryRecreationalMeasuresRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresSanitaryRecreationalMeasuresRow()
        {
            location = new location();
            specialPurpose = new reference();
            sanritaryRecreationalMeasure = new reference();
        }
    }
    
    /// <summary>
    /// Обоснование и характеристика проектируемых видов и объемов защитных мероприятий в зонах радиоактивного загрязнения (если таковые имеются)
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresZonesRadioactiveContamination
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralMeasuresZonesRadioactiveContaminationRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresZonesRadioactiveContamination class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresZonesRadioactiveContamination()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresZonesRadioactiveContaminationRow
    {
        /// <summary>
        /// Место расположения
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Зона загрязнения (слабая, средняя, сильная)
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresZonesRadioactiveContaminationRowContaminatedZone contaminatedZone { get; set; }
        /// <summary>
        /// Установка предупреждающих аншлагов, шт.
        /// </summary>
        public float installationWarningBoards { get; set; }
        /// <summary>
        /// Радиационный контроль лесных ресурсов (по видам)
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal radiationControl { get; set; }
        /// <summary>
        /// Дозиметрический контроль при проведении лесохозяйственных работ
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal dosimetricControl { get; set; }
        /// <summary>
        /// Прочие защитные мероприятия
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal otherEvents { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresZonesRadioactiveContaminationRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresZonesRadioactiveContaminationRow()
        {
            location = new location();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public enum forestDevelopmentProjectGeneralMeasuresZonesRadioactiveContaminationRowContaminatedZone
    {
        слабая,
        средняя,
        сильная,
    }
    
    /// <summary>
    /// Ведомость лесотаксационных выделов, входящих в зоны радиоактивного загрязнения (если таковые имеются), с указанием ограничений по видам использования лесных участков и заготовки лесных ресурсов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresListTaxationUnitRadioactiveZone
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralMeasuresListTaxationUnitRadioactiveZoneRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresListTaxationUnitRadioactiveZone class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresListTaxationUnitRadioactiveZone()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresListTaxationUnitRadioactiveZoneRow
    {
        /// <summary>
        /// Место расположения
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal area { get; set; }
        /// <summary>
        /// Зона загрязнения (слабая, средняя, сильная)
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresListTaxationUnitRadioactiveZoneRowContaminatedZone contaminatedZone { get; set; }
        /// <summary>
        /// Ограничение использования лесного участка
        /// </summary>
        public string usageRestriction { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresListTaxationUnitRadioactiveZoneRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresListTaxationUnitRadioactiveZoneRow()
        {
            location = new location();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public enum forestDevelopmentProjectGeneralMeasuresListTaxationUnitRadioactiveZoneRowContaminatedZone
    {
        слабая,
        средняя,
        сильная,
    }
    
    /// <summary>
    /// Площадь земель, нуждающихся в лесовосстановлении
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresAreaNeedReforestation
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralMeasuresAreaNeedReforestationRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresAreaNeedReforestation class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresAreaNeedReforestation()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresAreaNeedReforestationRow
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
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal area { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresAreaNeedReforestationRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresAreaNeedReforestationRow()
        {
            location = new location();
            landCategory = new reference();
        }
    }
    
    /// <summary>
    /// Плановые способы и объемы лесовосстановления
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresReforestationPlan
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralMeasuresReforestationPlanRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresReforestationPlan class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresReforestationPlan()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresReforestationPlanRow
    {
        /// <summary>
        /// Категория земель
        /// </summary>
        public reference landCategory { get; set; }
        /// <summary>
        /// Посев
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal sowing { get; set; }
        /// <summary>
        /// Посадка
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal landing { get; set; }
        /// <summary>
        /// Комбинированное лесовосстановление
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal combined { get; set; }
        /// <summary>
        /// Естественное лесовосстановление
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal natural { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresReforestationPlanRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresReforestationPlanRow()
        {
            landCategory = new reference();
        }
    }
    
    /// <summary>
    /// Ведомость лесотаксационных выделов, в которых планируются мероприятия по лесовосстановлению
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresListReforestation
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralMeasuresListReforestationRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresListReforestation class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresListReforestation()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresListReforestationRow
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
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal area { get; set; }
        /// <summary>
        /// Планируемый способ лесовосстановления
        /// </summary>
        public string conditionsProjectedMethod { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresListReforestationRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresListReforestationRow()
        {
            location = new location();
            landCategory = new reference();
        }
    }
    
    /// <summary>
    /// Ведомость лесотаксационных выделов, в которых планируются мероприятия по лесовосстановлению
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresListForestCare
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralMeasuresListForestCareRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresListForestCare class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresListForestCare()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresListForestCareRow
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
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal area { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresListForestCareRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresListForestCareRow()
        {
            tree = new reference();
            location = new location();
        }
    }
    
    /// <summary>
    /// Площадь лесов, нуждающихся в уходе за лесами, проектируемые виды и ежегодные объемы ухода за лесами при воспроизводстве лесов, не связанные с заготовкой древесины
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresAreaForestCare
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralMeasuresAreaForestCareRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresAreaForestCare class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresAreaForestCare()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresAreaForestCareRow
    {
        /// <summary>
        /// Хозяйство
        /// </summary>
        public farm farm { get; set; }
        /// <summary>
        /// Порода
        /// </summary>
        public reference tree { get; set; }
        /// <summary>
        /// Площадь лесов, нуждающихся в уходе за лесами га
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal area { get; set; }
        /// <summary>
        /// Ежегодная площадь ухода за лесами, га
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal annualArea { get; set; }
        /// <summary>
        /// Вид ухода
        /// </summary>
        public string typeCare { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresAreaForestCareRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresAreaForestCareRow()
        {
            tree = new reference();
        }
    }
    
    /// <summary>
    /// Ведомость лесотаксационных выделов, в которых проектируются мероприятия по охране объектов животного и растительного мира, водных объектов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresListProtection
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectGeneralMeasuresListProtectionRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresListProtection class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresListProtection()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectGeneralMeasuresListProtectionRow
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
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
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
        /// forestDevelopmentProjectGeneralMeasuresListProtectionRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresListProtectionRow()
        {
            unitType = new reference();
            location = new location();
        }
    }
    
    /// <summary>
    /// II. Организация использования лесов (по виду разрешенного использования лесов)
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUse
    {
        /// <summary>
        /// Заготовка древесины
        /// </summary>
        public forestDevelopmentProjectForestUseWoodHarvesting woodHarvesting { get; set; }
        /// <summary>
        /// Заготовка живицы
        /// </summary>
        public forestDevelopmentProjectForestUseResinHarvesting resinHarvesting { get; set; }
        /// <summary>
        /// Заготовка и сбор недревесных лесных ресурсов
        /// </summary>
        public forestDevelopmentProjectForestUseNonTimberResourcesHarvesting nonTimberResourcesHarvesting { get; set; }
        /// <summary>
        /// Заготовка пищевых лесных ресурсов и сбор лекарственных растений
        /// </summary>
        public forestDevelopmentProjectForestUseFoodForestrResourcesHarvesting foodForestrResourcesHarvesting { get; set; }
        /// <summary>
        /// Осуществление видов деятельности в сфере охотничьего хозяйства
        /// </summary>
        public forestDevelopmentProjectForestUseHunting hunting { get; set; }
        /// <summary>
        /// Ведение сельского хозяйства
        /// </summary>
        public forestDevelopmentProjectForestUseAgriculture agriculture { get; set; }
        /// <summary>
        /// Использование лесов для осуществления рыболовства
        /// </summary>
        public forestDevelopmentProjectForestUseFishing fishing { get; set; }
        /// <summary>
        /// Осуществление научно-исследовательской деятельности,образовательной деятельности
        /// </summary>
        public forestDevelopmentProjectForestUseResearchActivities researchActivities { get; set; }
        /// <summary>
        /// Осуществление рекреационной деятельности
        /// </summary>
        public forestDevelopmentProjectForestUseRecreation recreation { get; set; }
        /// <summary>
        /// Создание лесных плантаций и их эксплуатация
        /// </summary>
        public forestDevelopmentProjectForestUseForestPlantations forestPlantations { get; set; }
        /// <summary>
        /// Создание лесных питомников и их эксплуатация
        /// </summary>
        public forestDevelopmentProjectForestUseForestNurseries forestNurseries { get; set; }
        /// <summary>
        /// Выращивание лесных плодовых, ягодных, декоративных растений, лекарственных растений
        /// </summary>
        public forestDevelopmentProjectForestUsePlantGrowing plantGrowing { get; set; }
        /// <summary>
        /// Выполнение работ по геологическому изучению недр, разведке и добыче полезных ископаемых
        /// </summary>
        public forestDevelopmentProjectForestUseGeology geology { get; set; }
        /// <summary>
        /// Строительство и эксплуатация водохранилищ и иных искусственных водных объектов, создание и расширение территорий морских и речных портов, строительство, реконструкция и эксплуатация гидротехнических сооружений
        /// </summary>
        public forestDevelopmentProjectForestUseHydrology hydrology { get; set; }
        /// <summary>
        /// Строительство, реконструкция, эксплуатация линейных объектов
        /// </summary>
        public forestDevelopmentProjectForestUseLinearObjects linearObjects { get; set; }
        /// <summary>
        /// Создание и эксплуатация объектов лесоперерабатывающей инфраструктуры
        /// </summary>
        public forestDevelopmentProjectForestUseTimberProcessing timberProcessing { get; set; }
        /// <summary>
        /// Изыскательские работы
        /// </summary>
        public forestDevelopmentProjectForestUseSurveyWork surveyWork { get; set; }
    }
    
    /// <summary>
    /// Заготовка древесины
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseWoodHarvesting
    {
        /// <summary>
        /// Установленный объем заготовки древесины на лесном участке
        /// </summary>
        public forestDevelopmentProjectForestUseWoodHarvestingVolumeCuttingsWood volumeCuttingsWood { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируется заготовка древесины
        /// </summary>
        public forestDevelopmentProjectForestUseWoodHarvestingLocationCuttings locationCuttings { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseWoodHarvesting class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseWoodHarvesting()
        {
            locationCuttings = new forestDevelopmentProjectForestUseWoodHarvestingLocationCuttings();
            volumeCuttingsWood = new forestDevelopmentProjectForestUseWoodHarvestingVolumeCuttingsWood();
        }
    }
    
    /// <summary>
    /// Установленный объем заготовки древесины на лесном участке
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseWoodHarvestingVolumeCuttingsWood
    {
        [XmlElement("row")]
        [Required]
        public List<forestDevelopmentProjectForestUseWoodHarvestingVolumeCuttingsWoodRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseWoodHarvestingVolumeCuttingsWood class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseWoodHarvestingVolumeCuttingsWood()
        {
            row = new List<forestDevelopmentProjectForestUseWoodHarvestingVolumeCuttingsWoodRow>();
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseWoodHarvestingVolumeCuttingsWoodRow
    {
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public reference specialPurpose { get; set; }
        /// <summary>
        /// Форма рубки
        /// </summary>
        public formCutting formCutting { get; set; }
        /// <summary>
        /// Указывается элемент справочника "Виды рубок"
        /// </summary>
        public reference typeCutting { get; set; }
        /// <summary>
        /// Хозяйство
        /// </summary>
        public farm farm { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal area { get; set; }
        /// <summary>
        /// запас (корневой), тыс. м3
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal rootVolume { get; set; }
        /// <summary>
        /// запас (ликвидный), тыс. м3
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal liquidVolume { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseWoodHarvestingVolumeCuttingsWoodRow class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseWoodHarvestingVolumeCuttingsWoodRow()
        {
            typeCutting = new reference();
            specialPurpose = new reference();
        }
    }
    
    /// <summary>
    /// Форма рубки
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/sTypes/4.1")]
    public enum formCutting
    {
        [XmlEnumAttribute("Сплошная рубка")]
        Сплошнаярубка,
        [XmlEnumAttribute("Выборочная рубка")]
        Выборочнаярубка,
    }
    
    /// <summary>
    /// Ведомость лесотаксационных выделов, в которых проектируется заготовка древесины
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseWoodHarvestingLocationCuttings
    {
        [XmlElement("row")]
        [Required]
        public List<forestDevelopmentProjectForestUseWoodHarvestingLocationCuttingsRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseWoodHarvestingLocationCuttings class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseWoodHarvestingLocationCuttings()
        {
            row = new List<forestDevelopmentProjectForestUseWoodHarvestingLocationCuttingsRow>();
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseWoodHarvestingLocationCuttingsRow
    {
        public location location { get; set; }
        /// <summary>
        /// Преобладающая порода
        /// </summary>
        public reference tree { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal area { get; set; }
        /// <summary>
        /// Запас средний на 1 га, м3
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal averageVolume { get; set; }
        /// <summary>
        /// Запас на выделе, м3
        /// </summary>
        [MaxDigitsAttribute(10)]
        [FractionDigitsAttribute(2)]
        [RangeAttribute(0D, 9999999.99D)]
        public decimal volume { get; set; }
        /// <summary>
        /// Форма рубки
        /// </summary>
        public formCutting formCutting { get; set; }
        /// <summary>
        /// Указывается элемент справочника "Виды рубок"
        /// </summary>
        public reference typeCutting { get; set; }
        /// <summary>
        /// % выборки (для выборочных рубок спелых и перестойных лесных насаждений)
        /// </summary>
        [FractionDigitsAttribute(2)]
        [MaxDigitsAttribute(5)]
        [RangeAttribute(0D, 100D)]
        public decimal samplePercentage { get; set; }
        /// <summary>
        /// Планируемый способ лесовосстановления
        /// </summary>
        public string reforestationMethod { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseWoodHarvestingLocationCuttingsRow class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseWoodHarvestingLocationCuttingsRow()
        {
            typeCutting = new reference();
            tree = new reference();
            location = new location();
        }
    }
    
    /// <summary>
    /// Заготовка живицы
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseResinHarvesting
    {
        /// <summary>
        /// Проектируемые технологии проведения работ по заготовке живицы
        /// </summary>
        public string technologies { get; set; }
        /// <summary>
        /// Фонд заготовки живицы
        /// </summary>
        public forestDevelopmentProjectForestUseResinHarvestingVolumeResource volumeResource { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, проектируемых для заготовки живицы
        /// </summary>
        public forestDevelopmentProjectForestUseResinHarvestingLocationResource locationResource { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseResinHarvesting class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseResinHarvesting()
        {
            locationResource = new forestDevelopmentProjectForestUseResinHarvestingLocationResource();
            volumeResource = new forestDevelopmentProjectForestUseResinHarvestingVolumeResource();
        }
    }
    
    /// <summary>
    /// Фонд заготовки живицы
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseResinHarvestingVolumeResource
    {
        [XmlElement("row")]
        [Required]
        public List<forestDevelopmentProjectForestUseResinHarvestingVolumeResourceRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseResinHarvestingVolumeResource class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseResinHarvestingVolumeResource()
        {
            row = new List<forestDevelopmentProjectForestUseResinHarvestingVolumeResourceRow>();
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseResinHarvestingVolumeResourceRow
    {
        /// <summary>
        /// 1. Всего спелых и перестойных насаждений, пригодных для подсочки
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal useful { get; set; }
        /// <summary>
        /// 1.1. Из них не вовлечены в подсочку:
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal completion { get; set; }
        /// <summary>
        /// в том числе нерентабельные
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal unprofitable { get; set; }
        /// <summary>
        /// 2. Может ежегодно находиться в подсочке
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal maybe { get; set; }
        /// <summary>
        /// 3. Фактически находится в подсочке
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal used { get; set; }
        /// <summary>
        /// 4. Вышедшие из подсочки, всего
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal finished { get; set; }
    }
    
    /// <summary>
    /// Ведомость лесотаксационных выделов, проектируемых для заготовки живицы
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseResinHarvestingLocationResource
    {
        [XmlElement("row")]
        [Required]
        public List<forestDevelopmentProjectForestUseResinHarvestingLocationResourceRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseResinHarvestingLocationResource class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseResinHarvestingLocationResource()
        {
            row = new List<forestDevelopmentProjectForestUseResinHarvestingLocationResourceRow>();
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseResinHarvestingLocationResourceRow
    {
        public location location { get; set; }
        /// <summary>
        /// Преобладающая порода
        /// </summary>
        public reference tree { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
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
        /// forestDevelopmentProjectForestUseResinHarvestingLocationResourceRow class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseResinHarvestingLocationResourceRow()
        {
            tree = new reference();
            location = new location();
        }
    }
    
    /// <summary>
    /// Заготовка и сбор недревесных лесных ресурсов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseNonTimberResourcesHarvesting
    {
        /// <summary>
        /// Проектируемые технологии проведения работ по заготовке недревесных лесных ресурсов
        /// </summary>
        public string technologies { get; set; }
        /// <summary>
        /// Фонд (биологический запас) недревесных лесных ресурсов и проектируемые ежегодные объемы заготовки недревесных лесных ресурсов
        /// </summary>
        public forestDevelopmentProjectForestUseNonTimberResourcesHarvestingVolumeResource volumeResource { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируется заготовка недревесных лесных ресурсов
        /// </summary>
        public forestDevelopmentProjectForestUseNonTimberResourcesHarvestingLocationResource locationResource { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseNonTimberResourcesHarvesting class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseNonTimberResourcesHarvesting()
        {
            locationResource = new forestDevelopmentProjectForestUseNonTimberResourcesHarvestingLocationResource();
            volumeResource = new forestDevelopmentProjectForestUseNonTimberResourcesHarvestingVolumeResource();
        }
    }
    
    /// <summary>
    /// Фонд (биологический запас) недревесных лесных ресурсов и проектируемые ежегодные объемы заготовки недревесных лесных ресурсов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseNonTimberResourcesHarvestingVolumeResource
    {
        [XmlElement("row")]
        [Required]
        public List<volumeResourceRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseNonTimberResourcesHarvestingVolumeResource class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseNonTimberResourcesHarvestingVolumeResource()
        {
            row = new List<volumeResourceRow>();
            number = "1";
        }
    }
    
    /// <summary>
    /// Ведомость лесотаксационных выделов, в которых проектируется заготовка недревесных лесных ресурсов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseNonTimberResourcesHarvestingLocationResource
    {
        [XmlElement("row")]
        [Required]
        public List<locationResourceRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseNonTimberResourcesHarvestingLocationResource class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseNonTimberResourcesHarvestingLocationResource()
        {
            row = new List<locationResourceRow>();
            number = "1";
        }
    }
    
    /// <summary>
    /// Заготовка пищевых лесных ресурсов и сбор лекарственных растений
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseFoodForestrResourcesHarvesting
    {
        /// <summary>
        /// Общая характеристика видов пищевых лесных ресурсов и лекарственных растений, предусмотренная лесохозяйственным регламентом лесничества
        /// </summary>
        public string characteristics { get; set; }
        /// <summary>
        /// Проектируемые технологии проведения работ по заготовке пищевых лесных ресурсов и сбору лекарственных растений с указанием сроков и допустимого объема их заготовки и сбора (%) от фонда (биологического запаса)
        /// </summary>
        public string technologies { get; set; }
        /// <summary>
        /// Фонд (биологический запас) пищевых лесных ресурсов и лекарственных растений и проектируемые ежегодные объемы заготовки пищевых лесных ресурсов и сбора
        /// </summary>
        public forestDevelopmentProjectForestUseFoodForestrResourcesHarvestingVolumeResource volumeResource { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируется заготовка пищевых лесных ресурсов и сбор лекарственных растений
        /// </summary>
        public forestDevelopmentProjectForestUseFoodForestrResourcesHarvestingLocationResource locationResource { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseFoodForestrResourcesHarvesting class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseFoodForestrResourcesHarvesting()
        {
            locationResource = new forestDevelopmentProjectForestUseFoodForestrResourcesHarvestingLocationResource();
            volumeResource = new forestDevelopmentProjectForestUseFoodForestrResourcesHarvestingVolumeResource();
        }
    }
    
    /// <summary>
    /// Фонд (биологический запас) пищевых лесных ресурсов и лекарственных растений и проектируемые ежегодные объемы заготовки пищевых лесных ресурсов и сбора
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseFoodForestrResourcesHarvestingVolumeResource
    {
        [XmlElement("row")]
        [Required]
        public List<volumeResourceRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseFoodForestrResourcesHarvestingVolumeResource class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseFoodForestrResourcesHarvestingVolumeResource()
        {
            row = new List<volumeResourceRow>();
            number = "1";
        }
    }
    
    /// <summary>
    /// Ведомость лесотаксационных выделов, в которых проектируется заготовка пищевых лесных ресурсов и сбор лекарственных растений
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseFoodForestrResourcesHarvestingLocationResource
    {
        [XmlElement("row")]
        [Required]
        public List<locationResourceRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseFoodForestrResourcesHarvestingLocationResource class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseFoodForestrResourcesHarvestingLocationResource()
        {
            row = new List<locationResourceRow>();
            number = "1";
        }
    }
    
    /// <summary>
    /// Осуществление видов деятельности в сфере охотничьего хозяйства
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseHunting
    {
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируется создание объектов охотничьей инфраструктуры и связанные с их созданием рубки лесных насаждений
        /// </summary>
        public forestDevelopmentProjectForestUseHuntingLocationObject locationObject { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируется проведение биотехнических мероприятий
        /// </summary>
        public forestDevelopmentProjectForestUseHuntingLocationMeasure locationMeasure { get; set; }
    }
    
    /// <summary>
    /// Ведомость лесотаксационных выделов, в которых проектируется создание объектов охотничьей инфраструктуры и связанные с их созданием рубки лесных насаждений
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseHuntingLocationObject
    {
        [XmlElement("row")]
        [Required]
        public List<locationObjectRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseHuntingLocationObject class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseHuntingLocationObject()
        {
            row = new List<locationObjectRow>();
            number = "1";
        }
    }
    
    /// <summary>
    /// Ведомость лесотаксационных выделов, в которых проектируется проведение биотехнических мероприятий
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseHuntingLocationMeasure
    {
        [XmlElement("row")]
        [Required]
        public List<locationMeasureRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseHuntingLocationMeasure class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseHuntingLocationMeasure()
        {
            row = new List<locationMeasureRow>();
            number = "1";
        }
    }
    
    /// <summary>
    /// Ведение сельского хозяйства
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseAgriculture
    {
        /// <summary>
        /// Характеристика проектируемых технологий ведения сельского хозяйства
        /// </summary>
        public string technologies { get; set; }
        /// <summary>
        /// Основные проектируемые параметры использования лесов для ведения сельского хозяйства
        /// </summary>
        public forestDevelopmentProjectForestUseAgricultureVolumeResource volumeResource { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируются мероприятия по ведению сельского хозяйства
        /// </summary>
        public forestDevelopmentProjectForestUseAgricultureLocationResource locationResource { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseAgriculture class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseAgriculture()
        {
            locationResource = new forestDevelopmentProjectForestUseAgricultureLocationResource();
            volumeResource = new forestDevelopmentProjectForestUseAgricultureVolumeResource();
        }
    }
    
    /// <summary>
    /// Основные проектируемые параметры использования лесов для ведения сельского хозяйства
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseAgricultureVolumeResource
    {
        [XmlElement("row")]
        [Required]
        public List<volumeResourceRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseAgricultureVolumeResource class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseAgricultureVolumeResource()
        {
            row = new List<volumeResourceRow>();
            number = "1";
        }
    }
    
    /// <summary>
    /// Ведомость лесотаксационных выделов, в которых проектируются мероприятия по ведению сельского хозяйства
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseAgricultureLocationResource
    {
        [XmlElement("row")]
        [Required]
        public List<locationResourceRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseAgricultureLocationResource class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseAgricultureLocationResource()
        {
            row = new List<locationResourceRow>();
            number = "1";
        }
    }
    
    /// <summary>
    /// Использование лесов для осуществления рыболовства
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseFishing
    {
        /// <summary>
        /// Обоснование и характеристика проектируемых видов и объемов работ по возведению на лесном участке некапитальных строений, сооружений, необходимых для осуществления рыболовства
        /// </summary>
        public forestDevelopmentProjectForestUseFishingCharacteristicsWork characteristicsWork { get; set; }
        /// <summary>
        /// Характеристика существующих и проектируемых некапитальных строений и сооружений на лесном участке при использовании лесов для осуществления рыболовства
        /// </summary>
        public forestDevelopmentProjectForestUseFishingLocationObject locationObject { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseFishing class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseFishing()
        {
            locationObject = new forestDevelopmentProjectForestUseFishingLocationObject();
            characteristicsWork = new forestDevelopmentProjectForestUseFishingCharacteristicsWork();
        }
    }
    
    /// <summary>
    /// Обоснование и характеристика проектируемых видов и объемов работ по возведению на лесном участке некапитальных строений, сооружений, необходимых для осуществления рыболовства
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseFishingCharacteristicsWork
    {
        [XmlElement("row")]
        public List<characteristicsWorkRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseFishingCharacteristicsWork class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseFishingCharacteristicsWork()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Характеристика существующих и проектируемых некапитальных строений и сооружений на лесном участке при использовании лесов для осуществления рыболовства
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseFishingLocationObject
    {
        [XmlElement("row")]
        public List<locationObjectRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseFishingLocationObject class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseFishingLocationObject()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Осуществление научно-исследовательской деятельности,образовательной деятельности
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseResearchActivities
    {
        /// <summary>
        /// Сведения о программе научно-исследовательской или образовательной деятельности на лесном участке с обоснованием и характеристикой проектируемых видов и объемов работ
        /// </summary>
        public forestDevelopmentProjectForestUseResearchActivitiesCharacteristicsWork characteristicsWork { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируется осуществление мероприятий по научно-исследовательской и/или образовательной деятельности и проектируемый объем рубок лесных насаждений, проводимых в целях научно-исследовательской, образовательной деятельности
        /// </summary>
        public forestDevelopmentProjectForestUseResearchActivitiesLocationMeasure locationMeasure { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseResearchActivities class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseResearchActivities()
        {
            locationMeasure = new forestDevelopmentProjectForestUseResearchActivitiesLocationMeasure();
            characteristicsWork = new forestDevelopmentProjectForestUseResearchActivitiesCharacteristicsWork();
        }
    }
    
    /// <summary>
    /// Сведения о программе научно-исследовательской или образовательной деятельности на лесном участке с обоснованием и характеристикой проектируемых видов и объемов работ
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseResearchActivitiesCharacteristicsWork
    {
        [XmlElement("row")]
        public List<characteristicsWorkRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseResearchActivitiesCharacteristicsWork class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseResearchActivitiesCharacteristicsWork()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Ведомость лесотаксационных выделов, в которых проектируется осуществление мероприятий по научно-исследовательской и/или образовательной деятельности и проектируемый объем рубок лесных насаждений, проводимых в целях научно-исследовательской, образовательной деятельности
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseResearchActivitiesLocationMeasure
    {
        [XmlElement("row")]
        public List<locationMeasureRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseResearchActivitiesLocationMeasure class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseResearchActivitiesLocationMeasure()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Осуществление рекреационной деятельности
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseRecreation
    {
        /// <summary>
        /// Ведомость учета деревьев на лесном участке для осуществления рекреационной деятельности
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable=false)]
        public List<forestDevelopmentProjectForestUseRecreationRow> treeRegister { get; set; }
        /// <summary>
        /// Характеристика существующих и проектируемых объектов капитального строительства, не связанных с созданием лесной инфраструктуры, на лесном участке при использовании лесов для рекреационной деятельности
        /// </summary>
        public forestDevelopmentProjectForestUseRecreationLocationCapitalObject locationCapitalObject { get; set; }
        /// <summary>
        /// Характеристика существующих и проектируемых некапитальных строений и сооружений, не связанных с созданием лесной инфраструктуры, на лесном участке при использовании лесов для рекреационной деятельности
        /// </summary>
        public forestDevelopmentProjectForestUseRecreationLocationNonCapitalObject locationNonCapitalObject { get; set; }
        /// <summary>
        /// Объем рубок лесных насаждений на лесном участке при создании объектов капитального строительства, не связанных с созданием лесной инфраструктуры при использовании лесов для рекреационной деятельности
        /// </summary>
        public forestDevelopmentProjectForestUseRecreationVolumeCuttings volumeCuttings { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseRecreation class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseRecreation()
        {
            volumeCuttings = new forestDevelopmentProjectForestUseRecreationVolumeCuttings();
            locationNonCapitalObject = new forestDevelopmentProjectForestUseRecreationLocationNonCapitalObject();
            locationCapitalObject = new forestDevelopmentProjectForestUseRecreationLocationCapitalObject();
            treeRegister = new List<forestDevelopmentProjectForestUseRecreationRow>();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseRecreationRow
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
        /// forestDevelopmentProjectForestUseRecreationRow class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseRecreationRow()
        {
            tree = new reference();
        }
    }
    
    /// <summary>
    /// Характеристика существующих и проектируемых объектов капитального строительства, не связанных с созданием лесной инфраструктуры, на лесном участке при использовании лесов для рекреационной деятельности
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseRecreationLocationCapitalObject
    {
        [XmlElement("row")]
        public List<locationObjectRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseRecreationLocationCapitalObject class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseRecreationLocationCapitalObject()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Характеристика существующих и проектируемых некапитальных строений и сооружений, не связанных с созданием лесной инфраструктуры, на лесном участке при использовании лесов для рекреационной деятельности
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseRecreationLocationNonCapitalObject
    {
        [XmlElement("row")]
        public List<locationObjectRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseRecreationLocationNonCapitalObject class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseRecreationLocationNonCapitalObject()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Объем рубок лесных насаждений на лесном участке при создании объектов капитального строительства, не связанных с созданием лесной инфраструктуры при использовании лесов для рекреационной деятельности
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseRecreationVolumeCuttings
    {
        [XmlElement("row")]
        public List<volumeCuttingsRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseRecreationVolumeCuttings class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseRecreationVolumeCuttings()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Создание лесных плантаций и их эксплуатация
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseForestPlantations
    {
        /// <summary>
        /// Сведения о характеристиках и обосновании проектируемых видов и объемов работ по созданию лесных плантаций и их эксплуатации, технология создания и эксплуатации лесных плантаций
        /// </summary>
        public forestDevelopmentProjectForestUseForestPlantationsCharacteristicsWork characteristicsWork { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируется создание лесных плантаций и их эксплуатация
        /// </summary>
        public forestDevelopmentProjectForestUseForestPlantationsLocationWork locationWork { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseForestPlantations class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseForestPlantations()
        {
            locationWork = new forestDevelopmentProjectForestUseForestPlantationsLocationWork();
            characteristicsWork = new forestDevelopmentProjectForestUseForestPlantationsCharacteristicsWork();
        }
    }
    
    /// <summary>
    /// Сведения о характеристиках и обосновании проектируемых видов и объемов работ по созданию лесных плантаций и их эксплуатации, технология создания и эксплуатации лесных плантаций
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseForestPlantationsCharacteristicsWork
    {
        [XmlElement("row")]
        public List<characteristicsWorkRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseForestPlantationsCharacteristicsWork class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseForestPlantationsCharacteristicsWork()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Ведомость лесотаксационных выделов, в которых проектируется создание лесных плантаций и их эксплуатация
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseForestPlantationsLocationWork
    {
        [XmlElement("row")]
        public List<locationWorkRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseForestPlantationsLocationWork class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseForestPlantationsLocationWork()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Создание лесных питомников и их эксплуатация
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseForestNurseries
    {
        /// <summary>
        /// Сведения об обосновании и характеристиках проектируемых
        /// технологий по выращиванию посадочного материала лесных
        /// растений (саженцев, сеянцев), проектируемом объеме и видах
        /// мероприятий, направленных на соблюдение технологий
        /// по выращиванию посадочного материала лесных растений
        /// </summary>
        public forestDevelopmentProjectForestUseForestNurseriesCharacteristicsWork characteristicsWork { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируется
        /// создание лесных питомников и их эксплуатация
        /// </summary>
        public forestDevelopmentProjectForestUseForestNurseriesLocationWork locationWork { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseForestNurseries class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseForestNurseries()
        {
            locationWork = new forestDevelopmentProjectForestUseForestNurseriesLocationWork();
            characteristicsWork = new forestDevelopmentProjectForestUseForestNurseriesCharacteristicsWork();
        }
    }
    
    /// <summary>
    /// Сведения об обосновании и характеристиках проектируемых
    /// технологий по выращиванию посадочного материала лесных
    /// растений (саженцев, сеянцев), проектируемом объеме и видах
    /// мероприятий, направленных на соблюдение технологий
    /// по выращиванию посадочного материала лесных растений
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseForestNurseriesCharacteristicsWork
    {
        [XmlElement("row")]
        public List<characteristicsWorkRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseForestNurseriesCharacteristicsWork class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseForestNurseriesCharacteristicsWork()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Ведомость лесотаксационных выделов, в которых проектируется
    /// создание лесных питомников и их эксплуатация
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseForestNurseriesLocationWork
    {
        [XmlElement("row")]
        public List<locationWorkRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseForestNurseriesLocationWork class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseForestNurseriesLocationWork()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Выращивание лесных плодовых, ягодных, декоративных растений, лекарственных растений
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUsePlantGrowing
    {
        /// <summary>
        /// Сведения о характеристиках и обосновании проектируемых видов и объемов работ по выращиванию лесных плодовых, ягодных, декоративных растений, лекарственных растений; характеристика проектируемых технологий
        /// </summary>
        public forestDevelopmentProjectForestUsePlantGrowingCharacteristicsWork characteristicsWork { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируется выращивание лесных плодовых, ягодных, декоративных и лекарственных растений
        /// </summary>
        public forestDevelopmentProjectForestUsePlantGrowingLocationMeasure locationMeasure { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUsePlantGrowing class constructor
        /// </summary>
        public forestDevelopmentProjectForestUsePlantGrowing()
        {
            locationMeasure = new forestDevelopmentProjectForestUsePlantGrowingLocationMeasure();
            characteristicsWork = new forestDevelopmentProjectForestUsePlantGrowingCharacteristicsWork();
        }
    }
    
    /// <summary>
    /// Сведения о характеристиках и обосновании проектируемых видов и объемов работ по выращиванию лесных плодовых, ягодных, декоративных растений, лекарственных растений; характеристика проектируемых технологий
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUsePlantGrowingCharacteristicsWork
    {
        [XmlElement("row")]
        public List<characteristicsWorkRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUsePlantGrowingCharacteristicsWork class constructor
        /// </summary>
        public forestDevelopmentProjectForestUsePlantGrowingCharacteristicsWork()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Ведомость лесотаксационных выделов, в которых проектируется выращивание лесных плодовых, ягодных, декоративных и лекарственных растений
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUsePlantGrowingLocationMeasure
    {
        [XmlElement("row")]
        public List<locationMeasureRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUsePlantGrowingLocationMeasure class constructor
        /// </summary>
        public forestDevelopmentProjectForestUsePlantGrowingLocationMeasure()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Выполнение работ по геологическому изучению недр, разведке и добыче полезных ископаемых
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseGeology
    {
        /// <summary>
        /// Сведения о характеристиках и обосновании проектируемых видов и объемов работ, при использовании лесов в целях геологического изучения недр, разведки и добычи полезных ископаемых
        /// </summary>
        public forestDevelopmentProjectForestUseGeologyCharacteristicsWork characteristicsWork { get; set; }
        /// <summary>
        /// Характеристика существующих и проектируемых объектов, строений и сооружений при использовании лесов в целях геологического изучения недр, разведки и добычи полезных ископаемых
        /// </summary>
        public forestDevelopmentProjectForestUseGeologyLocationObject locationObject { get; set; }
        /// <summary>
        /// Проектируемый объем рубок лесных насаждений на лесном участке при создании объектов, строений и сооружений для геологического изучения недр, разведки и добычи полезных ископаемых
        /// </summary>
        public forestDevelopmentProjectForestUseGeologyVolumeCuttings volumeCuttings { get; set; }
        /// <summary>
        /// Сведения о рекультивации нарушенных при геологическом изучении недр, разведке и добыче полезных ископаемых земель на лесном участке при выполнении работ по геологическому изучению недр, разведке и добыче полезных ископаемых, а также подвергшихся нефтяному или иному загрязнению и подлежащих рекультивации
        /// </summary>
        public forestDevelopmentProjectForestUseGeologyLocationMeasure locationMeasure { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseGeology class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseGeology()
        {
            locationMeasure = new forestDevelopmentProjectForestUseGeologyLocationMeasure();
            volumeCuttings = new forestDevelopmentProjectForestUseGeologyVolumeCuttings();
            locationObject = new forestDevelopmentProjectForestUseGeologyLocationObject();
            characteristicsWork = new forestDevelopmentProjectForestUseGeologyCharacteristicsWork();
        }
    }
    
    /// <summary>
    /// Сведения о характеристиках и обосновании проектируемых видов и объемов работ, при использовании лесов в целях геологического изучения недр, разведки и добычи полезных ископаемых
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseGeologyCharacteristicsWork
    {
        [XmlElement("row")]
        public List<characteristicsWorkRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseGeologyCharacteristicsWork class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseGeologyCharacteristicsWork()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Характеристика существующих и проектируемых объектов, строений и сооружений при использовании лесов в целях геологического изучения недр, разведки и добычи полезных ископаемых
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseGeologyLocationObject
    {
        [XmlElement("row")]
        public List<locationObjectRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseGeologyLocationObject class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseGeologyLocationObject()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Проектируемый объем рубок лесных насаждений на лесном участке при создании объектов, строений и сооружений для геологического изучения недр, разведки и добычи полезных ископаемых
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseGeologyVolumeCuttings
    {
        [XmlElement("row")]
        public List<volumeCuttingsRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseGeologyVolumeCuttings class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseGeologyVolumeCuttings()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Сведения о рекультивации нарушенных при геологическом изучении недр, разведке и добыче полезных ископаемых земель на лесном участке при выполнении работ по геологическому изучению недр, разведке и добыче полезных ископаемых, а также подвергшихся нефтяному или иному загрязнению и подлежащих рекультивации
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseGeologyLocationMeasure
    {
        [XmlElement("row")]
        public List<locationMeasureRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseGeologyLocationMeasure class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseGeologyLocationMeasure()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Строительство и эксплуатация водохранилищ и иных искусственных водных объектов, создание и расширение территорий морских и речных портов, строительство, реконструкция и эксплуатация гидротехнических сооружений
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseHydrology
    {
        /// <summary>
        /// Сведения о характеристиках и обосновании проектируемых видов и объемов работ по строительству и эксплуатации водохранилищ и иных искусственных водных объектов, созданию и расширению территорий морских и речных портов, строительству, реконструкции и эксплуатации гидротехнических сооружений
        /// </summary>
        public forestDevelopmentProjectForestUseHydrologyCharacteristicsWork characteristicsWork { get; set; }
        /// <summary>
        /// Характеристика существующих и проектируемых объектов,строений и сооружений при строительстве и эксплуатации водохранилищ и иных искусственных водных объектов, создании и расширении территорий морских и речных портов, строительстве, реконструкции и эксплуатации гидротехнических сооружений на лесном участке
        /// </summary>
        public forestDevelopmentProjectForestUseHydrologyLocationObject locationObject { get; set; }
        /// <summary>
        /// Проектируемый объем рубок лесных насаждений на лесном участке при строительстве и эксплуатации водохранилищ и иных искусственных водных объектов, создании и расширении территорий морских и речных портов,строительстве, реконструкции и эксплуатациигидротехнических сооружений
        /// </summary>
        public forestDevelopmentProjectForestUseHydrologyVolumeCuttings volumeCuttings { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseHydrology class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseHydrology()
        {
            volumeCuttings = new forestDevelopmentProjectForestUseHydrologyVolumeCuttings();
            locationObject = new forestDevelopmentProjectForestUseHydrologyLocationObject();
            characteristicsWork = new forestDevelopmentProjectForestUseHydrologyCharacteristicsWork();
        }
    }
    
    /// <summary>
    /// Сведения о характеристиках и обосновании проектируемых видов и объемов работ по строительству и эксплуатации водохранилищ и иных искусственных водных объектов, созданию и расширению территорий морских и речных портов, строительству, реконструкции и эксплуатации гидротехнических сооружений
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseHydrologyCharacteristicsWork
    {
        [XmlElement("row")]
        public List<characteristicsWorkRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseHydrologyCharacteristicsWork class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseHydrologyCharacteristicsWork()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Характеристика существующих и проектируемых объектов,строений и сооружений при строительстве и эксплуатации водохранилищ и иных искусственных водных объектов, создании и расширении территорий морских и речных портов, строительстве, реконструкции и эксплуатации гидротехнических сооружений на лесном участке
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseHydrologyLocationObject
    {
        [XmlElement("row")]
        public List<locationObjectRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseHydrologyLocationObject class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseHydrologyLocationObject()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Проектируемый объем рубок лесных насаждений на лесном участке при строительстве и эксплуатации водохранилищ и иных искусственных водных объектов, создании и расширении территорий морских и речных портов,строительстве, реконструкции и эксплуатациигидротехнических сооружений
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseHydrologyVolumeCuttings
    {
        [XmlElement("row")]
        public List<volumeCuttingsRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseHydrologyVolumeCuttings class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseHydrologyVolumeCuttings()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Строительство, реконструкция, эксплуатация линейных объектов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseLinearObjects
    {
        /// <summary>
        /// Сведения о характеристиках и обосновании проектируемых видов и объемов работ по строительству, реконструкции,эксплуатации линейных объектов
        /// </summary>
        public forestDevelopmentProjectForestUseLinearObjectsCharacteristicsWork characteristicsWork { get; set; }
        /// <summary>
        /// Характеристика существующих и проектируемых объектов,строений и сооружений при строительстве, реконструкции,эксплуатации линейных объектов на лесном участке
        /// </summary>
        public forestDevelopmentProjectForestUseLinearObjectsLocationObject locationObject { get; set; }
        /// <summary>
        /// Проектируемый объем рубок лесных насаждений на лесном участке, предназначенном для строительства,реконструкции, эксплуатации линейных объектов
        /// </summary>
        public forestDevelopmentProjectForestUseLinearObjectsVolumeCuttings volumeCuttings { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseLinearObjects class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseLinearObjects()
        {
            volumeCuttings = new forestDevelopmentProjectForestUseLinearObjectsVolumeCuttings();
            locationObject = new forestDevelopmentProjectForestUseLinearObjectsLocationObject();
            characteristicsWork = new forestDevelopmentProjectForestUseLinearObjectsCharacteristicsWork();
        }
    }
    
    /// <summary>
    /// Сведения о характеристиках и обосновании проектируемых видов и объемов работ по строительству, реконструкции,эксплуатации линейных объектов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseLinearObjectsCharacteristicsWork
    {
        [XmlElement("row")]
        public List<characteristicsWorkRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseLinearObjectsCharacteristicsWork class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseLinearObjectsCharacteristicsWork()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Характеристика существующих и проектируемых объектов,строений и сооружений при строительстве, реконструкции,эксплуатации линейных объектов на лесном участке
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseLinearObjectsLocationObject
    {
        [XmlElement("row")]
        public List<locationObjectRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseLinearObjectsLocationObject class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseLinearObjectsLocationObject()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Проектируемый объем рубок лесных насаждений на лесном участке, предназначенном для строительства,реконструкции, эксплуатации линейных объектов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseLinearObjectsVolumeCuttings
    {
        [XmlElement("row")]
        public List<volumeCuttingsRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseLinearObjectsVolumeCuttings class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseLinearObjectsVolumeCuttings()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Создание и эксплуатация объектов лесоперерабатывающей инфраструктуры
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseTimberProcessing
    {
        /// <summary>
        /// Сведения о характеристиках и обосновании проектируемых видов и объемов работ по переработке древесины и иных лесных ресурсов
        /// </summary>
        public forestDevelopmentProjectForestUseTimberProcessingCharacteristicsWork characteristicsWork { get; set; }
        /// <summary>
        /// Характеристика существующих и проектируемых объектов лесоперерабатывающей инфраструктуры
        /// </summary>
        public forestDevelopmentProjectForestUseTimberProcessingLocationObject locationObject { get; set; }
        /// <summary>
        /// Проектируемый объем рубок лесных насаждений на лесном участке, предназначенном для создания объектов лесоперерабатывающей инфраструктуры
        /// </summary>
        public forestDevelopmentProjectForestUseTimberProcessingVolumeCuttings volumeCuttings { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseTimberProcessing class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseTimberProcessing()
        {
            volumeCuttings = new forestDevelopmentProjectForestUseTimberProcessingVolumeCuttings();
            locationObject = new forestDevelopmentProjectForestUseTimberProcessingLocationObject();
            characteristicsWork = new forestDevelopmentProjectForestUseTimberProcessingCharacteristicsWork();
        }
    }
    
    /// <summary>
    /// Сведения о характеристиках и обосновании проектируемых видов и объемов работ по переработке древесины и иных лесных ресурсов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseTimberProcessingCharacteristicsWork
    {
        [XmlElement("row")]
        public List<characteristicsWorkRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseTimberProcessingCharacteristicsWork class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseTimberProcessingCharacteristicsWork()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Характеристика существующих и проектируемых объектов лесоперерабатывающей инфраструктуры
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseTimberProcessingLocationObject
    {
        [XmlElement("row")]
        public List<locationObjectRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseTimberProcessingLocationObject class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseTimberProcessingLocationObject()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Проектируемый объем рубок лесных насаждений на лесном участке, предназначенном для создания объектов лесоперерабатывающей инфраструктуры
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseTimberProcessingVolumeCuttings
    {
        [XmlElement("row")]
        public List<volumeCuttingsRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseTimberProcessingVolumeCuttings class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseTimberProcessingVolumeCuttings()
        {
            number = "1";
        }
    }
    
    /// <summary>
    /// Изыскательские работы
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseSurveyWork
    {
        /// <summary>
        /// Сведения о характеристиках и обосновании проектируемых видов и объемах проектируемых изыскательских работ
        /// </summary>
        public forestDevelopmentProjectForestUseSurveyWorkCharacteristicsWork characteristicsWork { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseSurveyWork class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseSurveyWork()
        {
            characteristicsWork = new forestDevelopmentProjectForestUseSurveyWorkCharacteristicsWork();
        }
    }
    
    /// <summary>
    /// Сведения о характеристиках и обосновании проектируемых видов и объемах проектируемых изыскательских работ
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseSurveyWorkCharacteristicsWork
    {
        [XmlElement("row")]
        public List<forestDevelopmentProjectForestUseSurveyWorkCharacteristicsWorkRow> row { get; set; }
        /// <summary>
        /// Номер таблицы
        /// </summary>
        [XmlAttribute]
        [DefaultValue("1")]
        public string number { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseSurveyWorkCharacteristicsWork class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseSurveyWorkCharacteristicsWork()
        {
            number = "1";
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.2")]
    public class forestDevelopmentProjectForestUseSurveyWorkCharacteristicsWorkRow
    {
        public characteristicsWorkRow characteristics { get; set; }
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        [FractionDigitsAttribute(6)]
        [MaxDigitsAttribute(17)]
        [RangeAttribute(0D, 9999999999.9999981D)]
        public decimal area { get; set; }
        
        /// <summary>
        /// forestDevelopmentProjectForestUseSurveyWorkCharacteristicsWorkRow class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseSurveyWorkCharacteristicsWorkRow()
        {
            location = new location();
            characteristics = new characteristicsWorkRow();
        }
    }
}
#pragma warning restore
