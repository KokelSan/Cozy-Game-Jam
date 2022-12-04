using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CountButton : InteractivePuzzleElement
{
    public UnityEvent OnClick;
    public CountProblem RelatedCountProblem;

    public override void Click()
    {
        OnClick?.Invoke();
        RelatedCountProblem.IncrementCount();
    }
}
