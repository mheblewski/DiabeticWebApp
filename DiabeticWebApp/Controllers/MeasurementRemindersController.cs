using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DiabeticWebApp.Models;
using DiabeticWebApp.Service.MeasurementReminderService;
using Dtos;
using Microsoft.AspNet.Identity;

namespace DiabeticWebApp.Controllers
{
    [Authorize]
    public class MeasurementRemindersController : ApiController
    {
        private readonly IMeasurementRemindersService _measurementRemindersService;

        public MeasurementRemindersController(IMeasurementRemindersService measurementRemindersService)
        {
            this._measurementRemindersService = measurementRemindersService;
        }

        // GET: api/MeasurementsReminder
        public IHttpActionResult GetMeasurements()
        {
            var userId = GetCurrentUserId();
            var measurementRemindersList = _measurementRemindersService.GetMeasurementReminders(userId);
            if (measurementRemindersList.Count == 0) return StatusCode(HttpStatusCode.NoContent);
            return Ok(measurementRemindersList);
        }

        // POST: api/MeasurementsReminder
        public IHttpActionResult PostMeasurement([FromBody]MeasurementReminderDto measurement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetCurrentUserId();
            _measurementRemindersService.AddMeasurementReminder(userId, measurement);

            return Ok();
        }

        // PUT: api/MeasurementsReminder/5
        public IHttpActionResult PutMeasurement(int id, [FromBody]MeasurementReminderDto measurementReminderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetCurrentUserId();
            if (!_measurementRemindersService.DoesMeasurementReminderExists(userId, id))
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

            measurementReminderDto.Id = id;
            _measurementRemindersService.UpdateMeasurementReminder(userId, measurementReminderDto);
            return Ok();
        }

        // DELETE: api/MeasurementsReminder/5
        public IHttpActionResult DeleteMeasurementReminder(int id)
        {
            var userId = GetCurrentUserId();
            if (!_measurementRemindersService.DoesMeasurementReminderExists(userId, id))
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

            _measurementRemindersService.DeleteMeasurementReminder(userId, id);
            return Ok();
        }

        #region Helpers
    
        private string GetCurrentUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId();
        }

        #endregion
    }
}
