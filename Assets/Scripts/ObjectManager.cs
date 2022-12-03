using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectManager : MonoBehaviour
{    
    public Interactive CurrentSelectedObject => m_CurrentSelectedObject;
    private Interactive m_CurrentSelectedObject = null;

    public bool TryGetCurrentSelectedObject(out Interactive puzzleObject)
    {
        puzzleObject = m_CurrentSelectedObject;
        return m_CurrentSelectedObject != null;
    }

    public void SelectObject(Interactive puzzleObject)
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
