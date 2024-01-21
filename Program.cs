using System.Text;
using Spectre.Console;
class Program
{
	static void Main()
	{
		GameController gm = new GameController(4);
		if(gm.StartGame())
		{
			while(true)
			{
				var player_menu_select = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.PageSize(10)
					.MoreChoicesText("Players")
					.AddChoices(new[] {
						"Add New Player","Remove","Next"
					}));
				// if (gm.playerDataDict.Count>=gm.maxplayer)
				// {
				// 	Console.WriteLine("Maximum number of players reached");
				// 	break;
				// }
				if (player_menu_select == "Next")
				{
					if (gm.playerDataDict.Count==0)
					{
						Console.WriteLine("No Player Added");
						return;
					}
					else
					{
						break;
					}
				}
				
				string addingplayer = AnsiConsole.Ask<string>("What's your [green]name[/]?");
				gm.AddPlayer(new Player(addingplayer));
		
			}
		}
	}	
}