using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    bool _isEnemy;
    const int ACTION_POINTS_MAX = 2;
    public static event EventHandler OnAnyActionPointsChanged;
    GridPosition _currentGridPosition;
    MoveAction _moveAction;
    SpinAction _spinAction;
    BaseAction[] _baseActionArray;
    int _actionPoints = 2;
    void Awake()
    {
        _moveAction = GetComponent<MoveAction>();
        _spinAction = GetComponent<SpinAction>();
        _baseActionArray = GetComponents<BaseAction>();        
    }
    void Start()
    {
        _currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(_currentGridPosition, this);
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
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
    public SpinAction GetSpinAction() => _spinAction;
    public GridPosition GetGridPosition() => _currentGridPosition;
    public Vector3 GetWorldPosition() => transform.position;
    public BaseAction[] GetBaseActionArray() => _baseActionArray;
    public bool TrySpendingActionPointsToTakeAction(BaseAction action)
    {
        if (CanSpendActionPointsToTakeAction(action))
        {
            SpendActionPoints(action.GetActionPointsCost());
            return true;
        }
        
        return false;        
    }
    bool CanSpendActionPointsToTakeAction(BaseAction action) => action != null && _actionPoints >= action.GetActionPointsCost();
    void SpendActionPoints(int amount)
    {
        _actionPoints -= amount;
        if (_actionPoints < 0)
            _actionPoints = 0;

        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }
    public int GetActionPoints() => _actionPoints;
    void TurnSystem_OnTurnChanged(object sender, System.EventArgs e)
    {
        if (!ShouldUpdateActionPoints())
            return;

        _actionPoints = ACTION_POINTS_MAX;

        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        
    }
    bool ShouldUpdateActionPoints() => (IsEnemy() && !TurnSystem.Instance.IsPlayerTurn()) ||
               (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn());
    public bool IsEnemy() => _isEnemy;
    public void Damage()
    {
        Debug.Log(IsEnemy() ? "Enemy damaged!" : "Player damaged!");
    }
}
