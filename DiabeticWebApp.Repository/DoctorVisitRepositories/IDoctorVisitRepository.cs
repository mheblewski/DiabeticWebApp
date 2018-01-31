using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiabeticWebApp.Models;

namespace DiabeticWebApp.Repository.DoctorVisitRepositories
{
    public interface IDoctorVisitRepository
    {
        List<DoctorVisitDto> GetDoctorVisits(string userId);
        void AddDoctorVisit(string userId, DoctorVisitDto doctorVisitDto);
        void UpdateDoctorVisit(string userId, DoctorVisitDto doctorVisitDto);
        bool DoesDoctorVisitExists(string userId, int id);
        void DeleteDoctorVisit(string userId, int id);
    }
}
