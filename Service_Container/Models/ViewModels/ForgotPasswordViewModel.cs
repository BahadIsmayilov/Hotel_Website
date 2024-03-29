﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
