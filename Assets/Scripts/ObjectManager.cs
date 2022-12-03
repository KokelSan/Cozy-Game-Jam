using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectManager : MonoBehaviour
{    
    public PuzzleObject CurrentSelectedObject => m_CurrentSelectedObject;
    private PuzzleObject m_CurrentSelectedObject = null;

    public bool TryGetCurrentSelectedObject(out PuzzleObject puzzleObject)
    {
        puzzleObject = m_CurrentSelectedObject;
        return m_CurrentSelectedObject != null;
    }

    public void SelectObject(PuzzleObject puzzleObject)
    {
        m_CurrentSelectedObject = puzzleObject;
        m_CurrentSelectedObject.Select();
    }

    public void UnselectCurrentObject()
    {
        if (m_CurrentSelectedObject)
        {
            Debug.Log("UnSelecting current puzzle object");
            m_CurrentSelectedObject.UnSelect();
            m_CurrentSelectedObject = null;
        }
    }

    public void HoldCurrentObject(bool isHolding)
    {
        m_CurrentSelectedObject.Drag(isHolding);
    }
}
