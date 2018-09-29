using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static float score = 0f;
    public Text highScore;
    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        score += Time.fixedDeltaTime;
        int s = (int) (score * 5);
        text.text = s.ToString().PadLeft(5, '0');
        highScore.text = "HI " + s.ToString().PadLeft(5, '0');
	}
}
