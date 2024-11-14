using UnityEngine;
using System.Collections.Generic;

public static class Config
{
    // Spritesheet Path (ensure the spritesheet is assigned in Unity Editor)
    public static string SPRITESHEET_PATH = "Assets/Asset_gambar/punyworld-overworld-tileset.png"; // Adjust path according to Unity asset path

    // World size in tiles
    public static int WORLD_X = 60; 
    public static int WORLD_Y = 34;

    // Spritesheet tile size (original), and upscale factor
    public static int TILESIZE = 16;
    public static int SCALETILE = 2;



    // Tile Types
    public const int TILE_GRASS     = 0;
    public const int TILE_WATER     = 1;
    public const int TILE_FOREST    = 2;
    public const int TILE_COASTN    = 3;
    public const int TILE_COASTE    = 4;
    public const int TILE_COASTS    = 5;
    public const int TILE_COASTW    = 6;
    public const int TILE_COASTNE   = 7;
    public const int TILE_COASTSE   = 8;
    public const int TILE_COASTSW   = 9;
    public const int TILE_COASTNW   = 10;
    public const int TILE_COASTNE2  = 11;
    public const int TILE_COASTSE2  = 12;
    public const int TILE_COASTSW2  = 13;
    public const int TILE_COASTNW2  = 14;
    public const int TILE_ROCKN     = 15;
    public const int TILE_ROCKE     = 16;
    public const int TILE_ROCKS     = 17;
    public const int TILE_ROCKW     = 18;
    public const int TILE_ROCKNE    = 19;
    public const int TILE_ROCKSE    = 20;
    public const int TILE_ROCKSW    = 21;
    public const int TILE_ROCKNW    = 22;
    public const int TILE_FORESTN   = 23;
    public const int TILE_FORESTE   = 24;
    public const int TILE_FORESTS   = 25;
    public const int TILE_FORESTW   = 26;
    public const int TILE_FORESTNE  = 27;
    public const int TILE_FORESTSE  = 28;
    public const int TILE_FORESTSW  = 29;
    public const int TILE_FORESTNW  = 30;
    public const int TILE_FORESTNE2 = 31;
    public const int TILE_FORESTSE2 = 32;
    public const int TILE_FORESTSW2 = 33;
    public const int TILE_FORESTNW2 = 34;



    // Edges
    public const int GRASS = 0;
    public const int WATER = 1;
    public const int FOREST = 2;
    public const int COAST_N = 3;
    public const int COAST_E  = 4;
    public const int COAST_S  = 5;
    public const int COAST_W  = 6;
    public const int FOREST_N = 7;
    public const int FOREST_E = 8;
    public const int FOREST_S = 9;
    public const int FOREST_W = 10;
    public const int ROCK_N   = 11;
    public const int ROCK_E   = 12;
    public const int ROCK_S   = 13;
    public const int ROCK_W   = 14;
    public const int ROCK = 15;
    // Add other edges...

    // 
    public const int NORTH = 0;
    public const int EAST = 1;
    public const int SOUTH = 2;
    public const int WEST = 3;
    public static int[] directions = { NORTH, EAST, SOUTH, WEST };


    public static readonly Dictionary<int, int[]> tileRules = new Dictionary<int, int[]>
    {
        { TILE_GRASS, new[] { GRASS, GRASS, GRASS, GRASS } },
        { TILE_WATER, new[] { WATER, WATER, WATER, WATER } },
        { TILE_FOREST, new[] { FOREST, FOREST, FOREST, FOREST } },
        { TILE_COASTN, new[] { GRASS, COAST_N, WATER, COAST_N } },
        {TILE_COASTE, new[]  {COAST_E, GRASS, COAST_E, WATER} },
        {TILE_COASTS   , new[]  {WATER, COAST_S, GRASS, COAST_S} },
        {TILE_COASTW   , new[]  {COAST_W, WATER, COAST_W, GRASS} },
        {TILE_COASTNE  , new[]  {GRASS, GRASS, COAST_E, COAST_N} },
        {TILE_COASTSE  , new[]  {COAST_E, GRASS, GRASS, COAST_S} },
        {TILE_COASTSW  , new[]  {COAST_W, COAST_S, GRASS, GRASS} },
        {TILE_COASTNW  , new[]  {GRASS, COAST_N, COAST_W, GRASS} },
        {TILE_COASTNE2 , new[]  {COAST_E, COAST_N, WATER, WATER} },
        {TILE_COASTSE2 , new[]  {WATER, COAST_S, COAST_E, WATER} },
        {TILE_COASTSW2 , new[]  {WATER, WATER, COAST_W, COAST_S} },
        {TILE_COASTNW2 , new[]  {COAST_W, WATER, WATER, COAST_N } },
        {TILE_ROCKN    , new[]  {ROCK, ROCK_N, GRASS, ROCK_N } },
        {TILE_ROCKE    , new[]  {ROCK_E, ROCK, ROCK_E, GRASS } },
        {TILE_ROCKS    , new[]  {GRASS, ROCK_S, ROCK, ROCK_S } },
        {TILE_ROCKW    , new[]  {ROCK_W, GRASS, ROCK_W, ROCK } },
        {TILE_ROCKNE   , new[]  {ROCK_E, ROCK_N, GRASS, GRASS} },
        {TILE_ROCKSE   , new[]  {GRASS, ROCK_S, ROCK_E, GRASS} },
        {TILE_ROCKSW   , new[]  {GRASS, GRASS, ROCK_W, ROCK_S} },
        {TILE_ROCKNW   , new[]  {ROCK_W, GRASS, GRASS, ROCK_N } },
        {TILE_FORESTN  , new[]  {FOREST, FOREST_N, GRASS, FOREST_N } },
        {TILE_FORESTE  , new[]  {FOREST_E, FOREST, FOREST_E, GRASS } },
        {TILE_FORESTS  , new[]  {GRASS, FOREST_S, FOREST, FOREST_S } },
        {TILE_FORESTW  , new[]  {FOREST_W, GRASS, FOREST_W, FOREST } },
        {TILE_FORESTNE , new[]  {FOREST_E, FOREST_N, GRASS, GRASS  } },
        {TILE_FORESTSE , new[]  {GRASS, FOREST_S, FOREST_E, GRASS  } },
        {TILE_FORESTSW , new[]  {GRASS, GRASS, FOREST_W, FOREST_S  } },
        {TILE_FORESTNW , new[]  {FOREST_W, GRASS, GRASS, FOREST_N  } },
        {TILE_FORESTNE2, new[]  {FOREST, FOREST, FOREST_E, FOREST_N} },
        {TILE_FORESTSE2, new[]  {FOREST_E, FOREST, FOREST, FOREST_S} },
        {TILE_FORESTSW2, new[]  {FOREST_W, FOREST_S, FOREST, FOREST} },
        {TILE_FORESTNW2, new[]  {FOREST, FOREST_N, FOREST_W, FOREST } },

    };

