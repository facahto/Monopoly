public interface IPlayer
{
	Guid Id { get; }
	string Name { get; }
}
public class Player : IPlayer
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; }

    public Player(string name)
    {
        Name = name;
    }
}