using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    GameObject[] Planets;
    Rigidbody2D Rb { get; set; }
    Animator Anim { get; set; }
    // Use this for initialization
    void Start()
    {
        Planets = GameObject.FindGameObjectsWithTag("Planet");
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, GetAngle(transform.position - FindClosestPlanet().transform.position) - 90));
        transform.rotation = rot;
        Anim.SetFloat("Speed", Rb.velocity.magnitude);
    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.D) && Rb.velocity.magnitude < 5)
            Rb.AddForce(transform.right * 50);
        if (Input.GetKey(KeyCode.A) && Rb.velocity.magnitude < 5)
            Rb.AddForce(transform.right * -50);
    }

    GameObject FindClosestPlanet()
    {
        GameObject closestPlanet = Planets[0];

        foreach (GameObject planet in Planets)
        {
            closestPlanet = ComputeDist(planet) < ComputeDist(closestPlanet) ? planet : closestPlanet;
        }

        return closestPlanet;
    }

    //Gets the angle between the vector and the positive x axis
    float GetAngle(Vector3 v)
    {
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
    
    //Computes the distance between the current gameobject and a given gameobject
    float ComputeDist(GameObject gameObject)
    {
        return (transform.position - gameObject.transform.position).magnitude;
    }
}
