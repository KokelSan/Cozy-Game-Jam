using UnityEngine;

public class RotationElement : InteractivePuzzleElement
{
    public RotationProblem RelatedRotationProblem;

    void Start()
    {
        RelatedRotationProblem.Init();
    }
    
    private void Update()
    {
        RelatedRotationProblem.CheckRotation();
    }
}