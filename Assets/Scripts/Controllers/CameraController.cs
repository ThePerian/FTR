using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Collider2D worldBounds;
    public float minSize = 1;
    public float maxSize = 3f;
    public float zoomSpeed = 128f;
    public float moveSpeed = 10f;

    public delegate void CameraSet();
    public static event CameraSet OnCameraSet;
    
    CinemachineVirtualCamera _vCamera;

    bool isDoingStartMovement;
    Vector2 startPosition;
    float startTravelDistance;
    float startZoomDelta;

    private void Awake()
    {
        _vCamera = GetComponent<CinemachineVirtualCamera>();
        GlobalMapController.OnPlayerReady += MoveCameraToStart;
        isDoingStartMovement = false;
        //we want to zoom camera to 1.5f
        startZoomDelta = _vCamera.m_Lens.OrthographicSize - 1.5f;
    }

    void Start()
    {
    }

    void Update()
    {
        if (isDoingStartMovement)
        {
            Vector2 newPosition2 = 
                Vector2.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
            float moveStep = Vector2.Distance(newPosition2, transform.position);
            float zoomStep = startZoomDelta * moveStep / startTravelDistance;
            //we need to keep Z position, otherwise it will be set to 0
            Vector3 newPosition3 = new Vector3(newPosition2.x, newPosition2.y, transform.position.z);
            transform.SetPositionAndRotation(newPosition3, Quaternion.identity);
            _vCamera.m_Lens.OrthographicSize =
                Mathf.Clamp(_vCamera.m_Lens.OrthographicSize - zoomStep, minSize, maxSize);

            if ((Vector2)transform.position == startPosition)
            {
                isDoingStartMovement = false;
                OnCameraSet?.Invoke();
            }
            
            return;
        }

        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
        _vCamera.m_Lens.OrthographicSize = 
            Mathf.Clamp(_vCamera.m_Lens.OrthographicSize - zoom, minSize, maxSize);

        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        Vector2 moveTo = new Vector2(moveX, moveY);

        if (worldBounds.OverlapPoint((Vector2)transform.position + moveTo))
            transform.Translate(moveTo);
    }

    public void MoveCameraToStart(Vector2 playerPosition)
    {
        startPosition = playerPosition;
        startTravelDistance = Vector2.Distance(transform.position, playerPosition);
        isDoingStartMovement = true;
        GlobalMapController.OnPlayerReady -= MoveCameraToStart;
    }
}
