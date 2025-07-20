using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField]
    TMP_Text _text;
    GridObject _gridObject;
    public void SetGridObject(GridObject gridObject)
    {
        _gridObject = gridObject;        
    }
    void Update()
    {
        _text.text = _gridObject.ToString();
    }
}
