using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dtos;

namespace DiabeticWebApp.Tests.Builders
{
    class MeasurementBuilder
    {
        public int Id { get; set; }
        public int Result { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }


        public MeasurementBuilder AMeasurement()
        {
            return new MeasurementBuilder();
        }

        public MeasurementBuilder WithId(int newId)
        {
            this.Id = newId;
            return this;
        }

        public MeasurementBuilder WithResult(int newResult)
        {
            this.Result = newResult;
            return this;
        }

        public MeasurementBuilder WithDescription(string newDescription)
        {
            this.Description = newDescription;
            return this;
        }

        public MeasurementBuilder WithDate(DateTime newDate)
        {
            this.Date = newDate;
            return this;
        }

        public MeasurementBuilder WithNoResult()
        {
            this.Result = 0;
            return this;
        }

        public MeasurementBuilder WithNoDate()
        {
            this.Date = DateTime.MinValue;
            return this;
        }

        public MeasurementBuilder WithNegativeResult()
        {
            if (this.Result > 0)
            {
                this.Result *= -1;
            }

            return this;
        }

        public MeasurementDto Build()
        {
            return new MeasurementDto
            {
                Id = this.Id,
                Result = this.Result,
                Date = this.Date,
                Description = this.Description
            };
        }
    }
}
