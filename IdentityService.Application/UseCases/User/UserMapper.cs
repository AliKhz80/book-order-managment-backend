using AutoMapper;
using IdentityService.Application.UseCases.User.Commonds.RegisterUserCommand;
using Microsoft.Extensions.Logging.Abstractions;

namespace IdentityService.Application.UseCases.User
{
    public static class UserMapper
    {
        public static Domain.Entities.User ToEntity(this RegisterUserCommandRequest input)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<RegisterUserCommandRequest, Domain.Entities.User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore()), NullLoggerFactory.Instance);

            var mapper = new Mapper(config);

            return mapper.Map<Domain.Entities.User>(input);
        }

    }
}
