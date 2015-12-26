using UnityEngine;
using System.Collections;

public class MoveSpaceship : MonoBehaviour {
    public ParticleSystem Ps;
    Rigidbody2D Rb;
    float EnginePower = 4;

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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Rb.AddForce(transform.up * EnginePower);
            Ps.Play();

        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
            Ps.Stop();

        if (Input.GetKey(KeyCode.LeftArrow))
            Rb.AddTorque(EnginePower / 4);
        if (Input.GetKey(KeyCode.RightArrow))
            Rb.AddTorque(-EnginePower / 4);
    }
}
