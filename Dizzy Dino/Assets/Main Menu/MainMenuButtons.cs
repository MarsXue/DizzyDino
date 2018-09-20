using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Play button
	public void Play() {
		SceneManager.LoadScene(1);
	}

	// Continue button
	public void Continue() {

	}
	
	// Quit button
	public void Quit() {
		Application.Quit();
	}
}
