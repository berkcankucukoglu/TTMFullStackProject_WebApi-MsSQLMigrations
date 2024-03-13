using TTM.DataAccess;
using TTM.Domain;

namespace TTM.Business.Services
{
    public class CategoryService : BaseService<CategoryDto, Category>
    {
        public CategoryService(TTMContext ttmContext) : base(ttmContext)
        {
        }
        static CategoryService()
        {
            _mapperConfigurationExpression.CreateMap<Project, ProjectDto>();
            _mapperConfigurationExpression.CreateMap<ProjectDto, Project>();
        }
    }
}
