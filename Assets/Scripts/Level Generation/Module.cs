using UnityEngine;

namespace LevelGeneration
{
    /// <summary>
    /// Scriptable Object asset for one specific module.
    /// </summary>
    [CreateAssetMenu(fileName = "New Module", menuName = "Map Generation/Module")]
    public class Module : ScriptableObject
    {
        /// <summary>
        /// Different edge connection types.
        /// </summary>
        public enum EdgeConnectionTypes
        {
            NONE,
            TILE_GRASS,
            TILE_WATER, 
            TILE_FOREST5,
            TILE_COASTN,
            TILE_COASTE,
            TILE_COASTS,
            TILE_COASTW,
            TILE_COASTNE,
            TILE_COASTSE,
            TILE_COASTSW,
            TILE_COASTNW,
            TILE_FOREST1,
            TILE_FOREST2,
            TILE_FOREST3,
            TILE_FOREST4,
            TILE_FOREST6,
            TILE_FOREST7,
            TILE_FOREST8,     
            TILE_FOREST9,
            TILE_PATH1, 
            TILE_PATH2, 
            TILE_PATH3, 
            TILE_PATH4, 
            TILE_PATH5, 
            TILE_PATH6, 
            TILE_PATH7, 
            TILE_PATH8, 
            TILE_PATH9
            
            
            //TILE_COASTNE2,
            //TILE_COASTSE2,
            //TILE_COASTSW2,
            //TILE_COASTNW2,
            //TILE_ROCKN,
            //TILE_ROCKE,
            //TILE_ROCKS,
            //TILE_ROCKW,
            //TILE_ROCKNE,
            //TILE_ROCKSE,
            //TILE_ROCKSW,
            //TILE_ROCKNW,

            //TILE_FORESTNE2,
            //TILE_FORESTSE2,
            //TILE_FORESTSW2,
            //TILE_FORESTNW2,
        }

        public int weight;

        /// <summary>
        /// The module`s game object.
        /// </summary>
        public GameObject moduleGO;

        /// <summary>
        /// The module`s edge connections starting with the bottom one going counter clockwise.
        ///
        /// [bottom, right, top, left]
        /// </summary>
        public EdgeConnectionTypes[] edgeConnections = new EdgeConnectionTypes[4];
    }
}