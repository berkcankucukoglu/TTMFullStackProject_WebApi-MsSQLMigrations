using System.Diagnostics;
using TTM.Business.Validators;
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

        private readonly ProjectValidator _validotar = new ProjectValidator();
        public override CommandResult Create(ProjectDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            try
            {
                var entity = new Project();
                _mapper.Map(model, entity, typeof(ProjectDto), typeof(Project));

                var validationResult = _validotar.Validate(entity);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                if (entity.Id != null || entity.Duties.Count > 0)
                {
                    return CommandResult.Error("Record was found! This creation terminated!", new Exception());
                }
                _context.Projects.Add(entity);
                _context.SaveChanges();
                return CommandResult.Success("Project created successfully!");
            }
            catch (Exception ex)
            {
                Trace.TraceError($"{DateTime.Now} - {ex}");
                return CommandResult.Error("Project Creation Error!", ex);
            }
        }
        public override CommandResult Update(ProjectDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            try
            {
                var entity = _context.Projects.Find(model.Id);
                var validationResult = _validotar.Validate(entity);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                if (entity == null)
                {
                    return CommandResult.Error("Record was not found!", new Exception());
                }
                entity.Duties.Clear();
                _mapper.Map(model, entity, typeof(ProjectDto), typeof(Project));
                _context.Projects.Update(entity);
                _context.SaveChanges();
                return CommandResult.Success("Project updated successfully!");
            }
            catch (Exception ex)
            {
                Trace.TraceError($"{DateTime.Now} - {ex}");
                return CommandResult.Error("Project Update Error!", ex);
            }
        }
    }
}
