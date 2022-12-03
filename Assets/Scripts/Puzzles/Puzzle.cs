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
    }
    
    public Events events;
}
