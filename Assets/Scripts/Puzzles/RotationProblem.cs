using System;
using UnityEngine;

public class RotationProblem : Problem
{
    public float RotationToSolveProblem;
    public float Delta;
    private float m_startAngle;

    public void Init()
    {
        m_startAngle = Mathf.Abs(transform.eulerAngles.y);
    }
    
    public void CheckRotation()
    {
        if (Mathf.Abs(m_startAngle - Mathf.Abs(transform.eulerAngles.y) - RotationToSolveProblem) < Delta)
        {
            SolveProblem();
        }
    }
}