using System;
using System.Collections.Generic;
using DiabeticWebApp.Models;

namespace DiabeticWebApp.Service.MeasurementService
{
    public interface IMeasurementsService
    {
        void AddMeasurement(string userId, MeasurementDto measurement);
        MeasurementDto GetMeasurement(string userId, int measurementId);
        List<MeasurementDto> GetMeasurements(string userId);
        List<MeasurementDto> GetMeasurements(string userId, DateTime dateFrom, DateTime? dateTo = null);
        void DeleteMeasurement(string userId, int measurementId);
        void UpdateMeasurement(string userId, MeasurementDto measurementDto);
        bool DoesMeasurementExists(string userId, int measurementId);
    }
}
