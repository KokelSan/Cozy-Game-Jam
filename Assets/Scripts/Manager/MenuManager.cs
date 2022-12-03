using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Serializable]
    struct References
    {
        public Button playButton;
    }

    [Serializable]
    public struct Events
    {
        public UnityEvent onStart;
        public UnityEvent onPlay;
    }
    
    public Events events;
    
    [SerializeField]
    private References m_refs;
    
    [Scene] [SerializeField]
    private string gameScene;

    private void Start()
    {
        events.onStart?.Invoke();
        
        m_refs.playButton.onClick.AddListener(Play);
    }

    void Play()
    {
        events.onPlay?.Invoke();
        AsyncOperation asyn = SceneManager.LoadSceneAsync(gameScene);
    }
}
