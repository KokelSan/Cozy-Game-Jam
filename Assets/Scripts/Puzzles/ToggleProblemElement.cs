using System;
using UnityEngine;

public enum ObjectType
{
    GameObject,
    MeshRenderer,
    Light
}

public class ToggleProblemElement : MonoBehaviour
{
    public ObjectType objectToToggleType;
    public GameObject objectToToggle;

    private bool Value = false;

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

        return Value;
    }
}