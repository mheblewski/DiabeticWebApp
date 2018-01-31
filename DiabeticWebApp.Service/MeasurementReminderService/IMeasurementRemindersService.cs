using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiabeticWebApp.Models;
using Dtos;

namespace DiabeticWebApp.Service.MeasurementReminderService
{
    public interface IMeasurementRemindersService
    {
        List<MeasurementReminderDto> GetMeasurementReminders(string userId);
        void AddMeasurementReminder(string userId, MeasurementReminderDto measurementReminderDto);
        void UpdateMeasurementReminder(string userId, MeasurementReminderDto measurementReminderDto);
        bool DoesMeasurementReminderExists(string userId, int measurementReminderId);
        void DeleteMeasurementReminder(string userId, int measurementReminderId);
    }
}
