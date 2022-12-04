using UnityEngine;

public class RotationElement : InteractivePuzzleElement
{
    public RotationProblem RelatedRotationProblem;

    private void Update()
    {
        RelatedRotationProblem.CheckRotation();
    }
}