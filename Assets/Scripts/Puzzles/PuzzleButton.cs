using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleButton : MonoBehaviour, IInteractive
{
    [Serializable]
    public struct Events
    {
        public UnityEvent onTrigger;
    }
    
    public Events events;

    [SerializeField]
    private readonly List<TogglePuzzle> m_TogglePuzzles = new List<TogglePuzzle>(); 

    public void Select()
    {
        foreach (TogglePuzzle m_TogglePuzzle in m_TogglePuzzles)
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
}
