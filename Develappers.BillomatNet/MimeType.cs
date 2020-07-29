// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Develappers.BillomatNet
{
    public static class MimeType
    {
        /// <summary>Specifies the type of text data in an e-mail message attachment.</summary>
        public static class Text
        {
            /// <summary>Specifies that the data is in plain text format.</summary>
            public const string Plain = "text/plain";

            /// <summary>Specifies that the data is in HTML format.</summary>
            public const string Html = "text/html";

            /// <summary>Specifies that the data is in XML format.</summary>
            public const string Xml = "text/xml";

            /// <summary>Specifies that the data is in Rich Text Format (RTF).</summary>
            public const string RichText = "text/richtext";
        }

        /// <summary>Specifies the kind of application data in an e-mail message attachment.</summary>
        public static class Application
        {
            /// <summary>Specifies that the data is a SOAP document.</summary>
            public const string Soap = "application/soap+xml";

            /// <summary>Specifies that the data is not interpreted.</summary>
            public const string Octet = "application/octet-stream";

            /// <summary>Specifies that the data is in Rich Text Format (RTF).</summary>
            public const string Rtf = "application/rtf";

            /// <summary>Specifies that the data is in Portable Document Format (PDF).</summary>
            public const string Pdf = "application/pdf";

            /// <summary>Specifies that the data is compressed.</summary>
            public const string Zip = "application/zip";
        }

        /// <summary>Specifies the type of image data in an e-mail message attachment.</summary>
        public static class Image
        {
            /// <summary>Specifies that the data is in Graphics Interchange Format (GIF).</summary>
            public const string Gif = "image/gif";

            /// <summary>Specifies that the data is in Tagged Image File Format (TIFF).</summary>
            public const string Tiff = "image/tiff";

            /// <summary>Specifies that the data is in Joint Photographic Experts Group (JPEG) format.</summary>
            public const string Jpeg = "image/jpeg";
        }
    }
}
