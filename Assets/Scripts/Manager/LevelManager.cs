using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MultipleProblem
{
    [Serializable]
    public struct LevelManagerEvents
    {
        public UnityEvent onStart;
        public UnityEvent onWin;
    }
    
    public LevelManagerEvents LMevents;
    private ObjectManager ObjectManager;
    Puzzle m_CurrentPuzzle;
    public static LevelManager m_Instance;

    public Puzzle CurrentPuzzle
    {
        get => m_CurrentPuzzle;
        set
        {
            if (m_CurrentPuzzle && value != null)
                ObjectManager.UnSelectObject(m_CurrentPuzzle);
            m_CurrentPuzzle = value;
        }
    }
    
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
        problems = FindObjectsOfType<Problem>().ToList();
        ObjectManager = FindObjectOfType<ObjectManager>();
        events.onSolved.AddListener(Win);
        LMevents.onStart?.Invoke();
    }

    void Win()
    {
        LMevents.onWin?.Invoke();
    }
    
}
