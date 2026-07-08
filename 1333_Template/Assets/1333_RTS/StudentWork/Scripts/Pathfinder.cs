using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using IngameDebugConsole;

public class Pathfinder : MonoBehaviour
{
    public enum PathfindingType
    {
        Naive
    }

    public enum VisualizationState
    {
        Idle,
        Exploring,
        Reconstructing,
        Paused
    }

    [Header("Required Reference")]
    [SerializeField] private GridManager gridManager;

    [Header("Pathfinding Settings")]
    [SerializeField] private PathfindingType pathfindingType = PathfindingType.Naive;
    [SerializeField, Range(0, 100)] private int framesPerStep = 10;
    [SerializeField] private bool visualizePathfinding = true;

    [Header("Visualization Colors")]
    [SerializeField] private Color startNodeColor = Color.green;
    [SerializeField] private Color endNodeColor = Color.red;
    [SerializeField] private Color currentPathColor = Color.yellow;
    [SerializeField] private Color visitedNodeColor = new Color(0.5f, 0.5f, 1f, 0.5f);
    [SerializeField] private Color unvisitedNodeColor = new Color(0.3f, 0.3f, 0.3f, 0.3f);
    [SerializeField] private Color finalPathColor = Color.cyan;
    [SerializeField] private Color currentNodeColor = Color.magenta;
    [SerializeField] private Color currentNeighborColor = new Color(1f, 0.5f, 0f, 0.5f); //Orange
    [SerializeField] private Color explorationLineColor = new Color(1f, 1f, 0f, 0.3f); //semi-transparent yellow

    [Header("Visualization Settings")]
    [SerializeField] private int currentSeed = 0;
    [SerializeField] private bool useSeedRandom = true;
    [SerializeField] private float minWeight = 1f;
    [SerializeField] private float maxWeight = 10f;

    //Pathfinder instances
    private NaivePathfinder naivePathfinder = null;

    //Visualizer instances
    private NaivePathfinderVisualizer naivePathfinderViusalizer;

    private System.Random seededRandom;

    //Visualization State
    private HashSet<Vector2Int> visitedNodes = new HashSet<Vector2Int>();
    private Dictionary<Vector2Int, int> nodeDistance = new Dictionary<Vector2Int, int>(); // track distances from start
    private List<Vector2Int> currentPath = new List<Vector2Int>();
    private List<Vector2Int> reconstructionPath = new List<Vector2Int>();
    private Vector2Int? startNode;
    private Vector2Int? endnode;
    private VisualizationState visualization = VisualizationState.Idle;
    private Coroutine currentVisuallization;
    private List<Vector3> finalPath;
    private bool shouldPause;
    private bool isStepMode;
    private List<Vector2Int> explorationOrder;
    private Vector2Int? currentNode;
    private List<Vector2Int> currentNeighbours;

    private void Awake()
    {
        if (gridManager == null)
        {
            Debug.LogError("Pathfinder: GridManager reference is mission. Please assign it in the inspector");
            enabled = false;
            return;
        }

        //initialize pathfinders
        //naivePathfinder = new NaivePathfinder(GetNeighbours, WaitforNextStep)
    }

    private bool IsValidCoordinate(Vector2Int coord)
    {
        return coord.x >= 0 && coord.x < gridManager.GridSettings.GridSizeX &&
               coord.y >= 0 && coord.y < gridManager.GridSettings.GridSizeY;
    }

    private List<Vector2Int> GetNeighbours (Vector2Int coord)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();
        int[] dx = { -1, 0, 1 ,0, -1, -1, 1, 1};
        int[] dy = { 0, 1, 0, -1, -1, 1, -1, 1};

        //Check cardinal direction first
        for(int i = 0; i < 4; i++)
        {
            Vector2Int neighbour = new Vector2Int(coord.x + dx[i], coord.y + dy[i]);
            if(IsValidCoordinate(neighbour) && gridManager.IsWalkable(neighbour))
            {
                currentNeighbours.Add(neighbour);
            }
        }
        return neighbours;
    }
}
