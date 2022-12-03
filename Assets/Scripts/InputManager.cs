using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private ObjectManager m_ObjectManager;
    private Camera m_Camera;
    private bool m_IsHolding = false;

    private void Start()
    {
        m_Camera = Camera.main;
        m_ObjectManager = FindObjectOfType<ObjectManager>();
    }

    public void OnClick(InputValue input)
    {
        Ray ray = m_Camera.ScreenPointToRay(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue()));
        
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out IInteractive interactiveSelected))
            {
                if (m_ObjectManager.TryGetCurrentSelectedObject(out IInteractive SelectedObject))
                {
                    if (interactiveSelected == SelectedObject)
                    {
                        Debug.Log("Clicking on current puzzle object");
                        // ObjectManager.Whatever...
                    }
                    else
                    {
                        m_ObjectManager.UnselectCurrentObject();
                    }
                }
                else
                {
                    if (hitInfo.collider.TryGetComponent(out PuzzleObject puzzleObject))
                    {
                        Debug.Log("Selecting a new puzzle object");
                        m_ObjectManager.SelectObject(puzzleObject);
                    }
                }
            }
        }
        else
        {
            m_ObjectManager.UnselectCurrentObject();
        }
        
    }

    public void OnHold(InputValue input)
    {
        if (m_IsHolding)
        {
            Debug.Log("Releasing current object");
            m_ObjectManager.HoldCurrentObject(false);
            m_IsHolding = false;
        }
        else
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            if (CheckIfMousePointCurrentObject(mousePosition) && Mouse.current.leftButton.isPressed)
            {
                Debug.Log("Holding current object");
                m_IsHolding = true;                
                m_ObjectManager.HoldCurrentObject(true);
            }
        }               
    }

    public void OnLook(InputValue input)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        if (CheckIfMousePointPuzzleObject(mousePosition, out PuzzleObject pointedObject))
        {
            if (pointedObject != m_ObjectManager.CurrentSelectedObject)
            {
                Debug.Log("Mouse over a puzzle object");
                // Display glow/outline
            }
        }
    }

    private bool CheckIfMousePointPuzzleObject(Vector2 mousePosition, out PuzzleObject pointedObject)
    {
        Ray ray = m_Camera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y));

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out PuzzleObject puzzleObject))
            {
                pointedObject = puzzleObject;
                return true;
            }
        }
        pointedObject = null;
        return false;
    }

    private bool CheckIfMousePointCurrentObject(Vector2 mousePosition)
    {
        if (CheckIfMousePointPuzzleObject(mousePosition, out PuzzleObject pointedObject))
        {
            if (pointedObject == m_ObjectManager.CurrentSelectedObject)
            {
                return true;
            }
        }
        return false;
    }
}
