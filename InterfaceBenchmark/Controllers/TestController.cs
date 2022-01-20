
using InterfaceBenchmark.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InterfaceBenchmark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IInterface @interface;
        private readonly RealClass realClass;
        private const uint steps = 4294967295;

        public TestController(IInterface @interface, RealClass realClass)
        {
            this.@interface = @interface;
            this.realClass = realClass;
        }

        [HttpGet]
        public IActionResult TestGet()
        {
            var s1 = Stopwatch.StartNew();
            for (uint i = 0; i < steps; i++)
                @interface.Method();

            s1.Stop();

            var s2 = Stopwatch.StartNew();
            for (uint i = 0; i < steps; i++)
                realClass.Method();

            s2.Stop();

            return Ok(new { interfaceTimeElapsed = $"{s1.Elapsed.TotalMilliseconds} ms", realClassTimeElapsed = $"{s2.Elapsed.TotalMilliseconds} ms" });
        }
    }
}
