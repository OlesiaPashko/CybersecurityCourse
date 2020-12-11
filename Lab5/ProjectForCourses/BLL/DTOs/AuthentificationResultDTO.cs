using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class AuthentificationResultDTO
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public IEnumerable<IdentityError> Errors { get; set; }
    }
}
