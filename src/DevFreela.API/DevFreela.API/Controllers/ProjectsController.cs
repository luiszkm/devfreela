using DevFreela.API.ApiModels.Response;
using DevFreela.Application.UseCases.Project.FreelancersInterested;
using DevFreela.Application.UseCases.Project.ChangeStatus;
using DevFreela.Application.UseCases.Project.Common;
using DevFreela.Application.UseCases.Project.ContractFreelancer;
using DevFreela.Application.UseCases.Project.CreateProject;
using DevFreela.Application.UseCases.Project.DeleteProject;
using DevFreela.Application.UseCases.Project.GetProject;
using DevFreela.Application.UseCases.Project.UpdateProject;
using DevFreela.Domain.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;


[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{

    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
        => _mediator = mediator;


    [HttpPost]
    [Authorize(Roles = nameof(UserRole.Client))]
    [ProducesResponseType(typeof(ApiResponse<ProjectModelOutput>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Create(
        [FromBody] CreateProjectInput inputModel)
    {

        var output = await _mediator.Send(inputModel);
        var response = new ApiResponse<ProjectModelOutput>(output);

        return CreatedAtAction(nameof(Create), new { id = output }, response);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<ProjectModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var project = await _mediator.Send(new GetProjectInput(id));
        var response = new ApiResponse<ProjectModelOutput>(project);
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteProjectInput(id));
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<ProjectModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProjectInput inputModel)
    {
        var project = await _mediator.Send(inputModel);
        var response = new ApiResponse<ProjectModelOutput>(project);

        return Ok(response);
    }

    [HttpPatch("{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public async Task<IActionResult> ChangeStatus([FromRoute] Guid id, [FromBody] ChangeStatusInputModel inputModel)
    {
        await _mediator.Send(inputModel);
        return NoContent();
    }

    [HttpPatch("{id:guid}/interested")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<ProjectModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public async Task<IActionResult>
        AddFreelancerInterested([FromBody] FreelancersInterestedInput inputModel)
    {
        var project = await _mediator.Send(inputModel);
        var response = new ApiResponse<ProjectModelOutput>(project);

        return Ok(response);
    }

    [HttpPatch("{id:guid}/contract")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<ProjectModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public async Task<IActionResult>
        Contract([FromBody] ContractFreelancerInput inputModel)
    {
        var project = await _mediator.Send(inputModel);
        var response = new ApiResponse<ProjectModelOutput>(project);

        return Ok(response);
    }


}
