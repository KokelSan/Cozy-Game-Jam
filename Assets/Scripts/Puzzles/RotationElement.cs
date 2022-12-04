using UnityEngine;

public class RotationElement : InteractivePuzzleElement
{
    public RotationProblem RelatedRotationProblem;

    public override void Start()
    {
        base.Start();
        RelatedRotationProblem.Init();
    }
    
    private void Update()
    {
        RelatedRotationProblem.CheckRotation();
    }
}