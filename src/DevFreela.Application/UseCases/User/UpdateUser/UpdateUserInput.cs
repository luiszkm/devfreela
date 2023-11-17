
using MediatR;

namespace DevFreela.Application.UseCases.User.UpdateUser;

public class UpdateUserInput : IRequest
{
    public UpdateUserInput(
        Guid id,
        string name,
        string email,
        DateTime birthDate,
        bool active)
    {
        Id = id;
        Name = name;
        Email = email;
        BirthDate = birthDate;
        Active = active;
    }


    public Guid Id { get; private set; }
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public bool? Active { get; private set; }


}
