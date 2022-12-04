//using System.Collections.Generic;
//using UnityEngine;

//public class MultipleProblem : Problem
//{
//    [SerializeField]
//    public List<Problem> problems = new List<Problem>();
//    private int m_ProblemUnsolvedCount;
    
//    private void Start()
//    {
//        foreach (Problem puzzle in problems)
//        {
//            puzzle.events.onSolved.AddListener(SolvePartOfPuzzle);
//            puzzle.events.onUnSolved.AddListener(UnSolvePartOfPuzzle);
//        }
//    }
    
//    public void SolvePartOfPuzzle()
//    {
//        m_ProblemUnsolvedCount--;

//        if (m_ProblemUnsolvedCount == 0)
//        {
//            events.onSolved?.Invoke();
//        }
//    }

//    public void UnSolvePartOfPuzzle()
//    {
//        m_ProblemUnsolvedCount++;
    
//        if (m_ProblemUnsolvedCount != 0)
//        {
//            events.onUnSolved?.Invoke();
//        }
//    }
//}
