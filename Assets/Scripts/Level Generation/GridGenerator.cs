using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelGeneration
{
    /// <summary>
    /// Controls the grid.
    /// </summary>
    public class GridGenerator : MonoBehaviour
    {
        /// <summary>
        /// Width of grid.
        /// </summary>
        public int width;

        /// <summary>
        /// Height of grid.
        /// </summary>
        public int height;

        /// <summary>
        /// Cell prefab.
        /// </summary>
        public GameObject cellPrefab;

        /// <summary>
        /// Cells matrix ([width, height]).
        /// </summary>
        protected Cell[,] cells;

        /// <summary>
        /// Generates the two-dimensional grid.
        /// </summary>
        protected void GenerateGrid(LevelGenerator levelGenerator)
        {
            if (width <= 0 || height <= 0)
            {
                Debug.LogError("Impossible grid dimensions!", gameObject);
                return;
            }

            // generate grid
            cells = new Cell[width, height];

            var scale = cellPrefab.transform.localScale;
            var origin = transform.position;
            var bottomLeft = new Vector2(
                origin.x - width * scale.x / 2f + scale.x / 2,
                origin.y - height * scale.y / 2f + scale.y / 2
            //origin.z - height * scale.z / 2f + scale.z / 2
            );

            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                var curPos = new Vector2(bottomLeft.x + x * scale.x, bottomLeft.y + y * scale.y);

                // create new cell
                var cellObj = Instantiate(cellPrefab, curPos, Quaternion.identity, gameObject.transform);
                cellObj.name = $"({x}, {y})";
                var cell = cellObj.GetComponent<Cell>();
                cell.levelGenerator = levelGenerator;
                cell.PopulateCell();
                cells[x, y] = cell;

                /*
                 * Assign neighbours
                 */

                if (x > 0)
                {
                    var leftCell = cells[x - 1, y];
                    cell.neighbours[3] = leftCell;
                    leftCell.neighbours[1] = cell;
                }

                if (y > 0)
                {
                    var bottomCell = cells[x, y - 1];
                    cell.neighbours[0] = bottomCell;
                    bottomCell.neighbours[2] = cell;
                }
            }
        }

        /// <summary>
        /// Destroys the current grid.
        /// </summary>
        protected void RemoveGrid()
        {
            foreach (Transform child in gameObject.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}