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
	public override bool Equals(object? obj)
	{
		if (obj == null || GetType() != obj.GetType())
		{
			return false;
		}

		Player otherPlayer = (Player)obj;
		return Id.Equals(otherPlayer.Id) && Name.Equals(otherPlayer.Name);
	}
	public override int GetHashCode()
    {
        return Id.GetHashCode() ^ Name.GetHashCode();
    }
}