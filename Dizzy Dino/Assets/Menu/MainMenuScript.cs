using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	public void Update() {

		if (Input.GetKey(KeyCode.Space)) {

			Debug.Log("Did start the game!");

			// game scene
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		
	}

	public void Quit() {

		Debug.Log("Did quit the game!");

		Application.Quit();
	}

}
