class PlayerData
{
	public int money;
	//public TokenTypes Token {get;}
	public PlayerData(int money)
	{
		this.money = money;
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