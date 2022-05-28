using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(800, 300, false);
        SaveSystem.Initialize("Profile");
    }
    
    public void OnChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
