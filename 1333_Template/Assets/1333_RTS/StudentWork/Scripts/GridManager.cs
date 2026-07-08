using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using Unity.Mathematics;

public class GridManager : MonoBehaviour
{
    //var to allow us to plug in our GridSettings scriptableOBJ
    [SerializeField] private GridSettings _gridSettings;
    public GridSettings GridSettings => _gridSettings;
    [SerializeField] private TerrainType _defaultTerrainType; // Default terrain type to use for new nodes

    //2D array of GridNoide structs that represents our grid.
    private GridNode[,] _gridNodes;

#if UNITY_EDITOR
    [Header("Debug for editor playmode only")]
    [SerializeField] private List<GridNode> AllNodes = new();
    [SerializeField] private bool showGrid = true;
    [SerializeField] private bool showNodeInfo = false;
#endif

    //flag for other scripts or this one to use to make sure grid is initialized before doing something else
    public bool IsInitialized { get; private set; } = false;

    public void InitializeGrid()
    {
        //initializing our array of GridNode structs using the dimensions from the Scriptable Objects
        _gridNodes = new GridNode[_gridSettings.GridSizeX, _gridSettings.GridSizeY];

        //Nest for Loop to iterate over all _gridNodes
        //at each grid position, instantiate a new GridNode struct, give it some default values and add it
        //to our gridNode 2D array
        for (int x = 0; x < _gridSettings.GridSizeX; x++) 
        { 
            for (int y = 0; y < _gridSettings.GridSizeY; y++)
            {
                /*
                 * if(_gridSettings.UseXZPlane)
                 *      worldPos = new Vector3(x, 0, y) * _gridSettings.NodeSize;
                 * else
                 *      worldPos = new Vector3(x, y, 0) * _gridSettings.NodeSize;
                 *      
                 * [(condition) ? result if true : result if false]
                 */
                Vector3 worldPos = _gridSettings.UseXZPlane
                    ? new Vector3(x, 0, y) * _gridSettings.NodeSize
                    : new Vector3(x, y, 0) * _gridSettings.NodeSize;

                GridNode node = new GridNode
                {
                    Name = $"Cell_{x + _gridSettings.GridSizeX * x + y}",
                    WorldPos = worldPos,
                    TerrainType = _defaultTerrainType

                    //Walkable = true, //Default all nodes to be walkable, modify later
                    //Weight = 1 //Default weight, useful for varied terrain costs
                };

                _gridNodes[x, y] = node;
            }
        }
        IsInitialized = true;
    }

    public void SetTerrainType(int x, int y, TerrainType terrain)
    {
        if (!IsValidCoordinate(x, y)) return;
        //pull a node out from our array
        //set its terraintype
        //put it back in the array
        GridNode node = _gridNodes[x, y];
        node.TerrainType = terrain;
        _gridNodes[x, y] = node;
    }

    private bool IsValidCoordinate(int x, int y)
    {
        return x >= 0 && x < GridSettings.GridSizeX && y >= 0 && y < GridSettings.GridSizeY;
    }

    public bool IsWalkable(Vector2Int coord)
    {
        if (!IsValidCoordinate(coord.x, coord.y)) return false;
        return _gridNodes[coord.x, coord.y].Walkable;
    }

    public float GetNodeWright(Vector2Int coord)
    {
        if (!IsValidCoordinate(coord.x, coord.y)) return float.MaxValue;
        return _gridNodes[coord.x,coord.y].Weight;
    }

#if UNITY_EDITOR
    private void PopulateDebugList()
    {
        //clear our debug list of _gridNodes
        //Then for each already existing GridNode, create a new GridNode, grab its info, set it to the newly created GridNode
        //for debug purposes
        AllNodes.Clear();

        for(int x = 0; x < _gridSettings.GridSizeX; x++)
        {
            for(int y = 0; y < _gridSettings.GridSizeY; y++)
            {
                AllNodes.Add(_gridNodes[x, y]);
                
            }
        }
    }

    //Create a custom editor button that, when pressed, calls PopulateDebugList and refreshes the Editor GUI
    [CustomEditor(typeof(GridManager))]
    public class GridManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //first draw the normal inspector GUI
            DrawDefaultInspector();

            //then look at the GridManager class this is attached to and call the PopulateDebugList function
            GridManager grid = (GridManager)target;
            if (grid.IsInitialized)
            {
                if (GUILayout.Button("Refresh Grid Debug View"))
                {
                    grid.PopulateDebugList();
                }
            }
        }
    }
#endif

    //Function to retrieve GridNode data efficiently
    public GridNode GetNode(int x, int y)
    {
        //first check if function arguments are out of bounds of the grid
        //otherwise return the proper GridNode
        if (!IsValidCoordinate(x,y))
            throw new System.IndexOutOfRangeException("Grid node indices out of range");

        return _gridNodes[x, y];
    }

    //Example setter for walkability, expanded in future logic
    //public void SetWalkable(int x, int y, bool walkable)
    //{
    //    _gridNodes[x, y]
    //}

    //Efficient visualization using Gizmoes, toggleable through Unity Editor
    private void OnDrawGizmos()
    {
        if (!showGrid || _gridNodes == null || GridSettings == null) return;

        //Draw the gridnode gizmos, size is 90% of GridNode Size for visual clarity
        for(int x = 0; x < _gridSettings.GridSizeX; x++)
        {
            for(int y = 0; y < _gridSettings.GridSizeY; y++)
            {
                GridNode node = _gridNodes[x, y];
                Gizmos.color = node.GizmoColor;
                Gizmos.DrawWireCube(node.WorldPos, Vector3.one * GridSettings.NodeSize * 0.9f);

#if UNITY_EDITOR
                if (showNodeInfo)
                {
                    Handles.Label(node.WorldPos + Vector3.up * 0.1f, $"{x},{y}");
                }
#endif
            }
        }
    }
}