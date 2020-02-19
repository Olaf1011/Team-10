using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public static int score;
    public Text scoreText;


	// Use this for initialization
	void Start () {
        score = 0;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public static void IncreaseScore()
    {
        score++;
    }
    void LateUpdate()
    {
        //scoreText.text = "Score: " + score;
    }
}
