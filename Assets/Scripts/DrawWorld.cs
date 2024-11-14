using UnityEngine;

public class DrawWorld : MonoBehaviour
{
    private World world;
    private Texture2D spritesheet;

    public void SetWorld(World newWorld)
    {
        world = newWorld;
        spritesheet = LoadSpriteSheet(Config.SPRITESHEET_PATH);
    }

    private Texture2D LoadSpriteSheet(string path)
    {
        return Resources.Load<Texture2D>(path);
    }

    public void UpdateDraw()
    {
        // Implement any update logic related to drawing
    }

    void OnGUI()
    {
        if (world != null)
        {
            Draw();
        }
    }

    public void Draw()
    {
        for (int y = 0; y < Config.WORLD_Y; y++)
        {
            for (int x = 0; x < Config.WORLD_X; x++)
            {
                int tileType = world.GetType(x, y);
                Vector2Int spriteCoords = Config.tileSprites[tileType];

                // Create the Rect for the tile's position on the screen
                Rect tileRect = new Rect(x * Config.TILESIZE * Config.SCALETILE,
                                          y * Config.TILESIZE * Config.SCALETILE,
                                          Config.TILESIZE * Config.SCALETILE,
                                          Config.TILESIZE * Config.SCALETILE);

                // Create the Rect for the tile's coordinates in the spritesheet
                Rect spriteRect = new Rect(spriteCoords.x, spriteCoords.y,
                                           Config.TILESIZE, Config.TILESIZE);

                // Draw the tile on the screen using GUI
                GUI.DrawTexture(tileRect, spritesheet, ScaleMode.ScaleToFit, true);
                // Alternatively, if you want to use spriteRect for cropping:
                // GUI.DrawTextureWithTexCoords(tileRect, spritesheet, spriteRect);
            }
        }
    }

}
