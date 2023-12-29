using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick_Play()
    {
        // Load the main gameplay scene
        SceneManager.LoadScene("MainScene");
    }

    public void OnClick_Quit()
    {
        Application.Quit();
    }
}
