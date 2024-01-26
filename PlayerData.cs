public class PlayerData : IPlayer
{
	public decimal money;
	public TokenTypes token {get;}
	public ILocationTiles currentTile;
	public Guid Id {get;}
	public int Position {get; set;}
	public LocationTypes LocationType {get;set;}
	public string Name {get;}
	public string Description {get;}
	public IEnumerable<ILocationTiles> listProperty { get; set; } = new List<ILocationTiles>();
	public PlayerData(TokenTypes token, decimal money)
	{
		this.money = money;
		this.token = token;
	}
	
}
public enum TokenTypes
{
	Battleship,
	Location,
	TopHat,
	Cat,
	Penguin,
	RubberDucky,
	Thimble
}