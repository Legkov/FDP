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

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://tempuri.org/XMLSchema.xsd")]
    [XmlRootAttribute("project", Namespace = "http://tempuri.org/XMLSchema.xsd", IsNullable = false)]
    public class ProjectType
    {
        private static XmlSerializer _serializerXml;
        [XmlArrayItemAttribute("Pipeline", IsNullable = false)]
        public List<PipelinesTypePipeline> pipelines { get; set; }
        [XmlAttribute]
        public string name1 { get; set; }
        [XmlAttribute]
        public string document1 { get; set; }
        [XmlAttribute]
        public string object1 { get; set; }
        [XmlAttribute]
        public string installation { get; set; }
        [XmlAttribute]
        public string organization { get; set; }
        [XmlAttribute]
        public string stage { get; set; }
        [XmlAttribute]
        public string fio1 { get; set; }
        [XmlAttribute]
        public string fio2 { get; set; }
        [XmlAttribute]
        public string fio3 { get; set; }
        [XmlAttribute]
        public string fio4 { get; set; }
        [XmlAttribute]
        public string fio5 { get; set; }
        [XmlAttribute]
        public string fio6 { get; set; }
        [XmlAttribute]
        public string pos1 { get; set; }
        [XmlAttribute]
        public string pos2 { get; set; }
        [XmlAttribute]
        public string pos3 { get; set; }
        [XmlAttribute]
        public string pos4 { get; set; }
        [XmlAttribute]
        public string pos5 { get; set; }
        [XmlAttribute]
        public string pos6 { get; set; }
        [XmlAttribute]
        public double avertemp { get; set; }
        [XmlAttribute]
        public double intemp { get; set; }

        /// <summary>
        /// ProjectType class constructor
        /// </summary>
        public ProjectType()
        {
            pipelines = new List<PipelinesTypePipeline>();
        }

        private static XmlSerializer SerializerXml
        {
            get
            {
                if ((_serializerXml == null))
                {
                    _serializerXml = new XmlSerializerFactory().CreateSerializer(typeof(ProjectType));
                }
                return _serializerXml;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serialize ProjectType object
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
        /// Deserializes ProjectType object
        /// </summary>
        /// <param name="input">string to deserialize</param>
        /// <param name="obj">Output ProjectType object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string input, out ProjectType obj, out Exception exception)
        {
            exception = null;
            obj = default(ProjectType);
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

        public static bool Deserialize(string input, out ProjectType obj)
        {
            Exception exception = null;
            return Deserialize(input, out obj, out exception);
        }

        public static ProjectType Deserialize(string input)
        {
            StringReader stringReader = null;
            try
            {
                stringReader = new StringReader(input);
                return ((ProjectType)(SerializerXml.Deserialize(XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static ProjectType Deserialize(Stream s)
        {
            return ((ProjectType)(SerializerXml.Deserialize(s)));
        }
        #endregion

        /// <summary>
        /// Serializes current ProjectType object into file
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
        /// Deserializes xml markup from file into an ProjectType object
        /// </summary>
        /// <param name="fileName">File to load and deserialize</param>
        /// <param name="obj">Output ProjectType object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out ProjectType obj, out Exception exception)
        {
            exception = null;
            obj = default(ProjectType);
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

        public static bool LoadFromFile(string fileName, out ProjectType obj)
        {
            Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static ProjectType LoadFromFile(string fileName)
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
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public class PipelinesTypePipeline
    {
        [XmlArrayItemAttribute("Fluid", IsNullable = false)]
        public List<FluidsTypeFluid> fluids { get; set; }
        [XmlArrayItemAttribute("Branch", IsNullable = false)]
        public List<BranchesTypeBranch> branches { get; set; }
        [XmlAttribute]
        public string name { get; set; }
        [XmlAttribute]
        public string QUnits { get; set; }
        [XmlAttribute]
        [DefaultValue(true)]
        public bool weldLosses { get; set; }
        [XmlAttribute]
        [DefaultValue(false)]
        public bool recalcRate { get; set; }
        [XmlAttribute]
        [DefaultValue(0.2D)]
        public double roughness { get; set; }
        [XmlAttribute]
        [DefaultValue(0D)]
        public double maxVelocity { get; set; }
        [XmlAttribute]
        [DefaultValue(0D)]
        public double AbsHeight { get; set; }

        /// <summary>
        /// PipelinesTypePipeline class constructor
        /// </summary>
        public PipelinesTypePipeline()
        {
            branches = new List<BranchesTypeBranch>();
            fluids = new List<FluidsTypeFluid>();
            weldLosses = true;
            recalcRate = false;
            roughness = 0.2D;
            maxVelocity = 0D;
            AbsHeight = 0D;
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public class FluidsTypeFluid
    {
        public componentsType components { get; set; }
        [XmlAttribute]
        public string name { get; set; }
        [XmlAttribute]
        public FluidsTypeFluidState state { get; set; }
        [XmlAttribute]
        public FluidsTypeFluidMethod method { get; set; }
        [XmlAttribute]
        public FluidsTypeFluidComposition composition { get; set; }
        [XmlAttribute]
        [DefaultValue(true)]
        public bool checkState { get; set; }
        [XmlAttribute]
        [DefaultValue(FluidsTypeFluidCheckMethod.Maxwell)]
        public FluidsTypeFluidCheckMethod checkMethod { get; set; }

        /// <summary>
        /// FluidsTypeFluid class constructor
        /// </summary>
        public FluidsTypeFluid()
        {
            components = new componentsType();
            checkState = true;
            checkMethod = FluidsTypeFluidCheckMethod.Maxwell;
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public class componentsType
    {
        [XmlElement("individual")]
        public List<componentsTypeIndividual> individual { get; set; }
        [XmlElement("fraction")]
        public List<componentsTypeFraction> fraction { get; set; }
        public componentsTypeMazut mazut { get; set; }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public class componentsTypeIndividual
    {
        [XmlAttribute]
        public string name { get; set; }
        [XmlAttribute(DataType = "positiveInteger")]
        public string code { get; set; }
        [XmlAttribute]
        public decimal percentage { get; set; }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public class insulationType
    {
        [XmlAttribute]
        public string thermoMaterial1 { get; set; }
        [XmlAttribute]
        public double thickness1 { get; set; }
        [XmlAttribute]
        public string thermoMaterial2 { get; set; }
        [XmlAttribute]
        public double thickness2 { get; set; }
        [XmlAttribute]
        public string jacketMaterial { get; set; }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public class groundType
    {
        [XmlAttribute]
        public string type { get; set; }
        [XmlAttribute]
        public double conductivity { get; set; }
        [XmlAttribute]
        [DefaultValue(1.5D)]
        public double depth { get; set; }
        [XmlAttribute]
        [DefaultValue(4D)]
        public double temperature { get; set; }
        [XmlAttribute]
        public double height { get; set; }
        [XmlAttribute]
        public double width { get; set; }

        /// <summary>
        /// groundType class constructor
        /// </summary>
        public groundType()
        {
            depth = 1.5D;
            temperature = 4D;
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public class environmentType
    {
        [XmlAttribute]
        [DefaultValue(environmentTypeLocation.Outside)]
        public environmentTypeLocation Location { get; set; }
        [XmlAttribute]
        public double temperature { get; set; }
        [XmlAttribute]
        public double wSpeed { get; set; }
        [XmlAttribute]
        [DefaultValue("Не учитывать")]
        public string material { get; set; }
        [XmlAttribute]
        public double conductivity { get; set; }
        [XmlAttribute]
        [DefaultValue(true)]
        public bool isInsulated { get; set; }

        /// <summary>
        /// environmentType class constructor
        /// </summary>
        public environmentType()
        {
            Location = environmentTypeLocation.Outside;
            material = "Не учитывать";
            isInsulated = true;
        }
    }

    
    [Serializable]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public enum environmentTypeLocation
    {
        Outside,
        Inside,
        Underground,
        InDuct,
        InTunnel,
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public class NodeType
    {
        [XmlAttribute(DataType = "positiveInteger")]
        public string number { get; set; }
        [XmlAttribute]
        public string name { get; set; }
        [XmlAttribute]
        public double pressure { get; set; }
        [XmlAttribute]
        [DefaultValue(false)]
        public bool fixDisbalance { get; set; }
        [XmlAttribute]
        public double inflow { get; set; }
        [XmlAttribute]
        public double inflowT { get; set; }

        /// <summary>
        /// NodeType class constructor
        /// </summary>
        public NodeType()
        {
            fixDisbalance = false;
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public class componentsTypeFraction
    {
        [XmlAttribute]
        public string name { get; set; }
        [XmlAttribute]
        public decimal percentage { get; set; }
        [XmlAttribute]
        public double tStart { get; set; }
        [XmlAttribute]
        public double tEnd { get; set; }
        [XmlAttribute]
        public double Ro420 { get; set; }
        [XmlAttribute]
        public double Mass { get; set; }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public class componentsTypeMazut
    {
        [XmlElement("mazutPoint")]
        public List<componentsTypeMazutMazutPoint> mazutPoint { get; set; }
        [XmlAttribute]
        [DefaultValue(componentsTypeMazutMazutType.M100)]
        public componentsTypeMazutMazutType mazutType { get; set; }
        [XmlAttribute]
        public double Ro420 { get; set; }

        /// <summary>
        /// componentsTypeMazut class constructor
        /// </summary>
        public componentsTypeMazut()
        {
            mazutType = componentsTypeMazutMazutType.M100;
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public class componentsTypeMazutMazutPoint
    {
        [XmlAttribute]
        public double temp { get; set; }
        [XmlAttribute]
        public double viscCond { get; set; }
        [XmlAttribute]
        public double viscCyn { get; set; }
    }

    
    [Serializable]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public enum componentsTypeMazutMazutType
    {
        M100,
        M40,
        M80,
        Ф12,
        Ф35,
    }

    
    [Serializable]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public enum FluidsTypeFluidState
    {
        Gas,
        Liquid,
        Undefined,
    }

    
    [Serializable]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public enum FluidsTypeFluidMethod
    {
        Properties,
        PropLib,
        Stars,
        WSP,
    }

    
    [Serializable]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public enum FluidsTypeFluidComposition
    {
        molar,
        mass,
    }

    
    [Serializable]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public enum FluidsTypeFluidCheckMethod
    {
        Maxwell,
        Bonell,
        Chao,
        Greyson,
        Ashwart,
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public class BranchesTypeBranch
    {
        public NodeType begNode { get; set; }
        public NodeType endNode { get; set; }
        [XmlArrayItemAttribute("resistance", IsNullable = false)]
        public List<resistancesTypeResistance> resistances { get; set; }
        [XmlAttribute]
        public string name { get; set; }
        [XmlAttribute]
        public double inDiam { get; set; }
        [XmlAttribute]
        public double outDiam { get; set; }
        [XmlAttribute]
        public double flow { get; set; }
        [XmlAttribute]
        public decimal tFluid { get; set; }
        [XmlAttribute]
        public decimal Density { get; set; }
        [XmlAttribute]
        public decimal Viscosity { get; set; }
        [XmlAttribute]
        public decimal GasContent { get; set; }
        [XmlAttribute]
        [DefaultValue(false)]
        public bool Kavitation { get; set; }

        /// <summary>
        /// BranchesTypeBranch class constructor
        /// </summary>
        public BranchesTypeBranch()
        {
            resistances = new List<resistancesTypeResistance>();
            endNode = new NodeType();
            begNode = new NodeType();
            Kavitation = false;
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/XMLSchema.xsd")]
    public class resistancesTypeResistance
    {
        public environmentType environment { get; set; }
        public groundType ground { get; set; }
        public insulationType insulation { get; set; }
        [XmlAttribute]
        public string name { get; set; }
        [XmlAttribute(DataType = "nonNegativeInteger")]
        public string code { get; set; }
        [XmlAttribute]
        public double parameter1 { get; set; }
        [XmlAttribute]
        public double parameter2 { get; set; }
        [XmlAttribute]
        public double parameter3 { get; set; }
        [XmlAttribute]
        public double parameter4 { get; set; }
        [XmlAttribute]
        public double height { get; set; }
        [XmlAttribute(DataType = "positiveInteger")]
        [DefaultValue("1")]
        public string quantity { get; set; }
        [XmlAttribute]
        public double X { get; set; }
        [XmlAttribute]
        public double Y { get; set; }
        [XmlAttribute]
        public double Z { get; set; }
        [XmlAttribute]
        [DefaultValue(false)]
        public bool isKv { get; set; }
        [XmlAttribute]
        [DefaultValue(false)]
        public bool isTDrop { get; set; }

        /// <summary>
        /// resistancesTypeResistance class constructor
        /// </summary>
        public resistancesTypeResistance()
        {
            quantity = "1";
            isKv = false;
            isTDrop = false;
        }
    }

    
    [Serializable]
    
    
    [XmlTypeAttribute(Namespace = "http://tempuri.org/XMLSchema.xsd")]
    [XmlRootAttribute(Namespace = "http://tempuri.org/XMLSchema.xsd", IsNullable = true)]
    public class PipelinesType
    {
        private static XmlSerializer _serializerXml;
        [XmlElement("Pipeline")]
        [RequiredAttribute()]
        public List<PipelinesTypePipeline> Pipeline { get; set; }

        /// <summary>
        /// PipelinesType class constructor
        /// </summary>
        public PipelinesType()
        {
            Pipeline = new List<PipelinesTypePipeline>();
        }

        private static XmlSerializer SerializerXml
        {
            get
            {
                if ((_serializerXml == null))
                {
                    _serializerXml = new XmlSerializerFactory().CreateSerializer(typeof(PipelinesType));
                }
                return _serializerXml;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serialize PipelinesType object
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
        /// Deserializes PipelinesType object
        /// </summary>
        /// <param name="input">string to deserialize</param>
        /// <param name="obj">Output PipelinesType object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string input, out PipelinesType obj, out Exception exception)
        {
            exception = null;
            obj = default(PipelinesType);
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

        public static bool Deserialize(string input, out PipelinesType obj)
        {
            Exception exception = null;
            return Deserialize(input, out obj, out exception);
        }

        public static PipelinesType Deserialize(string input)
        {
            StringReader stringReader = null;
            try
            {
                stringReader = new StringReader(input);
                return ((PipelinesType)(SerializerXml.Deserialize(XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static PipelinesType Deserialize(Stream s)
        {
            return ((PipelinesType)(SerializerXml.Deserialize(s)));
        }
        #endregion

        /// <summary>
        /// Serializes current PipelinesType object into file
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
        /// Deserializes xml markup from file into an PipelinesType object
        /// </summary>
        /// <param name="fileName">File to load and deserialize</param>
        /// <param name="obj">Output PipelinesType object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out PipelinesType obj, out Exception exception)
        {
            exception = null;
            obj = default(PipelinesType);
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

        public static bool LoadFromFile(string fileName, out PipelinesType obj)
        {
            Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static PipelinesType LoadFromFile(string fileName)
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
    
    
    [XmlTypeAttribute(Namespace = "http://tempuri.org/XMLSchema.xsd")]
    [XmlRootAttribute(Namespace = "http://tempuri.org/XMLSchema.xsd", IsNullable = true)]
    public class FluidsType
    {
        private static XmlSerializer _serializerXml;
        [XmlElement("Fluid")]
        [RequiredAttribute()]
        public List<FluidsTypeFluid> Fluid { get; set; }

        /// <summary>
        /// FluidsType class constructor
        /// </summary>
        public FluidsType()
        {
            Fluid = new List<FluidsTypeFluid>();
        }

        private static XmlSerializer SerializerXml
        {
            get
            {
                if ((_serializerXml == null))
                {
                    _serializerXml = new XmlSerializerFactory().CreateSerializer(typeof(FluidsType));
                }
                return _serializerXml;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serialize FluidsType object
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
        /// Deserializes FluidsType object
        /// </summary>
        /// <param name="input">string to deserialize</param>
        /// <param name="obj">Output FluidsType object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string input, out FluidsType obj, out Exception exception)
        {
            exception = null;
            obj = default(FluidsType);
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

        public static bool Deserialize(string input, out FluidsType obj)
        {
            Exception exception = null;
            return Deserialize(input, out obj, out exception);
        }

        public static FluidsType Deserialize(string input)
        {
            StringReader stringReader = null;
            try
            {
                stringReader = new StringReader(input);
                return ((FluidsType)(SerializerXml.Deserialize(XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static FluidsType Deserialize(Stream s)
        {
            return ((FluidsType)(SerializerXml.Deserialize(s)));
        }
        #endregion

        /// <summary>
        /// Serializes current FluidsType object into file
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
        /// Deserializes xml markup from file into an FluidsType object
        /// </summary>
        /// <param name="fileName">File to load and deserialize</param>
        /// <param name="obj">Output FluidsType object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out FluidsType obj, out Exception exception)
        {
            exception = null;
            obj = default(FluidsType);
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

        public static bool LoadFromFile(string fileName, out FluidsType obj)
        {
            Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static FluidsType LoadFromFile(string fileName)
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
    
    
    [XmlTypeAttribute(Namespace = "http://tempuri.org/XMLSchema.xsd")]
    [XmlRootAttribute(Namespace = "http://tempuri.org/XMLSchema.xsd", IsNullable = true)]
    public class BranchesType
    {
        private static XmlSerializer _serializerXml;
        [XmlElement("Branch")]
        [RequiredAttribute()]
        public List<BranchesTypeBranch> Branch { get; set; }

        /// <summary>
        /// BranchesType class constructor
        /// </summary>
        public BranchesType()
        {
            Branch = new List<BranchesTypeBranch>();
        }

        private static XmlSerializer SerializerXml
        {
            get
            {
                if ((_serializerXml == null))
                {
                    _serializerXml = new XmlSerializerFactory().CreateSerializer(typeof(BranchesType));
                }
                return _serializerXml;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serialize BranchesType object
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
        /// Deserializes BranchesType object
        /// </summary>
        /// <param name="input">string to deserialize</param>
        /// <param name="obj">Output BranchesType object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string input, out BranchesType obj, out Exception exception)
        {
            exception = null;
            obj = default(BranchesType);
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

        public static bool Deserialize(string input, out BranchesType obj)
        {
            Exception exception = null;
            return Deserialize(input, out obj, out exception);
        }

        public static BranchesType Deserialize(string input)
        {
            StringReader stringReader = null;
            try
            {
                stringReader = new StringReader(input);
                return ((BranchesType)(SerializerXml.Deserialize(XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static BranchesType Deserialize(Stream s)
        {
            return ((BranchesType)(SerializerXml.Deserialize(s)));
        }
        #endregion

        /// <summary>
        /// Serializes current BranchesType object into file
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
        /// Deserializes xml markup from file into an BranchesType object
        /// </summary>
        /// <param name="fileName">File to load and deserialize</param>
        /// <param name="obj">Output BranchesType object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out BranchesType obj, out Exception exception)
        {
            exception = null;
            obj = default(BranchesType);
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

        public static bool LoadFromFile(string fileName, out BranchesType obj)
        {
            Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static BranchesType LoadFromFile(string fileName)
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
    
    
    [XmlTypeAttribute(Namespace = "http://tempuri.org/XMLSchema.xsd")]
    [XmlRootAttribute(Namespace = "http://tempuri.org/XMLSchema.xsd", IsNullable = true)]
    public class resistancesType
    {
        private static XmlSerializer _serializerXml;
        [XmlElement("resistance")]
        [RequiredAttribute()]
        public List<resistancesTypeResistance> resistance { get; set; }

        /// <summary>
        /// resistancesType class constructor
        /// </summary>
        public resistancesType()
        {
            resistance = new List<resistancesTypeResistance>();
        }

        private static XmlSerializer SerializerXml
        {
            get
            {
                if ((_serializerXml == null))
                {
                    _serializerXml = new XmlSerializerFactory().CreateSerializer(typeof(resistancesType));
                }
                return _serializerXml;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serialize resistancesType object
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
        /// Deserializes resistancesType object
        /// </summary>
        /// <param name="input">string to deserialize</param>
        /// <param name="obj">Output resistancesType object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string input, out resistancesType obj, out Exception exception)
        {
            exception = null;
            obj = default(resistancesType);
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

        public static bool Deserialize(string input, out resistancesType obj)
        {
            Exception exception = null;
            return Deserialize(input, out obj, out exception);
        }

        public static resistancesType Deserialize(string input)
        {
            StringReader stringReader = null;
            try
            {
                stringReader = new StringReader(input);
                return ((resistancesType)(SerializerXml.Deserialize(XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public static resistancesType Deserialize(Stream s)
        {
            return ((resistancesType)(SerializerXml.Deserialize(s)));
        }
        #endregion

        /// <summary>
        /// Serializes current resistancesType object into file
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
        /// Deserializes xml markup from file into an resistancesType object
        /// </summary>
        /// <param name="fileName">File to load and deserialize</param>
        /// <param name="obj">Output resistancesType object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out resistancesType obj, out Exception exception)
        {
            exception = null;
            obj = default(resistancesType);
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

        public static bool LoadFromFile(string fileName, out resistancesType obj)
        {
            Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static resistancesType LoadFromFile(string fileName)
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
}
