using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFunctionCollapse : MonoBehaviour
{

    public bool interactive = true;
    public bool interactiveKeypress = false;

    private World world;
    private DrawWorld drawWorld;
    //private bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize world and drawing system
        world = new World(Config.WORLD_X,Config.WORLD_Y);
        drawWorld = gameObject.AddComponent<DrawWorld>(); // Add the component to this GameObject
        drawWorld.SetWorld(world);
        world.WaveFunctionCollapse();
        //if (!interactive)
        //{
        //    while (!done)
        //    {
        //        int result = world.WaveFunctionCollapse();
        //        if (result == 0)
        //        {
        //            done = true;
        //        }
        //    }
        //}

        //drawWorld.UpdateWorld();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle input events
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Exit the application
            Application.Quit();
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (interactive && interactiveKeypress)
        //    {
        //        world.WaveFunctionCollapse();
        //        drawWorld.UpdateWorld();
        //    }
        //}

        //// Non-keypress interactive mode
        //if (interactive && !interactiveKeypress)
        //{
        //    if (!done)
        //    {
        //        int result = world.WaveFunctionCollapse();
        //        if (result == 0)
        //        {
        //            done = true;
        //        }
        //    }
        //    drawWorld.UpdateWorld();
        //}

        // Render the world (draw the tiles)
        drawWorld.Draw();
    }
}
