using DevFreela.API.ApiModels.Response;
using DevFreela.Application.UseCases.Project.Common;
using DevFreela.Application.UseCases.Project.UpdateProject;
using DevFreela.Application.UseCases.User.Common;
using DevFreela.Application.UseCases.User.CreateUser;
using DevFreela.Application.UseCases.User.GetUser;
using DevFreela.Application.UseCases.User.UpdatePassword;
using DevFreela.Application.UseCases.User.UpdateUser;
using DevFreela.Application.UseCases.User.UserSkill;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("[controller]")]

public class UsersController : ControllerBase
{

    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<UserModelOutput>), StatusCodes.Status201Created)]
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

    [HttpPost("/users/skills")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<UserModelOutput>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]

    public async Task<IActionResult>
        InsertSkill([FromBody] UserSkillInput inputModel)
    {


        var output = await _mediator.Send(inputModel);
        var response = new ApiResponse<UserModelOutput>(output);

        return CreatedAtAction(
            nameof(Create),
            new { id = output.Id }, response);
    }


    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<UserModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var output = await _mediator.Send(new GetUserInput(id));
        var response = new ApiResponse<UserModelOutput>(output);

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<UserModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateUserInput inputModel)
    {

        var output = await _mediator.Send<UserModelOutput>(inputModel);
        var response = new ApiResponse<UserModelOutput>(output);

        return Ok(response);
    }


    [HttpPatch("{id:guid}/password")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> UpdatePassword(
        [FromRoute] Guid id,
        [FromBody] UpdatePasswordInput inputModel)
    {
        await _mediator.Send(inputModel);

        return NoContent();
    }



}
