using System;
public static class Minigame
{
    public static void Play(Player player)
    {
        string[] choices = { "rock", "paper", "scissors" };
        Random rnd = new Random();
        string npcChoice = choices[rnd.Next(choices.Length)];
        Console.WriteLine("Choose [r]ock, [p]aper, [s]cissors:");
        string input = Console.ReadLine()?.ToLower() ?? String.Empty;
        string playerChoice = input switch
        {
            "r" => "rock",
            "p" => "paper",
            "s" => "scissors",
            _ => "rock"
        };
        Console.WriteLine($"NPC's choice {npcChoice}. Your choice {playerChoice}.");
        if (playerChoice == npcChoice)
        {
            Console.WriteLine("It's a draw!");
        }
        else if ((playerChoice == "rock" && npcChoice == "scissors") ||
                 (playerChoice == "paper" && npcChoice == "rock") ||
                 (playerChoice == "scissors" && npcChoice == "paper"))
        {
            Console.WriteLine("You won!");
        }
        else
        {
            Console.WriteLine("You lost! -20 health.");
            player.Health -= 20;
        }
    }
}