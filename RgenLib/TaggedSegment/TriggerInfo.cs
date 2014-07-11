using EnvDTE80;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RgenLib.Extensions;

namespace RgenLib.TaggedSegment {
    
    /// <summary>
    /// A class holding information of the cause of code generation.
    /// </summary>
    /// <remarks>
    /// created as a class, so it can be easily de/serialized as json
    /// </remarks>
    public class TriggerInfo {
        private TriggerTypes _type;

        public TriggerInfo()
        {
            Type = TriggerTypes.Default;
        }
        public TriggerInfo(TriggerTypes type)
        {
            Type = type;
        }

        [JsonConverter(typeof (StringEnumConverter))]
        [XmlAttribute("Type")]
        public TriggerTypes Type
        {
            get { return _type; }
            set
            {
                _type = value;
            }
        }

        [JsonConverter(typeof(CodeClassJsonConverter))]
        [XmlAttribute("Base")]
        public CodeClass2 TriggeringBaseClass {get;set;}

    }
}
