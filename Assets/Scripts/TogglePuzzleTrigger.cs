using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TogglePuzzleTrigger : MonoBehaviour, IInteractive
{
    [Serializable]
    public struct Events
    {
        public UnityEvent onSelected;
    }
    
    public Events events;

    [SerializeField]
    private readonly List<TogglePuzzleTriggered> m_TogglePuzzleTriggeredList = new List<TogglePuzzleTriggered>(); 

    public void Select()
    {
        foreach (TogglePuzzleTriggered TogglePuzzleTriggered in m_TogglePuzzleTriggeredList)
        {
            TogglePuzzleTriggered.Toggle();
        }
        
        events.onSelected?.Invoke();
    }

    public void UnSelect()
    {
    }

    public void Drag(bool isHeld)
    {
    }
}
