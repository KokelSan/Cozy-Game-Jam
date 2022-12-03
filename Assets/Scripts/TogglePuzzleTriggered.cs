
using UnityEngine;

public class TogglePuzzleTriggered : MonoBehaviour, ISolvable
{
    private bool m_CurrentState;
    public bool CurrentState => m_CurrentState;

    public void Toggle()
    {
        m_CurrentState = !m_CurrentState;
        LevelManager.Instance.ChangePuzzleStatus(this);
    }

    public bool IsSolved()
    {
        return m_CurrentState;
    }
}
