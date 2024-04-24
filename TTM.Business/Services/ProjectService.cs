using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
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
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.Id != null)
            {
                return CommandResult.Error("Some other record was found about this project! This creation terminated!", new Exception());
            }
            try
            {
                var entity = new Project();
                _mapper.Map(model, entity, typeof(ProjectDto), typeof(Project));
                entity.CreatedDate = DateTime.Now;

                var validationResult = _validotar.Validate(entity);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
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
        public override List<ProjectDto> GetAllByUserToken(string? token)
        {
            if (token == null)
                return new List<ProjectDto>();

            try
            {
                var userEntity = GetUserEntityFromToken(token);
                var dtoList = new List<ProjectDto>();
                var allEntities = _context.Projects.Where(p => p.UserId == userEntity.Id);
                foreach (var entity in allEntities)
                {
                    var dto = _mapper.Map<ProjectDto>(entity);
                    dtoList.Add(dto);
                }
                return dtoList;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<ProjectDto>();
            }
        }
        public override CommandResult CreateByUserToken(ProjectDto model, string token)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.Id != null)
            {
                return CommandResult.Error("Some other record was found about this project! This creation terminated!", new Exception());
            }
            try
            {
                var userEntity = GetUserEntityFromToken(token);
                var entity = new Project();
                _mapper.Map(model, entity, typeof(ProjectDto), typeof(Project));
                entity.CreatedDate = DateTime.Now;
                entity.UserId = userEntity.Id;

                var validationResult = _validotar.Validate(entity);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
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
        private User GetUserEntityFromToken(string token)
        {
            token = token.Substring(7);
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("veryverysecret.....veryverysecret.....");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
            };

            SecurityToken securityToken;
            var principal = jwtTokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null)
            {
                throw new SecurityTokenException("This is Invalid Token");
            }
            var emailClaim = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "email");
            var emailValue = string.Empty;
            if (emailClaim != null)
            {
                emailValue = emailClaim.Value;
            }
            var user = _context.Users.FirstOrDefault(u => u.Email == emailValue && u.Token == token);
            return user;
        }
    }
}
