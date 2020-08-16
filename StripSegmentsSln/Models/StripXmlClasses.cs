using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Models
{

    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "root")]
    public class RootXml
    {
        [XmlElement()]
        public double step { get; set; }

        [XmlElement()]
        public SegmentXml size { get; set; }

        [XmlElement()]
        public SegmentXml range { get; set; }

        [XmlArrayItem("strip", IsNullable = false)]
        public List<StripXml> strips { get; set; }
    }

    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class SegmentXml
    {

        [XmlAttribute()]
        public int id { get; set; }

        [XmlAttribute()]
        public double begin { get; set; }

        [XmlAttribute()]
        public double end { get; set; }
    }

    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class StripXml
    {
        [XmlElement("segment")]
        public List<SegmentXml> segments { get; set; }

        [XmlAttribute()]
        public int id { get; set; }

        [XmlAttribute()]
        public string name { get; set; }
    }
}
