using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{   
    public List<InteractivePuzzleElement> InteractiveElements = new List<InteractivePuzzleElement>();

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
}
