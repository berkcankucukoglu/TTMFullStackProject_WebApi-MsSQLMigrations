using System.Diagnostics;
using TTM.Business.Validators;
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

        private readonly UserValidator _validotar = new UserValidator();
        public override CommandResult Create(UserDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.Id != null || model.Projects.Count > 0 || model.Duties.Count > 0)
            {
                return CommandResult.Error("Some other record was found about this user! This creation terminated!", new Exception());
            }
            try
            {
                var entity = new User();
                _mapper.Map(model, entity, typeof(UserDto), typeof(User));

                var validationResult = _validotar.Validate(entity);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                _context.Users.Add(entity);
                _context.SaveChanges();
                return CommandResult.Success("User created successfully!");
            }
            catch (Exception ex)
            {
                Trace.TraceError($"{DateTime.Now} - {ex}");
                return CommandResult.Error("User Creation Error!", ex);
            }
        } 
        public override CommandResult Update(UserDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            try
            {
                var entity = _context.Users.Find(model.Id);
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
                entity.Duties.Clear();
                _mapper.Map(model, entity, typeof(UserDto), typeof(User));
                _context.Users.Update(entity);
                _context.SaveChanges();
                return CommandResult.Success("User updated successfully!");
            }
            catch (Exception ex)
            {
                Trace.TraceError($"{DateTime.Now} - {ex}");
                return CommandResult.Error("User Update Error!", ex);
            }
        }
    }
}
