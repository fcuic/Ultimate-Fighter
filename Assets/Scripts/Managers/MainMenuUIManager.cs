using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClickStart()
    {
        SceneManager.LoadScene("CharacterSelection");
    }
    public void OnClickSettings()
    {

    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
