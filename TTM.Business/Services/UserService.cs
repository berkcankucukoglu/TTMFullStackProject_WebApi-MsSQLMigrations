﻿using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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
            if (model.Id != null)
            {
                return CommandResult.Error("Some records were found about this user! This creation has been canceled.", new Exception());
            }
            if (_context.Users.Any(u => u.Email == model.Email))
            {
                return CommandResult.Error("The email address is already in use! This creation has been canceled.", new Exception());
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

                entity.Password = PasswordHasher.HashPassword(entity.Password);
                entity.Token = string.Empty;

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
        public override CommandResult RecordExists(UserDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            try
            {
                var entity = _context.Users.FirstOrDefault(u => u.Email == model.Email);
                if (entity == null)
                {
                    return CommandResult.Failure("User record was not found!");
                }
                if (!PasswordHasher.VerifyPassword(model.Password, entity.Password))
                {
                    return CommandResult.Failure("Password is incorrect!");
                }

                entity.Token = CreateJwtToken(entity);
                _context.Users.Update(entity);
                _context.SaveChanges();

                return CommandResult.Success("User record found and authenticated.", entity.Token);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"{DateTime.Now} - {ex}");
                return CommandResult.Error("User record search Error!", ex);
            }
        }

        private string CreateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("veryverysecret.....veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Role == 0 ? "User" : "Admin" ),
                new Claim(ClaimTypes.Email, user.Email)
            });
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddHours(8),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(descriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
