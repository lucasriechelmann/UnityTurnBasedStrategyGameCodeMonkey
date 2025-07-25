using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TurnSystemUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _turnText;
    [SerializeField]
    GameObject _enemyTurnVisualGameObject;
    [SerializeField]
    Button _endTurnButton;
    void Start()
    {        
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        UpdateTurnNumberText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisibility();
    }
    public void NextTurn()
    {
        TurnSystem.Instance.NextTurn();
    }
    void UpdateTurnNumberText()
    {
        _turnText.text = $"TURN {TurnSystem.Instance.GetTurnNumber()}";
    }
    void TurnSystem_OnTurnChanged(object sender, System.EventArgs e)
    {
        UpdateTurnNumberText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisibility();
    }
    void UpdateEnemyTurnVisual()
    {
        _enemyTurnVisualGameObject.SetActive(!TurnSystem.Instance.IsPlayerTurn());
    }
    void UpdateEndTurnButtonVisibility()
    {
        _endTurnButton.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
    }
}
