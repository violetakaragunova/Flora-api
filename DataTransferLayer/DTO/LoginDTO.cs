﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlantTrackerAPI.DataTransferLayer.DTO
{
    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
