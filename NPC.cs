using System;
public class NPC
{
    public string Name;
    public char Symbol;
    public int X, Y;
    private static Random random = new Random();
    public NPC(string name, char symbol)
    {
        Name = name;
        Symbol = symbol;
    }
    public void Update(World world, Player player)
    {
        int distance = Math.Abs(player.X - X) + Math.Abs(player.Y - Y);
        if (distance == 1)
        {
            Console.WriteLine($"{Name} challenges you to rock, paper, scissors for being too close to him!");
            Minigame.Play(player);
            world.TeleportNPC(this);
            return;
        }
        if (distance != 1)
        {
            int dx = Math.Sign(player.X - X);
            int dy = Math.Sign(player.Y - Y);


            if (Math.Abs(player.X - X) > Math.Abs(player.Y - Y)) dy = 0; else dx = 0;


            int newX = X + dx;
            int newY = Y + dy;


            if (newX >= 0 && newX < World.Width && newY >= 0 && newY < World.Height)
            {
                var targetTile = world.Map[newX, newY];
                if (!targetTile.IsWall && !targetTile.HasNPC && !(player.X == newX && player.Y == newY))
                {
                    world.Map[X, Y].HasNPC = false;
                    X = newX;
                    Y = newY;
                    world.Map[X, Y].HasNPC = true;
                }
            }
        }
    }
}