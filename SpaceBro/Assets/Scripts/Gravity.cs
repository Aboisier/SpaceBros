using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour
{
    public const float GRAVITATIONNAL_CONSTANT = 50;
    const float MIN_GRAVITY = 0.40f; // When the gravity force is lower than this, it has no effect

    Planets planets;
    public float GravityMagnitude;
    Vector3 GravityForce;
    public float GravityScale = 1;

    // Use this for initialization
    void Start()
    {
        planets = FindObjectOfType<Planets>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleGravity();
    }



    void HandleGravity()
    {
        GravityForce = Vector3.zero;

        foreach (GameObject go in planets.PlanetsList)
        {
            Vector3 gravityDirection = go.transform.position - transform.position;
            float gravityForce = GRAVITATIONNAL_CONSTANT * (GetComponent<Rigidbody2D>().mass * go.GetComponent<Rigidbody2D>().mass) /
                                 (Mathf.Pow(gravityDirection.magnitude, 2));

            GetComponent<Rigidbody2D>().AddForce((gravityDirection).normalized * gravityForce * GravityScale);

            if(gravityForce * GravityScale > MIN_GRAVITY)
                GravityForce += (gravityDirection).normalized * gravityForce * GravityScale;
        }

        GravityMagnitude = GravityForce.magnitude;
    }
}
