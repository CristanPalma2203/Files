using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Application.Dtos
{
    public class DownloadFileDto : IResponse
    {

        public Stream File { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
