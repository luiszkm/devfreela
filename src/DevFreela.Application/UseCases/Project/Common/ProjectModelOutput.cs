

namespace DevFreela.Application.UseCases.Project.Common;
public class ProjectModelOutput
{
    public ProjectModelOutput(
        Guid id,
        Guid clientId,
        string title,
        string description,
        decimal totalCost,
        DateTime createdAt,
        IReadOnlyList<ProjectModelOutputFreelancerInterested> freelancersInterested)
    {
        Id = id;
        ClientId = clientId;
        Title = title;
        Description = description;
        TotalCost = totalCost;
        CreatedAt = createdAt;
        FreelancersInterested = freelancersInterested;
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid ClientId { get; private set; }

    public IReadOnlyList
        <ProjectModelOutputFreelancerInterested> FreelancersInterested
    { get; set; }



    public static ProjectModelOutput FromProject(DomainEntity.Project project)
    {
        return new ProjectModelOutput(
            project.Id,
            project.IdClient,
            project.Title,
            project.Description,
            project.TotalCost,
            project.CreatedAt,
            project.FreelancersInterested
                .Select(freelancer =>
                    new ProjectModelOutputFreelancerInterested(freelancer.Id, freelancer.Name))
                .ToList().AsReadOnly()
        );
    }

    public class ProjectModelOutputFreelancerInterested
    {

        public Guid FreelancerId { get; private set; }
        public string FreelancerFullName { get; private set; }
        public ProjectModelOutputFreelancerInterested(Guid freelancerId, string freelancerFullName)
        {
            FreelancerId = freelancerId;
            FreelancerFullName = freelancerFullName;
        }
    }
}
