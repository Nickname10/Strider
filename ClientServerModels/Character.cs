using System.Runtime.Serialization;

namespace ClientServerModels
{
    [DataContract]
    public class Character
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public byte MainRedColor { get; set; }
        [DataMember]
        public byte MainGreenColor { get; set; }
        [DataMember]
        public byte MainBlueColor { get; set; }
        [DataMember]
        public byte StrokeRedColor { get; set; }
        [DataMember]
        public byte StrokeGreenColor { get; set; }
        [DataMember]
        public byte StrokeBlueColor { get; set; }
        [DataMember]
        public byte StrokeLength { get; set; }
        [DataMember]
        public byte StrokeSpace { get; set; }
    }
}