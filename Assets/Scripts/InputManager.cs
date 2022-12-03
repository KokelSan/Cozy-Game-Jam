using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private ObjectManager m_ObjectManager;
    private Camera m_Camera;
    private bool m_IsHolding = false;

    public bool log;

    private void Start()
    {
        m_Camera = Camera.main;
        m_ObjectManager = FindObjectOfType<ObjectManager>();
    }

    void OnLeftClick(InputValue input)
    {
        if (input.isPressed)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = m_Camera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y));
        
            RaycastHit[] hitsInfo = Physics.RaycastAll(ray);

            bool isfirst = true;
            foreach (RaycastHit hitInfo in hitsInfo)
            {
                if (hitInfo.collider.TryGetComponent(out Interactive interactiveSelected))
                {
                    if (isfirst)
                    {
                        m_ObjectManager.DrapObject(interactiveSelected);
                        isfirst = false;
                    }

                    if (log)
                        Debug.Log("Selecting a new puzzle object");
                    m_ObjectManager.SelectObject(interactiveSelected);
                    interactiveSelected.Click();
                }
            }
            m_ObjectManager.ProcessUnselectObject();
        }
        else
        {
            m_ObjectManager.DropObject();
        }
    }

    public void OnLook(InputValue input)
    {
       Vector2 mousePosition = Mouse.current.position.ReadValue();
       Ray ray = m_Camera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y));
       
       RaycastHit[] hitsInfo = Physics.RaycastAll(ray);
       
       foreach (RaycastHit hitInfo in hitsInfo)
       {
           if (hitInfo.collider.TryGetComponent(out Interactive interactiveSelected))
           {
               //if (log) 
               //    Debug.Log("Is over objet");
               m_ObjectManager.HighlightObject(interactiveSelected);
           }
       }
       
       m_ObjectManager.ProcessMouseNotOver();
    }
}
