using System.Collections.Generic;
public class Player
{
    public int X, Y;
    public int Health = 100;
    public int Stamina = 100;
    public bool HasCompletedQuest = false;
    public List<Item> Inventory = new List<Item>();
    public Player(int x, int y)
    {
        X = x;
        Y = y;
    }
    public void Move(int dx, int dy, World world)
    {
        int newX = X + dx;
        int newY = Y + dy;
        if (newX < 0 || newY < 0 || newX >= World.Width || newY >= World.Height)
            return;
        if (world.Map[newX, newY].IsWall)
            return;
        X = newX;
        Y = newY;
        Stamina--;
        if (world.Map[X, Y].IsExit && Inventory.Exists(i => i.Name == "Key"))
            HasCompletedQuest = true;
    }
    public void PickUpItem(World world)
    {
        for (int i = world.Items.Count - 1; i >= 0; i--)
        {
            var item = world.Items[i];
            if (item.X == X && item.Y == Y)
            {
                Inventory.Add(item);
                world.Map[X, Y].HasItem = false;
                world.Items.RemoveAt(i);
                break;
            }
        }
    }
    public void UseItem(World world)
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            var item = Inventory[i];
            if (item.Name == "Key" && world.Map[X, Y].IsExit)
            {
                HasCompletedQuest = true;
                break;
            }
            else if (item.Name != "Key")
            {
                Health = 100;
                Inventory.RemoveAt(i);
                break;
            }
        }
    }
}
