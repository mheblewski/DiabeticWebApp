using DiabeticWebApp.Tests.Builders;
using System;

namespace DiabeticWebApp.Tests.TestObjects
{
    class TestMeasurements
    {
        public static MeasurementBuilder DefaultMeasurement()
        {
            var measurementId = 1;
            var measurementDate = DateTime.Parse("12/5/2018 3:23:11 PM");
            var measurementDescryption = "test descryption";
            var measurementResult = 123;

            return new MeasurementBuilder()
                .AMeasurement()
                .WithId(measurementId)
                .WithDate(measurementDate)
                .WithDescription(measurementDescryption)
                .WithResult(measurementResult);
        }

        public static MeasurementBuilder RandomMeasurement()
        {
            Helpers helpers = new Helpers();
            
            var measurementId = helpers.RandomNumberGenerator(0, 50);
            var measurementDate = helpers.RandomDateGenerator();
            var measurementDescryption = helpers.RandomStringGenerator(10);
            var measurementResult = helpers.RandomNumberGenerator(50, 150);

            return new MeasurementBuilder()
                .AMeasurement()
                .WithId(measurementId)
                .WithDate(measurementDate)
                .WithDescription(measurementDescryption)
                .WithResult(measurementResult);
        }
    }
}
