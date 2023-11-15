using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public sealed class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        Test test = new();
        test.Age = 1;
        test.Name = "Enes";
        return NoContent();
    }

    [HttpGet]
    public string Get2()
    {
        return "Enes Demirtas";
    }
}

public class Test
{
    public string Name { get; set; }
    public int Age;
}
