using Microsoft.AspNetCore.Mvc;
using TTM.Business;
using TTM;

namespace TTM_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DutyController : ControllerBase
    {
        private readonly ICrudService<DutyDto> _service;
        public DutyController(ICrudService<DutyDto> crudService)
        {
            _service = crudService;
        }

        [HttpGet]
        public IEnumerable<DutyDto> GetDuties()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public DutyDto GetADuty(int id)
        {
            return _service.GetById(id);
        }

        [HttpPost]
        public CommandResult Create([FromBody] DutyDto dutyDto)
        {
            return _service.Create(dutyDto);
        }

        [HttpPut("{id}")]
        public CommandResult Update(int id, [FromBody] DutyDto dutyDto)
        {
            dutyDto.Id = id;
            return _service.Update(dutyDto);
        }

        [HttpDelete("{id}")]
        public CommandResult Delete(int id)
        {
            var dutyDto = new DutyDto() { Id = id };
            return _service.Delete(dutyDto);
        }

        [HttpGet("project/{id}")]
        public DutyDto[] GetProjectDuties(int id)
        {
            return _service.GetAll().Where(d => d.ProjectId == id).ToArray();
        }
    }
}
