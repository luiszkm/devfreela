
using DevFreela.Application.UseCases.User.Common;
using DevFreela.Domain.Domain.Enums;
using MediatR;

namespace DevFreela.Application.UseCases.User.CreateUser;
public class CreateUserInput : IRequest<UserModelOutput>
{
    public CreateUserInput(
        string name,
        string email,
        DateTime birthDate,
        string password,
        UserRole role
       // List<DomainEntity.UserSkill> skills = null
       )
    {
        Name = name;
        Email = email;
        BirthDate = birthDate;
        Password = password;
        //  Skills = null ?? skills;
        Role = role;
    }


    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }

    //public List<DomainEntity.UserSkill>? Skills { get; set; }

}
