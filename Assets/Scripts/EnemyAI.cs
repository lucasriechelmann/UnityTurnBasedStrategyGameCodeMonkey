using System;
using UnityEngine;
using URandom = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    float _timer = 0.0f;
    void Start()
    {
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
    }
    void Update()
    {
        if(TurnSystem.Instance.IsPlayerTurn())
            return;

        _timer -= Time.deltaTime;
        if (_timer < 0f) // AI action every 1 second
        {
            TurnSystem.Instance.NextTurn();
        }
    }
    void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        _timer = URandom.Range(2f,5f);
    }
}
