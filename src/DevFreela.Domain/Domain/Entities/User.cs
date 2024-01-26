using DevFreela.Domain.Domain.seddwork;
using DevFreela.Domain.Domain.Validation;
using DevFreela.Domain.Domain.Enums;
using DevFreela.Domain.Domain.Entities.Models;
using DevFreela.Domain.Domain.Exceptions;

namespace DevFreela.Domain.Domain.Entities;
public class User : AggregateRoot
{
    public User(
        string name,
        string email,
        string password,
        DateTime birthDate,
        UserRole role)
    {
        Name = name;
        Email = email;
        BirthDate = birthDate;
        Password = password;
        Role = role;
        Active = true;
        Validate();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now.AddSeconds(1);

        Skills = new List<UserSkills>();
        FreelanceProjects = new List<Project>();
        OwnedProjects = new List<Project>();
        Comments = new List<ProjectComment>();
        OldPassword = password;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string Password { get; private set; }
    public string OldPassword { get; set; }
    public UserRole Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    //todo: implementar avatar
    public string? Avatar { get; private set; }
    public bool Active { get; private set; }
    public List<ProjectComment> Comments { get; set; }
    public List<Project> FreelanceProjects { get; set; }
    public List<Project> OwnedProjects { get; set; }
    public List<UserSkills> Skills { get; set; }

    public void Update(
        string? name = null
        , string? email = null
        , DateTime? birthDate = null)
    {
        Name = name ?? Name;
        Email = email ?? Email;
        BirthDate = birthDate ?? BirthDate;
        Validate();

    }

    public void UpdatePassword(string currentPassword, string newPassword)
    {
        if (currentPassword != Password) throw new EntityValidationExceptions("A senha atual não confere");

        var verifyOldPassword =
                newPassword == currentPassword ||
                OldPassword == newPassword;
        if (verifyOldPassword) throw new EntityValidationExceptions("A nova senha não pode ser igual a anterio");


        OldPassword = Password;
        Password = newPassword;
        Validate();
    }


    public void AddSkills(List<Skill> skills)
    {
        var userSkills = skills.Count;
        if (skills == null) return;
        Skills.Clear();

        foreach (var skill in skills)
        {
            var userSkill = new UserSkills(Id, skill.Id);
            Skills.Add(userSkill);
        }
    }


    public void AddOwnedProject(Project project)
    {
        var projectAlreadyExists = OwnedProjects.Any(p => p.Id == project.Id);
        if (projectAlreadyExists) return;
        OwnedProjects.Add(project);
    }


    public void AddFreelanceProject(Project project)
    {
        var projectAlreadyExists = FreelanceProjects.Any(p => p.Id == project.Id);
        if (projectAlreadyExists) return;
        FreelanceProjects.Add(project);
    }


    public void AddComment(ProjectComment comment)
    {
        var commentAlreadyExists = Comments.Any(c => c.Id == comment.Id);
        if (commentAlreadyExists) return;
        Comments.Add(comment);
    }

    public void RemoveComment(ProjectComment comment)
    {
        var commentAlreadyExists = Comments.Any(c => c.Id == comment.Id);
        if (!commentAlreadyExists) return;
        Comments.Remove(comment);
    }

    public void Inactive()
    {
        Active = false;
    }

    public void ActiveUser()
    {
        Active = true;
    }


    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        DomainValidation.NotNullOrEmpty(Email, nameof(Email));
        DomainValidation.NotNullOrEmpty(Password, nameof(Password));
        DomainValidation.MinLength(Password, 8, nameof(Password));
        DomainValidation.MinLength(Name, 3, nameof(Name));
        DomainValidation.MinLength(Email, 5, nameof(Email));
    }


}
