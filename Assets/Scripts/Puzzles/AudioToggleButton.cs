using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class AudioToggleButton : InteractivePuzzleElement
{
    public UnityEvent OnClick;
    public AudioToggleProblem RelatedToggleProblem;
    public List<AudioToggleProblemElement> ElementToToggle;

    public override void Click()
    {
        Debug.Log("click");
        OnClick?.Invoke();
        RelatedToggleProblem.ToggleClips(ElementToToggle);
    }
}
