using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ToggleButton : InteractivePuzzleElement
{
    public UnityEvent OnClick;
    public ToggleProblem RelatedToggleProblem;
    public List<ToggleProblemElement> ElementToToggle = new List<ToggleProblemElement>();

    public override void Click()
    {
        OnClick?.Invoke();
        RelatedToggleProblem.ToggleElements(ElementToToggle);
    }
}
