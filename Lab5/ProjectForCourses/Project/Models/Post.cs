using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public byte[] Image { get; set; }
    }
}
