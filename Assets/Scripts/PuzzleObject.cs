using System.Collections;
using UnityEngine;

public class PuzzleObject : MonoBehaviour
{    
    public Vector3 ColliderSizeWhenSelected;
    public float DepthOffsetWhenSelected;
    public float SelectionTranslationDuration;
    public float UnSelectionTranslationDuration;

    private Camera m_Camera;
    private BoxCollider m_Collider;
    private Vector3 m_InitialColliderSize;    
    private Vector3 m_InitialPosition;
    private Quaternion m_InitialRotation;

    private void Start()
    {
        m_Camera = Camera.main;
        m_Collider = GetComponent<BoxCollider>();
        m_InitialColliderSize = m_Collider.size;
        m_InitialPosition = transform.position;
        m_InitialRotation = transform.rotation;
    }

    public void Select()
    {
        m_Collider.size = ColliderSizeWhenSelected;
        StartCoroutine(MoveToPosition(m_Camera.transform.position + m_Camera.transform.forward * DepthOffsetWhenSelected, SelectionTranslationDuration));
    }

    public void UnSelect()
    {
        m_Collider.size = m_InitialColliderSize;
        StartCoroutine(MoveToPosition(m_InitialPosition, UnSelectionTranslationDuration));
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition, float translationTime)
    {
        float step = 0;
        while (step < translationTime)
        {
            step += Time.fixedDeltaTime;
            transform.position = Vector3.Lerp(m_InitialPosition, targetPosition, step/ translationTime);
            yield return new WaitForFixedUpdate();
        }        
    }
}
