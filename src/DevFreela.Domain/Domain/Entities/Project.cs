
using DevFreela.Domain.Domain.Entities.Models;
using DevFreela.Domain.Domain.Enums;
using DevFreela.Domain.Domain.seddwork;
using DevFreela.Domain.Domain.Validation;
using DevFreela.Domain.Domain.Exceptions;

namespace DevFreela.Domain.Domain.Entities;

public class Project : AggregateRoot
{

    public Project(string title,
        string description,
        decimal totalCost,
        Guid idClient)
    {
        Title = title;
        Description = description;
        IdClient = idClient;
        TotalCost = totalCost;
        Validate();
        CreatedAt = DateTime.Now;
        Status = ProjectStatusEnum.Created;
        Comments = new List<ProjectComment>();
        Skills = new List<ProjectSkills>();
        FreelancersInterested = new List<User>();
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public Guid IdClient { get; private set; }
    public Guid IdFreelancer { get; private set; }
    public decimal TotalCost { get; set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime FinishedAt { get; set; }
    public ProjectStatusEnum Status { get; private set; }
    public List<ProjectComment> Comments { get; private set; }
    public List<ProjectSkills> Skills { get; private set; }
    public List<User> FreelancersInterested { get; private set; }


    public void Update(
        string? title = null,
        string? description = null,
        decimal? totalCost = null)
    {
        Title = title ?? Title;
        Description = description ?? Description;
        TotalCost = totalCost ?? TotalCost;
        Validate();
    }

    public void AddSkills(ProjectSkills skill)
    {
        var skillExists = Skills.Find(x => x.Id == skill.Id);
        if (skillExists != null) return;
        Skills.Add(skill);
    }

    public void RemoveSkills(ProjectSkills skill)
    {
        var skillExists = Skills.Find(x => x.Id == skill.Id);
        if (skillExists != null)
            Skills.Remove(skillExists);
    }


    public void AddComment(ProjectComment comment)
    {
        var commentExists = Comments.Find(x => x.Id == comment.IdProject);
        if (commentExists != null) return;
        Comments.Add(comment);

    }

    public void RemoveComment(ProjectComment comment)
    {
        var commentExists = Comments.Find(x => x.Id == comment.Id);
        if (commentExists != null)
            Comments.Remove(commentExists);
    }

    public void AddFreelancersInterested(User freelancerId)
    {
        var freelancerExists = FreelancersInterested.Find(x => x.Id == freelancerId.Id);
        if (freelancerExists != null) return;
        FreelancersInterested.Add(freelancerId);
    }


    public void RemoveFreelancersInterested(User freelancerId)
    {
        var freelancerExists = FreelancersInterested.Find(x => x.Id == freelancerId.Id);
        if (freelancerExists != null)
            FreelancersInterested.Remove(freelancerExists);
    }

    public void ContractFreelancer(Guid freelancerId)
    {
        var freelancerExists = FreelancersInterested.Find(x => x.Id == freelancerId);
        if (freelancerExists == null)
            FreelancersInterested.Add(freelancerExists);
        IdFreelancer = freelancerExists.Id;
        Status = ProjectStatusEnum.InProgress;
        StartedAt = DateTime.Now;
    }

    private void Cancel()
    {
        if (Status == ProjectStatusEnum.Suspended)
            Status = ProjectStatusEnum.Cancelled;

    }

    private void Suspend()
    {
        if (Status == ProjectStatusEnum.InProgress || Status == ProjectStatusEnum.Created)
        {
            Status = ProjectStatusEnum.Suspended;
        }
    }

    private void Start()
    {
        if (Status == ProjectStatusEnum.Created)
        {
            Status = ProjectStatusEnum.InProgress;
            StartedAt = DateTime.Now;
        }
    }

    private void Finish()
    {
        if (Status == ProjectStatusEnum.InProgress)
        {
            Status = ProjectStatusEnum.PaymentPending;
            FinishedAt = DateTime.Now;
        }
    }

    private void Close()
    {
        if (Status == ProjectStatusEnum.PaymentPending)
        {
            Status = ProjectStatusEnum.Finished;
            FinishedAt = DateTime.Now;
        }
    }

    public void ChangeStatus(ProjectStatusEnum newStatus)
    {
        switch (newStatus)
        {
            case ProjectStatusEnum.Cancelled:
                Cancel();
                break;
            case ProjectStatusEnum.InProgress:
                Start();
                break;
            case ProjectStatusEnum.PaymentPending:
                Finish();
                break;
            case ProjectStatusEnum.Suspended:
                Suspend();
                break;
            case ProjectStatusEnum.Finished:
                Close();
                break;

            default:
                throw new Exception("Status inválido");
                break;
        }
    }


    private void Validate()
    {

        if (TotalCost < 0 || TotalCost == null)
            throw new EntityValidationExceptions("O custo total não pode ser menor que zero");

        DomainValidation.NotNullOrEmpty(Title, nameof(Title));
        DomainValidation.NotNullOrEmpty(Description, nameof(Description));
        DomainValidation.NotNullOrEmpty(IdClient, nameof(IdClient));
        DomainValidation.MinLength(Title, 8, nameof(Title));
        DomainValidation.MinLength(Description, 10, nameof(Description));
        DomainValidation.MinLength(Title, 5, nameof(Title));

    }
}