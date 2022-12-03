using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private ObjectManager m_ObjectManager;
    private Camera m_Camera;

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
            if (m_ObjectManager.TryGetCurrentSelectedObject(out PuzzleObject SelectedObject))
            {
                if (hitInfo.collider.gameObject == SelectedObject.gameObject)
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
        else
        {
            m_ObjectManager.UnselectCurrentObject();
        }
    }

    public void OnHold(InputValue input)
    {
        Debug.Log("Holding");
        // TODO
    }
}
