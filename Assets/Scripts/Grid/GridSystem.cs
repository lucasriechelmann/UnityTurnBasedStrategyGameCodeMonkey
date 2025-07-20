using UnityEngine;

public class GridSystem
{
    int _width;
    int _height;
    float _cellSize = 1f;
    GridObject[,] _gridObjectArra;
    public GridSystem(int width, int height, float cellSize)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;

        _gridObjectArra = new GridObject[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                _gridObjectArra[x, z] = new GridObject(this, new GridPosition(x, z));
            }
        }

        
    }
    public Vector3 GetWorldPosition(GridPosition gridPosition) => new Vector3(gridPosition.x, 0, gridPosition.z) * _cellSize;
    public GridPosition GetGridPosition(Vector3 worldPosition) =>
        new GridPosition(
            Mathf.RoundToInt(worldPosition.x / _cellSize),
            Mathf.RoundToInt(worldPosition.z / _cellSize));
    public void CreateDebugObjects(Transform debugPrefab, Transform parent)
    {
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                GridPosition gridPosition = new(x, z);
                Transform tranformDebug = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                GridDebugObject gridDebugObject = tranformDebug.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
                tranformDebug.parent = parent;
            }
        }
    }
    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return _gridObjectArra[gridPosition.x, gridPosition.z];
    }
}
