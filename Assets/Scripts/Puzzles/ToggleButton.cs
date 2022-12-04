using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ToggleButton : InteractivePuzzleElement
{
    public ToggleProblem RelatedToggleProblem;
    public List<ToggleProblemElement> ElementToToggle = new List<ToggleProblemElement>();

    public override void Click()
    {
        RelatedToggleProblem.ToggleElements(ElementToToggle);
    }
}
