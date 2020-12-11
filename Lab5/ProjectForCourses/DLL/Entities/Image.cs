using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DLL.Entities
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string Caption { get; set; }
    }
}
