using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    [SerializeField]
    Animator _unitAnimator;
    Vector3 _targetPosition;
    GridPosition _currentGridPosition;
    const string IS_WALKING = "IsWalking";
    void Awake()
    {
        _targetPosition = transform.position;
    }
    void Start()
    {
        _currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(_currentGridPosition, this);
    }
    void Update()
    {
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
            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

            if (newGridPosition != _currentGridPosition)
            {
                LevelGrid.Instance.UnitMovedGridPosition(this, _currentGridPosition, newGridPosition);
                _currentGridPosition = newGridPosition;
            }

            return;
        }

        _unitAnimator.SetBool(IS_WALKING, false);
        transform.position = _targetPosition;
    }
    public void Move(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}
