using System;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour
{
    [Serializable]
    public struct Events
    {
        public UnityEvent onSolved;
        public UnityEvent onUnSolved;
        public UnityEvent onActivated;
        public UnityEvent onDisable;
    }
    
    public Events events;
    private bool m_IsActive;
    public bool IsActive => m_IsActive;
    
    void SetActive(bool state)
    {
        if (state)
        
        
        m_IsActive = state;
    }
}
