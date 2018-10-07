using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackMainMernuScript : MonoBehaviour {

	public GameObject mainMenu;
	
	// Update is called once per frame
	void Update() {

		if (gameObject.activeInHierarchy) {
			
			if (Input.GetKeyDown(KeyCode.Space)) {

				gameObject.SetActive(false);

				mainMenu.SetActive(true);

				EventSystem.current.SetSelectedGameObject(null);

			}

		}

	}

}
