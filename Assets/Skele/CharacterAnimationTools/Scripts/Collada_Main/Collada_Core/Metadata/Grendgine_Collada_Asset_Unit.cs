using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
namespace grendgine_collada
{
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
	public partial class Grendgine_Collada_Asset_Unit
	{
		[XmlAttribute("meter")]
        //[System.ComponentModel.DefaultValueAttribute(1.0)]
		public double Meter;

		[XmlAttribute("name")]
        //[System.ComponentModel.DefaultValueAttribute("meter")]
		public string Name;
	}
}

