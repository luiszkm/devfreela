using DevFreela.API.ApiModels.Response;
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Create([FromBody] SessionInput inputModel)
    {

        var output = await _mediator.Send(inputModel);
        var response = new ApiResponse<SessionOutput>(output);
        return CreatedAtAction(
            nameof(Create),
            new { id = output.UserId }, response);
    }

}
