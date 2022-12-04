using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FocusableObject : MonoBehaviour
{
    [Serializable]
    public struct Events
    {
        public UnityEvent onFocus;
    }
    
    public Events events;
    
    public bool MoveParentTransform;
    public bool LerpRotation;
    public float DepthOffsetWhenFocused;
    public float FocusTranslationDuration;
    public float UnFocusTranslationDuration;
    public float RotationSpeed;

    private Camera m_Camera;
    private Mouse m_Mouse;
    private Transform m_TransformToMove;
    private Vector3 m_InitialPosition;   
    private Quaternion m_InitialRotation;

    public AnimationCurve m_FocusMovementAnimation;

    public bool IsHeld => m_IsHeld;
    [SerializeField]
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
            m_InitialPosition = m_TransformToMove.position;
            m_InitialRotation = m_TransformToMove.rotation;
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
        events.onFocus?.Invoke();
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
        Vector3 startPos = m_TransformToMove.position;
        Quaternion startRot = m_TransformToMove.rotation;
        float step = 0;
        while (step < translationTime)
        {
            step += Time.fixedDeltaTime;

            float t = m_FocusMovementAnimation.Evaluate(step / translationTime);
            m_TransformToMove.position = Vector3.LerpUnclamped(startPos, targetPosition, t);
            if (LerpRotation)
                m_TransformToMove.rotation = Quaternion.SlerpUnclamped(startRot, targetRotation, t);
            yield return new WaitForFixedUpdate();
        }
    }
}
