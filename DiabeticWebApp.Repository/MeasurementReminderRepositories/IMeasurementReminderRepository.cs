using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiabeticWebApp.Models;
using Dtos;

namespace DiabeticWebApp.Repository.MeasurementReminderRepositories
{
    public interface IMeasurementReminderRepository
    {
        List<MeasurementReminderDto> GetMeasurementReminders(string userId);
        void AddMeasurementReminder(string userId, MeasurementReminderDto measurementReminderDto);
        void UpdateMeasurementReminder(string userId, MeasurementReminderDto measurementReminderDto);
        bool MeasurementReminderExists(string userId, int measurementReminderId);
        void DeleteMeasurementReminder(string userId, int measurementReminderId);
    }
}
