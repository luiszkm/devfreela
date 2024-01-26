
using DevFreela.Application.UseCases.User.Common;
using MediatR;

namespace DevFreela.Application.UseCases.User.UpdateUser;

public class UpdateUserInput : IRequest, IRequest<UserModelOutput>
{
    public UpdateUserInput(
        Guid id,
        string? name = null,
        string? email = null,
        DateTime? birthDate = null,
        bool? active = null)
    {
        Id = id;
        Name = name;
        Email = email;
        BirthDate = birthDate;
        Active = active;
    }


    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime? BirthDate { get; set; }
    public bool? Active { get; set; }


}
