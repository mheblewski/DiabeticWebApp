using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DiabeticWebApp.Models;
using Dtos;

namespace DiabeticWebApp.Repository.DoctorVisitRepositories
{
    class DoctorVisitRepository : IDoctorVisitRepository
    {
        private readonly ApplicationDbContext _db;
        public DoctorVisitRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public List<DoctorVisitDto> GetDoctorVisits(string userId)
        {
            var doctorVisitsList = _db.DoctorVisits
                .Where(d => d.UserId == userId)
                .OrderBy(d => d.Date)
                .ToList();

            var doctorVisitsDtoList = Mapper.Map<List<DoctorVisitDto>>(doctorVisitsList);
            return doctorVisitsDtoList;
        }

        public void AddDoctorVisit(string userId, DoctorVisitDto doctorVisitDto)
        {
            var doctorVisit = Mapper.Map<DoctorVisit>(doctorVisitDto);
            doctorVisit.UserId = userId;
            _db.DoctorVisits.Add(doctorVisit);
            _db.SaveChanges();
        }

        public void UpdateDoctorVisit(string userId, DoctorVisitDto doctorVisitDto)
        {
            var doctorVisit = _db.DoctorVisits
                .Where(d => d.UserId == userId)
                .FirstOrDefault(d => d.Id == doctorVisitDto.Id);
            if (doctorVisit == null) return;

            doctorVisit.Id = doctorVisitDto.Id;
            doctorVisit.Date = doctorVisitDto.Date;
            doctorVisit.DoctorFirstName = doctorVisitDto.DoctorFirstName;
            doctorVisit.DoctorLastName = doctorVisitDto.DoctorLastName;
            doctorVisit.Speciality = doctorVisitDto.Speciality;
            doctorVisit.TelephoneNumber = doctorVisitDto.TelephoneNumber;
 
            _db.SaveChanges();
        }

        public bool DoesDoctorVisitExists(string userId, int id)
        {
            return _db.DoctorVisits
                       .Where(d => d.UserId == userId)
                       .Count(d => d.Id == id) > 0;
        }

        public void DeleteDoctorVisit(string userId, int id)
        {
            var doctorVisit = _db.DoctorVisits
                .Where(d => d.UserId == userId)
                .FirstOrDefault(d => d.Id == id);
            if (doctorVisit == null) return;
            _db.DoctorVisits.Remove(doctorVisit);
            _db.SaveChanges();
        }
    }
}
