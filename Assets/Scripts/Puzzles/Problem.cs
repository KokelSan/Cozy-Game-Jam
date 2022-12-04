using System;
using UnityEngine;
using UnityEngine.Events;

public class Problem : MonoBehaviour
{
    public UnityEvent onSolved;
    public UnityEvent onUnSolved;
    private bool isSolve = false;
    
    public void SetSolveStatus(bool flag)
    {
        if (flag == isSolve)
            return;
        
        if (flag)
        {
            onSolved?.Invoke();
            isSolve = true;
        }
        else
        {
            onUnSolved?.Invoke();
            isSolve = false;
        }
    }
}
