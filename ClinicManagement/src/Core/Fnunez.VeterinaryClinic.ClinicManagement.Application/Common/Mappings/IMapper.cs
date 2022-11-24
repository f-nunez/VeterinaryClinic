using AutoMapper;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Mappings;

public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}