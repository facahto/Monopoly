using System.Linq.Expressions;
using System.Text;
using Spectre.Console;
class Program
{
	static void Main()
	{
		Random random = new Random();
		Dictionary<CardTypes,List<ICard>> cardDict = new Dictionary<CardTypes,List<ICard>>();
		GameController gm = new GameController(4,new List<ILocationTiles>(),cardDict,new List<IDice>{new Dice(6),new Dice(6)},1200.00m);
		#region 
		gm.AddLocationTile(new Special(0, LocationTypes.Corner, "GO", "First Start"));
		gm.AddLocationTile(new City(1,60,2, LocationTypes.City, "Mediterranean Avenue", "street",null,0));
		gm.AddLocationTile(new CardTile(2, LocationTypes.CommunityChestCard, "Community", "Take Card"));
		gm.AddLocationTile(new City(3,60,4, LocationTypes.City, "Baltic Avenue", "street",null,0));
		gm.AddLocationTile(new Special(4, LocationTypes.IncomeTax, "Tax", "Get Tax"));
		gm.AddLocationTile(new RailRoad(5,200,0, LocationTypes.RailRoad, "Reading RailRoad", "RailRoad",null));
		gm.AddLocationTile(new City(6,100,6, LocationTypes.City, "Oriental Avenue", "street",null,0));
		gm.AddLocationTile(new CardTile(7, LocationTypes.ChanceCard, "Chance", "Take Card"));
		gm.AddLocationTile(new City(8,100,6, LocationTypes.City, "Vermont Avenue", "street",null,0));
		gm.AddLocationTile(new City(9,120,10, LocationTypes.City, "Connecticut Avenue", "street",null,0));
		gm.AddLocationTile(new Special(10, LocationTypes.Corner, "Get In Jail", "Get in Jail"));
		gm.AddLocationTile(new City(11,140,10, LocationTypes.City, "St. Charles Place", "street",null,0));
		gm.AddLocationTile(new Utility(12,150,0, LocationTypes.Utility, "Electric Company", "PLN",null));
		gm.AddLocationTile(new City(13,140,10, LocationTypes.City, "States Avenue", "street",null,0));
		gm.AddLocationTile(new City(14,160,12, LocationTypes.City, "Virginia Avenue", "PDAM",null,0));
		gm.AddLocationTile(new RailRoad(15,200,0, LocationTypes.RailRoad, "Pennsylvania Railroad", "RailRoad",null));
		gm.AddLocationTile(new City(16,180,14, LocationTypes.City, "St. James Place", "street",null,0));
		gm.AddLocationTile(new CardTile(17, LocationTypes.CommunityChestCard, "Community", "Take card"));
		gm.AddLocationTile(new City(18,180,14, LocationTypes.City, "Tennessee Avenue", "street",null,0));
		gm.AddLocationTile(new City(19,200,16, LocationTypes.City, "New York Avenue", "street",null,0));
		gm.AddLocationTile(new Special(20, LocationTypes.Corner, "FREE PARKING", "wiiih"));
		gm.AddLocationTile(new City(21,220,18, LocationTypes.City, "Kentucky Avenue", "street",null,0));
		gm.AddLocationTile(new CardTile(22, LocationTypes.ChanceCard, "Chance", "Take Card"));
		gm.AddLocationTile(new City(23,220,18, LocationTypes.City, "Indiana Avenue", "street",null,0));
		gm.AddLocationTile(new City(24,240,20, LocationTypes.City, "Illinois Avenue", "street",null,0));
		gm.AddLocationTile(new RailRoad(25,200,0, LocationTypes.RailRoad, "B. & O. Railroad", "RailRoad",null));
		gm.AddLocationTile(new City(26,260,20, LocationTypes.City, "Atlantic Avenue", "street",null,0));
		gm.AddLocationTile(new City(27,260,22, LocationTypes.City, "Ventnor Avenue", "street",null,0));
		gm.AddLocationTile(new Utility(28,150,0, LocationTypes.Utility, "Water Works", "PDAM",null));
		gm.AddLocationTile(new City(29,280,22, LocationTypes.City, "Ventnor Avenue", "street",null,0));
		gm.AddLocationTile(new Special(30, LocationTypes.Corner, "GO TO JAIL", "Go to Jail"));
		gm.AddLocationTile(new City(31,280,26, LocationTypes.City, "Pacific Avenue", "street",null,0));
		gm.AddLocationTile(new City(32,300,28, LocationTypes.City, "North Carolina Avenue", "street",null,0));
		gm.AddLocationTile(new CardTile(33, LocationTypes.CommunityChestCard, "Community", "Take Card"));
		gm.AddLocationTile(new City(34,320,0, LocationTypes.City, "Pennsylvania Avenue", "street",null,0));
		gm.AddLocationTile(new RailRoad(35,200,0, LocationTypes.RailRoad, "Short Line", "RailRoad",null));
		gm.AddLocationTile(new CardTile(36, LocationTypes.ChanceCard, "Chance", "Take Card"));
		gm.AddLocationTile(new City(37,350,35, LocationTypes.City, "Park Place", "street",null,0));
		gm.AddLocationTile(new Special(38, LocationTypes.LuxuryTax, "Tax", "Get Tax"));
		gm.AddLocationTile(new City(39,400,50, LocationTypes.City, "Boardwalk", "street",null,0));
		#endregion
		#region 
		List<ICard> chanceCard = new List<ICard>();
		List<ICard> communityCard = new List<ICard>();
		gm.AddCard(chanceCard,new Card(CardTypes.Chance,ChanceCardType.BankPaysYou,"BankPaysYou"));
		gm.AddCard(chanceCard,new Card(CardTypes.Chance,ChanceCardType.PayPoorTax,"PayPoorTax"));
		gm.AddCard(chanceCard,new Card(CardTypes.Chance,ChanceCardType.ToGo,"ToGo"));
		gm.AddCard(communityCard,new Card(CardTypes.CommunityChest,CommunityCardType.Inherit,"Inherit"));
		gm.AddCard(communityCard,new Card(CardTypes.CommunityChest,CommunityCardType.WonPrize,"WonPrize"));
		gm.AddCard(communityCard,new Card(CardTypes.CommunityChest,CommunityCardType.SchoolFee,"SchoolFee"));
		cardDict.Add(CardTypes.Chance,chanceCard);
		cardDict.Add(CardTypes.CommunityChest,communityCard);
		#endregion
		List<TokenTypes> tokenList = new List<TokenTypes>
							{
								TokenTypes.Battleship,TokenTypes.Location,TokenTypes.TopHat, TokenTypes.Cat, 
									TokenTypes.Penguin, TokenTypes.RubberDucky,TokenTypes.Thimble
							};
		AnsiConsole.Write(new FigletText("Monopoly").LeftJustified().Color(Color.Red));
		gm.StartGame();
		while(gm.currentGameStatus != GameStatus.End)
		{	
			Console.Clear();
			var font = FigletFont.Load("larry3d.flf");
			AnsiConsole.Write(
				new FigletText(font, "Monopoly")
					.Centered()
					.Color(Color.Red));
			var select_MainMenu = AnsiConsole.Prompt(
			new SelectionPrompt<string>()
				.PageSize(20)
				.MoreChoicesText("[grey](Move up and down to reveal)[/]")
				.AddChoices(new[] {
					"Play",
					"Exit"
				}));
			if (select_MainMenu == "Exit")
			{
				break;
			}
			else
			{
				while(true)
				{
					var select_PlayerMenu = AnsiConsole.Prompt(
					new SelectionPrompt<string>()
						.PageSize(20)
						.MoreChoicesText("[grey](Move up and down to reveal)[/]")
						.AddChoices(new[] {
							"Add New Player","Remove","Next","Back"
						}));
					if (select_PlayerMenu == "Back")
					{
						break;
					}
					else if(select_PlayerMenu =="Add New Player")
					{
						Console.Clear();
						string newPlayerName = AnsiConsole.Ask<string>("What's your [green]name[/]?");
						if (gm.playerDataDict.Keys.Any(player => player.Name == newPlayerName))
						{
							Console.Clear();
							Console.WriteLine($"{newPlayerName} has already been added to the game, please enter a new name");
						}
						else if(gm.playerDataDict.Count>gm.maxplayer-1)
						{
							Console.Clear();
							Console.WriteLine("Reached number of maximum player");
						}
						else
						{
							IPlayer newPlayer = new Player(newPlayerName);
							var select_Token = AnsiConsole.Prompt(
							new SelectionPrompt<TokenTypes>()
								.Title("Pick your token?")
								.PageSize(10)
								.MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
								.AddChoices(tokenList));
							gm.AddPlayer(newPlayer,select_Token);
							tokenList.Remove(select_Token);
							Console.Clear();
							Console.WriteLine($"Player {newPlayerName} been added");
						}	
					}
					else if(select_PlayerMenu =="Remove")
					{
						Console.Clear();
						if(gm.playerDataDict.Count==0)
						{
							Console.WriteLine("There is no player in the game");
						}
						else{
						Console.WriteLine("Select player you want to delete");
						List<string> playerList = new List<string>(gm.playerDataDict.Keys.Select(player => player.Name));
						var select_Removeplayer = AnsiConsole.Prompt(
						new SelectionPrompt<string>()
							.PageSize(20)
							.MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
							.AddChoices(playerList));
						IPlayer removePlayer = gm.playerDataDict.Keys.FirstOrDefault(player => player.Name == select_Removeplayer);
						tokenList.Add(gm.GetPlayerToken(removePlayer));
						gm.RemovePlayer(removePlayer);
						Console.WriteLine($"Player has been removed.");}
					}
					else
					{
						IEnumerable<IPlayer> orderList = gm.SortPlayingOrder();
						if (orderList.Count()>1){
							while(true)
							{
								int round = 0;
								foreach(var i in orderList)
								{	
									Console.Clear();
									int step = 4;
									//int step = gm.RollDice();
									gm.Move(i,step);
									Console.WriteLine($"Player: {i.Name} | Dice shows {step} | Arrived in {gm.GetPlayerLocation(i).Name} | Position {gm.playerDataDict[i].Position} | Current Balance: {gm.playerDataDict[i].money}");
									if (gm.GetLocationType(i) == LocationTypes.City)
									{
										if (gm.GetPlayerLocation(i).Owner == null)
										{
											var select_PlayerMenuCity = AnsiConsole.Prompt(
											new SelectionPrompt<string>()
												.PageSize(20)
												.MoreChoicesText("[grey](Move up and down to reveal)[/]")
												.AddChoices(new[] {
													"Buy Property","Keep Playing","Retire"
												}));
											if(select_PlayerMenuCity == "Buy Property")
											{
												gm.BuyProperty(i);
											}
											else if(select_PlayerMenuCity == "Retire")
											{
												Console.Clear();
												round = 1000;
												break;
											}
											else
											{
												
											}
										}
										else
										{
											if (gm.GetPlayerLocation(i).Owner != i){
												Console.WriteLine("PayRent");
												gm.PayRent(i);
												var select_PlayerRent = AnsiConsole.Prompt(
												new SelectionPrompt<string>()
													.PageSize(20)
													.MoreChoicesText("[grey](Move up and down to reveal)[/]")
													.AddChoices(new[] {
														"Keep Playing","Retire"
													}));
												if(select_PlayerRent == "Retire")
													{
														round = 1000;
														Console.Clear();
														break;												
													}
												else
												{
													
												}
											}
											else
											{
												var select_PlayerMenuCity2 = AnsiConsole.Prompt(
												new SelectionPrompt<string>()
													.PageSize(20)
													.MoreChoicesText("[grey](Move up and down to reveal)[/]")
													.AddChoices(new[] {
														"Buy Building","Keep Playing"
													}));
												if (select_PlayerMenuCity2 == "Buy Building")
												{
													gm.BuyHouse(i);
												}
												else
												{
													
												}
											}
										}
									}
									else if(gm.GetLocationType(i) == LocationTypes.IncomeTax)
									{
										var select_PlayerOnTaxIncome = AnsiConsole.Prompt(
										new SelectionPrompt<string>()
											.PageSize(20)
											.MoreChoicesText("[grey](Move up and down to reveal)[/]")
											.AddChoices(new[] {
												"Pay 200", "Pay 10%"
											}));
										if(select_PlayerOnTaxIncome=="Pay 200")
										{
											gm.SendMoney(i,select_PlayerOnTaxIncome);
										}
										else
										{
											gm.SendMoney(i,select_PlayerOnTaxIncome);
										}
									}
									else if(gm.GetLocationType(i) == LocationTypes.LuxuryTax)
									{
										gm.SendMoney(i,75);
									}
									else if(gm.GetLocationType(i) == LocationTypes.RailRoad)
									{
										if (gm.GetPlayerLocation(i).Owner == null)
										{
											var select_PlayerMenuCity = AnsiConsole.Prompt(
											new SelectionPrompt<string>()
												.PageSize(20)
												.MoreChoicesText("[grey](Move up and down to reveal)[/]")
												.AddChoices(new[] {
													"Buy Property","Keep Playing"
												}));
											if(select_PlayerMenuCity == "Buy Property")
											{
												gm.BuyProperty(i);
											}
											else
											{
												
											}
										}
										else
										{
											Console.WriteLine("PayRent");
											gm.PayRent(i);
											var select_PlayerRent = AnsiConsole.Prompt(
											new SelectionPrompt<string>()
												.PageSize(20)
												.MoreChoicesText("[grey](Move up and down to reveal)[/]")
												.AddChoices(new[] {
													"Keep Playing","Retire"
												}));
											if(select_PlayerRent == "Retire")
												{
													round = 1000;
													Console.Clear();
													break;												
												}
											else
											{
												
											}
										}
									}
									else if(gm.GetLocationType(i) == LocationTypes.Utility)
									{
										if (gm.GetPlayerLocation(i).Owner == null)
										{
											var select_PlayerMenuUtil = AnsiConsole.Prompt(
											new SelectionPrompt<string>()
												.PageSize(20)
												.MoreChoicesText("[grey](Move up and down to reveal)[/]")
												.AddChoices(new[] {
													"Buy Property","Keep Playing"
												}));
											if(select_PlayerMenuUtil == "Buy Property")
											{
												gm.BuyProperty(i);
											}
											else
											{
												
											}
										}
										else
										{
											Console.WriteLine("PayRent");
											gm.PayRent(i);
											var select_PlayerRent = AnsiConsole.Prompt(
											new SelectionPrompt<string>()
												.PageSize(20)
												.MoreChoicesText("[grey](Move up and down to reveal)[/]")
												.AddChoices(new[] {
													"Keep Playing","Retire"
												}));
											if(select_PlayerRent == "Retire")
												{
													round = 1000;
													Console.Clear();
													break;												
												}
											else
											{
												
											}
										}
									}				
									else if(gm.GetLocationType(i) == LocationTypes.Corner)
									{
										if(gm.GetPlayerLocation(i).Name == "GO")
										{ 
											gm.ReceiveMoney(i,200);
										}
										else if(gm.GetPlayerLocation(i).Name == "GO TO JAIL")
										{
											gm.MoveTo(i,10);
											gm.SendMoney(i,50);
											Console.WriteLine("Player in Jail");
											Thread.Sleep(2000);
										}
										else
										{
											Console.WriteLine("Choose position (number): ");
											string s2 = Console.ReadLine();
											int choosenstep = Convert.ToInt32(s2);
											gm.MoveTo(i,choosenstep);
										}
									}
									else if(gm.GetLocationType(i) == LocationTypes.ChanceCard)
									{	
										string a=cardDict[CardTypes.Chance][random.Next(3)].Description;
										gm.ExecutionCardChance(a,i);
										var select_PlayerOnPlay = AnsiConsole.Prompt(
										new SelectionPrompt<string>()
											.PageSize(20)
											.MoreChoicesText("[grey](Move up and down to reveal)[/]")
											.AddChoices(new[] {
												"Keep Playing","Retire"
											}));
										if(select_PlayerOnPlay == "Keep Playing")
										{
											Console.WriteLine("Lanjut");
										}
										else
										{
											Console.Clear();
											round = 1000;
											break;
										}
									}
									else
									{
										string b=cardDict[CardTypes.Chance][random.Next(3)].Description;
										gm.ExecutionCardCommunity(b,i);
										var select_PlayerOnPlay = AnsiConsole.Prompt(
										new SelectionPrompt<string>()
											.PageSize(20)
											.MoreChoicesText("[grey](Move up and down to reveal)[/]")
											.AddChoices(new[] {
												"Keep Playing","Retire"
											}));
										if(select_PlayerOnPlay == "Keep Playing")
										{
											Console.WriteLine("Lanjut");
										}
										else
										{
											Console.Clear();
											round = 1000;
											break;
										}
									}	
								}
								if (round == 1000)
								{
									Console.Clear();
									Console.WriteLine($"Winner is {gm.GetWinner().Name}");
									Thread.Sleep(2000);
									break;
								}
							}
						}
						
					}				
				}
			}
		}
		Console.WriteLine("GAME ENDED. Press any key...");
		Console.ReadLine();			
		Console.Clear();
		gm.EndGame();
	}
}