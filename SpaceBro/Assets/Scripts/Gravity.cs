using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {
    const float GRAVITATIONNAL_CONSTANT = 50;

    GameObject[] Planets;
    public float GravityMagnitude;
    Vector3 GravityForce;
    public float GravityScale = 1;

    // Use this for initialization
    void Start () {
        Planets = GameObject.FindGameObjectsWithTag("Planet");
    }
	
	// Update is called once per frame
	void Update () {
        HandleGravity();
    }



    void HandleGravity()
    {
        GravityForce = Vector3.zero;

        foreach (GameObject go in Planets)
        {
            Vector3 gravityDirection = go.transform.position - transform.position;
            float gravityForce = GRAVITATIONNAL_CONSTANT * (GetComponent<Rigidbody2D>().mass * go.GetComponent<Rigidbody2D>().mass) /
                                 (Mathf.Pow(gravityDirection.magnitude, 3));

            GetComponent<Rigidbody2D>().AddForce((gravityDirection).normalized * gravityForce * GravityScale);
            GravityForce += (gravityDirection).normalized * gravityForce * GravityScale;
        }

        GravityMagnitude = GravityForce.magnitude;
    }
}
