using UnityEngine;

// Represent each node on grid. Lightweight struct
[System.Serializable]
public struct GridNode
{
    public string Name; //index for us to keep track and organize nodes
    public Vector3 WorldPos;
    public TerrainType TerrainType;

    // if TerrainType is not null, set walkable to Terrain walkable
    // if null, set walkable to false
    public bool Walkable => TerrainType != null ? TerrainType.Walkable : false;

    // if TerrainType is not null, set weight to Terrain weight
    // if null, set weight to 1
    public int Weight => TerrainType != null ? TerrainType.MovementCost : 1;

    // if terraintype is not null, set color to terrain color
    // if null, set color to gray
    public Color GizmoColor => TerrainType != null ? TerrainType.TerrainColor : Color.gray;
}
