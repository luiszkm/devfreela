using DevFreela.API.ApiModels.Response;
using DevFreela.Application.UseCases.User.Common;
using DevFreela.Application.UseCases.User.CreateUser;
using DevFreela.Application.UseCases.User.GetUser;
using DevFreela.Application.UseCases.User.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize]
public class UsersController : ControllerBase
{

    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    => _mediator = mediator;

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult>
        Create([FromBody] CreateUserInput inputModel)
    {


        var output = await _mediator.Send(inputModel);
        var response = new ApiResponse<UserModelOutput>(output);
        return CreatedAtAction(
            nameof(Create),
            new { id = output.Id }, response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var user = await _mediator.Send(new GetUserInput(id));

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Put([FromBody] UpdateUserInput inputModel)
    {

        await _mediator.Send(inputModel);

        return NoContent();
    }

}
