using System.Reflection.Metadata.Ecma335;

public interface IProperty
{
	public int Cost {get;set;}
	public int Rent {get;}
	public IPlayer Owner {get;set;}
	public bool IsMortgaged {get;}
	public int NumberBuilding {get; internal set;}
	public int CalculateRent()
	{
		return 0;
	}
}


