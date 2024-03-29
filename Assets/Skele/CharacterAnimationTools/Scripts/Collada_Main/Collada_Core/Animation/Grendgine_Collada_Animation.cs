using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
namespace grendgine_collada
{
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
	public partial class Grendgine_Collada_Animation
	{

		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;			
		
				
		[XmlElement(ElementName = "animation")]
		public List<Grendgine_Collada_Animation> Animation;
		
		[XmlElement(ElementName = "source")]
		public Grendgine_Collada_Source[] Source;

		[XmlElement(ElementName = "sampler")]
		public Grendgine_Collada_Sampler[] Sampler;

        [XmlElement(ElementName = "channel")]
        public Grendgine_Collada_Channel[] Channel;
		
		[XmlElement(ElementName = "asset")]
		public Grendgine_Collada_Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public Grendgine_Collada_Extra[] Extra;	
		
	}
}

