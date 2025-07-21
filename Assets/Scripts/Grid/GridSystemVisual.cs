using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance { get; private set; }
    [SerializeField]
    Transform _gridSystemVisualSinglePrefab;
    GridSystemVisualSingle[,] _gridSystemVisualSingleArray;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one GridSystemVisual in the scene!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        _gridSystemVisualSingleArray = new GridSystemVisualSingle[LevelGrid.Instance.GetWidth(),LevelGrid.Instance.GetHeight()];

        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new(x, z);
                Transform gridSystemVisualSingleTransform = Instantiate(_gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
                gridSystemVisualSingleTransform.parent = transform;
                _gridSystemVisualSingleArray[x,z] = gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
            }
        }

        HideAllGridPosition();
    }
    void Update()
    {
        HideAllGridPosition();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        if (selectedUnit == null)
            return;

        ShowGridPositionList(selectedUnit.GetMoveAction().GetValidActionGridPositionList());
    }
    public void HideAllGridPosition()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                _gridSystemVisualSingleArray[x, z].Hide();
            }
        }
    }
    public void ShowGridPositionList(List<GridPosition> gridPositionList)
    {
        foreach (GridPosition gridPosition in gridPositionList)
        {
            _gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show();
        }
    }
}
