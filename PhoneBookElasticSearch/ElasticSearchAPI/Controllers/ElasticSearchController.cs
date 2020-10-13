using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticSearchService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ElasticSearchAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ElasticSearchController : ControllerBase
    {
        IESService service;
        public ElasticSearchController(IESService service)
        {
            this.service = service;
        }
        [Route("phonebook"), HttpGet]
        public IActionResult Index(string searchTerm)
        {
            return Ok(service.SearchDocuments(searchTerm));
        }
    }
}
