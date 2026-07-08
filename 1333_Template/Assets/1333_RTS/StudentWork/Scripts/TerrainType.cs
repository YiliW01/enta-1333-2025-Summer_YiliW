using UnityEngine;

[CreateAssetMenu(fileName = "TerrainType", menuName = "Game/TerrainType")]
public class TerrainType : ScriptableObject
{
    [SerializeField] private string terrainName = "Default";
    [SerializeField] private Color terrainColor = Color.green;
    [SerializeField] private bool walkable = true;
    [SerializeField] private int movementcost = 1;
    [SerializeField] private Texture2D terrainTexture;

    public string TerrainName => terrainName;
    public Color TerrainColor => terrainColor;
    public bool Walkable => walkable;
    public int MovementCost => movementcost;
    public Texture2D TerrainTexture => terrainTexture;
}
