using UnityEngine;

public class InteractivePuzzleElement : MonoBehaviour
{   
    private Collider m_Collider;

    private void Start()
    {
        m_Collider = GetComponent<Collider>();
    }

    public void SetActive(bool flag)
    {
        m_Collider.enabled = flag;
    }
    public virtual void Click()
    {

    }
}
