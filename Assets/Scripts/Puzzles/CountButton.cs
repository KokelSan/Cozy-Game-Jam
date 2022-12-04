using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CountButton : InteractivePuzzleElement
{
    public CountProblem RelatedCountProblem;

    public override void Click()
    {
        RelatedCountProblem.IncrementCount();
    }
}
