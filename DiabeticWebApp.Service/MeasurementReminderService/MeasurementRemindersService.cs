using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiabeticWebApp.Models;
using DiabeticWebApp.Repository.MeasurementReminderRepositories;
using Dtos;

namespace DiabeticWebApp.Service.MeasurementReminderService
{
    class MeasurementRemindersService : IMeasurementRemindersService
    {
        private readonly IMeasurementReminderRepository _repository;
        public MeasurementRemindersService(IMeasurementReminderRepository repository)
        {
            this._repository = repository;
        }

        public List<MeasurementReminderDto> GetMeasurementReminders(string userId)
        {
            return _repository.GetMeasurementReminders(userId);
        }

        public void AddMeasurementReminder(string userId, MeasurementReminderDto measurementReminderDto)
        {
            _repository.AddMeasurementReminder(userId, measurementReminderDto);
        }

        public void UpdateMeasurementReminder(string userId, MeasurementReminderDto measurementReminderDto)
        {
            _repository.UpdateMeasurementReminder(userId, measurementReminderDto);
        }

        public bool MeasurementReminderExists(string userId, int measurementReminderId)
        {
            return _repository.MeasurementReminderExists(userId, measurementReminderId);
        }

        public void DeleteMeasurementReminder(string userId, int measurementReminderId)
        {
            _repository.DeleteMeasurementReminder(userId, measurementReminderId);
        }
    }
}
