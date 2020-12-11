using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Contracts.V1.Responses
{
    public class AuthentificationResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public IEnumerable<IdentityError> Errors { get; set; }
    }
}
