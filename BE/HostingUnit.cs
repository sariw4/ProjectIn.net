using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class HostingUnit
    {
        public long HostingUnitKey { get; set; }
        public Host Owner { get; set; }
        public string HostingUnitName { get; set; }
        [XmlIgnore]
        public bool[,] Diary { get; set; }
        [XmlArray("Diary")]
        public bool[] BoardDto
        {
            get { return Diary.Flatten(); }
            set { Diary = value.Expand(32); } 
        }
        public bool Pool { get; set; }
        public bool Jacuzzi { get; set; }
        public bool Garden { get; set; }
        public bool ChildrensAttractions { get; set; }
        public AreaStatus Area { get; set; }
        public TypeStatus Type { get; set; }
        public int Price { get; set; }
        public int NumOfVacationers { get; set; }
        public override string ToString()
        {
            return Owner.ToString() + HostingUnitName.ToString() + Diary.ToString();
        }
    }
}

