﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextManager : MonoBehaviour {

    public Text textView;
    public float textLifeTime;
    public float fadeoutSpeed;
    public bool stop = false;

    private float lastTextTime = 0f;
    private float countTime = 0f;
    private int state = 0;
    // States:
    //  0: Idle
    //  1: Text is shown, counting down to fade out.
    //  2: Fading out.

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (stop) return;

        countTime += Time.deltaTime;

        switch (state) {
            case 1:
                if (Time.realtimeSinceStartup >= lastTextTime + textLifeTime) {
                    state = 2;
                }
            break;
            case 2:
                Color c = textView.color;
                if (c.a > 0)
                    c.a = Mathf.Clamp01(c.a - Time.smoothDeltaTime * fadeoutSpeed);
                else {
                    state = 0;
                    textView.text = "";
                    c.a = 1f;
                }
                textView.color = c;
            break;
        }
	}

    public void ShowText(string text) {
        state = 1;
        textView.text = text;
        lastTextTime = Time.realtimeSinceStartup;
        Color c = textView.color;
        c.a = 1f;
        textView.color = c;
    }
}
