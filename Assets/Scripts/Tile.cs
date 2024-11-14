using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public List<int> possibilities;
    public int entropy;
    public Dictionary<int, Tile> neighbours;

    public Tile(int x, int y)
    {
        possibilities = new List<int>(Config.tileRules.Count);
        entropy = possibilities.Count;
        neighbours = new Dictionary<int, Tile>();
    }

    public void AddNeighbour(int direction, Tile tile)
    {
        neighbours[direction] = tile;
    }

    public Tile GetNeighbour(int direction)
    {
        return neighbours[direction];
    }

    public List<int> GetDirections()
    {
        return new List<int>(neighbours.Keys);
    }

    public List<int> GetPossibilities()
    {
        return possibilities;
    }

    public void UpdatePossibilities(Tile tile)
    {
        tile.UpdatePossibilities(this);
    }

    public void Collapse()
    {
        List<int> weights = new List<int>();
        foreach (var possibility in possibilities)
        {
            weights.Add(Config.tileWeights[possibility]);
        }
        possibilities = new List<int> { RandomlySelectPossibility(weights) };
        entropy = 0;
    }

    public int RandomlySelectPossibility(List<int> weights)
    {
        // Implement weighted random selection logic here...
        return possibilities[0]; // Placeholder
    }

    public bool Constrain(List<int> neighbourPossibilities, int direction)
    {
        // Implement the logic for constraining possibilities based on neighbours
        return false; // Placeholder
    }
}
