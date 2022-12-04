using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
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
    private Outline m_outline;

    private void Awake()
    {
        m_outline = GetComponent<Outline>();
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

    public void Overing(bool flag)
    {
        events.onOvering?.Invoke();
        m_outline.enabled = flag;
    }
}
