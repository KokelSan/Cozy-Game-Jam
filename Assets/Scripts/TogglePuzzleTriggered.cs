public class TogglePuzzleTriggered : Puzzle
{
    private bool m_CurrentState;
    public bool CurrentState => m_CurrentState;

    public void Toggle()
    {
        m_CurrentState = !m_CurrentState;
        
        if (m_CurrentState)
            events.onSolved?.Invoke();
        else
            events.onUnSolved?.Invoke();
    }
}
