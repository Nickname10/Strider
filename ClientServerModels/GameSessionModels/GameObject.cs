using System.Runtime.Serialization;

namespace ClientServerModels.GameSessionModels
{
    [DataContract]
    public abstract class GameObject
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public double GlobalX { get; set; }
        [DataMember]
        public double GlobalY { get; set; }
        [DataMember]
        public double Radius  { get; set; }
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
        [DataMember]
        public byte StrokeThickness { get; set; }
    }
}