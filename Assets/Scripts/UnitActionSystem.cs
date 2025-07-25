using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    public event EventHandler<bool> OnBusyChanged;
    public event EventHandler OnActionStarted;
    [SerializeField]
    Unit _selectedUnit;
    [SerializeField]
    LayerMask _unitLayerMask;
    BaseAction _selectedAction;
    bool _isBusy;
    void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("More than one UnitActionSystem in the scene!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Update()
    {        
        if(_isBusy || EventSystem.current.IsPointerOverGameObject() || !TurnSystem.Instance.IsPlayerTurn())
            return;

        if (TryHandleUnitSelection())
            return;

        HandleSelectedAction();
    }
    void HandleSelectedAction()
    {
        if (_selectedUnit == null || _selectedAction == null)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (!_selectedAction.IsValidActionGridPosition(gridPosition))
                return;

            if (!_selectedUnit.TrySpendingActionPointsToTakeAction(_selectedAction))
                return;

            SetBusy();
            
            _selectedAction?.TakeAction(gridPosition, ClearBusy);    
            OnActionStarted?.Invoke(this, EventArgs.Empty);
        }
    }
    void SetBusy()
    {
        _isBusy = true;
        OnBusyChanged?.Invoke(this, _isBusy);
    }
    void ClearBusy()
    {
        _isBusy = false;
        OnBusyChanged?.Invoke(this, _isBusy);
    }
    bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, _unitLayerMask))
            {
                if (hitInfo.collider.TryGetComponent<Unit>(out Unit unit))
                {
                    if(_selectedUnit == unit)
                        return false;

                    if (unit.IsEnemy())
                        return false;

                    SetSelectedUnit(unit);
                    return true;
                }
            }
        }
            

        return false;
    }
    void SetSelectedUnit(Unit unit)
    {
        _selectedUnit = unit;
        SetSelectedAction(_selectedUnit?.GetMoveAction());
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);                
    }
    public void SetSelectedAction(BaseAction action)
    {
        _selectedAction = action;
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }
    public Unit GetSelectedUnit() => _selectedUnit;
    public BaseAction GetSelectedAction() => _selectedAction;
}
