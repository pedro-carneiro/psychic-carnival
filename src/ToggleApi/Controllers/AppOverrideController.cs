namespace ToggleApi.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using ToggleApi.Models.Requests;
    using ToggleApi.Models.Responses;
    using ToggleApi.Services;

    [Route("api/application")]
    public class AppOverrideController : Controller
    {
        private readonly ICreateService<AppOverrideRequest, AppOverrideResponse> _createService;
        private readonly IReadService<AppOverrideRequest, AppOverrideResponse> _readService;
        private readonly IUpdateService<AppOverrideRequest> _updateService;
        private readonly IDeleteService<AppOverrideRequest> _deleteService;

        public AppOverrideController(ICreateService<AppOverrideRequest, AppOverrideResponse> createService,
                                     IReadService<AppOverrideRequest, AppOverrideResponse> readService,
                                     IUpdateService<AppOverrideRequest> updateService,
                                     IDeleteService<AppOverrideRequest> deleteService)
        {
            _createService = createService;
            _readService = readService;
            _updateService = updateService;
            _deleteService = deleteService;
        }

        [HttpGet("{application}/toggle")]
        public IEnumerable<AppOverrideResponse> GetAll(string application)
        {
            return _readService.GetAll(new AppOverrideRequest() { Application = application });
        }

        [HttpGet("{application}/toggle/{toggleId}", Name = "GetApplicationOverride")]
        public IActionResult GetById(string application, long toggleId)
        {
            var appOverride = _readService.Get(new AppOverrideRequest() { ToggleId = toggleId, Application = application });
            return new ObjectResult(appOverride);
        }

        [HttpPost("{application}/toggle")]
        public IActionResult Create(string application, [FromBody] AppOverrideRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            request.Application = application;
            var newResource = _createService.Create(request);

            return CreatedAtRoute("GetApplicationOverride", new { toggleId = newResource.ToggleId }, newResource);
        }

        [HttpPut("{application}/toggle/{toggleId}")]
        public IActionResult Update(string application, long toggleId, [FromBody] AppOverrideRequest request)
        {
            if (request == null || request.ToggleId != toggleId)
            {
                return BadRequest();
            }

            request.Application = application;
            _updateService.Update(request);
            return new NoContentResult();
        }

        [HttpDelete("{application}/toggle/{toggleId}")]
        public IActionResult Delete(string application, long toggleId)
        {
            _deleteService.Delete(new AppOverrideRequest() { ToggleId = toggleId, Application = application });
            return new NoContentResult();
        }
    }
}
