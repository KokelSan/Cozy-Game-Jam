using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Serializable]
    public struct Events
    {
        public UnityEvent onClickNotValid;
    }

    public Events events;

    private Camera m_Camera;
    private Mouse m_Mouse;

    private Puzzle m_SelectedPuzzle = null;
    private FocusableObject m_FocusedObject = null;
    private Puzzle m_OverredPuzzle = null;

    public bool log;

    private void Start()
    {
        m_Camera = Camera.main;
        m_Mouse = Mouse.current;
    }

    public void OnLeftClick(InputValue input)
    {
        if (input.isPressed)
        {
            Vector2 mousePosition = m_Mouse.position.ReadValue();
            Ray ray = m_Camera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y));
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                bool isImportantObject = false;

                if (hitInfo.collider.TryGetComponent(out Puzzle puzzle))
                {
                    if (puzzle != m_SelectedPuzzle)
                    {
                        if (log) Debug.Log("Selecting new puzzle");
                        UnSelectObjects();                        
                        puzzle.Select();
                        m_SelectedPuzzle = puzzle;
                        isImportantObject = true;
                    }

                    if (CheckIfFocusable(hitInfo.collider.gameObject))
                    {
                        if (log) Debug.Log("Focusable puzzle");
                        isImportantObject = true;
                    }
                }
                else if(CheckIfFocusable(hitInfo.collider.gameObject))
                {
                    if (log) Debug.Log("Only focusable");
                    isImportantObject = true;
                }                

                if (m_SelectedPuzzle && hitInfo.collider.TryGetComponent(out InteractivePuzzleElement puzzleElement))
                {
                    if (m_SelectedPuzzle.CheckIfElementBelongsToPuzzle(puzzleElement))
                    {
                        if (log) Debug.Log("Clickin on interactive element");
                        puzzleElement.Click();
                        isImportantObject = true;
                    }
                }

                if (!isImportantObject)
                {
                    if (log) Debug.Log("Not important object");
                    UnSelectObjects();
                }
            }
            else
            {
                UnSelectObjects();
                
                events.onClickNotValid?.Invoke();
            }
        }
        else
        {
            if (m_FocusedObject)
            {
                if (log) Debug.Log("Dropping");
                m_FocusedObject.Drag(false);
            }
        }
    }

    public void UnSelectObjects()
    {
        if (m_SelectedPuzzle)
        {
            m_SelectedPuzzle.UnSelect();
            m_SelectedPuzzle = null;
        }

        if (m_FocusedObject)
        {
            m_FocusedObject.UnFocus();
            m_FocusedObject = null;
        }

        if (m_OverredPuzzle)
        {
            m_OverredPuzzle.StopOverring();
            m_OverredPuzzle = null;
        }
    }

    public bool CheckIfFocusable(GameObject objectToCheck)
    {
        if (objectToCheck.TryGetComponent(out FocusableObject focusObject))
        {
            if (focusObject != m_FocusedObject)
            {
                focusObject.Focus();
                m_FocusedObject = focusObject;
                return true;
            }
            else if(m_FocusedObject)
            {
                if (log) Debug.Log("Dragging");
                m_FocusedObject.Drag(true);
                return true;
            }
        }
        return false;
    }

    public void OnLook(InputValue input)
    {
        Vector2 mousePosition = m_Mouse.position.ReadValue();
        Ray ray = m_Camera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y));

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out Puzzle puzzle))
            {
                if (m_FocusedObject == null && m_SelectedPuzzle != puzzle)
                {
                    if (puzzle.ManageOverring(ray))
                    {
                        m_OverredPuzzle = puzzle;
                    }                    
                }
            }
            else if(m_OverredPuzzle != null)
            {
                StopOverring();
            }
        }
        else if (m_OverredPuzzle != null)
        {
            StopOverring();
        }
    }

    private void StopOverring()
    {
        m_OverredPuzzle.StopOverring();
        m_OverredPuzzle = null;
    }
}
