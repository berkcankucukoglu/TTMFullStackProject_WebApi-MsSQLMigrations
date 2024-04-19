using Microsoft.AspNetCore.Mvc;
using TTM.Business;
using TTM;

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

        [HttpGet]
        public IEnumerable<UserDto> GetAllUsers()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public UserDto GetAUser(int id)
        {
            return _service.GetById(id);
        }

        [HttpPost]
        public CommandResult Create([FromBody] UserDto userDto)
        {
            return _service.Create(userDto);
        }

        [HttpPut("{id}")]
        public CommandResult Update(int id, [FromBody] UserDto userDto)
        {
            userDto.Id = id;
            return _service.Update(userDto);
        }

        [HttpDelete("{id}")]
        public CommandResult Delete(int id)
        {
            var userDto = new UserDto() { Id = id };
            return _service.Delete(userDto);
        }

        [HttpPost("Authenticate")]
        public CommandResult Authenticate([FromBody] UserDto userDto)
        {
            return _service.RecordExists(userDto);
        }
    }
}
