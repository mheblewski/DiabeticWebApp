using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DiabeticWebApp.Models
{
    public class ICESettings
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsEnabled { get; set; }
        public string ContactNumber { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}