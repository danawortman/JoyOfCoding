using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    Text scoreText;

    [SerializeField]
    public int playerScore;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        scoreText.text = "Something Silly";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + playerScore;
        // If we've won the game, congratulate the player
        if (playerScore == 5)
        {
            Debug.Log("Player wins!");
            SceneManager.LoadScene("WinScene");
        }
    }
}
