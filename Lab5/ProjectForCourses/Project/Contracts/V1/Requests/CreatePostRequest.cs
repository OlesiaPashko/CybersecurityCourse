﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.V1.Requests
{
    public class CreatePostRequest
    {
        public string Name { get; set; }

        //public IFormFile Image { get; set; }
    }
}
