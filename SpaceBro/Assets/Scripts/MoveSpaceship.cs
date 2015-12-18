using UnityEngine;
using System.Collections;

public class MoveSpaceship : MonoBehaviour {

    GameObject[] planets;
    float gravity;
    public ParticleSystem ps;

	// Use this for initialization
	void Start () {
        planets = GameObject.FindGameObjectsWithTag("Planet");
        ps.Stop();
    }
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject go in planets)
        {

            gravity = (GetComponent<Rigidbody2D>().mass * go.GetComponent<Rigidbody2D>().mass) /
                      ((go.transform.position - transform.position).sqrMagnitude);

            GetComponent<Rigidbody2D>().AddForce((go.transform.position - transform.position).normalized * gravity);

            if (Input.GetKey(KeyCode.UpArrow))
            {
                GetComponent<Rigidbody2D>().AddForce(transform.up * 2);
                ps.Play();

            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
                ps.Stop();

            if (Input.GetKey(KeyCode.LeftArrow))
                GetComponent<Rigidbody2D>().AddTorque(0.5f);
            if (Input.GetKey(KeyCode.RightArrow))
                GetComponent<Rigidbody2D>().AddTorque(-0.5f);
        }
    }
}
