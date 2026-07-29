using Application.Common;
using Microsoft.AspNetCore.Http;

namespace Application.Commands
{
    public class UploadFile : IAppMessage
    {
        public IFormFile File { get; set; }
    }
}
