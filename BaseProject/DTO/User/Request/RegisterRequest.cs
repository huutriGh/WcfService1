﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.DTO.User.Request
{
    public class RegisterRequest
    {
        [Required]
        public string Username { get; set; }
     
        [Required]
        public string Password { get; set; }
    }
}
