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
    /// Субъекты РФ
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    [XmlRootAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1", IsNullable=true)]
    public class subject : reference
    {
        private static XmlSerializer _serializerXml;
        private static XmlSerializer SerializerXml
        {
            get
            {
                if ((_serializerXml == null))
                {
                    _serializerXml = new XmlSerializerFactory().CreateSerializer(typeof(subject));
                }
                return _serializerXml;
            }
        }
        
        #region Serialize/Deserialize
        /// <summary>
        /// Serialize subject object
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
        /// Deserializes subject object
        /// </summary>
        /// <param name="input">string to deserialize</param>
        /// <param name="obj">Output subject object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string input, out subject obj, out Exception exception)
        {
            exception = null;
            obj = default(subject);
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
        
        public static bool Deserialize(string input, out subject obj)
        {
            Exception exception = null;
            return Deserialize(input, out obj, out exception);
        }
        
        public new static subject Deserialize(string input)
        {
            StringReader stringReader = null;
            try
            {
                stringReader = new StringReader(input);
                return ((subject)(SerializerXml.Deserialize(XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }
        
        public static subject Deserialize(Stream s)
        {
            return ((subject)(SerializerXml.Deserialize(s)));
        }
        #endregion
        
        /// <summary>
        /// Serializes current subject object into file
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
        /// Deserializes xml markup from file into an subject object
        /// </summary>
        /// <param name="fileName">File to load and deserialize</param>
        /// <param name="obj">Output subject object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out subject obj, out Exception exception)
        {
            exception = null;
            obj = default(subject);
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
        
        public static bool LoadFromFile(string fileName, out subject obj)
        {
            Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }
        
        public new static subject LoadFromFile(string fileName)
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
    /// Ссылка на справочник (НСИ)
    /// </summary>
    [XmlInclude(typeof(subject))]
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
    /// Фазы развития очага вредных организмов
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
    /// Мероприятия по защите лесов
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
    /// Категория состояния деревьев
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
    /// Признак повреждения
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class damageSymptom : reference
    {
        /// <summary>
        /// Группы признаков
        /// </summary>
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
    /// Причина повреждения
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class damageReason : reference
    {
        /// <summary>
        /// Сокращенное наименование болезни или вредителя
        /// </summary>
        public string reduction { get; set; }
        /// <summary>
        /// Группы причин
        /// </summary>
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
    /// Лесные районы
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
    /// Муниципальные образования
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class municipalDistrict : reference
    {
        /// <summary>
        /// Родительский показатель
        /// </summary>
        public reference parent { get; set; }
        /// <summary>
        /// Тип муниципального образования
        /// </summary>
        public municipalDistrictType type { get; set; }
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
    /// Группы показателей отчетов
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
    /// Показатели отчетов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class reportRate : reference
    {
        /// <summary>
        /// Родительский показатель
        /// </summary>
        public reference parent { get; set; }
        /// <summary>
        /// Вид показателя
        /// </summary>
        public reference reportRateType { get; set; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public reference unitType { get; set; }
        /// <summary>
        /// Код строки отчета
        /// </summary>
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
    /// Категории защитных лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class protectionCategory : reference
    {
        /// <summary>
        /// Родительский показатель
        /// </summary>
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
    /// Целевые назначения лесов
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
    /// Объекты инфраструктуры
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
    
    /// <summary>
    /// Типы объектов мероприятий
    /// </summary>
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
    /// Виды мероприятий
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
    /// Вид заготавливаемых лесных ресурсов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class resource : reference
    {
        /// <summary>
        /// Вид использования лесов
        /// </summary>
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
    /// Вид использования
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
    /// Единицы измерения
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class unitType : reference
    {
        /// <summary>
        /// Сокращение
        /// </summary>
        public string abbreviation { get; set; }
    }
    
    /// <summary>
    /// Бонитет
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
    /// Вид рубки
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
    /// Сортименты
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
    /// Виды древесины
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class wood : reference
    {
        /// <summary>
        /// Код ОКПД
        /// </summary>
        public string okpd { get; set; }
        /// <summary>
        /// Код ТН ВЭД
        /// </summary>
        public string tnved { get; set; }
    }
    
    /// <summary>
    /// Древесные породы
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class tree : reference
    {
        /// <summary>
        /// Сокращение
        /// </summary>
        public string abbreviation { get; set; }
    }
    
    /// <summary>
    /// Урочища
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class tract : reference
    {
        /// <summary>
        /// Субъект РФ
        /// </summary>
        public reference subject { get; set; }
        /// <summary>
        /// Участковое лесничество
        /// </summary>
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
    /// Участковое лесничество
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class subforestry : reference
    {
        /// <summary>
        /// Субъект РФ
        /// </summary>
        public reference subject { get; set; }
        /// <summary>
        /// Лесничество
        /// </summary>
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
    /// Лесничество
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class forestry : reference
    {
        /// <summary>
        /// Субъект РФ
        /// </summary>
        public reference subject { get; set; }
        /// <summary>
        /// Вид земель
        /// </summary>
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
    /// Органы исполнительной власти
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://rosleshoz.gov.ru/xmlns/catalogsTypes/4.1")]
    public class executiveAuthority : reference
    {
        /// <summary>
        /// Субъект РФ
        /// </summary>
        public reference subject { get; set; }
        /// <summary>
        /// Вид земель
        /// </summary>
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
    /// Виды земель
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
