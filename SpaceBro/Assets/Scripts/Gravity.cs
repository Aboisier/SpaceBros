using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour
{
    public const float GRAVITATIONNAL_CONSTANT = 50;
    const float MIN_GRAVITY = 0.40f; /// When the gravity force is lower than this, it has no effect

    Planets planets;
    public float GravityMagnitude;
    Vector3 GravityForce;
    public float GravityScale = 1;

    Rigidbody2D Rb;

    void Start()
    {
        planets = FindObjectOfType<Planets>();
        Rb = GetComponent<Rigidbody2D>();
    }

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
            float gravityForce = GRAVITATIONNAL_CONSTANT * (Rb.mass * go.GetComponent<Rigidbody2D>().mass) /
                                 (Mathf.Pow(gravityDirection.magnitude, 2));

            Rb.AddForce((gravityDirection).normalized * gravityForce * GravityScale);

            if(gravityForce * GravityScale > MIN_GRAVITY)
                GravityForce += (gravityDirection).normalized * gravityForce * GravityScale;
        }

        GravityMagnitude = GravityForce.magnitude;
    }

    /// <summary>
    /// Computes the gravitationnal force at a given position. Only takes in account
    /// the closest planet.
    /// </summary>
    /// <param name="pos">Position.</param>
    /// <param name="mass">Mass of the object.</param>
    /// <returns>The gravitationnal force in a vectorial form.</returns>
    public static Vector3 GravityAt(Vector3 pos, float mass)
    {
        GameObject planet = FindObjectOfType<Planets>().FindClosestPlanet(pos);
        Vector3 gravityDirection = planet.transform.position - pos;
        float gravityForce = GRAVITATIONNAL_CONSTANT * (mass * planet.GetComponent<Rigidbody2D>().mass) /
                             (Mathf.Pow(gravityDirection.magnitude, 2));

        return (gravityDirection).normalized * gravityForce;
    }
}
