

namespace DevFreela.Application.InputModels;
public class NewUserInputModel
{
    public NewUserInputModel(
        string name,
        string email,
        string password,
        DateTime birthDate)
    {
        Name = name;
        Email = email;
        Password = password;
        BirthDate = birthDate;

    }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime BirthDate { get; set; }
}
