using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    public enum EContext
    {
        MainMenu,
        GamePlay
    }

    [Serializable]
    struct ContextualPosition
    {
        public Transform position;
        public EContext context;
        public float transitionDuration;
    }

    private EContext m_CurrentContext;
    
    [SerializeField]
    private List<ContextualPosition> m_ContextualPositions;
    public AnimationCurve m_MovementCurve;
    
    private Camera m_Camera;
    private Mouse m_Mouse;
    
    public float VerticalAmplitude;
    public float HorizontalAmplitude;
    public bool IsMoving;

    private Quaternion m_baseRotation;
    
    void Awake()
    {
        m_Camera = GetComponent<Camera>();
        m_Mouse = Mouse.current;
        m_baseRotation = m_Camera.transform.rotation;
    }

    public void GoToGameContext()
    {
        GoToContext(EContext.GamePlay);
    }

    public void GoToMenuContext()
    {
        GoToContext(EContext.MainMenu);
    }

    public void GoToContext(EContext context)
    {
        for (int i = 0; i < m_ContextualPositions.Count; i++)
        {
            if (m_ContextualPositions[i].context == context)
            {
                Transform target = m_ContextualPositions[i].position;
                StartCoroutine(MoveToPosition(target.position, target.rotation, m_ContextualPositions[i].transitionDuration));
            }
        }
    }

    public void Update()
    {
        if (!IsMoving)
        {
            Vector2 relativeMousePosition = m_Mouse.position.ReadValue() / new Vector2(Screen.width, Screen.height);
            relativeMousePosition -= Vector2.one / 2f;

            m_Camera.transform.rotation =
                Quaternion.AngleAxis(relativeMousePosition.x * HorizontalAmplitude, Vector3.up) *
                Quaternion.AngleAxis(-relativeMousePosition.y * VerticalAmplitude, Vector3.right) * m_baseRotation;
        }
    }
    
    private IEnumerator MoveToPosition(Vector3 targetPosition, Quaternion targetRotation, float translationTime)
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        float step = 0;
        IsMoving = true;
        while (step < translationTime)
        {
            step += Time.fixedDeltaTime;

            float t = m_MovementCurve.Evaluate(step / translationTime);
            transform.position = Vector3.LerpUnclamped(startPos, targetPosition, t);
            transform.rotation = Quaternion.SlerpUnclamped(startRot, targetRotation, t);
            yield return new WaitForFixedUpdate();
        }

        IsMoving = false;
        m_baseRotation = transform.rotation;
    }
}
