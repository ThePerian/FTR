using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Collider2D worldBounds;
    public float minSize = 1;
    public float maxSize = 2.82f;
    public float zoomSpeed = 128f;
    public float moveSpeed = 6;

    CinemachineVirtualCamera _vCamera;

    private void Start()
    {
        _vCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
        _vCamera.m_Lens.OrthographicSize = 
            Mathf.Clamp(_vCamera.m_Lens.OrthographicSize - zoom, minSize, maxSize);

        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        Vector2 moveTo = new Vector2(moveX, moveY);

        if (worldBounds.OverlapPoint((Vector2)transform.position + moveTo))
            transform.Translate(moveTo);
    }
}
