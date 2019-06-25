using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SwaggerGenEnums.Models;

namespace SwaggerGenEnums.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<TestDTO> Get(int id)
        {
            return new TestDTO{ Enum = TestEnum.Value01, Timestamp = DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture)};
        }
    }
}
