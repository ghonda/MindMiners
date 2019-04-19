using System;

namespace MindMiners.Domain.Entities
{
    public class FileHistory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] File {get; set;}
        public double Offset { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
