using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class MoveAction : BaseAction
{
    [SerializeField]
    Animator _unitAnimator;
    [SerializeField]
    int _maxMoveDistance = 4;
    const string IS_WALKING = "IsWalking";
    Vector3 _targetPosition;
    protected override void OnAwake()
    {
        base.OnAwake();
        _targetPosition = transform.position;
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
        MoveUnit();
    }
    void MoveUnit()
    {
        float stoppingDistance = 0.1f;
        if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
        {
            Vector3 moveDireciton = (_targetPosition - transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += moveDireciton * moveSpeed * Time.deltaTime;
            float rotationSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDireciton, Time.deltaTime * rotationSpeed);
            _unitAnimator.SetBool(IS_WALKING, true);   
            return;
        }

        _unitAnimator.SetBool(IS_WALKING, false);
        transform.position = _targetPosition;
        _isActive = false;
    }
    public void Move(GridPosition gridPosition)
    {
        _targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        _isActive = true;
    }
    public bool IsValidActionGridPosition(GridPosition gridPosition) => GetValidActionGridPositionList().Contains(gridPosition);
    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new();
        GridPosition unitGridPosition = _unit.GetGridPosition();

        for(int x = -_maxMoveDistance; x <= _maxMoveDistance; x++)
        {
            for (int z = -_maxMoveDistance; z <= _maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition) || _unit.GetGridPosition() == testGridPosition ||
                    LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                    continue;
                    
                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }
}
