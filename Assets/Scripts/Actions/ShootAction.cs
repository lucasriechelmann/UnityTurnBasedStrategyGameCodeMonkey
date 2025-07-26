using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootAction : BaseAction
{
    [SerializeField]
    Animator _unitAnimator;
    [SerializeField]
    enum State
    {
        Aiming,
        Shooting,
        CoolOff
    }
    int _maxShootDistance = 7;
    State _currentState;
    float _stateTimer;
    Unit _targetUnit;
    bool _canShootBullet = false;
    public override string GetActionName() => "Shoot";
    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new();
        GridPosition unitGridPosition = _unit.GetGridPosition();

        for (int x = -_maxShootDistance; x <= _maxShootDistance; x++)
        {
            for (int z = -_maxShootDistance; z <= _maxShootDistance; z++)
            {
                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);

                if (testDistance > _maxShootDistance)
                    continue;

                GridPosition offsetGridPosition = new(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition) || 
                    !LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                    continue;

                Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                if(targetUnit is null || (targetUnit.IsEnemy() && _unit.IsEnemy()) || (!targetUnit.IsEnemy() && !_unit.IsEnemy()))
                    continue;


                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }    
    protected override void OnActionExecuted(GridPosition gridPosition)
    {
        _targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
        _currentState = State.Aiming;
        float aimingStateTime = 1f;
        _stateTimer = aimingStateTime;
        _canShootBullet = true;
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();

        _stateTimer -= Time.deltaTime;

        switch (_currentState)
        {
            case State.Aiming:
                Vector3 direction = (_targetUnit.GetWorldPosition() - _unit.GetWorldPosition()).normalized;
                float rotationSpeed = 10f;
                transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime * rotationSpeed);
                break;
            case State.Shooting:
                if (_canShootBullet)
                {
                    _canShootBullet = false;
                    Shoot();
                }
                break;
            case State.CoolOff:

                break;
        }

        if(_stateTimer <= 0f)
        {
            NextState();
        }
    }
    void Shoot()
    {
        _targetUnit.Damage();
    }
    void NextState()
    {
        switch (_currentState)
        {
            case State.Aiming:
                _currentState = State.Shooting;
                float shootingStateTime = 0.1f;
                _stateTimer = shootingStateTime;
                break;
            case State.Shooting:
                _currentState = State.CoolOff;
                float coolOffStateTime = 0.5f;
                _stateTimer = coolOffStateTime;
                break;
            case State.CoolOff:
                ActionEnd();
                break;
        }
    }
}
