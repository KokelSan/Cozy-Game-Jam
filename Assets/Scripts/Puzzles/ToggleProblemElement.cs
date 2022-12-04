using System;
using UnityEngine;
using UnityEngine.Events;

public enum ObjectType
{
    GameObject,
    MeshRenderer,
    Light,
    NONE
}

public class ToggleProblemElement : MonoBehaviour
{
    public ObjectType objectToToggleType;
    public GameObject objectToToggle;
    public UnityEvent OnToggleOn;
    public UnityEvent OnToggleOff;

    public  bool Value = false;

    public bool Toggle()
    {
        Value = !Value;

        switch (objectToToggleType)
        {
            case ObjectType.GameObject:
                objectToToggle.SetActive(Value);
                break;
            case ObjectType.MeshRenderer:
                objectToToggle.GetComponent<MeshRenderer>().enabled = Value;
                break;
            case ObjectType.Light:
                objectToToggle.GetComponent<Light>().enabled = Value;
                break;
            default:
                break;
        }
        
        if (Value)
            OnToggleOn?.Invoke();
        else
        {
            OnToggleOff?.Invoke();
        }

        return Value;
    }
}