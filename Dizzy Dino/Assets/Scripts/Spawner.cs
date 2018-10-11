using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] objects;
	public float spawnWait;
	public float spawnMostWait;
	public float spawnLeastWait;
	public int startWait;
	public bool stop;

	private LaneProperties laneProperties;
	private int randomObject;

	// Use this for initialization
	void Start () {

		laneProperties =
            GameObject.FindWithTag("GameController")
                      .GetComponent<LaneProperties>();

		StartCoroutine(waitSpawner());
		
	}
	
	// Update is called once per frame
	void Update () {

		spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
		
	}

	IEnumerator waitSpawner () {

		yield return new WaitForSeconds(startWait);

		while (!stop) {

			randomObject = Random.Range(0, 7);

			Vector3 spawnPosition = new Vector3(Random.Range(-1, 2) * laneProperties.laneWidth, 
												1,
												Random.Range(-1, 2) * laneProperties.laneWidth);

			Instantiate(objects[randomObject], 
						spawnPosition + transform.TransformPoint(0,0,0), 
						gameObject.transform.rotation);

			yield return new WaitForSeconds(spawnWait);

		}

	}

}
