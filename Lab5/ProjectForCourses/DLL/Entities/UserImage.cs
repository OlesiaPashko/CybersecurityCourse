using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DLL.Entities
{
    public class UserImage
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        public Image Image { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

    }
}
