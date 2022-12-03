using System.Collections.Generic;
using UnityEngine;

public class MultiplePuzzle : Puzzle
{
    [SerializeField]
    public List<Puzzle> puzzles = new List<Puzzle>();
    private int m_PuzzleUnsolvedCount;
    
    private void Start()
    {
        foreach (Puzzle puzzle in puzzles)
        {
            puzzle.events.onSolved.AddListener(SolvePartOfPuzzle);
            puzzle.events.onUnSolved.AddListener(UnSolvePartOfPuzzle);
        }
    }
    
    public void SolvePartOfPuzzle()
    {
        m_PuzzleUnsolvedCount--;

        if (m_PuzzleUnsolvedCount == 0)
        {
            events.onSolved?.Invoke();
        }
    }

    public void UnSolvePartOfPuzzle()
    {
        m_PuzzleUnsolvedCount++;
    
        if (m_PuzzleUnsolvedCount != 0)
        {
            events.onUnSolved?.Invoke();
        }
    }
}
