using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    private Camera m_Camera;
    private Mouse m_Mouse;
    
    public float VerticalAmplitude;
    public float HorizontalAmplitude;

    private Quaternion m_baseRotation;
    
    void Awake()
    {
        m_Camera = GetComponent<Camera>();
        m_Mouse = Mouse.current;
        m_baseRotation = m_Camera.transform.rotation;
    }

    public void Update()
    {
        Vector2 relativeMousePosition = m_Mouse.position.ReadValue() / new Vector2(Screen.width, Screen.height);
        relativeMousePosition -= Vector2.one / 2f;
        
        m_Camera.transform.rotation = Quaternion.AngleAxis(relativeMousePosition.x * HorizontalAmplitude, Vector3.up) *
                                      Quaternion.AngleAxis(-relativeMousePosition.y * VerticalAmplitude, Vector3.right) * m_baseRotation;
    }
}
