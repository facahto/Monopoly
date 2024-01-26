public interface IDice
{
	int Side { get; }
	public int Roll();
}
public class Dice : IDice
{
	public int Side { get; }

	public Dice(int side)
	{
		Side = side;
	}
	public int Roll()
	{
		Random rnd = new Random();
		int diceout  = rnd.Next(1, Side+1);
		return diceout;
	}
}