﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiabeticWebApp.Models
{
    public class MeasurementDto
    {
        public int Id { get; set; }
        public int Result { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}