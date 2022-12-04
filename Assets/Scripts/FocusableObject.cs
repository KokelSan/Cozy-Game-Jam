using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FocusableObject : MonoBehaviour
{
    public bool MoveParentTransform;
    public float DepthOffsetWhenFocused;
    public float FocusTranslationDuration;
    public float UnFocusTranslationDuration;
    public float RotationSpeed;

    private Camera m_Camera;
    private Mouse m_Mouse;
    private Transform m_TransformToMove;
    private Vector3 m_InitialPosition;   
    private Quaternion m_InitialRotation;

    private bool m_IsHeld;

    private void Start()
    {
        m_Camera = Camera.main;
        m_Mouse = Mouse.current;

        if (MoveParentTransform)
        {
            m_TransformToMove = transform.parent;
            m_InitialPosition = m_TransformToMove.position;
            m_InitialRotation = m_TransformToMove.rotation;
        }
        else
        {
            m_TransformToMove = transform;
            m_InitialPosition = transform.position;
            m_InitialRotation = transform.rotation;
        }        
    }

    private void FixedUpdate()
    {
        if (m_IsHeld)
        {
            transform.rotation *= Quaternion.Euler(0, - m_Mouse.delta.x.ReadValue() * RotationSpeed * Time.fixedDeltaTime, 0);
        }
    }

    public void Focus()
    {
        StartCoroutine(MoveToPosition(m_Camera.transform.position + m_Camera.transform.forward * DepthOffsetWhenFocused, Quaternion.identity, FocusTranslationDuration));
    }

    public void UnFocus()
    {
        if (m_IsHeld) m_IsHeld = false;
        StartCoroutine(MoveToPosition(m_InitialPosition, m_InitialRotation, UnFocusTranslationDuration));
    }

    public void Drag(bool isHeld)
    {
        m_IsHeld = isHeld;
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition, Quaternion targetRotation, float translationTime)
    {
        float step = 0;
        while (step < translationTime)
        {
            step += Time.fixedDeltaTime;
            m_TransformToMove.position = Vector3.Lerp(m_InitialPosition, targetPosition, step / translationTime);
            m_TransformToMove.rotation = Quaternion.Lerp(m_InitialRotation, targetRotation, step / translationTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
