using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }
}
