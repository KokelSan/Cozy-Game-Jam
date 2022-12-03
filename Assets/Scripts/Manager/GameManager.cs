using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Scene]
    public string bootScene;
    
    void Start()
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadScene(bootScene);
    }
}
