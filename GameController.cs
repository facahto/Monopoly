using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using Spectre.Console;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Formats.Asn1;
using System.ComponentModel;
using System.Diagnostics;
public class GameController 
{
	public int maxplayer {get;}
	public decimal currentbalance;
	public GameStatus currentGameStatus = GameStatus.NotInitialized;
	public Dictionary<IPlayer, PlayerData> playerDataDict { get; } = new Dictionary<IPlayer, PlayerData>();
	private List<IDice> dice;
	public List<ILocationTiles> listLocation;
	public IEnumerable<IPlayer> listOrderPlayer = new List<IPlayer> ();
	public Dictionary<CardTypes,List<ICard>> listCard;
	public IPlayer winner;
	
	public GameController(int maxplayer,List<ILocationTiles> listLocation,Dictionary<CardTypes,List<ICard>> listCard, List<IDice> dice,decimal balance)
	{
		this.maxplayer = maxplayer;
		this.listLocation = listLocation;
		this.listCard = listCard;
		this.dice = dice;
		currentbalance = balance;
	}
	/// <summary>
	/// Starts the game and sets the current game status to Initialized.
	/// </summary>
	/// <returns>True if the game is successfully started; otherwise, false.</returns>
	public bool StartGame()
	{
		currentGameStatus = GameStatus.Initialized;
		return true;
	}
	/// <summary>
	/// Ends the game and sets the current game status to End.
	/// </summary>
	/// <returns>True if the game is successfully ended; otherwise, false.</returns>
	public  bool EndGame()
	{
		currentGameStatus = GameStatus.End;
		return true;
	}
	/// <summary>
	/// Adds a new player to the game with the specified player interface and token type.
	/// </summary>
	/// <param name="newPlayer">The new player to be added.</param>
	/// <param name="tokenplayer">The token type associated with the new player.</param>
	/// <returns>True if the player is successfully added; otherwise, false.</returns>
	public bool AddPlayer(IPlayer newPlayer,TokenTypes tokenplayer)
	{
		if (playerDataDict.ContainsKey(newPlayer))
		{
			return false;
		}
		else
		{
				
		PlayerData playerData = new PlayerData(tokenplayer,currentbalance);
		playerDataDict.Add(newPlayer, playerData);
		return true;}
	}
	/// <summary>
	/// Removes a player from the game.
	/// </summary>
	/// <param name="removeplayer">The player to be removed.</param>
	/// <returns>True if the player is successfully removed; otherwise, false.</returns>
	public bool RemovePlayer(IPlayer removeplayer)
	{
		if (playerDataDict.Keys.Contains(removeplayer))
		{
			playerDataDict.Remove(removeplayer);	
			return true;	
		}
		return false;
	}
	/// <summary>
	/// Gets the token type associated with a specific player.
	/// </summary>
	/// <param name="player">The player to get the token type for.</param>
	/// <returns>The token type associated with the player.</returns>
	/// <exception cref="System.NullReferenceException">Thrown if the player's data is not found.</exception>
	public TokenTypes GetPlayerToken(IPlayer player)
	{
		//throw exception kalo token ga ada
		playerDataDict.TryGetValue(player, out PlayerData playerdata);
		TokenTypes token = playerdata!.token;
		return token;
	}
	/// <summary>
	/// Gets a collection of all players currently in the game.
	/// </summary>
	/// <returns>An IEnumerable collection of players.</returns>
	public IEnumerable<IPlayer> GetPlayers()
	{
		return playerDataDict.Keys.ToList(); //harusnya IEnumerable
	}
	/// <summary>
	/// Sorts the players in the order of their turns based on the result of rolling the dice.
	/// </summary>
	/// <returns>An IEnumerable collection of players in the sorted playing order.</returns>
	public IEnumerable<IPlayer> SortPlayingOrder()
	{
		Dictionary<IPlayer, int> myDictionary = new Dictionary<IPlayer, int>();
		foreach (var player in playerDataDict.Keys)
		{
			myDictionary.Add(player, RollDice());
		}
		var sortedDictionary = myDictionary.OrderBy(kvp => kvp.Value).Reverse();
		foreach (var kvp in sortedDictionary)
		{
			listOrderPlayer =listOrderPlayer.Append(kvp.Key);
		}
		return listOrderPlayer;
	}
	/// <summary>
	/// Moves a player on the game board by a specified number of steps.
	/// </summary>
	/// <param name="player">The player to move.</param>
	/// <param name="step">The number of steps to move the player.</param>
	/// <returns>True if the player is successfully moved; otherwise, false.</returns>
	public bool Move(IPlayer player, int step)
	{
		//40 ganti dengan board.count
		playerDataDict[player].Position = (playerDataDict[player].Position + step)%listLocation.Count;
		playerDataDict[player].currentTile = GetPlayerLocation(player);
		return true;
	}
	/// <summary>
	/// Moves a player to a specific location on the game board.
	/// </summary>
	/// <param name="player">The player to move.</param>
	/// <param name="step">The position to move the player to.</param>
	/// <returns>True if the player is successfully moved; otherwise, false.</returns>
	public bool MoveTo(IPlayer player,int step)
	{
		playerDataDict[player].currentTile = listLocation[step];
		playerDataDict[player].Position = step;
		return true;
	}
	/// <summary>
	/// Gets the current location of a player on the game board.
	/// </summary>
	/// <param name="player">The player to get the location for.</param>
	/// <returns>The location tile where the player is currently positioned.</returns>
	public ILocationTiles GetPlayerLocation(IPlayer player)
	{
		return listLocation[playerDataDict[player].Position];
	} 
	/// <summary>
	/// Gets the collection of dice used in the game.
	/// </summary>
	/// <returns>An IEnumerable collection of dice.</returns>
	public IEnumerable<IDice> GetDice()
	{
		return dice;
	}
	/// <summary>
	/// Rolls the dice and returns the total sum of the rolled values.
	/// </summary>
	/// <returns>The total sum of the rolled values.</returns>
	public int RollDice()
	{
		int total=0;
		foreach(IDice dice in GetDice())
		{
			int rollResult = dice.Roll();
			total += rollResult;
		}
		return total;
	}
	/// <summary>
	/// Adds a new location tile to the game board.
	/// </summary>
	/// <param name="newLocationTile">The location tile to be added.</param>
	/// <returns>True if the location tile is successfully added; otherwise, false.</returns>
	public bool AddLocationTile(ILocationTiles newLocationTile)
	{
		listLocation.Add(newLocationTile);
		return true;
	}
	/// <summary>
	/// Gets the current game board, represented as a list of location tiles.
	/// </summary>
	/// <returns>A list of location tiles on the game board.</returns>
	public List<ILocationTiles> GetBoard() 
	{
		return listLocation;	
	}
	/// <summary>
	/// Removes a location tile from the game board.
	/// </summary>
	/// <param name="locationTile">The location tile to be removed.</param>
	/// <returns>True if the location tile is successfully removed; otherwise, false.</returns>
	public bool RemoveLocationTile(ILocationTiles locationTile)
	{
		listLocation.Remove(locationTile);
		return true;
	}
	/// <summary>
	/// Gets the description of a location tile at the specified position on the game board.
	/// </summary>
	/// <param name="position">The position of the location tile on the game board.</param>
	/// <returns>The description of the location tile.</returns>
	public string GetLocationInfo(int position) 
	{
		return listLocation[position].Description;
	}
	/// <summary>
	/// Gets the description of the location tile where a specific player is currently positioned.
	/// </summary>
	/// <param name="player">The player for whom to get the location information.</param>
	/// <returns>The description of the location tile where the player is currently positioned.</returns>
	public string GetLocationInfo(IPlayer player) 
	{
		return listLocation[playerDataDict[player].Position].Description;
	}
	/// <summary>
	/// Gets the type of a location tile where a specific player is currently positioned.
	/// </summary>
	/// <param name="player">The player for whom to get the location type.</param>
	/// <returns>The type of the location tile where the player is currently positioned.</returns>
	public LocationTypes GetLocationType(IPlayer player)
	{
		return GetPlayerLocation(player).LocationType;
	}
	/// <summary>
	/// Placeholder method to get player property information.
	/// </summary>
	/// <param name="player">The player for whom to get property information.</param>
	/// <returns>Always returns true; implementation pending.</returns>
	public bool GetPlayerProperty(IPlayer player)
	{
		return true;
	}
	/// <summary>
	/// Buys a property for the specified player.
	/// </summary>
	/// <param name="buyer">The player buying the property.</param>
	/// <returns>True if the property is successfully bought; otherwise, false.</returns>
	public bool BuyProperty(IPlayer buyer)
	{
		if(GetPlayerLocation(buyer).LocationType == LocationTypes.City)
			{
			City cityCost = GetPlayerLocation(buyer) as City;
			playerDataDict[buyer].money -= cityCost.Cost;
			GetPlayerLocation(buyer).Owner = buyer;
			playerDataDict[buyer].listProperty = playerDataDict[buyer].listProperty.Append(GetPlayerLocation(buyer));
			}
		else if (GetPlayerLocation(buyer).LocationType == LocationTypes.RailRoad)
		{
			RailRoad railCost = GetPlayerLocation(buyer) as RailRoad;
			playerDataDict[buyer].money -= railCost.cost;
			GetPlayerLocation(buyer).Owner = buyer;
			playerDataDict[buyer].listProperty = playerDataDict[buyer].listProperty.Append(GetPlayerLocation(buyer));
		}
		else if (GetPlayerLocation(buyer).LocationType == LocationTypes.Utility)
		{
			Utility utilCost = GetPlayerLocation(buyer) as Utility;
			playerDataDict[buyer].money -= utilCost.cost;
			GetPlayerLocation(buyer).Owner = buyer;
			playerDataDict[buyer].listProperty = playerDataDict[buyer].listProperty.Append(GetPlayerLocation(buyer));
		}
		return true;
	}
	/// <summary>
	/// Buys a house for the specified player on a city property.
	/// </summary>
	/// <param name="buyer">The player buying the house.</param>
	/// <returns>True if the house is successfully bought; otherwise, false.</returns>
	public bool BuyHouse(IPlayer buyer)
	{
		City cityCost = GetPlayerLocation(buyer) as City;
		SendMoney(buyer,100);
		cityCost.NumberBuilding +=1;
		return true;
	}
	/// <summary>
	/// Pays the rent for the current player based on the type of location they are on.
	/// </summary>
	/// <param name="player">The player paying the rent.</param>
	/// <returns>True if the rent is successfully paid; otherwise, false.</returns>
	public bool PayRent(IPlayer player)
	{	
		if(GetPlayerLocation(player).LocationType == LocationTypes.City)
		{
			City cityCost = GetPlayerLocation(player) as City;
			if(cityCost.NumberBuilding == 0){
				playerDataDict[player].money -= cityCost.Rent+10;
			}
			else if(cityCost.NumberBuilding == 1)
			{
				playerDataDict[player].money -= cityCost.Rent+15;
			}
			else if(cityCost.NumberBuilding == 2)
			{
				playerDataDict[player].money -= cityCost.Rent+20;
			}
			else if(cityCost.NumberBuilding == 3)
			{
				playerDataDict[player].money -= cityCost.Rent+30;
			}
			else
			{
				return true;
			}
		}
		else if (GetPlayerLocation(player).LocationType == LocationTypes.RailRoad)
		{
			RailRoad railCost = GetPlayerLocation(player) as RailRoad;
			railCost.rent = railCost.cost*5/100;
			playerDataDict[player].money -= railCost.rent;
		}
		else if (GetPlayerLocation(player).LocationType == LocationTypes.Utility)
		{
			Utility utilCost = GetPlayerLocation(player) as Utility;
			utilCost.rent = utilCost.cost*5/100;
			playerDataDict[player].money -= utilCost.rent;
		}
		return true;
	}
	/// <summary>
	/// Sends a specified amount of money from one player to another.
	/// </summary>
	/// <param name="player">The player sending the money.</param>
	/// <param name="amount">The amount of money to be sent.</param>
	/// <returns>True if the money is successfully sent; otherwise, false.</returns>
	public bool SendMoney(IPlayer player,decimal amount)
	{
		playerDataDict[player].money -= amount;
		return true;
	}
	/// <summary>
	/// Sends money based on the specified choice.
	/// </summary>
	/// <param name="player">The player involved in the transaction.</param>
	/// <param name="choice">The choice determining the amount to be sent.</param>
	/// <returns>True if the money is successfully sent; otherwise, false.</returns>
	public bool SendMoney(IPlayer player, string choice)
	{
		if (choice == "Pay 200")
		{
			SendMoney(player,200);
		}
		else{
			decimal tax;
			tax = playerDataDict[player].money*10/100;
			SendMoney(player,tax);
		}
		return true;
	}
	/// <summary>
	/// Receives a specified amount of money by the player.
	/// </summary>
	/// <param name="player">The player receiving the money.</param>
	/// <param name="amount">The amount of money to be received.</param>
	/// <returns>True if the money is successfully received; otherwise, false.</returns>
	public bool ReceiveMoney(IPlayer player,int amount)
	{
		playerDataDict[player].money +=amount;
		return true;
	}
	/// <summary>
	/// Adds a new card to the specified list of cards.
	/// </summary>
	/// <param name="newlist">The list of cards to which the new card is added.</param>
	/// <param name="newCard">The new card to be added.</param>
	/// <returns>True if the card is successfully added; otherwise, false.</returns>
	public bool AddCard(List<ICard> newlist,ICard newCard)
	{
		newlist.Add(newCard);
		return true;
	}
	/// <summary>
	/// Executes the effect of a Chance card for the specified player.
	/// </summary>
	/// <param name="desc">The description of the Chance card.</param>
	/// <param name="player">The player on whom the card's effect is executed.</param>
	/// <returns>True if the card is successfully executed; otherwise, false.</returns>
	public bool ExecutionCardChance(string desc,IPlayer player)
	{
		if(listCard[CardTypes.Chance].FirstOrDefault(card => card.Description == desc).Description == "BankPaysYou")
		{
			ReceiveMoney(player,100);
		}
		else if (listCard[CardTypes.Chance].FirstOrDefault(card => card.Description == desc).Description == "PayPoorTax")
		{
			SendMoney(player,50);
		}
		else
		{
			playerDataDict[player].currentTile=listLocation.FirstOrDefault(loc => loc.Position == 0);
		}
		return true;
	}
	/// <summary>
	/// Executes the effect of a Community Chest card for the specified player.
	/// </summary>
	/// <param name="desc">The description of the Community Chest card.</param>
	/// <param name="player">The player on whom the card's effect is executed.</param>
	/// <returns>True if the card is successfully executed; otherwise, false.</returns>
	public bool ExecutionCardCommunity(string desc,IPlayer player)
	{
		if(desc == "Inherit")
		{
			ReceiveMoney(player,300);
		}
		else if (desc == "WonPrize")
		{
			ReceiveMoney(player,50);
		}
		else
		{
			SendMoney(player,150);
		}
		return true;
	}
	/// <summary>
	/// Gets the player with the highest money as the winner of the game.
	/// </summary>
	/// <returns>The player with the highest money.</returns>
	public IPlayer GetWinner()
	{
		var playerWithMaxMoney = playerDataDict.OrderByDescending(kv => kv.Value.money).FirstOrDefault();
		IPlayer player = playerWithMaxMoney.Key;
		return player;
	}
}

public enum GameStatus
{
	NotInitialized,
	Initialized,
	End
}