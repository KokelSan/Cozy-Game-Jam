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
    
    public ISolvable[] Solvables;
    public int SolvableUnsolvedCount;
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
        Solvables = FindObjectsOfType<MonoBehaviour>().OfType<ISolvable>().ToArray();
        
        events.onStart?.Invoke();
    }

    public void ChangePuzzleStatus(ISolvable solvable)
    {
        if (solvable.IsSolved())
        {
            SolvableUnsolvedCount++;
        }
        else
        {
            SolvableUnsolvedCount--;
        }

        if (SolvableUnsolvedCount == 0)
        {
            events.onWin?.Invoke();
        }
    }
}
