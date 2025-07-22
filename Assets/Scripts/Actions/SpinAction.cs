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
            _isActive = false;
            transform.eulerAngles = _startPosition;
        }
    }
    public void Spin()
    {
        _isActive = true;
        _totalSpinAmount = 0f;
        _startPosition = transform.eulerAngles;
    }
}
