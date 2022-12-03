using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectManager : MonoBehaviour
{    
    private List<Interactive> m_PreviousSelection = new List<Interactive>();
    private List<Interactive> m_CurrentSelectedObjects = new List<Interactive>();

    private Interactive m_ObjectHolded;
    
    private List<Interactive> m_PreviousHighLight = new List<Interactive>();
    private List<Interactive> m_CurrentHighlightedObjects = new List<Interactive>();

    public bool TryGetCurrentSelectedObject(out Interactive[] puzzleObject)
    {
        puzzleObject = m_CurrentSelectedObjects.ToArray();
        return m_CurrentSelectedObjects != null;
    }

    public void SelectObject(Interactive puzzleObject)
    {
        m_CurrentSelectedObjects.Add(puzzleObject);
        
        if (!m_PreviousSelection.Contains(puzzleObject))
            puzzleObject.Select();
    }

    public void ProcessUnselectObject()
    {
        List<Interactive> deselectionList = m_PreviousSelection.Except(m_CurrentSelectedObjects).ToList();

        foreach (Interactive obj in deselectionList)
        {
            Debug.Log("UnSelecting current puzzle object");
            obj.UnSelect();
        }

        m_PreviousSelection = new List<Interactive>(m_CurrentSelectedObjects);
        m_CurrentSelectedObjects.Clear();
    }

    public void DrapObject(Interactive puzzleObject)
    {
        m_ObjectHolded = puzzleObject;
        m_ObjectHolded.Drag(true);
    }
    
    public void DropObject()
    {
        if (m_ObjectHolded)
        {
            m_ObjectHolded.Drag(false);
            m_ObjectHolded = null;
        }
    }
    
    public void HighlightObject(Interactive obj)
    {
        m_CurrentHighlightedObjects.Add(obj);
        
        if (!m_PreviousHighLight.Contains(obj))
            obj.Highlight(true);
    }
    
    public void ProcessMouseNotOver()
    {
        List<Interactive> deselectionList = m_PreviousHighLight.Except(m_CurrentHighlightedObjects).ToList();

        foreach (Interactive obj in deselectionList)
        {
            Debug.Log("Is not over object");
            obj.Highlight(false);
        }

        m_PreviousHighLight = new List<Interactive>(m_CurrentHighlightedObjects);
        m_CurrentHighlightedObjects.Clear();
    }
}
