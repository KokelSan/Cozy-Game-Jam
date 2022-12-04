using UnityEngine;
using UnityEngine.Events;

public class Problem : MonoBehaviour
{
    public UnityEvent onSolved;

    public virtual void SolveProblem() 
    {
        onSolved?.Invoke();
    }
}
