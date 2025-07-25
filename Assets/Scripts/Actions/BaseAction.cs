using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public abstract class BaseAction : MonoBehaviour
{
    protected Unit _unit;
    protected bool _isActive;
    protected Action _onActionComplete;
    void Awake()
    {
        _unit = GetComponent<Unit>();
        OnAwake();
    }
    void Update()
    {
        if(!_isActive)
            return;

        OnUpdate();
    }
    protected virtual void OnAwake()
    {

    }
    protected virtual void OnUpdate()
    {

    }
    public abstract string GetActionName();
    public void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);
        OnActionExecuted(gridPosition);
    }
    protected abstract void OnActionExecuted(GridPosition gridPosition);
    public virtual bool IsValidActionGridPosition(GridPosition gridPosition) => GetValidActionGridPositionList().Contains(gridPosition);
    public abstract List<GridPosition> GetValidActionGridPositionList();
    public virtual int GetActionPointsCost() => 1;
    protected virtual void ActionStart(Action onActionComplete)
    {
        _isActive = true;
        _onActionComplete = onActionComplete;
    }
    protected virtual void ActionEnd()
    {
        _isActive = false;
        _onActionComplete?.Invoke();
        _onActionComplete = null;
    }
}
