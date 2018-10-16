﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] objects;
	public float spawnWait;
	public float spawnMostWait;
	public float spawnLeastWait;
	public int startWait;
	public bool stop = false;

	private LaneProperties laneProperties;
	private int randomObject;
	private int minRange;
	private int maxRange;
	private float posX;
	private float posZ;
	private float countTime = 0f;

	// Use this for initialization
	void Start () {

		minRange = 0;
		maxRange = 6;

		laneProperties = GameObject.FindWithTag("GameController")
                      	.GetComponent<LaneProperties>();

		posX = 0f;
		posZ = 0f;

		StartCoroutine(obstacleSpawner());
		
	}
	
	// Update is called once per frame
	void Update () {

		countTime += Time.deltaTime;

		if (countTime >= 15) {
			minRange = 6;
			maxRange = 7;	
		}

		if (countTime >= 30) {
			minRange = 0;
			posX = Random.Range(-1, 2) * laneProperties.laneWidth;
			posZ = Random.Range(-1, 2) * laneProperties.laneWidth;
		}
 
		spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
		
	}

	IEnumerator obstacleSpawner () {

		yield return new WaitForSeconds(startWait);

		while (!stop) {

			randomObject = Random.Range(minRange, maxRange);

			Vector3 spawnPosition = new Vector3(posX, 1, posZ);

			Debug.Log(spawnPosition);

			Instantiate(objects[randomObject], 
						spawnPosition + transform.TransformPoint(0, 0, 0), 
						gameObject.transform.rotation);

			yield return new WaitForSeconds(spawnWait);

		}

	}

}
