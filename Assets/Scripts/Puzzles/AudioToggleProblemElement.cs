using System;
using UnityEngine;
using UnityEngine.Events;

public class AudioToggleProblemElement : MonoBehaviour
{
    public AudioSource AudioSource;

    private bool Value = false;

    public bool Toggle()
    {
        Debug.Log("toggle");
        Value = !Value;
        
        if (Value)
        {
            AudioSource.Play();
        }
        else
        {
            AudioSource.Pause();
        }

        return Value;
    }
}