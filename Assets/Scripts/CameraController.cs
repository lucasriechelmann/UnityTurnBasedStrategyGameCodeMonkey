using Unity.Cinemachine;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField]
    CinemachineFollow _cinemachineFollow;
    const float MIN_FOLLOW_OFFSET_Y = 2f;
    const float MAX_FOLLOW_OFFSET_Y = 12f;
    Vector3 _targetFollorOffset;
    void Start()
    {
        _targetFollorOffset = _cinemachineFollow.FollowOffset;
    }
    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleZoom()
    {
        Vector2 mouseWheel = Input.mouseScrollDelta;
        float zoomAmount = 1f;

        if (mouseWheel.y > 0)
        {
            _targetFollorOffset.y -= zoomAmount;
        }
        if (mouseWheel.y < 0)
        {
            _targetFollorOffset.y += zoomAmount;
        }

        _targetFollorOffset.y = Mathf.Clamp(_targetFollorOffset.y, MIN_FOLLOW_OFFSET_Y, MAX_FOLLOW_OFFSET_Y);

        float zoomSpped = 5f;
        _cinemachineFollow.FollowOffset = Vector3.Lerp(
            _cinemachineFollow.FollowOffset,
            _targetFollorOffset,
            Time.deltaTime * zoomSpped);        
    }

    private void HandleRotation()
    {
        Vector3 rotationVector = new(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y += 1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y -= 1;
        }

        float rotationSpeed = 100f;
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }

    private void HandleMovement()
    {
        Vector3 inputMovementDirection = new(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMovementDirection.z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMovementDirection.z -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMovementDirection.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMovementDirection.x += 1;
        }
        float moveSpeed = 5f;
        Vector3 moveVector = transform.forward * inputMovementDirection.z +
                             transform.right * inputMovementDirection.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }
}
