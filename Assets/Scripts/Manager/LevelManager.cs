using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [Serializable]
    public struct Events
    {
        public UnityEvent onStart;
        public UnityEvent onProblemSolved;
        public UnityEvent onWin;
    }
    
    public Events events;
    Problem[] problems;
    public static LevelManager m_Instance;
    private int m_solvedProblemCount = 0;

    public GameObject winPanel;

    public static LevelManager Instance
    {
        get
        {
            return m_Instance;
        }
    }
    
    void Start()
    {
        m_Instance = this;
        events.onStart?.Invoke();
        problems = FindObjectsOfType<Problem>();

        foreach (Problem problem in problems)
        {
            problem.onSolved.AddListener(OnProblemSolved);
        }
    }

    void OnProblemSolved()
    {
        Debug.Log("ProblemSolved");

        m_solvedProblemCount++;

        if (m_solvedProblemCount >= problems.Length)
            Win();
        else
            events.onProblemSolved?.Invoke();
    }

    void Win()
    {
        events.onWin?.Invoke();
        winPanel.SetActive(true);
        StartCoroutine(ReturnToMenu());
    }    
    
    private IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(3);
        winPanel.SetActive(false);
        Camera.main.GetComponent<CameraMovement>().GoToMenuContext();
    }
}
