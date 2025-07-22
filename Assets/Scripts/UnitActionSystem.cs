using System;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    public event EventHandler OnSelectedUnitChanged;
    [SerializeField]
    Unit _selectedUnit;
    [SerializeField]
    LayerMask _unitLayerMask;
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

        if (Input.GetMouseButtonDown(0))
        {
            bool unitHasBeenSelected = TryHandleUnitSelection();

            if (unitHasBeenSelected)
                return;

            GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if(_selectedUnit?.GetMoveAction()?.IsValidActionGridPosition(gridPosition) ?? false)
            {
                _selectedUnit?.GetMoveAction()?.Move(gridPosition);
            }
            
        }   
        
        if(Input.GetMouseButtonDown(1))
        {
            _selectedUnit?.GetSpinAction()?.Spin();
        }
    }
    bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, _unitLayerMask))
        {
            if(hitInfo.collider.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }            
        }

        return false;
    }
    void SetSelectedUnit(Unit unit)
    {
        _selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);                
    }
    public Unit GetSelectedUnit() => _selectedUnit;
}
