using TTM.DataAccess;
using TTM.Domain;

namespace TTM.Business.Services
{
    public class ProjectService : BaseService<ProjectDto, Project>
    {
        public ProjectService(TTMContext ttmContext) : base(ttmContext)
        {
        }
        static ProjectService()
        {
            _mapperConfigurationExpression.CreateMap<ProjectDto, Project>();
            _mapperConfigurationExpression.CreateMap<Project, ProjectDto>();
        }
    }
}
