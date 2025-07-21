using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField]
    MeshRenderer _meshRenderer;
    public void Show() => _meshRenderer.enabled = true;
    public void Hide() => _meshRenderer.enabled = false;
}

