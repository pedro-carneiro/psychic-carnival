namespace ToggleApi.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using ToggleApi.Models.Requests;
    using ToggleApi.Models.Responses;
    using ToggleApi.Services;

    [Route("api/[controller]")]
    public class ToggleController : Controller
    {
        private readonly ICreateService<ToggleRequest, ToggleResponse> _createService;
        private readonly IReadService<ToggleRequest, ToggleResponse> _readService;
        private readonly IUpdateService<ToggleRequest> _updateService;
        private readonly IDeleteService<ToggleRequest> _deleteService;

        public ToggleController(ICreateService<ToggleRequest, ToggleResponse> createService,
                                IReadService<ToggleRequest, ToggleResponse> readService,
                                IUpdateService<ToggleRequest> updateService,
                                IDeleteService<ToggleRequest> deleteService)
        {
            _createService = createService;
            _readService = readService;
            _updateService = updateService;
            _deleteService = deleteService;
        }

        [HttpGet]
        public IEnumerable<ToggleResponse> GetAll()
        {
            return _readService.GetAll(null);
        }

        [HttpGet("{id}", Name = "GetToggle")]
        public IActionResult GetById(long id)
        {
            var toggle = _readService.Get(new ToggleRequest() { Id = id });
            return new ObjectResult(toggle);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ToggleRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var response = _createService.Create(request);

            return CreatedAtRoute("GetToggle", new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] ToggleRequest request)
        {
            if (request == null || request.Id != id)
            {
                return BadRequest();
            }

            _updateService.Update(request);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _deleteService.Delete(new ToggleRequest() { Id = id });
            return new NoContentResult();
        }
    }
}
