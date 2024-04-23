using Microsoft.AspNetCore.Mvc;
using TTM.Business;
using TTM;
using Microsoft.AspNetCore.Authorization;

namespace TTM_Api.Controllers
{
    //[Authorize]
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
        public IEnumerable<DutyDto> GetDuties([FromHeader] string Authorization)
        {
            return _service.GetAllByUserToken(Authorization);
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

        [HttpDelete("Project/{id}")]
        public CommandResult DeleteProjectDuties(int id)
        {
            return _service.WipeProjectDuties(id);
        }

        [HttpGet("Project/{id}")]
        public DutyDto[] GetProjectDuties(int id)
        {
            return _service.GetAll().Where(d => d.ProjectId == id).ToArray();
        }

        [HttpPut("Status/{id}")]
        public CommandResult UpdateDutyStatus(int id)
        {
            var dutyDto = _service.GetById(id);
            dutyDto.Status = !dutyDto.Status;
            return _service.Update(dutyDto);
        }
    }
}
