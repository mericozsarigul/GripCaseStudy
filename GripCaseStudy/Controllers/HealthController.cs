﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

namespace GripCaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;
        private readonly HealthCheckService _healthCheckService;
        public HealthController(ILogger<HealthController> logger,
            HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var report = await _healthCheckService.CheckHealthAsync();

            return report.Status == HealthStatus.Healthy ? Ok(report) :
                StatusCode((int)HttpStatusCode.ServiceUnavailable, report);
        }
    }
}
