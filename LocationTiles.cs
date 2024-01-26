public interface ILocationTiles
{
	public Guid Id {get;}
	public int Position {get;}
	public LocationTypes LocationType {get;}
	public string Name {get;}
	public string Description {get;}
	public IPlayer Owner {get;set;}
}
class Utility : ILocationTiles
	{		
	public Guid Id {get;}
	public int Position { get; }
	public LocationTypes LocationType { get; }
	public string Name { get; }
	public string Description { get; }
	public int cost;

	public decimal rent;
	public IPlayer Owner {get;set;}
	public bool IsMortgaged {get;}

	public Utility(int position,int cost, int rent, LocationTypes type, string name, string description,IPlayer player)
	{
		Position = position;
		LocationType = type;
		Name = name;
		Description = description;
	}
}	
class RailRoad : ILocationTiles
	{
		public Guid Id {get;}
		public int Position { get; }
		public LocationTypes LocationType { get; }
		public string Name { get; }
		public string Description { get; }
		public IPlayer Owner { get;set; }
		public int cost;
		public decimal rent;
		public RailRoad(int position,int cost, int rent,  LocationTypes type, string name, string description,IPlayer player)
		{
			Position = position;
			Name = name;
			Description = description;
			LocationType = type;
			Owner = player;
			this.cost = cost;
		}
	}
class City : ILocationTiles,IProperty
	{
	public Guid Id {get;}
	public int Position { get; }
	public LocationTypes LocationType { get; }
	public string Name { get; }
	public string Description { get; }
	public int NumberBuilding { get; set;}
	public int Cost {get;set;}
	public int Rent {get;}
	public IPlayer Owner {get;set;}
	public bool IsMortgaged {get;set;}

	public City(int position,int cost, int rent, LocationTypes type, string name, string description,IPlayer player,int numberBuilding)
	{
		Position = position;
		Name = name;
		Description =description;
		Cost = cost;
		Rent = rent;
		Owner = player;
		NumberBuilding = numberBuilding;
	}
	
}
class Special : ILocationTiles
	{
	public Guid Id {get;}
	public int Position { get; }
	public LocationTypes LocationType { get; }
	public string Name { get; }
	public string Description { get; }
	public IPlayer Owner { get; set;}
	
	public Special(int position, LocationTypes type, string name, string description)
	{
		Position = position;
		Name = name;
		Description = description;
		LocationType = type;
	}
	}
class CardTile : ILocationTiles
	{
	public Guid Id {get;}
	public int Position { get; }
	public LocationTypes LocationType { get; }
	public string Name { get; }
	public string Description { get; }
	public IPlayer Owner { get; set;}
	public CardTile(int position, LocationTypes type, string name, string description)
	{
		Position = position;
		Name = name;
		Description = description;
		LocationType = type;
	}
	}
public enum LocationTypes
{
	City,
	RailRoad,
	Utility,
	ChanceCard,
	CommunityChestCard,
	Corner,
	IncomeTax,
	LuxuryTax
}