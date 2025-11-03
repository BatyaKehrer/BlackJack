using BlackJack;
using System;
public class Program
{
    public static void waitToClear()
    {
        Console.WriteLine("\nPress Enter To Continue");
        Console.ReadLine();
        Console.Clear();
    }

    public static void Main(string[] args)
    {
        GameManager manager = new GameManager();
        while (manager.players.Count > 0)
        {
            while (manager.dealer.getValue() < 1)
            {
                manager.playerBet();
                if (manager.players.Count < 1)
                {
                    Console.WriteLine("All players are done. Thanks for playing!");
                    return;
                }
                manager.dealCards();
            }
            manager.playOutHands();
            manager.levelBankrolls();
            manager.clearAllHands();
        }
        Console.WriteLine("All players are done. Thanks for playing!");
    }
}