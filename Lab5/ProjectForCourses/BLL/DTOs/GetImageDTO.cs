using System.IO;
namespace BLL.DTOs
{
    public class GetImageDTO
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public FileStream FileStream { get; set; }
        public string Caption { get; set; }
    }
}
