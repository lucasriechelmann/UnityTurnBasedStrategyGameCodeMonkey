using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _buttonText;
    [SerializeField]
    Button _button;
    [SerializeField]
    GameObject _selectedGameObject;
    BaseAction _baseAction;
    public void SetBaseAction(BaseAction action)
    {
        _baseAction = action;
        _buttonText.text = action.GetActionName().ToUpper();

        _button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(action);
        });
    }
    public void UpdateSelectedVisual()
    {
        _selectedGameObject.SetActive(UnitActionSystem.Instance.GetSelectedAction() == _baseAction);
    }
}
