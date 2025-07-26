using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class SpinAction : BaseAction
{
    float _totalSpinAmount = 0f;
    Vector3 _startPosition;
    protected override void OnUpdate()
    {
        base.OnUpdate();
        SpinUnit();
    }
    void SpinUnit()
    {
        float spinAddAmount = 360 * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
        _totalSpinAmount += spinAddAmount;

        if (_totalSpinAmount >= 360f)
        {            
            transform.eulerAngles = _startPosition;
            ActionEnd();
        }
    }
    protected override void OnActionExecuted(GridPosition gridPosition)
    {
        _totalSpinAmount = 0f;
        _startPosition = transform.eulerAngles;
    }
    public override List<GridPosition> GetValidActionGridPositionList() => new()
    {
        _unit.GetGridPosition()
    };
    public override string GetActionName() => "Spin";
}
