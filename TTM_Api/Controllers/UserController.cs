using Microsoft.AspNetCore.Mvc;
using TTM.Business;
using TTM;
using Microsoft.AspNetCore.Authorization;

namespace TTM_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ICrudService<UserDto> _service;
        public UserController(ICrudService<UserDto> crudService)
        {
            _service = crudService;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<UserDto> GetAllUsers()
        {
            return _service.GetAll();
        }

        [Authorize]
        [HttpGet("{id}")]
        public UserDto GetAUser(int id)
        {
            return _service.GetById(id);
        }

        [AllowAnonymous]
        [HttpPost]
        public CommandResult Create([FromBody] UserDto userDto)
        {
            return _service.Create(userDto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public CommandResult Update(int id, [FromBody] UserDto userDto)
        {
            userDto.Id = id;
            return _service.Update(userDto);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public CommandResult Delete(int id)
        {
            var userDto = new UserDto() { Id = id };
            return _service.Delete(userDto);
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public CommandResult Authenticate([FromBody] UserDto userDto)
        {
            return _service.RecordExists(userDto);
        }
    }
}
