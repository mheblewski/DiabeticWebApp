using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DiabeticWebApp.Models;
using DiabeticWebApp.Service.MeasurementService;
using Dtos;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Provider;

namespace DiabeticWebApp.Controllers
{
    [Authorize]
    public class MeasurementsController : ApiController
    {
        private readonly IMeasurementsService _measurementsService;

        public MeasurementsController(IMeasurementsService measurementsService)
        {
            this._measurementsService = measurementsService;
        }

        // GET: api/Measurements
        public IHttpActionResult GetMeasurements()
        {
            var userId = GetCurrentUserId();
            var measurementsList = _measurementsService.GetMeasurements(userId);
            if (measurementsList.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(measurementsList);
        }

        // GET: api/Measurement/2017-05-24
        [Route("api/measurements/{dateFrom:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        public IHttpActionResult GetMeasurements([FromUri]DateTime dateFrom)
        {
            var userId = GetCurrentUserId();
            var measurementsList = _measurementsService.GetMeasurements(userId, dateFrom);
            if (measurementsList.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(measurementsList);
        }

        // GET: api/Measurement/2017-05-24/2017-05-30
        [Route("api/measurements/{dateFrom:datetime:regex(\\d{4}-\\d{2}-\\d{2})}/{dateTo:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        public IHttpActionResult GetMeasurements([FromUri]DateTime dateFrom, [FromUri]DateTime dateTo)
        {
            var userId = GetCurrentUserId();
            var measurementsList = _measurementsService.GetMeasurements(userId, dateFrom, dateTo);
            if (measurementsList.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(measurementsList);
        }

        // GET: api/Measurements/5
        public IHttpActionResult GetMeasurement(int id)
        {
            var userId = GetCurrentUserId();
            var measurement = _measurementsService.GetMeasurement(userId, id);
            if (measurement == null)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            return Ok(measurement);
        }

        // PUT: api/Measurements/5
        public IHttpActionResult PutMeasurement(int id, [FromBody]MeasurementDto measurement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetCurrentUserId();
            if (!_measurementsService.DoesMeasurementExists(userId, id))
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

            measurement.Id = id;
            _measurementsService.UpdateMeasurement(userId, measurement);
            return Ok();
        }

        // POST: api/Measurements
        public IHttpActionResult PostMeasurement([FromBody]MeasurementDto measurement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetCurrentUserId();
            _measurementsService.AddMeasurement(userId, measurement);

            return Ok();
        }

        // DELETE: api/Measurements/5
        public IHttpActionResult DeleteMeasurement(int id)
        {
            var userId = GetCurrentUserId();
            if (!_measurementsService.DoesMeasurementExists(userId, id))
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            _measurementsService.DeleteMeasurement(userId, id);
            return Ok();
        }

        private string GetCurrentUserId()
        {
            return User.Identity.GetUserId();
        }
    }
}