using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiabeticWebApp.Models;
using DiabeticWebApp.Repository.DoctorVisitRepositories;

namespace DiabeticWebApp.Service.DoctorVisitService
{
    class DoctorVisitsService : IDoctorVisitsService
    {
        private readonly IDoctorVisitRepository _repository;

        public DoctorVisitsService(IDoctorVisitRepository repository)
        {
            this._repository = repository;
        }

        public List<DoctorVisitDto> GetDoctorVisits(string userId)
        {
            return _repository.GetDoctorVisits(userId);
        }

        public void AddDoctorVisit(string userId, DoctorVisitDto doctorVisitDto)
        {
            _repository.AddDoctorVisit(userId, doctorVisitDto);
        }

        public void UpdateDoctorVisit(string userId, DoctorVisitDto doctorVisitDto)
        {
            _repository.UpdateDoctorVisit(userId, doctorVisitDto);
        }

        public bool DoctorVisitExists(string userId, int id)
        {
            return _repository.DoctorVisitExists(userId, id);
        }

        public void DeleteDoctorVisit(string userId, int id)
        {
            _repository.DeleteDoctorVisit(userId, id);
        }
    }
}
