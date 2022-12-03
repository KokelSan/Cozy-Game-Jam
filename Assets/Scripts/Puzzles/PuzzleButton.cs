using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PuzzleButton : Interactive
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

    public override void Click()
    {
        foreach (ToggleProblem m_TogglePuzzle in m_TogglePuzzles)
        {
            m_TogglePuzzle.Toggle();
        }
        
        events.onTrigger?.Invoke();
    }

    public override bool IsActive()
    {
        return m_Collider.enabled;
    }

    public override void SetActive(bool flag)
    {
        m_Collider.enabled = flag;
    }
}
