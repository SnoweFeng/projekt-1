using System;
using System.Collections.Generic;
public class World
{
    public const int Width = 20;
    public const int Height = 10;
    public Tile[,] Map = new Tile[Width, Height];
    public List<NPC> Npcs = new List<NPC>();
    public List<Item> Items = new List<Item>();
    private Player? player;
    private Random rand = new Random();
    public World()
    {
        GenerateMap();
        SpawnNPCs();
        SpawnItems();
        PlaceExit();
    }
    public void SpawnPlayer(Player p)
    {
        player = p;
        Map[player.X, player.Y].HasPlayer = true;
    }
    public void Render()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (player!=null && player.X == x && player.Y == y) Console.Write("@");
                else if (Map[x, y].IsWall) Console.Write("#");
                else if (Map[x, y].HasNPC) Console.Write("N");
                else if (Map[x, y].HasItem) Console.Write("I");
                else if (Map[x, y].IsExit) Console.Write(">");
                else Console.Write(".");
            }
            Console.WriteLine();
        }
        if (player != null)
            Console.WriteLine($"Health: {player.Health} | Stamina: {player.Stamina} | Inventory: {player.Inventory.Count} items");
    }
    public void Update()
    {
        if (player == null) return;
        foreach (var npc in Npcs) npc.Update(this, player);
    }
    private void GenerateMap()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Map[x, y] = new Tile { IsWall = (x == 0 || y == 0 || x == Width - 1 || y == Height - 1) };
            }
        }
    }
    private (int, int) GetRandomEmptyPosition()
    {
        int x, y;
        do
        {
            x = rand.Next(1, Width - 1);
            y = rand.Next(1, Height - 1);
        } while (Map[x, y].IsWall || Map[x, y].HasNPC || Map[x, y].HasItem || Map[x, y].IsExit || (player != null && player.X == x && player.Y == y));


        return (x, y);
    }
    private void SpawnNPCs()
    {
        var npc = new NPC("Scout", 'N');
        var (x, y) = GetRandomEmptyPosition();
        npc.X = x;
        npc.Y = y;
        Map[x, y].HasNPC = true;
        Npcs.Add(npc);
    }
    private void SpawnItems()
    {
        var key = new Item("Key", "Opens door");
        var (x, y) = GetRandomEmptyPosition();
        key.X = x;
        key.Y = y;
        Items.Add(key);
        Map[x, y].HasItem = true;
    }
    private void PlaceExit()
    {
        var (x, y) = GetRandomEmptyPosition();
        Map[x, y].IsExit = true;
    }
    public void TeleportNPC(NPC npc)
    {
        Map[npc.X, npc.Y].HasNPC = false;
        var (x, y) = GetRandomEmptyPosition();
        npc.X = x;
        npc.Y = y;
        Map[x, y].HasNPC = true;
    }
}