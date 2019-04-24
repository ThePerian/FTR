using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Collider2D worldBounds;
    public float minSize = 1;
    public float maxSize = 2.82f;
    public float sensitivity = 2.5f;
    public float speed = 6;

    CinemachineVirtualCamera _vCamera;

    private void Start()
    {
        _vCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        _vCamera.m_Lens.OrthographicSize = 
            Mathf.Clamp(_vCamera.m_Lens.OrthographicSize - zoom, minSize, maxSize);

        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector2 moveTo = new Vector2(moveX, moveY);

        if (worldBounds.OverlapPoint((Vector2)transform.position + moveTo))
            transform.Translate(moveTo);
    }
}
