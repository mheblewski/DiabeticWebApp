using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DiabeticWebApp.Models;
using Dtos;

namespace DiabeticWebApp.Repository
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            this.CreateMap<MeasurementDto, Measurement>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.Id))
                .ForMember(x => x.Date, o => o.MapFrom(s => s.Date))
                .ForMember(x => x.Description, o => o.MapFrom(s => s.Description))
                .ForMember(x => x.Result, o => o.MapFrom(s => s.Result))               
                .ForMember(x => x.User, opt => opt.Ignore())
                .ForMember(x => x.UserId, opt => opt.Ignore());

            this.CreateMap<MeasurementReminderDto, MeasurementReminder>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.Id))
                .ForMember(x => x.Time, o => o.MapFrom(s => s.Time))
                .ForMember(x => x.User, opt => opt.Ignore())
                .ForMember(x => x.UserId, opt => opt.Ignore());

            this.CreateMap<DoctorVisitDto, DoctorVisit>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.Id))
                .ForMember(x => x.Date, o => o.MapFrom(s => s.Date))
                .ForMember(x => x.DoctorFirstName, o => o.MapFrom(s => s.DoctorFirstName))
                .ForMember(x => x.DoctorLastName, o => o.MapFrom(s => s.DoctorLastName))
                .ForMember(x => x.Speciality, o => o.MapFrom(s => s.Speciality))
                .ForMember(x => x.TelephoneNumber, o => o.MapFrom(s => s.TelephoneNumber))
                .ForMember(x => x.User, opt => opt.Ignore())
                .ForMember(x => x.UserId, opt => opt.Ignore());
        }
    }
}
