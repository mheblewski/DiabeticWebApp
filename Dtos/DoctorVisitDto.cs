using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiabeticWebApp.Models
{
    public class DoctorVisitDto
    {
        public int Id { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string Speciality { get; set; }
        public string TelephoneNumber { get; set; }
        public DateTime Date { get; set; }
    }
}