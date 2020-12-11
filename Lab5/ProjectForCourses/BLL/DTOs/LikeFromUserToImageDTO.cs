using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DLL.Entities
{
    public class LikeFromUserToImageDTO
    {
        public Guid ImageId { get; set; }
        public Image Image { get; set; }
        public Guid UserID { get; set; }
        public User User { get; set; }

    }
}
