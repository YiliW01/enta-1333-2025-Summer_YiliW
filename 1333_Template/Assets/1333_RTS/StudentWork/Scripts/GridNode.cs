using UnityEngine;

// Represent each node on grid. Lightweight struct
[System.Serializable]
public struct GridNode
{
    public string Name; //index for us to keep track and organize nodes
    public Vector3 WorldPos;
    public bool Walkable;
    public int Weight;
    public TerrainType TerrainType;
}
