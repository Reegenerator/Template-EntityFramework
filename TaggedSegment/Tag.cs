﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Linq;
using Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RgenLib.Extensions;
using RgenLib.TaggedSegment.Json;

namespace RgenLib.TaggedSegment {
    public partial class Manager<TRenderer, TOptionAttr> where TRenderer : TaggedCodeRenderer, new() where TOptionAttr : Attribute, new() {
        /// <summary>
        /// A superset of OptionAttribute, containing other information generated by the renderer
        /// </summary>
        [JsonObject(MemberSerialization.OptIn)]
        public abstract class Tag {
            public enum TagTypes {
                Generated,
                InsertPoint
            }
            protected Tag() {
                RegenMode = RegenModes.Never;
            }
            #region TagGeneration
            public const string XmlTagName = "Gen";
            public const string XmlRendererAttributeName = "Renderer";

            #region TagAlternateNames
            private const string RegenModePropertyName = "Regen";
            private const string CategoryPropertyName = "Cat";
            private const string VersionPropertyName = "V";
            public const string GenerateDatePropertyName = "Date";
            private const string TriggerPropertyName = "Trig";
            public const string TemplateNamePropertyName = "Template";
            #endregion



            public static IContractResolver OrderedPropertyResolver {
                get {
                    if (_OrderedPropertyResolver == null) {
                        const int defaultRank = 100;
                        var orderDict = new Dictionary<string, int>()
                        {
                            {TemplateNamePropertyName, 0},
                            {GenerateDatePropertyName, 1000},
                        };
                        _OrderedPropertyResolver = new OrderedContractResolver(

                            p => {
                                var rank= orderDict.ContainsKey(p.PropertyName)
                                    ? orderDict[p.PropertyName]
                                    : defaultRank ;
                                return string.Format("{0:0000} {1}", rank, p.PropertyName);
                            });
                    }

                    return _OrderedPropertyResolver;
                }
            }


            // ReSharper disable once StaticFieldInGenericType
            static XElement _TagPrototype;
            public static XElement TagPrototype {
                get {
                    if (_TagPrototype == null) {

                        _TagPrototype = new XElement(Tag.XmlTagName);
                        _TagPrototype.Add(new XAttribute(Tag.XmlRendererAttributeName, typeof(TRenderer).Name));
                    }
                    return _TagPrototype;
                }
            }

            #endregion

            [JsonProperty(PropertyName = TriggerPropertyName)]
            [XmlAttribute(TriggerPropertyName)]
            public TriggerInfo Trigger { get; set; }

            // ReSharper disable StaticFieldInGenericType
            private static readonly string _templateName = typeof(TRenderer).Name;
            private static IContractResolver _OrderedPropertyResolver;

            // ReSharper restore StaticFieldInGenericType

            [JsonProperty(TemplateNamePropertyName)]
            public string TemplateName { get { return _templateName; } }

            public TOptionAttr OptionAttribute { get; set; }

        
   
            
            [JsonProperty(PropertyName = CategoryPropertyName)]
            [XmlAttribute(CategoryPropertyName)]
            public string Category { get; set; }

            [JsonProperty(PropertyName = RegenModePropertyName)]
            [XmlAttribute(RegenModePropertyName)]
            [JsonConverter(typeof(StringEnumConverter))]
            public RegenModes RegenMode { get; set; }

            public TagTypes TagType { get; set; }

            [JsonConverter(typeof(Json.VersionConverter))]
            [JsonProperty(PropertyName = VersionPropertyName)]
            [XmlAttribute(VersionPropertyName)]
            public Version Version { get; set; }

            [JsonProperty(PropertyName = GenerateDatePropertyName)]
            [XmlAttribute(GenerateDatePropertyName)]
            public DateTime? GenerateDate { get; set; }
            /// <summary>
            /// Allow access to protected MemberwiseClone();
            /// </summary>
            /// <returns></returns>
            public new virtual Tag MemberwiseClone() {
                return (Tag)base.MemberwiseClone();
            }




        }
    }
}
