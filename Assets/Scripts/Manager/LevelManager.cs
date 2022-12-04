using System;
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
    Problem[] problems;
    public static LevelManager m_Instance;
    private int m_ProblemUnsolvedCount;

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
        events.onStart?.Invoke();
        problems = FindObjectsOfType<Problem>();

        m_ProblemUnsolvedCount = problems.Length;
        foreach (Problem problem in problems)
        {
            problem.onSolved.AddListener(OnProblemSolved);
            problem.onUnSolved.AddListener(OnProblemUnsolved);
        }
    }

    void OnProblemSolved()
    {
        m_ProblemUnsolvedCount--;
        
        if (m_ProblemUnsolvedCount <= 0)
            Win();
    }
    
    void OnProblemUnsolved()
    {
        m_ProblemUnsolvedCount++;
    }

    void Win()
    {
        events.onWin?.Invoke();
    }    
}
