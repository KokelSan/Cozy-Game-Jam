using System.Collections.Generic;

public class TVPuzzle : Puzzle
{
    private List<TogglePuzzleTriggered> triggered;
    public int PuzzleUnsolvedCount;
    
    private void Start()
    {
        foreach (TogglePuzzleTriggered trigger in triggered)
        {
            trigger.events.onSolved.AddListener(SolvePartOfPuzzle);
            trigger.events.onUnSolved.AddListener(UnSolvePartOfPuzzle);
        }
    }
    
    public void SolvePartOfPuzzle()
    {
        PuzzleUnsolvedCount--;

        if (PuzzleUnsolvedCount == 0)
        {
            events.onSolved?.Invoke();
        }
    }

    public void UnSolvePartOfPuzzle()
    {
        PuzzleUnsolvedCount++;
    
        if (PuzzleUnsolvedCount != 0)
        {
            events.onUnSolved?.Invoke();
        }
    }
}
