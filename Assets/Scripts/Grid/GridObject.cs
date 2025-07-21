using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GridObject
{
    GridSystem _gridSystem;
    GridPosition _gridPosition;
    List<Unit> _units;
    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        _gridSystem = gridSystem;
        _gridPosition = gridPosition;
        _units = new();
    }
    public void AddUnit(Unit unit)
    {
        if(_units.Contains(unit))
            return;
        _units.Add(unit);
    }
    public void RemoveUnit(Unit unit)
    {
        if (!_units.Contains(unit))
            return;
        _units.Remove(unit);
    }
    public List<Unit> GetUnitList() => _units;
    public override string ToString() 
    {
        StringBuilder sb = new();
        sb.AppendLine(_gridPosition.ToString());
        foreach (Unit unit in _units)
        {
            sb.AppendLine(unit.name);
        }
        return sb.ToString();
    }
    public bool HasAnyUnit() => _units.Count > 0;
}
