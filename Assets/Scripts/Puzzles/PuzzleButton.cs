using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PuzzleButton : MonoBehaviour, IInteractive
{
    [Serializable]
    public struct Events
    {
        public UnityEvent onTrigger;
    }
    
    public Events events;

    [SerializeField]
    private List<ToggleProblem> m_TogglePuzzles = new List<ToggleProblem>();

    private Collider m_Collider;

    private void Awake()
    {
        m_Collider = GetComponent<Collider>();
    }

    public void Select()
    {
        foreach (ToggleProblem m_TogglePuzzle in m_TogglePuzzles)
        {
            m_TogglePuzzle.Toggle();
        }
        
        events.onTrigger?.Invoke();
    }

    public void UnSelect()
    {
    }

    public void Drag(bool isHeld)
    {
    }

    public bool IsActive()
    {
        return m_Collider.enabled;
    }

    public void SetActive(bool flag)
    {
        m_Collider.enabled = flag;
    }
}
