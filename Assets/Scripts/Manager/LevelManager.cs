using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [Serializable]
    public struct Events
    {
        public UnityEvent onStart;
        public UnityEvent onWin;
    }
    
    public Events events;

    public MultipleProblem problems;
    public static LevelManager m_Instance;
    public static LevelManager Instance
    {
        get
        {
            return m_Instance;
        }
    }
    
    void Start()
    {
        m_Instance = this;
        problems.problems = FindObjectsOfType<Problem>().ToList();
        problems.events.onSolved.AddListener(Win);
        events.onStart?.Invoke();
    }

    void Win()
    {
        events.onWin?.Invoke();
    }
    
}
