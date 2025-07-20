using System;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField]
    Unit _unit;
    MeshRenderer _meshRenderer;
    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        
    }
    void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UpdateVisual();
    }
    void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs args)
    {
        UpdateVisual();
    }
    void UpdateVisual() => _meshRenderer.enabled = UnitActionSystem.Instance.GetSelectedUnit() == _unit;
}
