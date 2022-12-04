using UnityEngine;

public class InteractivePuzzleElement : MonoBehaviour
{   
    private Collider m_Collider;
    private Outline m_Outline;

    public virtual void Start()
    {
        m_Collider = GetComponent<Collider>();
        m_Outline = GetComponent<Outline>();
    }

    public void SetActive(bool flag)
    {
        if (m_Collider)
        {
            m_Collider.enabled = flag;
        }
    }
    public virtual void Click()
    {

    }

    public void Over(bool state)
    {
        if (m_Outline)
        {
            m_Outline.enabled = state;
        }
    }
}
