using DiabeticWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticWebApp.Repository.MeasurementRepositories
{
    public interface IMeasurementRepository
    {
        void InsertMeasurement(string userId, MeasurementDto measurementDto);
        MeasurementDto GetMeasurement(string userId, int measurementId);
        List<MeasurementDto> GetMeasurements(string userId);
        List<MeasurementDto> GetMeasurements(string userId, DateTime dateFrom, DateTime dateTo);
        void DeleteMeasurement(string userId, int id);
        void UpdateMeasurement(string userId, MeasurementDto measurementDto);
        bool MeasurementExists(string userId, int measurementId);
    }
}
