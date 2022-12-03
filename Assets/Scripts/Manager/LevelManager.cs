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
    
    public Puzzle[] Puzzles;
    public int PuzzleUnsolvedCount;
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
        Puzzles = FindObjectsOfType<Puzzle>();
        events.onStart?.Invoke();
    }

    public void ChangePuzzleStatus(Puzzle puzzle)
    {
        if (puzzle.IsSolved())
        {
            PuzzleUnsolvedCount++;
        }
        else
        {
            PuzzleUnsolvedCount--;
        }

        if (PuzzleUnsolvedCount == 0)
        {
            events.onWin?.Invoke();
        }
    }
}
