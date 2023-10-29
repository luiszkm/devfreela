using DevFreela.Domain.Domain.seddwork;

namespace DevFreela.Domain.Domain.Entities;
public class Skill : AggregateRoot
{
    public Skill(string description)
    {
        Description = description;
        CreatedAt = DateTime.Now;
    }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
}