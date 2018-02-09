using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dtos
{
    public class MeasurementDto : IValidatableObject
    {
        public int Id { get; set; }
        public int Result { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (this.Result == 0)
            {
                results.Add(new ValidationResult("The Result must be set", new string[] { "Result" }));
            }
            if (this.Result < 0)
            {
                results.Add(new ValidationResult("The Result must be positive number", new string[] { "Result" }));
            }
         
            return results;
        }
    }
}