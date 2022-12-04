using System.Collections.Generic;

public class ToggleProblem : Problem
{
    public int ToggleElementCount;

    private int TrueElementCount = 0;

    public void ToggleElements(List<ToggleProblemElement> list)
    {
        int negativeCount = 0;
        int positiveCount = 0;

        foreach (ToggleProblemElement item in list)
        {
            if (item.Toggle())
            {
                positiveCount++;
            }
            else
            {
                negativeCount++;
            }
        }
        UpdateTrueCount(negativeCount, positiveCount);
    }

    public void UpdateTrueCount(int negativeCount, int positiveCount)
    {
        TrueElementCount -= negativeCount;
        TrueElementCount += positiveCount;
        if (TrueElementCount == ToggleElementCount)
        {
            SolveProblem();
        }
    }
}