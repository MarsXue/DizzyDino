using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static float score = 0f;
    public Text highScore;
    public Text scoreText;

    private bool isBlinking = false;

    private float time = 0.0f;

    // Use this for initialization
    void Start () {

        highScore.text = "HI " + PlayerPrefs.GetInt("score0").ToString().PadLeft(5, '0');

    }
	
    // Update is called once per frame
    void Update () {
        score += Time.deltaTime;
        int s = (int) (score * 10);

        if (isBlinking) {
            
            time += Time.deltaTime;

            if (time > 2.0f) {
                time = 0.0f;
                isBlinking = false;

                StopBlinking();
            }

        } else {
            
            if (s % 100 == 0 && s != 0) {
                isBlinking = true;

                StartBlinking();
            }

            scoreText.text = s.ToString().PadLeft(5, '0');

        }

    }

    	IEnumerator Blink() {

		while (true) {

			switch(scoreText.color.a.ToString()) {
				case "0":
					scoreText.color = new Color(scoreText.color.r, 
										        scoreText.color.g, 
										        scoreText.color.b, 1);
					yield return new WaitForSeconds(0.25f);
					break;
				case "1":
					scoreText.color = new Color(scoreText.color.r, 
										        scoreText.color.g, 
										        scoreText.color.b, 0);
					yield return new WaitForSeconds(0.25f);
					break;
			}

		}

	}
	
	public void StartBlinking() {

		StopCoroutine("Blink");
		StartCoroutine("Blink");
	
	}

	public void StopBlinking() {

		StopCoroutine("Blink");

	}

}
