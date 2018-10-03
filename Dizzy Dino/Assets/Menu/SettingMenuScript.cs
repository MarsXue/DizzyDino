using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenuScript : MonoBehaviour {

	GameObject mainMenu;
	GameObject settingMenu;

	void Back() {

		if (Input.GetKey(KeyCode.Space)) {
			Debug.Log("Space");

			// mainMenu.SetActive(true);
			// settingMenu.SetActive(false);
		
		}

	}

}
