using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewHiScore : MonoBehaviour {

    public GameObject inputField;
    public Text nameText;
    private ScoreManager scoreManager;

    public GameObject highScoreOverlay;
    public GameObject gameOverOverlay;

    private float shakeStartTime = -1f;
    private float shakeTime = 0.5f;
    private RectTransform rectTrans;

	// Use this for initialization
	void Start () {
        scoreManager = GameObject.FindWithTag("GameController")
                       .GetComponent<ScoreManager>();
        rectTrans = inputField.GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 pos = rectTrans.anchoredPosition;
        if (Time.time < shakeStartTime + shakeTime) {
            pos.x = Random.Range(-10, 10);
        } else if (shakeStartTime > 0) {
            pos.x = 0;
            shakeStartTime = -1f;
        }
        rectTrans.anchoredPosition = pos;
    }

    public void onSubmit(){
        string name = nameText.text;
        if (name.Length == 0) {
            // shake the input field.
            shakeStartTime = Time.time;
        } else {
            RankingManager.UpdateList(name, (int) (ScoreManager.score * 10));
            highScoreOverlay.SetActive(false);
            gameOverOverlay.SetActive(true);
        }
    }


}
