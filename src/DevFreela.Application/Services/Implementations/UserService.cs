using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Domain.Domain.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations;
internal class UserService : IUserService
{
    private readonly DevFreelaDbContext _dbContext;

    public UserService(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Guid Create(NewUserInputModel inputModel)
    {
        var user = new User(
            inputModel.Name,
            inputModel.Email,
            inputModel.Password,
            inputModel.BirthDate);

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return user.Id;
    }

    public void Update(NewUserInputModel inputModel)
    {
        throw new NotImplementedException();
    }

    public UserViewModel GetById(Guid id)
    {
        var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);
        if (user == null)
            return null;

        return new UserViewModel(user.Name, user.Email);

    }


}
