using DevFreela.Application.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;


[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{


    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok();
    }




}
