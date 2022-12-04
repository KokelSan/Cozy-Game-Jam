public class CountProblem : Problem
{
    public int ClickNumberToSolveProblem;

    private int ClickCount = 0;

    public void IncrementCount()
    {
        ClickCount++;

        SetSolveStatus(ClickCount == ClickNumberToSolveProblem);
    }
}