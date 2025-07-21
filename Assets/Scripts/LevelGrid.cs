using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }
    [SerializeField]
    Transform _debugTransformPrefab;
    GridSystem _gridSystem;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _gridSystem = new(10, 10, 2f);
        _gridSystem.CreateDebugObjects(_debugTransformPrefab, transform);
    }
    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit) =>
        _gridSystem.GetGridObject(gridPosition)?.AddUnit(unit);
    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition) => _gridSystem
        .GetGridObject(gridPosition)?
        .GetUnitList();
    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit) => _gridSystem
        .GetGridObject(gridPosition)?
        .RemoveUnit(unit);
    public GridPosition GetGridPosition(Vector3 worldPosition) =>
        _gridSystem.GetGridPosition(worldPosition);
    public Vector3 GetWorldPosition(GridPosition gridPosition) =>
        _gridSystem.GetWorldPosition(gridPosition);
    public bool IsValidGridPosition(GridPosition gridPosition) => _gridSystem
        .IsValidGridPosition(gridPosition);
    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition) =>
        _gridSystem.GetGridObject(gridPosition)?.HasAnyUnit() ?? false;
    public int GetWidth() => _gridSystem.GetWidth();    
    public int GetHeight() => _gridSystem.GetHeight();
    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition, unit);
    }
}
