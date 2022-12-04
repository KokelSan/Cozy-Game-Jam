using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //private ObjectManager m_ObjectManager;

    private Camera m_Camera;
    private Mouse m_Mouse;

    private bool m_IsHolding = false;

    private Puzzle m_SelectedPuzzle = null;
    private FocusableObject m_FocusedObject = null;
    private Puzzle m_OveredPuzzle = null;

    public bool log;

    private void Start()
    {
        m_Camera = Camera.main;
        m_Mouse = Mouse.current;
        //m_ObjectManager = FindObjectOfType<ObjectManager>();
    }

    public void OnLeftClick(InputValue input)
    {
        if (log) Debug.Log("Click");

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

    public void OnLeftHold(InputValue input)
    {
        if (m_IsHolding)
        {
            if (log) Debug.Log("Dropping");
            if (m_FocusedObject)
                m_FocusedObject.Drag(false);
            m_IsHolding = false;
        }
        else if(input.isPressed)
        {           
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            if (CheckIfMousePointCurrentFocusedObject(mousePosition))
            {
                if (log) Debug.Log("Holding");
                m_IsHolding = true;
                m_FocusedObject.Drag(true);
            }
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

    private bool CheckIfMousePointCurrentFocusedObject(Vector2 mousePosition)
    {
        Ray ray = m_Camera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y));
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out FocusableObject focusableObject))
            {
                return focusableObject == m_FocusedObject;
            }
        }
        return false;
    }
}
