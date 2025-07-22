using UnityEngine;

[RequireComponent(typeof(Unit))]
public abstract class BaseAction : MonoBehaviour
{
    protected Unit _unit;
    protected bool _isActive;
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
}
