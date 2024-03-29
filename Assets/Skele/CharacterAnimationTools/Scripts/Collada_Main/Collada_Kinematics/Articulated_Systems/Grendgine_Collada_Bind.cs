using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
namespace grendgine_collada
{
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="bind", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Grendgine_Collada_Bind
	{
		[XmlAttribute("symbol")]
		public string Symbol;
		
		[XmlElement(ElementName = "param")]
		public Grendgine_Collada_Param Param;

		[XmlElement(ElementName = "float")]
		public float Float;

		[XmlElement(ElementName = "int")]
		public int Int;

		[XmlElement(ElementName = "bool")]
		public bool Bool;

		[XmlElement(ElementName = "SIDREF")]
		public string SIDREF;
		
	}
}

