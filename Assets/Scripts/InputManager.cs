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

    public void OnClick(InputValue input)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = m_Camera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y));
        
        RaycastHit[] hitsInfo = Physics.RaycastAll(ray);

        foreach (RaycastHit hitInfo in hitsInfo)
        {
            if (hitInfo.collider.TryGetComponent(out Interactive interactiveSelected))
            {
                if (log)
                    Debug.Log("Selecting a new puzzle object");
                m_ObjectManager.SelectObject(interactiveSelected);
            }
        }
        
        m_ObjectManager.ProcessUnselectObject();
    }

    public void OnHold(InputValue input)
    {
        if (m_IsHolding)
        {
            if (log)
                Debug.Log("Releasing current object");
            m_IsHolding = false;
            m_ObjectManager.DropObject();
        }
        else
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            
            Ray ray = m_Camera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y));
        
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.TryGetComponent(out Interactive pointed))
                {
                    if (log)
                        Debug.Log("Holding current object");
                    m_IsHolding = true;
                    m_ObjectManager.DrapObject(pointed);
                }
            }
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
               if (log) 
                   Debug.Log("Is over objet");
               m_ObjectManager.HighlightObject(interactiveSelected);
           }
       }
       
       m_ObjectManager.ProcessMouseNotOver();
    }
}
