
using System;
using UnityEngine;
using UnityEngine.Events;

public class TogglePuzzleTriggered : MonoBehaviour, ISolvable
{
    [Serializable]
    public struct Events
    {
        public UnityEvent onSolved;
    }
    
    public Events events;
    private bool m_CurrentState;
    public bool CurrentState => m_CurrentState;

    public void Toggle()
    {
        m_CurrentState = !m_CurrentState;
        if (m_CurrentState)
            events.onSolved?.Invoke();
    }

    public bool IsSolved()
    {
        return m_CurrentState;
    }
}
