using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    public float Offset;
    public float Amplitude;
    public float Speed;
    private Vector3 m_StartScale;
    
    void Start()
    {
        m_StartScale = transform.localScale;
    }
    
    void Update()
    {
        transform.localScale = m_StartScale * (Mathf.Sin(Speed * Time.time) * Amplitude + Offset);
    }
}
