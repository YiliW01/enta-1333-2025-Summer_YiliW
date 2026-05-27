using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    //var to allow us to plug in our GridSettings scriptableOBJ
    [SerializeField] private GridSettings _gridSettings;
    public GridSettings gridSettings => _gridSettings;

    //2D array of GridNoide structs that represents our grid.
    private GridNode[,] _gridNodes;

#if UNITY_EDITOR
    [Header("Debug for editor playmode only")]
    [SerializeField] private List<GridNode> AllNodes = new();
#endif

    //flag for other scripts or this one to use to make sure grid is initialized before doing something else
    public bool IsInitialized { get; private set; } = false;

    public void InitializeGrid()
    {
        //initializing our array of GridNode structs using the dimensions from the Scriptable Objects
        _gridNodes = new GridNode[_gridSettings.GridSizeX, _gridSettings.GridSizeY];

        for (int x = 0; x < _gridSettings.GridSizeX; x++) 
        { 
            for (int y = 0; y < _gridSettings.GridSizeY; y++)
            {

            }
        }
    }
}
