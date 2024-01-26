

namespace DevFreela.Application.UseCases.Project.Common;
public class ListFreelancers
{

    public ListFreelancers(List<FreelancerOutput>? freelancers)
    {
        Freelancers = freelancers;
    }

    public List<FreelancerOutput>? Freelancers { get; set; }


    internal static ListFreelancers FromFreelancer(List<DomainEntity.User> freelancers)
    {
        var listFreelancers = new List<FreelancerOutput>();
        foreach (var freelancer in freelancers)
        {
            var freelancerOutput = new FreelancerOutput(
                         freelancer.Id,
                         freelancer.Name,
                         freelancer.Email);
            listFreelancers.Add(freelancerOutput);
        }

        return new ListFreelancers(listFreelancers);

    }
}
