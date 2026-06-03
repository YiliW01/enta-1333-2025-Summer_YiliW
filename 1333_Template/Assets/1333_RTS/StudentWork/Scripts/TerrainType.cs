using UnityEngine;

[CreateAssetMenu(fileName = "TerrainType")]
public class TerrainType : ScriptableObject
{
    [SerializeField] private string _terrainName;
    [SerializeField] private Color _terrainColor;
    [SerializeField] private bool _walkable;
    [SerializeField] private int _movementCost;
    [SerializeField] private Texture2D _terrainTexture;

    public string TerrainName => _terrainName;
    public Color TerrainColor => _terrainColor;
    public bool Walkable => _walkable;
    public int MovementCost => _movementCost;
    public Texture2D TerrainTexture => _terrainTexture;
}
