using System.Collections.Generic;
using UnityEngine;

public class World
{
    private int cols;
    private int rows;
    private Tile[][] tileRows;

    public World(int sizeX, int sizeY)
    {
        cols = sizeX;
        rows = sizeY;
        tileRows = new Tile[rows][];

        for (int y = 0; y < sizeY; y++)
        {
            tileRows[y] = new Tile[cols];
            for (int x = 0; x < sizeX; x++)
            {
                tileRows[y][x] = new Tile(x, y);
            }
        }

        SetNeighbours();
    }

    private void SetNeighbours()
    {
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Tile tile = tileRows[y][x];
                if (y > 0) tile.AddNeighbour(Config.NORTH, tileRows[y - 1][x]);
                if (x < cols - 1) tile.AddNeighbour(Config.EAST, tileRows[y][x + 1]);
                if (y < rows - 1) tile.AddNeighbour(Config.SOUTH, tileRows[y + 1][x]);
                if (x > 0) tile.AddNeighbour(Config.WEST, tileRows[y][x - 1]);
            }
        }
    }

    public int GetEntropy(int x, int y)
    {
        return tileRows[y][x].entropy;
    }

    public int GetType(int x, int y)
    {
        return tileRows[y][x].possibilities[0];
    }

    public int GetLowestEntropy()
    {
        int lowestEntropy = int.MaxValue; // Start with the highest possible value
        int lowestX = -1;
        int lowestY = -1;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Tile tile = tileRows[y][x];
                if (tile.entropy > 0 && tile.entropy < lowestEntropy) // Only consider tiles with possibilities
                {
                    lowestEntropy = tile.entropy;
                    lowestX = x;
                    lowestY = y;
                }
            }
        }

        // If no valid tile found, return -1 as an indicator
        return (lowestEntropy == int.MaxValue) ? -1 : lowestY * cols + lowestX; // Return a combined index
    }


    public void WaveFunctionCollapse()
    {
        // Get the position of the tile with the lowest entropy
        int lowestTileIndex = GetLowestEntropy();

        if (lowestTileIndex == -1)
        {
            return; // All tiles are fully collapsed or there are no tiles left to process
        }

        int lowestY = lowestTileIndex / cols;
        int lowestX = lowestTileIndex % cols;

        Tile tile = tileRows[lowestY][lowestX];

        // Choose a random tile from the possibilities
        //int chosenType = tile.RandomlySelectPossibility(tile.possibilities);

        // Collapse this tile to the chosen type
        tile.Collapse();

        // Propagate the change to neighbors
        Propagate(lowestX, lowestY);
    }

    private void Propagate(int x, int y)
    {
        // Implement propagation logic to update neighbors' entropies and possibilities
        Tile tile = tileRows[y][x];

        foreach (var direction in Config.directions)
        {
            Tile neighbour = tile.GetNeighbour(direction);
            if (neighbour != null)
            {
                // Update the neighbour based on the collapsed tile
                neighbour.UpdatePossibilities(tile);
            }
        }
    }

}
