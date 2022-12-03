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

    public bool TryGetCurrentSelectedObject(out Interactive[] obj)
    {
        obj = m_CurrentSelectedObjects.ToArray();
        return m_CurrentSelectedObjects != null;
    }

    public void SelectObject(Interactive obj)
    {
        m_CurrentSelectedObjects.Add(obj);
        
        if (!m_PreviousSelection.Contains(obj))
            obj.Select();
    }
    
    public void UnSelectObject(Interactive obj)
    {
        m_PreviousSelection.Remove(obj);
        m_CurrentSelectedObjects.Remove(obj);
        obj.UnSelect();
    }

    public void ProcessUnselectObject()
    {
        List<Interactive> deselectionList = m_PreviousSelection.Except(m_CurrentSelectedObjects).ToList();

        foreach (Interactive obj in deselectionList)
        {
            obj.UnSelect();
        }

        m_PreviousSelection = new List<Interactive>(m_CurrentSelectedObjects);
        m_CurrentSelectedObjects.Clear();
    }

    public void DrapObject(Interactive obj)
    {
        m_ObjectHolded = obj;
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
            obj.Highlight(false);
        }

        m_PreviousHighLight = new List<Interactive>(m_CurrentHighlightedObjects);
        m_CurrentHighlightedObjects.Clear();
    }
}
