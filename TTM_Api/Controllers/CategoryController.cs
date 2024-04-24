using Microsoft.AspNetCore.Mvc;
using TTM.Business;
using TTM;
using Microsoft.AspNetCore.Authorization;

namespace TTM_Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICrudService<CategoryDto> _service;
        public CategoryController(ICrudService<CategoryDto> crudService)
        {
            _service = crudService;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<CategoryDto> GetCategories([FromHeader] string Authorization)
        {
            return _service.GetAllByUserToken(Authorization);
        }

        [Authorize]
        [HttpGet("{id}")]
        public CategoryDto GetACategory(int id)
        {
            return _service.GetById(id);
        }

        [Authorize]
        [HttpPost]
        public CommandResult Create([FromBody] CategoryDto categoryDto, [FromHeader] string Authorization)
        {
            return _service.CreateByUserToken(categoryDto, Authorization);
        }

        [Authorize]
        [HttpPut("{id}")]
        public CommandResult Update(int id, [FromBody] CategoryDto categoryDto)
        {
            categoryDto.Id = id;
            return _service.Update(categoryDto);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public CommandResult Delete(int id)
        {
            var categoryDto = new CategoryDto() { Id = id };
            return _service.Delete(categoryDto);
        }
    }
}
