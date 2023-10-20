namespace DevFreela.Domain.Domain.seddwork;
public class Entity
{
    public Guid Id { get; private set; }

    protected Entity() => Id = Guid.NewGuid();
}
