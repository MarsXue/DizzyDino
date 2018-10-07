using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	private const string menuString = "MainMenu";

	public void Update() {

		if (gameObject.activeInHierarchy) {

			if (Input.GetKeyDown(KeyCode.Space)) {

				Debug.Log("Did start the game!");

				// load the game scene
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			
			}

		}
		
	}

	public void Quit() {

		Debug.Log("Did quit the game!");

		Application.Quit();
	}

}
