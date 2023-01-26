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

#pragma warning disable
namespace FDP
{
     /// <summary>
    /// Описание проекта освоения лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    [XmlRootAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1", IsNullable = false)]
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
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
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
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
    public class file
    {
        public string id { get; set; }
        public string name { get; set; }
        public string extension { get; set; }
        [XmlElement(DataType = "base64Binary")]
        public byte[] signature { get; set; }
    }

    /// <summary>
    /// Местоположение работ
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
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
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
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
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
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
    /// Характеристика и обоснование проектируемых видов и объемов работ
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
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
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
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
        public decimal area { get; set; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public reference unitType { get; set; }
        /// <summary>
        /// Объем
        /// </summary>
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
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
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
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
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
        public decimal fund { get; set; }
        /// <summary>
        /// Проектируемые ежегодные объемы заготовки
        /// </summary>
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
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
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
        public decimal area { get; set; }
        /// <summary>
        /// Корневой запас
        /// </summary>
        public decimal rootVolume { get; set; }
        /// <summary>
        /// Корневой запас хвойных насаждений
        /// </summary>
        public decimal rootConiferVolume { get; set; }
        /// <summary>
        /// Ликвидный запас
        /// </summary>
        public decimal liquidVolume { get; set; }
        /// <summary>
        /// Ликвидный запас хвойных насаждений
        /// </summary>
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
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
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
        public decimal rootVolume { get; set; }
        /// <summary>
        /// Объем рубок(в том числе хвойные корневой запас), м3
        /// </summary>
        public decimal rootConiferVolume { get; set; }
        /// <summary>
        /// Объем рубок(ликвидный запас), м3
        /// </summary>
        public decimal liquidVolume { get; set; }
        /// <summary>
        /// Объем рубок(в том числе хвойные ликвидный запас), м3
        /// </summary>
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
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
    public class employee
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string patronimic_name { get; set; }
        public string post { get; set; }
        public string basisAuthority { get; set; }
        public string number { get; set; }
        [XmlElement(DataType = "date")]
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
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
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
    /// Индивидуальный предприниматель
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
    public class individualEntrepreneur
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string patronimic_name { get; set; }
        [XmlElement(IsNullable = true)]
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
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
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
    /// Юр.лицо
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
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
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
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
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/cTypes/4.2")]
    public class contract
    {
        public string type { get; set; }
        public string number { get; set; }
        [XmlElement(DataType = "date")]
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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectPreamble
    {
        /// <summary>
        /// Описание местоположения лесного участка
        /// </summary>
        public string location { get; set; }
        /// <summary>
        /// Площадь лесного участка, га
        /// </summary>
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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
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
        [XmlElement(DataType = "date")]
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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralInfo
    {
        /// <summary>
        /// Перечень предоставленных в аренду, постоянное(бессрочное) пользование, в отношении которых установлен сервитут или публичный сервитут, лесных кварталов, лесотаксационных выделов или их частей
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralInfoRow> listQuartersTaxationUnit { get; set; }
        /// <summary>
        /// Распределение площади лесного участка по видам целевого назначения лесов на защитные (по их категориям), эксплуатационные и резервные леса
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralInfoRow1> distributionSpecialPurpose { get; set; }
        /// <summary>
        /// Таксационная характеристика лесных насажденийна лесном участке
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralInfoRow2> averageTaxationRates { get; set; }
        /// <summary>
        /// Характеристика имеющихся в границах лесного участка особо охраняемых природных территорий и объектов (границы и режим особой охраны), мероприятия по сохранению объектов биоразнообразия
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralInfoRow3> speciallyProtectedNaturalAreas { get; set; }
        /// <summary>
        /// Сведения о наличии загрязнения лесов (в том числе нефтяного, радиоактивного)
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralInfoRow5> contamination { get; set; }
        /// <summary>
        /// Сведения о наличии мест обитания редких и находящихся под угрозой исчезновения видов животных и мест произрастания редких и находящихся под угрозой исчезновения видов деревьев, кустарников, лиан и иных лесных растений
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralInfoRow6> rarePlantsAnimals { get; set; }
        /// <summary>
        /// Сведения о материалах специальных изысканий, исследований или иных специальных обследований (при наличии)
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralInfoRow8> surveys { get; set; }
        /// <summary>
        /// Сведения об обременениях лесного участка
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralInfoRow9> encumbrances { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralInfo class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfo()
        {
            averageTaxationRates = new List<forestDevelopmentProjectGeneralInfoRow2>();
            distributionSpecialPurpose = new List<forestDevelopmentProjectGeneralInfoRow1>();
            listQuartersTaxationUnit = new List<forestDevelopmentProjectGeneralInfoRow>();
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralInfoRow
    {
        /// <summary>
        /// В поле taxationUnit может присутствовать только одно значение (без перечисления через запятую или тире
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Площадь эксплуатационная
        /// </summary>
        public decimal area { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralInfoRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoRow()
        {
            location = new location();
        }
    }

    /// <summary>
    /// Сведения о лесном участке
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralInfoRow1
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
        /// Площадь эксплуатационная
        /// </summary>
        public decimal area { get; set; }
        /// <summary>
        /// Процент
        /// </summary>
        public decimal percent { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralInfoRow1 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoRow1()
        {
            specialPurpose = new reference();
        }
    }

    /// <summary>
    /// Сведения о лесном участке
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralInfoRow2
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
        /// Площадь эксплуатационная
        /// </summary>
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
        public decimal volume { get; set; }
        /// <summary>
        /// Средний запас спелых и перестойных насаждений на 1 га, м.куб
        /// </summary>
        public decimal ripeAndOverripe { get; set; }
        /// <summary>
        /// Средний прирост по запасу на 1 га, м.куб
        /// </summary>
        public decimal growth { get; set; }
        /// <summary>
        /// Состав насаждения
        /// </summary>
        public string composition { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralInfoRow2 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoRow2()
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
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/sTypes/4.1")]
    public enum farm
    {
        Мягколиственное,
        Твердолиственное,
        Хвойное,
    }

    /// <summary>
    /// Сведения о лесном участке
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralInfoRow3
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
        /// В поле taxationUnit может присутствовать только одно значение (без перечисления через запятую или тире
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<location> location { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralInfoRow3 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoRow3()
        {
            location = new List<location>();
        }
    }

    /// <summary>
    /// Сведения о лесном участке
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralInfoRow5
    {
        /// <summary>
        /// В поле taxationUnit может присутствовать только одно значение (без перечисления через запятую или тире
        /// </summary>
        public location location { get; set; }
        /// <summary>
        /// Площадь эксплуатационная
        /// </summary>
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
        /// Средний запас насаждений покрытых лесной растительностью на 1 га, м.куб
        /// </summary>
        public float volume { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralInfoRow5 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoRow5()
        {
            unitType = new reference();
            location = new location();
        }
    }

    /// <summary>
    /// Сведения о лесном участке
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralInfoRow6
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
        /// В поле taxationUnit может присутствовать только одно значение (без перечисления через запятую или тире
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<location> location { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralInfoRow6 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralInfoRow6()
        {
            location = new List<location>();
        }
    }

    /// <summary>
    /// Сведения о лесном участке
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralInfoRow8
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
    /// Сведения о лесном участке
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralInfoRow9
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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralObjects
    {
        /// <summary>
        /// Характеристика существующих и проектируемых объектов лесной инфраструктуры на лесном участке
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationObjectRow> locationObject { get; set; }
        /// <summary>
        /// Проектируемый объем рубок лесных насаждений, при создании
        /// объектов лесной инфраструктуры
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<volumeCuttingsRow> volumeCuttings { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralObjects class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralObjects()
        {
            volumeCuttings = new List<volumeCuttingsRow>();
            locationObject = new List<locationObjectRow>();
        }
    }

    /// <summary>
    /// Мероприятия по охране, защите и воспроизводству лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasures
    {
        /// <summary>
        /// Характеристика территории лесного участка по классам пожарной опасности
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralMeasuresRow> characteristicFireClasses { get; set; }
        /// <summary>
        /// Характеристика водных объектов
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralMeasuresRow1> characteristicWaterObjects { get; set; }
        /// <summary>
        /// Обоснование и характеристика проектируемых видов, объемов и сроков выполнения мероприятий по противопожарному обустройству лесов с учетом объектов, созданных при использовании лесов в соответствии с лесохозяйственным регламентом лесничества, и их территориальное размещение
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralMeasuresRow3> designedFirePrevention { get; set; }
        /// <summary>
        /// Сведения о наличии и потребности в пожарной технике,оборудовании, снаряжении и инвентаре на лесном участке
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralMeasuresRow4> fireEquipment { get; set; }
        /// <summary>
        /// Обоснование и характеристика видов и объемов планируемых профилактических мероприятий по защите лесов, с указанием мест проведения профилактических мероприятий
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralMeasuresRow5> prevention { get; set; }
        /// <summary>
        /// Сведения о наличии очагов вредных организмов на лесном участке, с указанием их местоположения и мероприятий,необходимых для ликвидации очагов вредных организмов
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralMeasuresRow6> fociHarmfulOrganisms { get; set; }
        /// <summary>
        /// Сведения о повреждении и гибели лесов на начало действия проекта освоения лесов с указанием их местоположения
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralMeasuresRow7> damageDeath { get; set; }
        /// <summary>
        /// Обоснование и характеристика видов и объемов планируемых санитарно-оздоровительных мероприятий на лесном участке, с указанием мест проведения санитарно-оздоровительных мероприятий
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralMeasuresRow8> sanitaryRecreationalMeasures { get; set; }
        /// <summary>
        /// Обоснование и характеристика проектируемых видов и объемов защитных мероприятий в зонах радиоактивного загрязнения (если таковые имеются)
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralMeasuresRow9> zonesRadioactiveContamination { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, входящих в зоны радиоактивного загрязнения (если таковые имеются), с указанием ограничений по видам использования лесных участков и заготовки лесных ресурсов
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralMeasuresRow10> listTaxationUnitRadioactiveZone { get; set; }
        /// <summary>
        /// Площадь земель, нуждающихся в лесовосстановлении
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralMeasuresRow11> areaNeedReforestation { get; set; }
        /// <summary>
        /// Плановые способы и объемы лесовосстановления
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralMeasuresRow12> reforestationPlan { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых планируются мероприятия по лесовосстановлению
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralMeasuresRow13> listReforestation { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых планируются мероприятия по лесовосстановлению
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralMeasuresRow14> listForestCare { get; set; }
        /// <summary>
        /// Площадь лесов, нуждающихся в уходе за лесами, проектируемые виды и ежегодные объемы ухода за лесами при воспроизводстве лесов, не связанные с заготовкой древесины
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralMeasuresRow15> areaForestCare { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируются мероприятия по охране объектов животного и растительного мира, водных объектов
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectGeneralMeasuresRow16> listProtection { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralMeasures class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasures()
        {
            listProtection = new List<forestDevelopmentProjectGeneralMeasuresRow16>();
            areaForestCare = new List<forestDevelopmentProjectGeneralMeasuresRow15>();
            listForestCare = new List<forestDevelopmentProjectGeneralMeasuresRow14>();
            listReforestation = new List<forestDevelopmentProjectGeneralMeasuresRow13>();
            reforestationPlan = new List<forestDevelopmentProjectGeneralMeasuresRow12>();
            areaNeedReforestation = new List<forestDevelopmentProjectGeneralMeasuresRow11>();
            listTaxationUnitRadioactiveZone = new List<forestDevelopmentProjectGeneralMeasuresRow10>();
            zonesRadioactiveContamination = new List<forestDevelopmentProjectGeneralMeasuresRow9>();
            sanitaryRecreationalMeasures = new List<forestDevelopmentProjectGeneralMeasuresRow8>();
            damageDeath = new List<forestDevelopmentProjectGeneralMeasuresRow7>();
            fociHarmfulOrganisms = new List<forestDevelopmentProjectGeneralMeasuresRow6>();
            prevention = new List<forestDevelopmentProjectGeneralMeasuresRow5>();
            fireEquipment = new List<forestDevelopmentProjectGeneralMeasuresRow4>();
            designedFirePrevention = new List<forestDevelopmentProjectGeneralMeasuresRow3>();
            characteristicWaterObjects = new List<forestDevelopmentProjectGeneralMeasuresRow1>();
            characteristicFireClasses = new List<forestDevelopmentProjectGeneralMeasuresRow>();
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRow
    {
        [XmlElement(Order = 0)]
        public location location { get; set; }
        /// <summary>
        /// Площадь I класса пожарной опасности
        /// </summary>
        [XmlElement(Order = 1)]
        public decimal areaFireClasses_I { get; set; }
        /// <summary>
        /// Площадь II класса пожарной опасности
        /// </summary>
        [XmlElement(Order = 2)]
        public decimal areaFireClasses_II { get; set; }
        /// <summary>
        /// Площадь III класса пожарной опасности
        /// </summary>
        [XmlElement(Order = 3)]
        public decimal areaFireClasses_III { get; set; }
        /// <summary>
        /// Площадь IV класса пожарной опасности
        /// </summary>
        [XmlElement(Order = 4)]
        public decimal areaFireClasses_IV { get; set; }
        /// <summary>
        /// Площадь V класса пожарной опасности
        /// </summary>
        [XmlElement(Order = 5)]
        public decimal areaFireClasses_V { get; set; }
        [XmlElement("areaFireClasses_V", Order = 6)]
        public decimal areaFireClasses_V1 { get; set; }
        /// <summary>
        /// Площадь общая
        /// </summary>
        [XmlElement(Order = 7)]
        public decimal area { get; set; }
        /// <summary>
        /// Средний класс
        /// </summary>
        [XmlElement(Order = 8)]
        public string middleClass { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresRow class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRow()
        {
            location = new location();
        }
    }

    /// <summary>
    /// Мероприятия по охране, защите и воспроизводству лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRow1
    {
        /// <summary>
        /// Водный объект
        /// </summary>
        public string waterObject { get; set; }
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<location> location { get; set; }
        /// <summary>
        /// Площадь общая
        /// </summary>
        public decimal area { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresRow1 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRow1()
        {
            location = new List<location>();
        }
    }

    /// <summary>
    /// Мероприятия по охране, защите и воспроизводству лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRow3
    {
        /// <summary>
        /// Объект противопожарного обустройства
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRowFirePreventionObject firePreventionObject { get; set; }
        /// <summary>
        /// Вид мероприятия
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRowTypeEvent typeEvent { get; set; }
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
        public forestDevelopmentProjectGeneralMeasuresRowDeadlines deadlines { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresRow3 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRow3()
        {
            unitType = new reference();
            location = new location();
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public enum forestDevelopmentProjectGeneralMeasuresRowFirePreventionObject
    {
        I,
        II,
        IV,
        V,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public enum forestDevelopmentProjectGeneralMeasuresRowTypeEvent
    {
        I,
        II,
        IV,
        V,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public enum forestDevelopmentProjectGeneralMeasuresRowDeadlines
    {
        I,
        II,
        IV,
        V,
    }

    /// <summary>
    /// Мероприятия по охране, защите и воспроизводству лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRow4
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRowName name { get; set; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public reference unitType { get; set; }
        /// <summary>
        /// В соответствии с действующими нормативами
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRowRegulations regulations { get; set; }
        /// <summary>
        /// В наличие
        /// </summary>
        public float availability { get; set; }
        /// <summary>
        /// Потребность
        /// </summary>
        public float need { get; set; }
        public string location { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresRow4 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRow4()
        {
            unitType = new reference();
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public enum forestDevelopmentProjectGeneralMeasuresRowName
    {
        I,
        II,
        IV,
        V,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public enum forestDevelopmentProjectGeneralMeasuresRowRegulations
    {
        I,
        II,
        IV,
        V,
    }

    /// <summary>
    /// Мероприятия по охране, защите и воспроизводству лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRow5
    {
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public reference specialPurpose { get; set; }
        public location location { get; set; }
        /// <summary>
        /// Лечение деревьев, шт.
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRowTreatmentTtrees treatmentTtrees { get; set; }
        /// <summary>
        /// Применение пестицидов для предотвращения появления очагов вредных организмов, га
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRowPesticideApplication pesticideApplication { get; set; }
        /// <summary>
        /// Использование удобрений, га
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRowFertilizerUse fertilizerUse { get; set; }
        /// <summary>
        /// Улучшение условий, шт
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRowImprovingConditions improvingConditions { get; set; }
        /// <summary>
        /// Охрана местообитаний, га
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRowHabitatProtection habitatProtection { get; set; }
        /// <summary>
        /// Посев травянистых нектароносных растений, га
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRowSowing sowing { get; set; }
        /// <summary>
        /// Использование феромонов, шт.
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRowUsePheromones usePheromones { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresRow5 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRow5()
        {
            usePheromones = new forestDevelopmentProjectGeneralMeasuresRowUsePheromones();
            sowing = new forestDevelopmentProjectGeneralMeasuresRowSowing();
            habitatProtection = new forestDevelopmentProjectGeneralMeasuresRowHabitatProtection();
            improvingConditions = new forestDevelopmentProjectGeneralMeasuresRowImprovingConditions();
            fertilizerUse = new forestDevelopmentProjectGeneralMeasuresRowFertilizerUse();
            pesticideApplication = new forestDevelopmentProjectGeneralMeasuresRowPesticideApplication();
            treatmentTtrees = new forestDevelopmentProjectGeneralMeasuresRowTreatmentTtrees();
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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRowTreatmentTtrees
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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRowPesticideApplication
    {
        /// <summary>
        /// Всего
        /// </summary>
        public decimal volume { get; set; }
        /// <summary>
        /// Ежегодный объем
        /// </summary>
        public decimal annualVolume { get; set; }
    }

    /// <summary>
    /// Использование удобрений, га
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRowFertilizerUse
    {
        /// <summary>
        /// Всего
        /// </summary>
        public decimal volume { get; set; }
        /// <summary>
        /// Ежегодный объем
        /// </summary>
        public decimal annualVolume { get; set; }
    }

    /// <summary>
    /// Улучшение условий, шт
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRowImprovingConditions
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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRowHabitatProtection
    {
        /// <summary>
        /// Всего
        /// </summary>
        public decimal volume { get; set; }
        /// <summary>
        /// Ежегодный объем
        /// </summary>
        public decimal annualVolume { get; set; }
    }

    /// <summary>
    /// Посев травянистых нектароносных растений, га
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRowSowing
    {
        /// <summary>
        /// Всего
        /// </summary>
        public decimal volume { get; set; }
        /// <summary>
        /// Ежегодный объем
        /// </summary>
        public decimal annualVolume { get; set; }
    }

    /// <summary>
    /// Использование феромонов, шт.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRowUsePheromones
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
    /// Мероприятия по охране, защите и воспроизводству лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRow6
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRowName1 name { get; set; }
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public reference specialPurpose { get; set; }
        public location location { get; set; }
        /// <summary>
        /// Площадь общая
        /// </summary>
        public decimal area { get; set; }
        /// <summary>
        /// Мероприятия, необходимые для ликвидации очагов вредных организмов
        /// </summary>
        public string measure { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresRow6 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRow6()
        {
            location = new location();
            specialPurpose = new reference();
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public enum forestDevelopmentProjectGeneralMeasuresRowName1
    {
        I,
        II,
        IV,
        V,
    }

    /// <summary>
    /// Мероприятия по охране, защите и воспроизводству лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRow7
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRowName2 name { get; set; }
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public reference specialPurpose { get; set; }
        public location location { get; set; }
        /// <summary>
        /// Площадь поврежденных и погибших насаждений нарастающим итогом, га
        /// </summary>
        public decimal areaDamage { get; set; }
        /// <summary>
        /// Площадь погибших насаждений нарастающим итогом, га
        /// </summary>
        public decimal areaDeath { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresRow7 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRow7()
        {
            location = new location();
            specialPurpose = new reference();
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public enum forestDevelopmentProjectGeneralMeasuresRowName2
    {
        I,
        II,
        IV,
        V,
    }

    /// <summary>
    /// Мероприятия по охране, защите и воспроизводству лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRow8
    {
        /// <summary>
        /// Вид санитарно-оздоровительного мероприятия
        /// </summary>
        public reference sanritaryRecreationalMeasure { get; set; }
        /// <summary>
        /// Целевое назначение лесов
        /// </summary>
        public reference specialPurpose { get; set; }
        public location location { get; set; }
        /// <summary>
        /// Площадь общая
        /// </summary>
        public decimal area { get; set; }
        /// <summary>
        /// Всего
        /// </summary>
        public decimal volume { get; set; }
        /// <summary>
        /// Вырубаемый запас (ликвидный), м3
        /// </summary>
        public decimal liquidVolume { get; set; }
        /// <summary>
        /// Вырубаемый запас (деловой), м3
        /// </summary>
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
        /// forestDevelopmentProjectGeneralMeasuresRow8 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRow8()
        {
            location = new location();
            specialPurpose = new reference();
            sanritaryRecreationalMeasure = new reference();
        }
    }

    /// <summary>
    /// Мероприятия по охране, защите и воспроизводству лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRow9
    {
        public location location { get; set; }
        /// <summary>
        /// Зона загрязнения (слабая, средняя, сильная)
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRowContaminatedZone contaminatedZone { get; set; }
        /// <summary>
        /// Установка предупреждающих аншлагов, шт.
        /// </summary>
        public float installationWarningBoards { get; set; }
        /// <summary>
        /// Радиационный контроль лесных ресурсов (по видам)
        /// </summary>
        public decimal radiationControl { get; set; }
        /// <summary>
        /// Дозиметрический контроль при проведении лесохозяйственных работ
        /// </summary>
        public decimal dosimetricControl { get; set; }
        /// <summary>
        /// Прочие защитные мероприятия
        /// </summary>
        public decimal otherEvents { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresRow9 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRow9()
        {
            location = new location();
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public enum forestDevelopmentProjectGeneralMeasuresRowContaminatedZone
    {
        слабая,
        средняя,
        сильная,
    }

    /// <summary>
    /// Мероприятия по охране, защите и воспроизводству лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRow10
    {
        public location location { get; set; }
        /// <summary>
        /// Площадь общая
        /// </summary>
        public decimal area { get; set; }
        /// <summary>
        /// Зона загрязнения (слабая, средняя, сильная)
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRowContaminatedZone1 contaminatedZone { get; set; }
        /// <summary>
        /// Ограничение использования лесного участка
        /// </summary>
        public string usageRestriction { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresRow10 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRow10()
        {
            location = new location();
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public enum forestDevelopmentProjectGeneralMeasuresRowContaminatedZone1
    {
        слабая,
        средняя,
        сильная,
    }

    /// <summary>
    /// Мероприятия по охране, защите и воспроизводству лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRow11
    {
        /// <summary>
        /// Категория земель
        /// </summary>
        public reference landCategory { get; set; }
        public location location { get; set; }
        /// <summary>
        /// Площадь общая
        /// </summary>
        public decimal area { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresRow11 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRow11()
        {
            location = new location();
            landCategory = new reference();
        }
    }

    /// <summary>
    /// Мероприятия по охране, защите и воспроизводству лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRow12
    {
        /// <summary>
        /// Категория земель
        /// </summary>
        public reference landCategory { get; set; }
        /// <summary>
        /// Посев травянистых нектароносных растений, га
        /// </summary>
        public decimal sowing { get; set; }
        /// <summary>
        /// Посадка
        /// </summary>
        public decimal landing { get; set; }
        /// <summary>
        /// Комбинированное лесовосстановление
        /// </summary>
        public decimal combined { get; set; }
        /// <summary>
        /// Естественное лесовосстановление
        /// </summary>
        public decimal natural { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresRow12 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRow12()
        {
            landCategory = new reference();
        }
    }

    /// <summary>
    /// Мероприятия по охране, защите и воспроизводству лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRow13
    {
        /// <summary>
        /// Категория земель
        /// </summary>
        public reference landCategory { get; set; }
        public location location { get; set; }
        /// <summary>
        /// Площадь общая
        /// </summary>
        public decimal area { get; set; }
        /// <summary>
        /// Планируемый способ лесовосстановления
        /// </summary>
        public string conditionsProjectedMethod { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresRow13 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRow13()
        {
            location = new location();
            landCategory = new reference();
        }
    }

    /// <summary>
    /// Мероприятия по охране, защите и воспроизводству лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRow14
    {
        /// <summary>
        /// Вид ухода
        /// </summary>
        public string typeCare { get; set; }
        public location location { get; set; }
        /// <summary>
        /// Целевая порода
        /// </summary>
        public reference tree { get; set; }
        /// <summary>
        /// Площадь общая
        /// </summary>
        public decimal area { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresRow14 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRow14()
        {
            tree = new reference();
            location = new location();
        }
    }

    /// <summary>
    /// Мероприятия по охране, защите и воспроизводству лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRow15
    {
        /// <summary>
        /// Хозяйство
        /// </summary>
        public farm farm { get; set; }
        /// <summary>
        /// Целевая порода
        /// </summary>
        public reference tree { get; set; }
        /// <summary>
        /// Площадь общая
        /// </summary>
        public decimal area { get; set; }
        /// <summary>
        /// Ежегодная площадь ухода за лесами, га
        /// </summary>
        public decimal annualArea { get; set; }
        /// <summary>
        /// Вид ухода
        /// </summary>
        public string typeCare { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresRow15 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRow15()
        {
            tree = new reference();
        }
    }

    /// <summary>
    /// Мероприятия по охране, защите и воспроизводству лесов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectGeneralMeasuresRow16
    {
        /// <summary>
        /// Наименование объекта, строения, сооружения
        /// </summary>
        public string objectProtection { get; set; }
        /// <summary>
        /// Проектируемые мероприятия
        /// </summary>
        public string measureProtection { get; set; }
        public location location { get; set; }
        /// <summary>
        /// Площадь общая
        /// </summary>
        public decimal area { get; set; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public reference unitType { get; set; }
        /// <summary>
        /// Всего
        /// </summary>
        public float volume { get; set; }

        /// <summary>
        /// forestDevelopmentProjectGeneralMeasuresRow16 class constructor
        /// </summary>
        public forestDevelopmentProjectGeneralMeasuresRow16()
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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseWoodHarvesting
    {
        /// <summary>
        /// Установленный объем заготовки древесины на лесном участке
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectForestUseWoodHarvestingRow> volumeCuttingsWood { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируется заготовка древесины
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectForestUseWoodHarvestingRow1> locationCuttings { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseWoodHarvesting class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseWoodHarvesting()
        {
            locationCuttings = new List<forestDevelopmentProjectForestUseWoodHarvestingRow1>();
            volumeCuttingsWood = new List<forestDevelopmentProjectForestUseWoodHarvestingRow>();
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseWoodHarvestingRow
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
        public decimal area { get; set; }
        /// <summary>
        /// запас (корневой), тыс. м3
        /// </summary>
        public decimal rootVolume { get; set; }
        /// <summary>
        /// запас (ликвидный), тыс. м3
        /// </summary>
        public decimal liquidVolume { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseWoodHarvestingRow class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseWoodHarvestingRow()
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
    [XmlTypeAttribute(Namespace = "http://rosleshoz.gov.ru/xmlns/sTypes/4.1")]
    public enum formCutting
    {
        [XmlEnumAttribute("Сплошная рубка")]
        Сплошнаярубка,
        [XmlEnumAttribute("Выборочная рубка")]
        Выборочнаярубка,
    }

    /// <summary>
    /// Заготовка древесины
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseWoodHarvestingRow1
    {
        public location location { get; set; }
        /// <summary>
        /// Преобладающая порода
        /// </summary>
        public reference tree { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        public decimal area { get; set; }
        /// <summary>
        /// Запас средний на 1 га, м3
        /// </summary>
        public decimal averageVolume { get; set; }
        /// <summary>
        /// Запас на выделе, м3
        /// </summary>
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
        public decimal samplePercentage { get; set; }
        /// <summary>
        /// Планируемый способ лесовосстановления
        /// </summary>
        public string reforestationMethod { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseWoodHarvestingRow1 class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseWoodHarvestingRow1()
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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseResinHarvesting
    {
        /// <summary>
        /// Проектируемые технологии проведения работ по заготовке живицы
        /// </summary>
        public string technologies { get; set; }
        /// <summary>
        /// Фонд заготовки живицы
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectForestUseResinHarvestingRow> volumeResource { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, проектируемых для заготовки живицы
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectForestUseResinHarvestingRow1> locationResource { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseResinHarvesting class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseResinHarvesting()
        {
            locationResource = new List<forestDevelopmentProjectForestUseResinHarvestingRow1>();
            volumeResource = new List<forestDevelopmentProjectForestUseResinHarvestingRow>();
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseResinHarvestingRow
    {
        /// <summary>
        /// 1. Всего спелых и перестойных насаждений, пригодных для подсочки
        /// </summary>
        public decimal useful { get; set; }
        /// <summary>
        /// 1.1. Из них не вовлечены в подсочку:
        /// </summary>
        public decimal completion { get; set; }
        /// <summary>
        /// в том числе нерентабельные
        /// </summary>
        public decimal unprofitable { get; set; }
        /// <summary>
        /// 2. Может ежегодно находиться в подсочке
        /// </summary>
        public decimal maybe { get; set; }
        /// <summary>
        /// 3. Фактически находится в подсочке
        /// </summary>
        public decimal used { get; set; }
        /// <summary>
        /// 4. Вышедшие из подсочки, всего
        /// </summary>
        public decimal finished { get; set; }
    }

    /// <summary>
    /// Заготовка живицы
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseResinHarvestingRow1
    {
        public location location { get; set; }
        /// <summary>
        /// Преобладающая порода
        /// </summary>
        public reference tree { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        public decimal area { get; set; }
        /// <summary>
        /// Способ подсочки
        /// </summary>
        public string typeHarvesting { get; set; }
        /// <summary>
        /// Выход живицы, кг/га
        /// </summary>
        public decimal resinOutlet { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseResinHarvestingRow1 class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseResinHarvestingRow1()
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
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseNonTimberResourcesHarvesting
    {
        /// <summary>
        /// Проектируемые технологии проведения работ по заготовке недревесных лесных ресурсов
        /// </summary>
        public string technologies { get; set; }
        /// <summary>
        /// Фонд (биологический запас) недревесных лесных ресурсов и проектируемые ежегодные объемы заготовки недревесных лесных ресурсов
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<volumeResourceRow> volumeResource { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируется заготовка недревесных лесных ресурсов
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationResourceRow> locationResource { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseNonTimberResourcesHarvesting class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseNonTimberResourcesHarvesting()
        {
            locationResource = new List<locationResourceRow>();
            volumeResource = new List<volumeResourceRow>();
        }
    }

    /// <summary>
    /// Заготовка пищевых лесных ресурсов и сбор лекарственных растений
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
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
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<volumeResourceRow> volumeResource { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируется заготовка пищевых лесных ресурсов и сбор лекарственных растений
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationResourceRow> locationResource { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseFoodForestrResourcesHarvesting class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseFoodForestrResourcesHarvesting()
        {
            locationResource = new List<locationResourceRow>();
            volumeResource = new List<volumeResourceRow>();
        }
    }

    /// <summary>
    /// Осуществление видов деятельности в сфере охотничьего хозяйства
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseHunting
    {
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируется создание объектов охотничьей инфраструктуры и связанные с их созданием рубки лесных насаждений
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationObjectRow> locationObject { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируется проведение биотехнических мероприятий
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationMeasureRow> locationMeasure { get; set; }
    }

    /// <summary>
    /// Ведение сельского хозяйства
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseAgriculture
    {
        /// <summary>
        /// Характеристика проектируемых технологий ведения сельского хозяйства
        /// </summary>
        public string technologies { get; set; }
        /// <summary>
        /// Основные проектируемые параметры использования лесов для ведения сельского хозяйства
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<volumeResourceRow> volumeResource { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируются мероприятия по ведению сельского хозяйства
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationResourceRow> locationResource { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseAgriculture class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseAgriculture()
        {
            locationResource = new List<locationResourceRow>();
            volumeResource = new List<volumeResourceRow>();
        }
    }

    /// <summary>
    /// Использование лесов для осуществления рыболовства
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseFishing
    {
        /// <summary>
        /// Обоснование и характеристика проектируемых видов и объемов работ по возведению на лесном участке некапитальных строений, сооружений, необходимых для осуществления рыболовства
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<characteristicsWorkRow> characteristicsWork { get; set; }
        /// <summary>
        /// Характеристика существующих и проектируемых некапитальных строений и сооружений на лесном участке при использовании лесов для осуществления рыболовства
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationObjectRow> locationObject { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseFishing class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseFishing()
        {
            locationObject = new List<locationObjectRow>();
            characteristicsWork = new List<characteristicsWorkRow>();
        }
    }

    /// <summary>
    /// Осуществление научно-исследовательской деятельности,образовательной деятельности
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseResearchActivities
    {
        /// <summary>
        /// Сведения о программе научно-исследовательской или образовательной деятельности на лесном участке с обоснованием и характеристикой проектируемых видов и объемов работ
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<characteristicsWorkRow> characteristicsWork { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируется осуществление мероприятий по научно-исследовательской и/или образовательной деятельности и проектируемый объем рубок лесных насаждений, проводимых в целях научно-исследовательской, образовательной деятельности
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationMeasureRow> locationMeasure { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseResearchActivities class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseResearchActivities()
        {
            locationMeasure = new List<locationMeasureRow>();
            characteristicsWork = new List<characteristicsWorkRow>();
        }
    }

    /// <summary>
    /// Осуществление рекреационной деятельности
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseRecreation
    {
        /// <summary>
        /// Ведомость учета деревьев на лесном участке для осуществления рекреационной деятельности
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectForestUseRecreationRow> treeRegister { get; set; }
        /// <summary>
        /// Характеристика существующих и проектируемых объектов капитального строительства, не связанных с созданием лесной инфраструктуры, на лесном участке при использовании лесов для рекреационной деятельности
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationObjectRow> locationCapitalObject { get; set; }
        /// <summary>
        /// Характеристика существующих и проектируемых некапитальных строений и сооружений, не связанных с созданием лесной инфраструктуры, на лесном участке при использовании лесов для рекреационной деятельности
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationObjectRow> locationNonCapitalObject { get; set; }
        /// <summary>
        /// Объем рубок лесных насаждений на лесном участке при создании объектов капитального строительства, не связанных с созданием лесной инфраструктуры при использовании лесов для рекреационной деятельности
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<volumeCuttingsRow> volumeCuttings { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseRecreation class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseRecreation()
        {
            volumeCuttings = new List<volumeCuttingsRow>();
            locationNonCapitalObject = new List<locationObjectRow>();
            locationCapitalObject = new List<locationObjectRow>();
            treeRegister = new List<forestDevelopmentProjectForestUseRecreationRow>();
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
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
    /// Создание лесных плантаций и их эксплуатация
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseForestPlantations
    {
        /// <summary>
        /// Сведения о характеристиках и обосновании проектируемых видов и объемов работ по созданию лесных плантаций и их эксплуатации, технология создания и эксплуатации лесных плантаций
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<characteristicsWorkRow> characteristicsWork { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируется создание лесных плантаций и их эксплуатация
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationWorkRow> locationWork { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseForestPlantations class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseForestPlantations()
        {
            locationWork = new List<locationWorkRow>();
            characteristicsWork = new List<characteristicsWorkRow>();
        }
    }

    /// <summary>
    /// Создание лесных питомников и их эксплуатация
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseForestNurseries
    {
        /// <summary>
        /// Сведения об обосновании и характеристиках проектируемых
        /// технологий по выращиванию посадочного материала лесных
        /// растений (саженцев, сеянцев), проектируемом объеме и видах
        /// мероприятий, направленных на соблюдение технологий
        /// по выращиванию посадочного материала лесных растений
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<characteristicsWorkRow> characteristicsWork { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируется
        /// создание лесных питомников и их эксплуатация
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationWorkRow> locationWork { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseForestNurseries class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseForestNurseries()
        {
            locationWork = new List<locationWorkRow>();
            characteristicsWork = new List<characteristicsWorkRow>();
        }
    }

    /// <summary>
    /// Выращивание лесных плодовых, ягодных, декоративных растений, лекарственных растений
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUsePlantGrowing
    {
        /// <summary>
        /// Сведения о характеристиках и обосновании проектируемых видов и объемов работ по выращиванию лесных плодовых, ягодных, декоративных растений, лекарственных растений; характеристика проектируемых технологий
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<characteristicsWorkRow> characteristicsWork { get; set; }
        /// <summary>
        /// Ведомость лесотаксационных выделов, в которых проектируется выращивание лесных плодовых, ягодных, декоративных и лекарственных растений
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationMeasureRow> locationMeasure { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUsePlantGrowing class constructor
        /// </summary>
        public forestDevelopmentProjectForestUsePlantGrowing()
        {
            locationMeasure = new List<locationMeasureRow>();
            characteristicsWork = new List<characteristicsWorkRow>();
        }
    }

    /// <summary>
    /// Выполнение работ по геологическому изучению недр, разведке и добыче полезных ископаемых
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseGeology
    {
        /// <summary>
        /// Сведения о характеристиках и обосновании проектируемых видов и объемов работ, при использовании лесов в целях геологического изучения недр, разведки и добычи полезных ископаемых
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<characteristicsWorkRow> characteristicsWork { get; set; }
        /// <summary>
        /// Характеристика существующих и проектируемых объектов, строений и сооружений при использовании лесов в целях геологического изучения недр, разведки и добычи полезных ископаемых
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationObjectRow> locationObject { get; set; }
        /// <summary>
        /// Проектируемый объем рубок лесных насаждений на лесном участке при создании объектов, строений и сооружений для геологического изучения недр, разведки и добычи полезных ископаемых
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<volumeCuttingsRow> volumeCuttings { get; set; }
        /// <summary>
        /// Сведения о рекультивации нарушенных при геологическом изучении недр, разведке и добыче полезных ископаемых земель на лесном участке при выполнении работ по геологическому изучению недр, разведке и добыче полезных ископаемых, а также подвергшихся нефтяному или иному загрязнению и подлежащих рекультивации
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationMeasureRow> locationMeasure { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseGeology class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseGeology()
        {
            locationMeasure = new List<locationMeasureRow>();
            volumeCuttings = new List<volumeCuttingsRow>();
            locationObject = new List<locationObjectRow>();
            characteristicsWork = new List<characteristicsWorkRow>();
        }
    }

    /// <summary>
    /// Строительство и эксплуатация водохранилищ и иных искусственных водных объектов, создание и расширение территорий морских и речных портов, строительство, реконструкция и эксплуатация гидротехнических сооружений
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseHydrology
    {
        /// <summary>
        /// Сведения о характеристиках и обосновании проектируемых видов и объемов работ по строительству и эксплуатации водохранилищ и иных искусственных водных объектов, созданию и расширению территорий морских и речных портов, строительству, реконструкции и эксплуатации гидротехнических сооружений
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<characteristicsWorkRow> characteristicsWork { get; set; }
        /// <summary>
        /// Характеристика существующих и проектируемых объектов,строений и сооружений при строительстве и эксплуатации водохранилищ и иных искусственных водных объектов, создании и расширении территорий морских и речных портов, строительстве, реконструкции и эксплуатации гидротехнических сооружений на лесном участке
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationObjectRow> locationObject { get; set; }
        /// <summary>
        /// Проектируемый объем рубок лесных насаждений на лесном участке при строительстве и эксплуатации водохранилищ и иных искусственных водных объектов, создании и расширении территорий морских и речных портов,строительстве, реконструкции и эксплуатациигидротехнических сооружений
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<volumeCuttingsRow> volumeCuttings { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseHydrology class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseHydrology()
        {
            volumeCuttings = new List<volumeCuttingsRow>();
            locationObject = new List<locationObjectRow>();
            characteristicsWork = new List<characteristicsWorkRow>();
        }
    }

    /// <summary>
    /// Строительство, реконструкция, эксплуатация линейных объектов
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseLinearObjects
    {
        /// <summary>
        /// Сведения о характеристиках и обосновании проектируемых видов и объемов работ по строительству, реконструкции,эксплуатации линейных объектов
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<characteristicsWorkRow> characteristicsWork { get; set; }
        /// <summary>
        /// Характеристика существующих и проектируемых объектов,строений и сооружений при строительстве, реконструкции,эксплуатации линейных объектов на лесном участке
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationObjectRow> locationObject { get; set; }
        /// <summary>
        /// Проектируемый объем рубок лесных насаждений на лесном участке, предназначенном для строительства,реконструкции, эксплуатации линейных объектов
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<volumeCuttingsRow> volumeCuttings { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseLinearObjects class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseLinearObjects()
        {
            volumeCuttings = new List<volumeCuttingsRow>();
            locationObject = new List<locationObjectRow>();
            characteristicsWork = new List<characteristicsWorkRow>();
        }
    }

    /// <summary>
    /// Создание и эксплуатация объектов лесоперерабатывающей инфраструктуры
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseTimberProcessing
    {
        /// <summary>
        /// Сведения о характеристиках и обосновании проектируемых видов и объемов работ по переработке древесины и иных лесных ресурсов
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<characteristicsWorkRow> characteristicsWork { get; set; }
        /// <summary>
        /// Характеристика существующих и проектируемых объектов лесоперерабатывающей инфраструктуры
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<locationObjectRow> locationObject { get; set; }
        /// <summary>
        /// Проектируемый объем рубок лесных насаждений на лесном участке, предназначенном для создания объектов лесоперерабатывающей инфраструктуры
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<volumeCuttingsRow> volumeCuttings { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseTimberProcessing class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseTimberProcessing()
        {
            volumeCuttings = new List<volumeCuttingsRow>();
            locationObject = new List<locationObjectRow>();
            characteristicsWork = new List<characteristicsWorkRow>();
        }
    }

    /// <summary>
    /// Изыскательские работы
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseSurveyWork
    {
        /// <summary>
        /// Сведения о характеристиках и обосновании проектируемых видов и объемах проектируемых изыскательских работ
        /// </summary>
        [XmlArrayItemAttribute("row", IsNullable = false)]
        public List<forestDevelopmentProjectForestUseSurveyWorkRow> characteristicsWork { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseSurveyWork class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseSurveyWork()
        {
            characteristicsWork = new List<forestDevelopmentProjectForestUseSurveyWorkRow>();
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://rosleshoz.gov.ru/xmlns/forestDevelopmentProject/4.1")]
    public class forestDevelopmentProjectForestUseSurveyWorkRow
    {
        public characteristicsWorkRow characteristics { get; set; }
        public location location { get; set; }
        /// <summary>
        /// Площадь, га
        /// </summary>
        public decimal area { get; set; }

        /// <summary>
        /// forestDevelopmentProjectForestUseSurveyWorkRow class constructor
        /// </summary>
        public forestDevelopmentProjectForestUseSurveyWorkRow()
        {
            location = new location();
            characteristics = new characteristicsWorkRow();
        }
    }
}
#pragma warning restore
