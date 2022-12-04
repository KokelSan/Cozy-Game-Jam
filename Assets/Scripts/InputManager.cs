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
    private Puzzle m_OveredPuzzle = null;

    public bool log;

    private void Start()
    {
        m_Camera = Camera.main;
        m_Mouse = Mouse.current;
    }

    public void OnLeftClick(InputValue input)
    {
        if (log) Debug.Log("Click");

        if (input.isPressed)
        {
            Vector2 mousePosition = m_Mouse.position.ReadValue();
            Ray ray = m_Camera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y));
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (log) Debug.Log("Collision");

                bool isImportantObject = false;

                if (hitInfo.collider.TryGetComponent(out FocusableObject focusableObject))
                {
                    if (focusableObject != m_FocusedObject)
                    {
                        focusableObject.Focus();
                        m_FocusedObject = focusableObject;
                    }

                    m_FocusedObject.Drag(true);
                    isImportantObject = true;
                }

                if (hitInfo.collider.TryGetComponent(out Puzzle puzzle))
                {
                    if (puzzle != m_SelectedPuzzle)
                    {
                        if (log) Debug.Log("Selecting object");
                        puzzle.Select();
                        m_SelectedPuzzle = puzzle;
                        isImportantObject = true;
                    }
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
                if (log) Debug.Log("No collision");
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
    }

    public void OnLook(InputValue input)
    {
        Vector2 mousePosition = m_Mouse.position.ReadValue();
        Ray ray = m_Camera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y));

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out Puzzle puzzle))
            {
                if (log) Debug.Log("Overing object");
                m_OveredPuzzle = puzzle;
                m_OveredPuzzle.Overing(true);
            }
        }
        else if (m_OveredPuzzle != null)
        {
            if (log) Debug.Log("Overing finished");
            m_OveredPuzzle.Overing(false);
        }
    }
}
