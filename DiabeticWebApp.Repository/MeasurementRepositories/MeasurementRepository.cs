using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiabeticWebApp.Models;
using Dtos;

namespace DiabeticWebApp.Repository.MeasurementRepositories
{
    public class MeasurementRepository : IMeasurementRepository
    {
        private readonly ApplicationDbContext _db;

        public MeasurementRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public void InsertMeasurement(string userId, MeasurementDto measurementDto)
        {
            var measurement = Mapper.Map<Measurement>(measurementDto);
            measurement.UserId = userId;
            _db.Measurements.Add(measurement);
            _db.SaveChanges();
        }

        public MeasurementDto GetMeasurement(string userId, int measurementId)
        {
            var measurement = _db.Measurements
                .Where(m => m.UserId == userId)
                .FirstOrDefault(m => m.Id == measurementId);

            var measurementDto = Mapper.Map<MeasurementDto>(measurement);
            return measurementDto;
        }

        public List<MeasurementDto> GetMeasurements(string userId)
        {
            var measurementList = _db.Measurements
                .Where(m => m.UserId == userId)
                .OrderBy(m => m.Date)
                .ToList();

            var measurementsDtoList = Mapper.Map<List<MeasurementDto>>(measurementList);
            return measurementsDtoList;
        }

        public List<MeasurementDto> GetMeasurements(string userId, DateTime dateFrom, DateTime dateTo)
        { 
            var measurementList = _db.Measurements
                .Where(m => m.UserId == userId)
                .Where(m => m.Date >= dateFrom)
                .Where(m => m.Date < dateTo)
                .OrderBy(m => m.Date)
                .ToList();

            var measurementsDtoList = Mapper.Map<List<MeasurementDto>>(measurementList);
            return measurementsDtoList;
        }

        public void DeleteMeasurement(string userId, int id)
        {
            var measurement = _db.Measurements
                .Where(m => m.UserId == userId)
                .First(m => m.Id == id);
            if (measurement == null) return;
            _db.Measurements.Remove(measurement);
            _db.SaveChanges();
        }

        public void UpdateMeasurement(string userId, MeasurementDto measurementDto)
        {
            var measurement = _db.Measurements
                .Where(m => m.UserId == userId)
                .First(m => m.Id == measurementDto.Id);
            if (measurement == null) return;
            measurement.Date = measurementDto.Date;
            measurement.Description = measurementDto.Description;
            measurement.Result = measurementDto.Result;
            //measurement = _mapper.Map<Measurement>(measurementDto); // To rozwiązanie nie działa, dowiedzieć się czemu
            _db.SaveChanges();
        }

        public bool DoesMeasurementExists(string userId, int measurementId)
        {
            return _db.Measurements
                       .Where(m => m.UserId == userId)
                       .Count(m => m.Id == measurementId) > 0;
        }
    }
}
