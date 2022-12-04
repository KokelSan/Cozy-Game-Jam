using System;
using UnityEngine;

public class RotationProblem : Problem
{
    public float RotationToSolveProblem;
    public float Delta;

    public void CheckRotation()
    {
        SetSolveStatus(transform.eulerAngles.y > RotationToSolveProblem - Delta && transform.eulerAngles.y < RotationToSolveProblem + Delta);
    }
}