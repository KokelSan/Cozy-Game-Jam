using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectManager : MonoBehaviour
{    
    public IInteractive CurrentSelectedObject => m_CurrentSelectedObject;
    private IInteractive m_CurrentSelectedObject = null;

    public bool TryGetCurrentSelectedObject(out IInteractive puzzleObject)
    {
        puzzleObject = m_CurrentSelectedObject;
        return m_CurrentSelectedObject != null;
    }

    public void SelectObject(IInteractive puzzleObject)
    {
        m_CurrentSelectedObject = puzzleObject;
        m_CurrentSelectedObject.Select();
    }

    public void UnselectCurrentObject()
    {
        if (m_CurrentSelectedObject != null)
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
