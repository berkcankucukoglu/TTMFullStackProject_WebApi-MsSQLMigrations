using Microsoft.AspNetCore.Mvc;
using System.Collections;
using TTM;
using TTM.Business;

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
        public IEnumerable GetUsers()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public UserDto GetUser(int id)
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
            var recipeDto = new UserDto() { Id = id };
            return _service.Delete(recipeDto);
        }
    }
}
