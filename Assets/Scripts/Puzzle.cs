using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour
{   
    [Serializable]
    public struct Events
    {
        public UnityEvent onSelect;
        public UnityEvent onUnSelect;
        public UnityEvent onOvering;
    }
    
    public Events events;
    
    public List<InteractivePuzzleElement> InteractiveElements = new List<InteractivePuzzleElement>();
    public BoxCollider[] m_Colliders;

    private InteractivePuzzleElement m_OverredElement = null;     
    private Outline m_Outline;

    private void Start()
    {
        m_Outline = GetComponent<Outline>();
    }

    public virtual void Select()
    {
        events.onSelect?.Invoke();
        foreach (InteractivePuzzleElement element in InteractiveElements)
        {
            element.SetActive(true);
        }        
    }

    public virtual void UnSelect()
    {
        events.onUnSelect?.Invoke();
        foreach (InteractivePuzzleElement element in InteractiveElements)
        {
            element.SetActive(false);
        }
    }

    public bool CheckIfElementBelongsToPuzzle(InteractivePuzzleElement element)
    {
        return InteractiveElements.Contains(element);
    }

    public void SetPuzzleActive(bool state)
    {
        events.onOvering?.Invoke();

        foreach (BoxCollider col in m_Colliders)
        {
            col.enabled = state;
        }   
    }

    public bool ManageOverring(Ray ray) 
    {
        if (m_Outline)
        {
            m_Outline.enabled = true;
            return true;
        }
        else
        {
            return OverInteractiveElement(ray);
        }        
    }

    private bool OverInteractiveElement(Ray ray)
    {
        Select();

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out InteractivePuzzleElement element))
            {
                if (m_OverredElement)
                {
                    m_OverredElement.Over(false);
                }

                element.Over(true);
                m_OverredElement = element;
            }
        }

        UnSelect();

        return m_OverredElement != null;
    }

    public void StopOverring()
    {
        if (m_Outline)
        {
            m_Outline.enabled = false;
        }
        else if (m_OverredElement)
        {
            m_OverredElement.Over(false);
            m_OverredElement = null;
        }
    }
}
