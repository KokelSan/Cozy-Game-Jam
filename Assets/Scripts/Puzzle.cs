using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Puzzle : MonoBehaviour
{   
    public List<InteractivePuzzleElement> InteractiveElements = new List<InteractivePuzzleElement>();
    private Outline m_outline;

    private void Awake()
    {
        m_outline = GetComponent<Outline>();
    }

    public virtual void Select()
    {
        foreach (InteractivePuzzleElement element in InteractiveElements)
        {
            element.SetActive(true);
        }        
        //LevelManager.Instance.CurrentPuzzle = this;
    }

    public virtual void UnSelect()
    {
        foreach (InteractivePuzzleElement element in InteractiveElements)
        {
            element.SetActive(false);
        }
        //LevelManager.Instance.CurrentPuzzle = null;
    }

    public bool CheckIfElementBelongsToPuzzle(InteractivePuzzleElement element)
    {
        return InteractiveElements.Contains(element);
    }

    public void Overing(bool flag)
    {
        m_outline.enabled = flag;
    }
}
