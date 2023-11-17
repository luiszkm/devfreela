using DevFreela.Application.UseCases.Session;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SessionController : ControllerBase
{
    private readonly IMediator _mediator;

    public SessionController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Login([FromBody] SessionInput inputModel)
    {

        var user = await _mediator.Send(inputModel);

        if (user == null)
        {
            return BadRequest();
        }

        return Ok(user);
    }

}
