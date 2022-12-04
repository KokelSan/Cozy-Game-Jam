using System;
using UnityEngine;

public class RotationProblem : Problem
{
    public FocusableObject FocusableObject;
    public float RotationToSolveProblem;
    public float Delta;

    private void Update()
    {
        if (FocusableObject.IsHeld)
        {
            CheckRotation();
        }
    }

    public void CheckRotation()
    {
        SetSolveStatus(transform.eulerAngles.y > RotationToSolveProblem - Delta && transform.eulerAngles.y < RotationToSolveProblem + Delta);
    }
}