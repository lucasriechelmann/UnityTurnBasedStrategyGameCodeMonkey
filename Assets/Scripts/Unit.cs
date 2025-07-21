using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{    
    GridPosition _currentGridPosition;
    MoveAction _moveAction;
    void Awake()
    {
        _moveAction = GetComponent<MoveAction>();
    }
    void Start()
    {
        _currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(_currentGridPosition, this);
    }
    void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != _currentGridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, _currentGridPosition, newGridPosition);
            _currentGridPosition = newGridPosition;
        }
    }
    public MoveAction GetMoveAction() => _moveAction;
    public GridPosition GetGridPosition() => _currentGridPosition;
}
