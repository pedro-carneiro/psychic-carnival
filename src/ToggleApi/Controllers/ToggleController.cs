namespace ToggleApi.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using ToggleApi.Models.Resources;
    using ToggleApi.Services;

    [Route("api/[controller]")]
    public class ToggleController : Controller
    {
        private readonly ICreateService<ToggleResource> _createService;
        private readonly IReadService<ToggleResource> _readService;
        private readonly IUpdateService<ToggleResource> _updateService;
        private readonly IDeleteService<ToggleResource> _deleteService;

        public ToggleController(ICreateService<ToggleResource> createService,
                                IReadService<ToggleResource> readService,
                                IUpdateService<ToggleResource> updateService,
                                IDeleteService<ToggleResource> deleteService)
        {
            _createService = createService;
            _readService = readService;
            _updateService = updateService;
            _deleteService = deleteService;
        }

        [HttpGet]
        public IEnumerable<ToggleResource> GetAll()
        {
            return _readService.GetAll();
        }

        [HttpGet("{id}", Name = "GetToggle")]
        public IActionResult GetById(long id)
        {
            var toggle = _readService.Get(id);
            return new ObjectResult(toggle);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ToggleResource toggle)
        {
            if (toggle == null)
            {
                return BadRequest();
            }

            var newResource = _createService.Create(toggle);

            return CreatedAtRoute("GetToggle", new { id = newResource.Id }, newResource);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] ToggleResource toggle)
        {
            if (toggle == null || toggle.Id != id)
            {
                return BadRequest();
            }

            _updateService.Update(toggle);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _deleteService.Delete(new ToggleResource() { Id = id });
            return new NoContentResult();
        }
    }
}
