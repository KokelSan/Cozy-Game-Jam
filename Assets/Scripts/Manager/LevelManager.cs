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

    public MultiplePuzzle puzzles;
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
        puzzles.puzzles = FindObjectsOfType<Puzzle>().ToList();
        puzzles.events.onSolved.AddListener(Win);
        events.onStart?.Invoke();
    }

    void Win()
    {
        events.onWin?.Invoke();
    }
    
}
