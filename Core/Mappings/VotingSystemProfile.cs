using AutoMapper;
using OnlineClinic.Core.DTOs;
using OnlineClinic.Data.Entities;

namespace OnlineClinic.Core.Mappings
{
    public class OnlineClinicProfile : Profile
    {
        public OnlineClinicProfile()
        {
            CreateMap<DoctorCreateDto, Doctor>();
            CreateMap<Doctor, DoctorGetDto>()
                .ForMember(dest => dest.DoctorFullname, opt => opt.MapFrom(src => src.Person.Fullname))
                .ForMember(dest => dest.SpecializationName, opt => opt.MapFrom(src => src.Specialization.Name));
            CreateMap<Doctor, DoctorUserCredentialDto>();

            CreateMap<SpecializationCreateDto, Specialization>();
            CreateMap<Specialization, SpecializationGetDto>();

            CreateMap<SpecializationEditDto, Specialization>(); 
            CreateMap<Specialization, SpecializationEditDto>();

            CreateMap<SpecializationGetDto, SpecializationEditDto>();     

            CreateMap<PersonCreateDto, Person>(); 
            CreateMap<Person, PersonGetDto>(); 
            CreateMap<PersonGetDto, PersonEditDto>()
                .ForMember(dest => dest.DOBString, opt => opt.MapFrom(src => $"{src.DOB.ToString("yyyy-MM-dd")}"));
            CreateMap<PersonEditDto, Person>();
        }
    }
}