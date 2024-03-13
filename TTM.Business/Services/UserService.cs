using TTM.DataAccess;
using TTM.Domain;

namespace TTM.Business.Services
{
    public class UserService : BaseService<UserDto, User>
    {
        public UserService(TTMContext ttmContext) : base(ttmContext)
        {
        }
        static UserService() 
        {
            _mapperConfigurationExpression.CreateMap<Project, ProjectDto>();
            _mapperConfigurationExpression.CreateMap<ProjectDto, Project>();
            _mapperConfigurationExpression.CreateMap<Duty, DutyDto>();
            _mapperConfigurationExpression.CreateMap<DutyDto, Duty>();
        }
    }
}
