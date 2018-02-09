using System;
using System.Collections.Generic;
using DiabeticWebApp.Models;
using DiabeticWebApp.Repository.MeasurementRepositories;
using Dtos;

namespace DiabeticWebApp.Service.MeasurementService
{
    public class MeasurementsService : IMeasurementsService
    {
        private readonly IMeasurementRepository _repository;
        public MeasurementsService(IMeasurementRepository repository)
        {
            this._repository = repository;
        }

        public void AddMeasurement(string userId, MeasurementDto measurementDto)
        {
            measurementDto.Date = DateTime.Now;
            _repository.InsertMeasurement(userId, measurementDto);
        }

        public void DeleteMeasurement(string userId, int measurementId)
        {
            _repository.DeleteMeasurement(userId, measurementId);
        }

        public MeasurementDto GetMeasurement(string userId, int measurementId)
        {
            return _repository.GetMeasurement(userId, measurementId);
        }

        public List<MeasurementDto> GetMeasurements(string userId)
        {
            return _repository.GetMeasurements(userId);
        }

        public List<MeasurementDto> GetMeasurements(string userId, DateTime dateFrom, DateTime? dateTo = null)
        {
            var effectiveEnd = dateTo ?? DateTime.Today;
            effectiveEnd = effectiveEnd.Date.AddDays(1);
            return _repository.GetMeasurements(userId, dateFrom, effectiveEnd);
        }

        public void UpdateMeasurement(string userId, MeasurementDto measurementDto)
        {
            _repository.UpdateMeasurement(userId, measurementDto);
        }

        public bool DoesMeasurementExists(string userId, int measurementId)
        {
            return _repository.DoesMeasurementExists(userId, measurementId);
        }
    }
}
