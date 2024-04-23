using Microsoft.AspNetCore.Mvc;
using TTM.Business;
using TTM;
using Microsoft.AspNetCore.Authorization;

namespace TTM_Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICrudService<CategoryDto> _service;
        public CategoryController(ICrudService<CategoryDto> crudService)
        {
            _service = crudService;
        }

        [HttpGet]
        public IEnumerable<CategoryDto> GetCategories([FromHeader] string Authorization)
        {
            return _service.GetAllByUserToken(Authorization);
        }

        [HttpGet("{id}")]
        public CategoryDto GetACategory(int id)
        {
            return _service.GetById(id);
        }

        [HttpPost]
        public CommandResult Create([FromBody] CategoryDto categoryDto)
        {
            return _service.Create(categoryDto);
        }

        [HttpPut("{id}")]
        public CommandResult Update(int id, [FromBody] CategoryDto categoryDto)
        {
            categoryDto.Id = id;
            return _service.Update(categoryDto);
        }

        [HttpDelete("{id}")]
        public CommandResult Delete(int id)
        {
            var categoryDto = new CategoryDto() { Id = id };
            return _service.Delete(categoryDto);
        }
    }
}
