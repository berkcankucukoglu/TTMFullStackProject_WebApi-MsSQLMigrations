using Microsoft.AspNetCore.Mvc;
using TTM.Business;
using TTM;
using Microsoft.AspNetCore.Authorization;

namespace TTM_Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ICrudService<ProjectDto> _service;
        public ProjectController(ICrudService<ProjectDto> crudService)
        {
            _service = crudService;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<ProjectDto> GetProjects([FromHeader] string Authorization)
        {

            return _service.GetAllByUserToken(Authorization);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ProjectDto GetAProject(int id)
        {
            return _service.GetById(id);
        }

        [Authorize]
        [HttpPost]
        public CommandResult Create([FromBody] ProjectDto projectDto, [FromHeader] string Authorization)
        {
            return _service.CreateByUserToken(projectDto, Authorization);
        }

        [Authorize]
        [HttpPut("{id}")]
        public CommandResult Update(int id, [FromBody] ProjectDto projectDto)
        {
            projectDto.Id = id;
            return _service.Update(projectDto);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public CommandResult Delete(int id)
        {
            var projectDto = new ProjectDto() { Id = id };
            return _service.Delete(projectDto);
        }
    }
}
