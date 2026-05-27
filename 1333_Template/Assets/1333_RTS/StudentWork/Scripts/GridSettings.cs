using UnityEngine;

//GridSettings is a ScriptableObject for easy customization of grid dimensions and orientation
[CreateAssetMenu(fileName = "GridSettings", menuName = "Game/GridSettings")]
public class GridSettings : ScriptableObject
{
    [SerializeField] private int _gridSizeX = 10;
    [SerializeField] private int _gridSizeY = 10;
    [SerializeField] private float _nodeSize = 1;
    [SerializeField] private bool _useXZPlane = true;

    public int GridSizeX => _gridSizeX;
    public int GridSizeY => _gridSizeY;
    public float NodeSize => _nodeSize;
    public bool UseXZPlane => _useXZPlane;
}
