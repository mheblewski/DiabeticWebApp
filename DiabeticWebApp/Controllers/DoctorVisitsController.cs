using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DiabeticWebApp.Models;
using DiabeticWebApp.Service.DoctorVisitService;
using Microsoft.AspNet.Identity;

namespace DiabeticWebApp.Controllers
{
    [Authorize]
    public class DoctorVisitsController : ApiController
    {
        private readonly IDoctorVisitsService _doctorVisitsService;
        public DoctorVisitsController(IDoctorVisitsService doctorVisitsService)
        {
            this._doctorVisitsService = doctorVisitsService;
        }

        // GET: api/DoctorVisits
        public IHttpActionResult GetDoctorVisits()
        {
            var userId = GetCurrentUserId();
            var doctorVisitsList = _doctorVisitsService.GetDoctorVisits(userId);
            if (doctorVisitsList.Count == 0) return StatusCode(HttpStatusCode.NoContent);
            return Ok(doctorVisitsList);
        }


        // POST: api/DoctorVisits
        public IHttpActionResult PostDoctorVisit([FromBody]DoctorVisitDto doctorVisitDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetCurrentUserId();
            _doctorVisitsService.AddDoctorVisit(userId, doctorVisitDto);

            return Ok();
        }

        // PUT: api/DoctorVisits/5
        public IHttpActionResult PutMeasurement(int id, [FromBody]DoctorVisitDto doctorVisitDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetCurrentUserId();
            if (!_doctorVisitsService.DoctorVisitExists(userId, id))
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

            doctorVisitDto.Id = id;
            _doctorVisitsService.UpdateDoctorVisit(userId, doctorVisitDto);
            return Ok();
        }

        // DELETE: api/DoctorVisits/5
        public IHttpActionResult DeleteDoctorVisits(int id)
        {
            var userId = GetCurrentUserId();
            if (!_doctorVisitsService.DoctorVisitExists(userId, id))
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

            _doctorVisitsService.DeleteDoctorVisit(userId, id);
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
