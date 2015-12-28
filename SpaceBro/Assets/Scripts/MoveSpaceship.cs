﻿using UnityEngine;
using System.Collections;

public class MoveSpaceship : MonoBehaviour {
    const float MAX_VELOCITY = 12;

    public ParticleSystem Ps;
    public GameObject CharacterPrefab;
    Rigidbody2D Rb;
    float EnginePower = 7;

	// Use this for initialization
	void Start () {
        Ps.Stop();
        Rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.W) && Rb.velocity.magnitude < MAX_VELOCITY)
        {
            Rb.AddForce(transform.up * EnginePower);
            Ps.Play();

        }
        if (Input.GetKeyUp(KeyCode.W))
            Ps.Stop();

        if (Input.GetKey(KeyCode.A))
            Rb.AddTorque(EnginePower / 4);
        if (Input.GetKey(KeyCode.D))
            Rb.AddTorque(-EnginePower / 4);
    }
}
