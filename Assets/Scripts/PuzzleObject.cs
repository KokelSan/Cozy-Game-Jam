using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzleObject : MonoBehaviour, IInteractive
{    
    public Vector3 ColliderSizeWhenSelected;
    public float DepthOffsetWhenSelected;
    [Space]
    public float SelectionTranslationDuration;
    public float UnSelectionTranslationDuration;
    [Space]
    public bool RotateOnDrag;
    public float RotationSpeed;

    private Camera m_Camera;
    private BoxCollider m_Collider;
    private Vector3 m_InitialColliderSize;    
    private Vector3 m_InitialPosition;
    private Quaternion m_InitialRotation;
    private bool m_IsHeld;

    private void Start()
    {
        m_Camera = Camera.main;
        m_Collider = GetComponent<BoxCollider>();
        m_InitialColliderSize = m_Collider.size;
        m_InitialPosition = transform.position;
        m_InitialRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        if (m_IsHeld && RotateOnDrag)
        {
            transform.rotation *= Quaternion.Euler(0, - Mouse.current.delta.x.ReadValue() * RotationSpeed * Time.fixedDeltaTime , 0);
        }
    }

    public void Select()
    {
        m_Collider.size = ColliderSizeWhenSelected;
        StartCoroutine(MoveToPosition(m_Camera.transform.position + m_Camera.transform.forward * DepthOffsetWhenSelected, Quaternion.identity, SelectionTranslationDuration));
    }

    public void UnSelect()
    {
        m_Collider.size = m_InitialColliderSize;
        StartCoroutine(MoveToPosition(m_InitialPosition, m_InitialRotation, UnSelectionTranslationDuration));
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
            transform.position = Vector3.Lerp(m_InitialPosition, targetPosition, step/ translationTime);
            transform.rotation = Quaternion.Lerp(m_InitialRotation, targetRotation, step / translationTime);
            yield return new WaitForFixedUpdate();
        }        
    }
}
