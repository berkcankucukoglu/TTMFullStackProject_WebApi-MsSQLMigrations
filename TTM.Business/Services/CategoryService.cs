using System.Diagnostics;
using TTM.Business.Validators;
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

        private readonly CategoryValidator _validotar = new CategoryValidator();
        public override CommandResult Create(CategoryDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            try
            {
                var entity = new Category();
                _mapper.Map(model, entity, typeof(CategoryDto), typeof(Category));

                var validationResult = _validotar.Validate(entity);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                if (entity.Id != null || entity.Projects.Count > 0)
                {
                    return CommandResult.Error("Record was found! This creation terminated!", new Exception());
                }
                _context.Categories.Add(entity);
                _context.SaveChanges();
                return CommandResult.Success("Category created successfully!");
            }
            catch (Exception ex)
            {
                Trace.TraceError($"{DateTime.Now} - {ex}");
                return CommandResult.Error("Category Creation Error!", ex);
            }
        }
        public override CommandResult Update(CategoryDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            try
            {
                var entity = _context.Categories.Find(model.Id);
                var validationResult = _validotar.Validate(entity);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                if (entity == null)
                {
                    return CommandResult.Error("Record was not found!", new Exception());
                }
                entity.Projects.Clear();
                _mapper.Map(model, entity, typeof(CategoryDto), typeof(Category));
                _context.Categories.Update(entity);
                _context.SaveChanges();
                return CommandResult.Success("Category updated successfully!");
            }
            catch (Exception ex)
            {
                Trace.TraceError($"{DateTime.Now} - {ex}");
                return CommandResult.Error("Category Update Error!", ex);
            }
        }
    }
}
