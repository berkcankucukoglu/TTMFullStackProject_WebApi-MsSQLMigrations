using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
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
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.Id != null)
            {
                return CommandResult.Error("Some other record was found about this category! This creation terminated!", new Exception());
            }
            try
            {
                var entity = new Category();
                _mapper.Map(model, entity, typeof(CategoryDto), typeof(Category));

                var validationResult = _validotar.Validate(entity);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
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
        public override List<CategoryDto> GetAllByUserToken(string? token)
        {
            try
            {
                var userEntity = GetUserEntityFromToken(token);
                var dtoList = new List<CategoryDto>();
                var allEntities = _context.Categories.Where(c => c.UserId == userEntity.Id || c.UserId == null);
                foreach (var entity in allEntities)
                {
                    var dto = _mapper.Map<CategoryDto>(entity);
                    dtoList.Add(dto);
                }
                return dtoList;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<CategoryDto>();
            }
        }
        public override CommandResult CreateByUserToken(CategoryDto model, string token)
        {
            if (model == null || token == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.Id != null)
            {
                return CommandResult.Error("Some other record was found about this category! This creation terminated!", new Exception());
            }
            try
            {
                var userEntity = GetUserEntityFromToken(token);
                var entity = new Category();
                _mapper.Map(model, entity, typeof(CategoryDto), typeof(Category));
                entity.UserId = userEntity.Id;

                var validationResult = _validotar.Validate(entity);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
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
