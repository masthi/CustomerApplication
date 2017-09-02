﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerApplication.DAL
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}