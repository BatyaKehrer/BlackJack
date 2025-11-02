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
        while (true)
        {
            manager.dealCards();
            manager.playOutHands();
            manager.clearAllHands();
        }
    }
}