    // Tile weights to control their frequency in the map, as a dictionary
    public static readonly Dictionary<int, int> tileWeights = new Dictionary<int, int>
    {
        { TILE_GRASS    , 16 },
        { TILE_WATER    , 4 },
        { TILE_FOREST   , 5 },
        { TILE_COASTN   , 6 },
        { TILE_COASTE   , 5},
        { TILE_COASTS   , 5},
        { TILE_COASTW   , 5},
        { TILE_COASTNE  , 5},
        { TILE_COASTSE  , 5},
        { TILE_COASTSW  , 5},
        { TILE_COASTNW  , 5},
        { TILE_COASTNE2 , 2},
        { TILE_COASTSE2 , 2},
        { TILE_COASTSW2 , 2},
        { TILE_COASTNW2 , 2},
        { TILE_FORESTN  , 4},
        { TILE_FORESTE  , 4},
        { TILE_FORESTS  , 4},
        { TILE_FORESTW  , 4},
        { TILE_FORESTNE , 4},
        { TILE_FORESTSE , 4},
        { TILE_FORESTSW , 4},
        { TILE_FORESTNW , 4},
        { TILE_FORESTNE2, 2},
        { TILE_FORESTSE2, 2},
        { TILE_FORESTSW2, 2},
        { TILE_FORESTNW2, 2},
        { TILE_ROCKN    , 4},
        { TILE_ROCKE    , 4},
        { TILE_ROCKS    , 4},
        { TILE_ROCKW    , 4},
        { TILE_ROCKNE   , 4},
        { TILE_ROCKSE   , 4},
        { TILE_ROCKSW   , 4},
        { TILE_ROCKNW   , 4 },
    };

    // Tile sprite positions in the spritesheet, to map tiles to specific regions in the spritesheet
    public static readonly Dictionary<int, Vector2Int> tileSprites = new Dictionary<int, Vector2Int>
    {
        { TILE_GRASS, new Vector2Int(16, 0 ) },
        { TILE_WATER, new Vector2Int(128, 176) },
        { TILE_FOREST, new Vector2Int(16, 128) },
        {TILE_COASTN   , new Vector2Int(128, 160) },
        {TILE_COASTE   , new Vector2Int(144, 176) },
        {TILE_COASTS   , new Vector2Int(128, 192) },
        {TILE_COASTW   , new Vector2Int(112, 176) },
        {TILE_COASTNE  , new Vector2Int(144, 160) },
        {TILE_COASTSE  , new Vector2Int(144, 192) },
        {TILE_COASTSW  , new Vector2Int(112, 192) },
        {TILE_COASTNW  , new Vector2Int(112, 160) },
        {TILE_COASTNE2 , new Vector2Int(176, 160) },
        {TILE_COASTSE2 , new Vector2Int(176, 176) },
        {TILE_COASTSW2 , new Vector2Int(160, 176) },
        {TILE_COASTNW2 , new Vector2Int(160, 160) },
        {TILE_ROCKSW   , new Vector2Int(32, 64) },
        {TILE_FORESTN  , new Vector2Int(16, 144) },
        {TILE_FORESTE  , new Vector2Int(0, 128) },
        {TILE_FORESTS  , new Vector2Int(16, 112) },
        {TILE_FORESTW  , new Vector2Int(32, 128) },
        {TILE_FORESTNE , new Vector2Int(0, 144) },
        {TILE_FORESTSE , new Vector2Int(0, 112) },
        {TILE_FORESTSW , new Vector2Int(32, 112) },
        {TILE_FORESTNW , new Vector2Int(32, 144) },
        {TILE_FORESTNE2, new Vector2Int(96, 128) },
        {TILE_FORESTSE2, new Vector2Int(96, 112) },
        {TILE_FORESTSW2, new Vector2Int(112, 112) },
        {TILE_FORESTNW2, new Vector2Int(112, 128) },
        {TILE_ROCKN    , new Vector2Int(16, 96) },
        {TILE_ROCKE    , new Vector2Int(0, 80) },
        {TILE_ROCKS    , new Vector2Int(16, 64) },
        {TILE_ROCKW    , new Vector2Int(32, 80) },
        {TILE_ROCKNE   , new Vector2Int(0, 96) },
        {TILE_ROCKSE   , new Vector2Int(0, 64) },
        {TILE_ROCKNW   , new Vector2Int(32, 96) },
    };
}
