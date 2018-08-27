using System;

namespace TaurusSoftware.BillomatNet.Types
{
    public abstract class Document
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public string FileName { get; set; }

        public byte[] Bytes { get; set; }

        public string MimeType { get; set; }

        public int FileSize { get; set; }
    }
}