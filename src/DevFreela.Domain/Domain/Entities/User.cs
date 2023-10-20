

using DevFreela.Domain.Domain.seddwork;
using DevFreela.Domain.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace DevFreela.Domain.Domain.Entities;
public class User : AgregateRoot
{
    public User(
        string fullName,
        string email,
        string password,
        DateTime birthDate)
    {
        Name = fullName;
        Email = email;
        BirthDate = birthDate;
        Active = true;
        SetPassword(password);
        Validate();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now.AddSeconds(1);
        Skills = new List<UserSkill>();
        FreelanceProjects = new List<Project>();

    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    private string _password { get; set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public bool Active { get; private set; }
    public List<UserSkill> Skills { get; private set; }

    public List<Project> FreelanceProjects { get; private set; }

    public List<Project> OwnedProjects { get; private set; }

    public List<ProjectComment> Comments { get; private set; }

    public void Inactivate()
    {
        Active = false;
    }

    public void Activate()
    {
        Active = true;
        Validate();
    }

    public void Update(string name, string email, DateTime birthDate)
    {
        Name = name;
        Email = email;
        BirthDate = birthDate;
        Validate();
    }

    public void SetPassword(string password)
    {
        _password = HashPassword(password);
        Validate();
    }

    public bool VerifyPassword(string password)
    {
        var passwordHash = HashPassword(password);
        Validate();
        bool valid = passwordHash.Equals(_password);
        //if (!valid) throw new Exception("Invalid password");
        return valid;
    }

    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    public void UpdatePassword(string password)
    {
        VerifyPassword(password);
        _password = password;
        Validate();
    }
    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        DomainValidation.NotNullOrEmpty(Name, nameof(Email));
        DomainValidation.NotNullOrEmpty(Name, nameof(_password));
        DomainValidation.MinLength(_password, 8, nameof(_password));
        DomainValidation.MinLength(Name, 3, nameof(Name));
    }
}
