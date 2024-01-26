using System.Dynamic;

public interface ICard
{
	public Guid Id {get;}
	public CardTypes CardType {get;}
	public string Description {get;}
	
	//public void ExecuteCardEffect(GameController gamecontroller,IPlayer player);
}
class Card : ICard
{
	public Guid Id {get;}
	public CardTypes CardType {get;}
	public string Description {get;}
	public ChanceCardType Chance;
	public CommunityCardType Community;
	
	public Card(CardTypes cardType, ChanceCardType ChanceCardType, string description)
	{
		Id = Guid.NewGuid();;
		CardType = cardType;
		Description = description;
		Chance = ChanceCardType;
	}
	public Card(CardTypes cardType, CommunityCardType CommunityCard, string description)
	{
		Id = Guid.NewGuid();;
		CardType = cardType;
		Description = description;
		Community = CommunityCard;
	}
}
public enum CardTypes
{
	Chance,
	CommunityChest
}
public enum ChanceCardType
{
	ToGo,
	PayPoorTax,
	BankPaysYou
}
public enum CommunityCardType
{
	Inherit,
	WonPrize,
	SchoolFee
}