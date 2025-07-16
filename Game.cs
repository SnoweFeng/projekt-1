using System;
public class Game
{
    private World? world;
    private Player? player;
    private bool isRunning = true;
    public void Start()
    {
        world = new World();
        player = new Player(1, 1);
        world.SpawnPlayer(player);
        while (isRunning)
        {
            world.Render();
            HandleInput();
            world.Update();


            if (player.Health <= 0)
            {
                Console.WriteLine("Game Over.");
                isRunning = false;
            }
            else if (player.HasCompletedQuest)
            {
                Console.WriteLine("Congratulations! You won!");
                isRunning = false;
            }
        }
    }
    private void HandleInput()
    {
        if (world == null || player == null) return;
        Console.WriteLine("Use WASD to move, I to pick up item or interact, U to use item:");
        string input = Console.ReadLine()?.ToLower() ?? String.Empty;
        switch (input)
        {
            case "w": player.Move(0, -1, world); break;
            case "s": player.Move(0, 1, world); break;
            case "a": player.Move(-1, 0, world); break;
            case "d": player.Move(1, 0, world); break;
            case "i":
                player.PickUpItem(world);
                foreach (var npc in world.Npcs)
                {
                    int dx = Math.Abs(npc.X - player.X);
                    int dy = Math.Abs(npc.Y - player.Y);
                    if (dx + dy == 1)
                    {
                        for (int i = 0; i < 30; i++) Console.WriteLine();
                        Console.WriteLine($"{npc.Name}: You must play rock, paper, scissors with me!");
                        Minigame.Play(player);
                        world.TeleportNPC(npc);
                        break;
                    }
                }
                break;
            case "u": player.UseItem(world); break;
        }
    }
}