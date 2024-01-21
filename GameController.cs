using System.Data.SqlTypes;
using Spectre.Console;
class GameController 
{
	public int maxplayer {get;}
	GameStatus currentGameStatus = GameStatus.NotInitialized;
	public Dictionary<IPlayer, PlayerData> playerDataDict { get; } = new Dictionary<IPlayer, PlayerData>();
	
	public GameController(int maxplayer)
	{
		this.maxplayer = maxplayer;
	}
	
	//PLAYER
	public bool StartGame()
	{
		var font = FigletFont.Load("larry3d.flf");
		AnsiConsole.Write(new FigletText(font, "Monopoly").Centered().Color(Color.Red));
		var select_start = AnsiConsole.Prompt(
			new SelectionPrompt<string>()
				.PageSize(10)
				.MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
				.AddChoices(new[] {
					"Play","Exit"
				}));
		if(select_start == "Exit")
		{
			EndGame();
			currentGameStatus = GameStatus.End;
			return false;
		}
		else{
			currentGameStatus = GameStatus.Initialized;
			Console.Clear();
			return true;
		}
	}
	public  void EndGame()
	{
		Console.WriteLine("GAME ENDED. Press any key...");
		Console.ReadLine();			
		Console.Clear();
	}
	public void AddPlayer(IPlayer newPlayer)
	{
		PlayerData playerData = new PlayerData(1200);
		playerDataDict.Add(newPlayer, playerData);
	}
	public void RemovePlayer()
	{
	}
	public void GetToken()
	{
		
	}
}


public enum GameStatus
{
	NotInitialized,
	Initialized,
	End
}