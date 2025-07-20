using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    static MouseWorld _instance;
    [SerializeField]
    LayerMask _mousePlaneLayerMask;
    void Awake()
    {
        _instance = this;
    }
    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, _instance._mousePlaneLayerMask);
        return hitInfo.point;        
    }
}
