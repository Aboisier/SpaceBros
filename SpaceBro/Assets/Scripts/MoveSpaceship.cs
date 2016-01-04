using UnityEngine;
using System.Collections;

public class MoveSpaceship : MonoBehaviour {
    const float MAX_VELOCITY = 12;
    const float MAX_ANGULAR_VELOCITY = 150;
    const float MAX_PITCH = 1.5f;
    const float MIN_PITCH = 0.5f;

    float pitch = 0.5f;

    public ParticleSystem Ps;
    public GameObject CharacterPrefab;
    Rigidbody2D Rb;
    float EnginePower = 7;
    AudioSource AS;

	// Use this for initialization
	void Start () {
        Ps.Stop();
        Rb = GetComponent<Rigidbody2D>();
        AS = GetComponent<AudioSource>();
        AS.pitch = MIN_PITCH;
        AS.dopplerLevel = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        HandleInput();

        SetSound();
    }

    void HandleInput()
    {

        if (Input.GetKey(KeyCode.W))
        {
            Ps.Play();
            if (Rb.velocity.magnitude < MAX_VELOCITY)
                Rb.AddForce(transform.up * EnginePower);

            if (pitch < MAX_PITCH)
                pitch += 0.008f;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            Ps.Stop();
        }

        // Rotation
        if (Input.GetKey(KeyCode.A) && Rb.angularVelocity < MAX_ANGULAR_VELOCITY)
        {
            Rb.AddTorque(EnginePower / 3);
            pitch += 0.0013f;
        }
        if (Input.GetKey(KeyCode.D) && Rb.angularVelocity > -MAX_ANGULAR_VELOCITY)
        {
            Rb.AddTorque(-EnginePower / 3);
            pitch += 0.0013f;
        }

        if (pitch > MIN_PITCH && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A))
            pitch -= 0.02f;

        if (Input.GetKey(KeyCode.Space))
        {
            Rb.AddForce(-Rb.velocity);
            Rb.AddTorque(-Rb.angularVelocity/50f);
        }
    }

    void SetSound()
    {
        AS.pitch = pitch;
        //if (go != null)
        //    AS.volume = 1 / (go.transform.position - transform.position).magnitude;
        //else
        //    AS.volume = 1;
    }
}
