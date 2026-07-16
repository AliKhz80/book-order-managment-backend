using AutoMapper;
using IdentityService.Domain.Entities;
using IdentityService.Application.UseCases.User.Commonds.RegisterUserCommand;

namespace IdentityService.Application.UseCases.User
{
    public static class UserMapper
    {
        public static Domain.Entities.User ToEntity(this RegisterUserCommandRequest input)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<RegisterUserCommandRequest, Domain.Entities.User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore()) , null);

            var mapper = new Mapper(config);

            return mapper.Map<Domain.Entities.User>(input);
        }

    }
}
