using Microsoft.AspNetCore.Mvc;
using TTM.Business;
using TTM;

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

        [HttpGet]
        public IEnumerable<ProjectDto> GetProjects()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public ProjectDto GetAProject(int id)
        {
            return _service.GetById(id);
        }

        [HttpPost]
        public CommandResult Create([FromBody] ProjectDto projectDto)
        {
            return _service.Create(projectDto);
        }

        [HttpPut("{id}")]
        public CommandResult Update(int id, [FromBody] ProjectDto projectDto)
        {
            projectDto.Id = id;
            return _service.Update(projectDto);
        }

        [HttpDelete("{id}")]
        public CommandResult Delete(int id)
        {
            var projectDto = new ProjectDto() { Id = id };
            return _service.Delete(projectDto);
        }
    }
}
