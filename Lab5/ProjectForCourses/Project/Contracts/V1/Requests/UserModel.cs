using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Contracts.V1.Requests
{
    public class UserModel
    {
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
