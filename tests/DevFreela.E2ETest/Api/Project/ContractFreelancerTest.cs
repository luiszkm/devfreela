

using System.Net;
using DevFreela.API.ApiModels.Response;
using DevFreela.Application.UseCases.Project.Common;
using DevFreela.Application.UseCases.Project.ContractFreelancer;
using DevFreela.Domain.Domain.Enums;
using DevFreela.E2ETest.Api.Project.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.E2ETest.Api.Project;

[Collection(nameof(ProjectAPITestFixture))]
public class ContractFreelancerTest
{

    private readonly ProjectAPITestFixture _fixture;

    public ContractFreelancerTest(ProjectAPITestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(ContractFreelancer))]
    [Trait("E2E/API", "Project/ContractFreelancer - Endpoints")]

    public async Task ContractFreelancer()
    {
        var dbContext = _fixture.CreateApiDbContextInMemory();
        var ownerUser = await _fixture.GetUserInDataBase();
        var freelancerUser = await _fixture.GetUserInDataBase();
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);

        var project = await _fixture.CreateProjectInDataBase(ownerUser.user.Id);
        userAuthenticate.Result.Should().BeTrue();

        var inputModel = new ContractFreelancerInput(project.Id, freelancerUser.user.Id);

        var (response, output) = await _fixture
            .ApiClient.Patch<ApiResponse<ProjectModelOutput>>($"/projects/{project.Id}/contract", inputModel);

        var projectInDataBase = await dbContext.Projects.SingleOrDefaultAsync(p => p.Id == project.Id);
        var userInDataBase = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == freelancerUser.user.Id);

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(HttpStatusCode.OK);
        output!.Data.Should().NotBeNull();
        output!.Data!.Id.Should().NotBeEmpty();
        output!.Data!.Id.Should().Be(project.Id);
        output!.Data!.Title.Should().Be(project.Title);

        projectInDataBase!.IdFreelancer.Should().Be(freelancerUser.user.Id);
        projectInDataBase!.Status.Should().Be(ProjectStatusEnum.InProgress);

        userInDataBase!.FreelanceProjects.Should().Contain(p => p.Id == project.Id);

    }

}
