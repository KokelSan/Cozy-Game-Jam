using System.Collections.Generic;

public class TVPuzzle : Puzzle
{
    private List<TogglePuzzleTriggered> triggered;
    
    private void Start()
    {
        foreach (TogglePuzzleTriggered trigger in triggered)
        {
            //Strigger.events.onSolved.AddListener(trigger);
        }
    }
}
