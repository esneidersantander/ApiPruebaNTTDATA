using AutoMapper;
using ApiPruebaNTTDATA.Dtos;
using ApiPruebaNTTDATA.Models;

namespace ApiPruebaNTTDATA.App_Start
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Cliente, ClienteDto>();
            Mapper.CreateMap<ClienteDto, Cliente>();

            Mapper.CreateMap<Cuenta, CuentaDto>();
            Mapper.CreateMap<CuentaDto, Cuenta>();
        }
    }
}