using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DiabeticWebApp.Models;
using Dtos;

namespace DiabeticWebApp.Repository.MeasurementReminderRepositories
{
    class MeasurementReminderRepository : IMeasurementReminderRepository
    {
        private readonly ApplicationDbContext _db;
        public MeasurementReminderRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public List<MeasurementReminderDto> GetMeasurementReminders(string userId)
        {
            var measurementRemindersList = _db.MeasurementReminders
                .Where(m => m.UserId == userId)
                .OrderBy(m => m.Time)
                .ToList();

            var measurementsRepositoryDtoList = Mapper.Map<List<MeasurementReminderDto>>(measurementRemindersList);
            return measurementsRepositoryDtoList;
        }

        public void AddMeasurementReminder(string userId, MeasurementReminderDto measurementReminderDto)
        {
            var measurementReminder = Mapper.Map<MeasurementReminder>(measurementReminderDto);
            measurementReminder.UserId = userId;
            _db.MeasurementReminders.Add(measurementReminder);
            _db.SaveChanges();
        }

        public void UpdateMeasurementReminder(string userId, MeasurementReminderDto measurementReminderDto)
        {
            var measurementReminder = _db.MeasurementReminders
                .Where(m => m.UserId == userId)
                .First(m => m.Id == measurementReminderDto.Id);
            if (measurementReminder == null) return;

            measurementReminder.Time = measurementReminderDto.Time;
            _db.SaveChanges();
        }

        public bool MeasurementReminderExists(string userId, int measurementReminderId)
        {
            return _db.MeasurementReminders
                       .Where(m => m.UserId == userId)
                       .Count(m => m.Id == measurementReminderId) > 0;
        }

        public void DeleteMeasurementReminder(string userId, int measurementReminderId)
        {
            var measurementReminder = _db.MeasurementReminders
                .Where(m => m.UserId == userId)
                .First(m => m.Id == measurementReminderId);
            if (measurementReminder == null) return;
            _db.MeasurementReminders.Remove(measurementReminder);
            _db.SaveChanges();
        }
    }
}
