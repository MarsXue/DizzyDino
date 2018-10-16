using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float force = 450f;
    public float jumpThreshold = 0.5f;
    public float rotationLimit = 45f;

    public bool stop = false;

    public ParticleSystem particleSystem;

    private LaneProperties laneProperties;

    private AudioSource audioSource;
    public AudioClip ButtonPress;

    private float z = 0.0f;
    private Rigidbody r;
    

    // Use this for initialization
    void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();

        laneProperties = GameObject.FindWithTag("GameController")
                        .GetComponent<LaneProperties>();
        z = gameObject.transform.localPosition.z;
        r = gameObject.GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
      
        Vector3 pos = gameObject.transform.position;
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !stop) {
            HitButton();
            pos.z = Mathf.Max(z -laneProperties.laneWidth, pos.z -laneProperties.laneWidth);
            r.angularVelocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !stop) {
            HitButton();
            pos.z = Mathf.Min(z +laneProperties.laneWidth, pos.z +laneProperties.laneWidth);
            r.angularVelocity = Vector3.zero;
        }
        gameObject.transform.position = pos;

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && !stop) {
            HitButton();
            if (gameObject.transform.position.y < jumpThreshold) {
                r.AddForce(force * Vector3.up);
            }
        }

        if (gameObject.transform.position.y > jumpThreshold) {
            particleSystem.Stop();
            r.angularVelocity = Vector3.zero;
        } else {
            particleSystem.Play();
        }
    }

    private void HitButton() {
        audioSource.clip = ButtonPress;
        audioSource.Play();
    }
}
