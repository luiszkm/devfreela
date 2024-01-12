

using DevFreela.Application.UseCases.Project.FreelancersInterested;
using DevFreela.Application.UseCases.Project.ListFreelancersInterested;
using DevFreela.UnitTests.Application.Common;
using Moq;

namespace DevFreela.UnitTests.Application.Project;
[Collection(nameof(ProjectApplicationTestFixture))]
public class FreelancersInterested
{

    private readonly ProjectApplicationTestFixture _fixture;

    public FreelancersInterested(ProjectApplicationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(AddFreelancersInterested))]
    [Trait("Application", "Create - Project")]

    public async Task AddFreelancersInterested()
    {
        var project = _fixture.CreateValidProject();
        var user = _fixture.GetValidUser();
        var repositoryMock = _fixture.GetProjectRepositoryMock();
        var userRepositoryMock = _fixture.GetUserRepositoryMock();
        repositoryMock.Setup(r => r
                .GetById(project.Id, CancellationToken.None))
            .ReturnsAsync(project);

        userRepositoryMock.Setup(r => r
            .GetById(user.Id, CancellationToken.None))
            .ReturnsAsync(user);


        var useCase = new ProjectUseCase.FreelancersInterested
            .FreelancersInterested(
                       repositoryMock.Object,
                       userRepositoryMock.Object);

        var input = new FreelancersInterestedInput(
            project.Id, user.Id, true);

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(r => r.AddFreelancerInterested(
            project.Id,
            user.Id,
            CancellationToken.None), Times.Once);

        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.FreelancersInterested.Should().HaveCount(1);
        output.FreelancersInterested!.First().FreelancerId.Should().Be(user.Id);


    }

    [Fact(DisplayName = nameof(AddFreelancersInterestedWithManyInterested))]
    [Trait("Application", "Create - Project")]

    public async Task AddFreelancersInterestedWithManyInterested()
    {
        var project = _fixture.CreateValidProject();
        var user = _fixture.GetValidUser();
        var userList = _fixture.GetListUsers(5);
        var repositoryMock = _fixture.GetProjectRepositoryMock();
        var userRepositoryMock = _fixture.GetUserRepositoryMock();
        repositoryMock.Setup(r => r
                .GetById(project.Id, CancellationToken.None))
            .ReturnsAsync(project);

        foreach (var item in userList)
        {
            userRepositoryMock.Setup(r => r
                           .GetById(item.Id, CancellationToken.None))
                .ReturnsAsync(item);

            project.AddFreelancersInterested(item);
        }

        userRepositoryMock.Setup(r => r
                .GetById(user.Id, CancellationToken.None))
            .ReturnsAsync(user);


        var useCase = new ProjectUseCase.FreelancersInterested
            .FreelancersInterested(
                repositoryMock.Object,
                userRepositoryMock.Object);


        var input = new FreelancersInterestedInput(
            project.Id, user.Id, true);

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(r => r.AddFreelancerInterested(
            project.Id,
            user.Id,
            CancellationToken.None), Times.Once);

        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.FreelancersInterested.Should().HaveCount(6);
        output.FreelancersInterested!.Should().Contain(f => f.FreelancerId == user.Id);

    }


    [Fact(DisplayName = nameof(RemoveFreelancersInterestedWithManyInterested))]
    [Trait("Application", "Create - Project")]

    public async Task RemoveFreelancersInterestedWithManyInterested()
    {
        var project = _fixture.CreateValidProject();
        var user = _fixture.GetValidUser();
        var userList = _fixture.GetListUsers(5);
        var repositoryMock = _fixture.GetProjectRepositoryMock();
        var userRepositoryMock = _fixture.GetUserRepositoryMock();
        repositoryMock.Setup(r => r
                .GetById(project.Id, CancellationToken.None))
            .ReturnsAsync(project);

        foreach (var item in userList)
        {
            userRepositoryMock.Setup(r => r
                    .GetById(item.Id, CancellationToken.None))
                .ReturnsAsync(item);

            project.AddFreelancersInterested(item);
        }
        project.AddFreelancersInterested(user);


        userRepositoryMock.Setup(r => r
                .GetById(user.Id, CancellationToken.None))
            .ReturnsAsync(user);

        project.AddFreelancersInterested(user);
        project.FreelancersInterested.Should().NotBeNullOrEmpty();


        var useCase = new ProjectUseCase.FreelancersInterested.FreelancersInterested(
                repositoryMock.Object,
                userRepositoryMock.Object);


        var input = new FreelancersInterestedInput(
            project.Id, user.Id, false);

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(r => r.RemoveFreelancerInterested(
            project.Id,
            user.Id,
            CancellationToken.None), Times.Once);

        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.FreelancersInterested.Should().HaveCount(userList.Count);
        output.FreelancersInterested!.Should().NotContain(f => f.FreelancerId == user.Id);

    }

    [Fact(DisplayName = nameof(RemoveFreelancersInterested))]
    [Trait("Application", "Create - Project")]

    public async Task RemoveFreelancersInterested()
    {
        var project = _fixture.CreateValidProject();
        var user = _fixture.GetValidUser();
        var repositoryMock = _fixture.GetProjectRepositoryMock();
        var userRepositoryMock = _fixture.GetUserRepositoryMock();
        repositoryMock.Setup(r => r
                .GetById(project.Id, CancellationToken.None))
            .ReturnsAsync(project);

        userRepositoryMock.Setup(r => r
                .GetById(user.Id, CancellationToken.None))
            .ReturnsAsync(user);

        project.AddFreelancersInterested(user);
        project.FreelancersInterested.Should().NotBeNullOrEmpty();


        var useCase = new ProjectUseCase.FreelancersInterested.FreelancersInterested(
            repositoryMock.Object,
            userRepositoryMock.Object);


        var input = new FreelancersInterestedInput(
            project.Id, user.Id, false);

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(r => r.RemoveFreelancerInterested(
            project.Id,
            user.Id,
            CancellationToken.None), Times.Once);

        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.FreelancersInterested.Should().BeEmpty();

    }


    [Fact(DisplayName = nameof(ListAllFreelancersInterested))]
    [Trait("Application", "Create - Project")]

    public async Task ListAllFreelancersInterested()
    {
        var project = _fixture.CreateValidProject();
        var userList = _fixture.GetListUsers(5);
        var repositoryMock = _fixture.GetProjectRepositoryMock();
        var userRepositoryMock = _fixture.GetUserRepositoryMock();
        repositoryMock.Setup(r => r
                .GetById(project.Id, CancellationToken.None))
            .ReturnsAsync(project);

        foreach (var item in userList)
        {
            userRepositoryMock.Setup(r => r
                    .GetById(item.Id, CancellationToken.None))
                .ReturnsAsync(item);

            project.AddFreelancersInterested(item);
        }

        project.FreelancersInterested.Should().NotBeNullOrEmpty();


        var useCase = new ProjectUseCase.ListFreelancersInterested.ListFreelancersInterested(
            repositoryMock.Object,
            userRepositoryMock.Object);


        var input = new ListFreelancersInterestedInput(
            project.Id);

        var output = await useCase.Handle(input, CancellationToken.None);

        var freelancerDataOutputExample = output.Freelancers!.First();

        output.Should().NotBeNull();
        output.Freelancers.Should().HaveCount(userList.Count);
        output.Freelancers!.Should().Contain(f => f.UserId == userList.First().Id);
        freelancerDataOutputExample.Name.Should().Be(userList!.First().Name);
        freelancerDataOutputExample.Email.Should().Be(userList!.First().Email);

    }


    [Fact(DisplayName = nameof(ListAllFreelancersInterestedWhenNoHaveAny))]
    [Trait("Application", "Create - Project")]

    public async Task ListAllFreelancersInterestedWhenNoHaveAny()
    {
        var project = _fixture.CreateValidProject();

        var repositoryMock = _fixture.GetProjectRepositoryMock();
        var userRepositoryMock = _fixture.GetUserRepositoryMock();
        repositoryMock.Setup(r => r
                .GetById(project.Id, CancellationToken.None))
            .ReturnsAsync(project);

        var useCase = new ProjectUseCase.ListFreelancersInterested.ListFreelancersInterested(
            repositoryMock.Object,
            userRepositoryMock.Object);


        var input = new ListFreelancersInterestedInput(
            project.Id);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Freelancers.Should().HaveCount(0);
        project.FreelancersInterested.Should().BeNullOrEmpty();


    }
}
