using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiabeticWebApp.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public int Result { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}