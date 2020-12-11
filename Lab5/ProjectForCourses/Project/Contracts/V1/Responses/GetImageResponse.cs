using System.IO;

namespace Project.Contracts.V1.Responses
{
    public class GetImageResponse
    {
        public FileStream File { get; set; }
        public string Caption { get; set; }
    }
}