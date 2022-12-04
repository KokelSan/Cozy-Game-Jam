using System.Collections;
using UnityEngine;
using TMPro;

public class UITimer : MonoBehaviour
{
    private TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    public void StartTimer(float seconds)
    {
        StartCoroutine(DisplayForSeconds(seconds));
    }

    public IEnumerator DisplayForSeconds(float seconds)
    {
        text.enabled = true;
        yield return new WaitForSeconds(seconds);
        text.enabled = false;
    }
}
