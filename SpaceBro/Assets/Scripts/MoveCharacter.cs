using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    const float INITIAL_DRAG = 20;
    const float FALLING_DRAG = 1;

    GameObject[] Planets;
    Rigidbody2D Rb { get; set; }
    Animator Anim { get; set; }
    bool isWalking { get; set; }
    bool isJumping { get; set; }

    // Use this for initialization
    void Start()
    {
        Planets = GameObject.FindGameObjectsWithTag("Planet");
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Anim.updateMode = AnimatorUpdateMode.AnimatePhysics;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Rb.IsTouchingLayers())
        {
            Rb.drag = INITIAL_DRAG;
            isJumping = false;
        }
        else
        {
            Rb.drag = FALLING_DRAG;

        }
        HandleInput(); 

        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, GetAngle(transform.position - FindClosestPlanet().transform.position) - 90));
        transform.rotation = rot;

        SetAnimationParameters();
    }

    void HandleInput()
    {
        isWalking = false;

        if (Input.GetKey(KeyCode.W) && !isJumping)
        {
            isJumping = true;
            Rb.AddForce(transform.up * 140);
            Rb.drag = 1;

        }

        if (Input.GetKey(KeyCode.D) && Rb.velocity.magnitude < 5)
        {

            Vector3 theScale = transform.localScale;
            theScale.x = Mathf.Abs(theScale.x);
            transform.localScale = theScale;
            Rb.AddForce(transform.right * Rb.drag * 3f);
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.A) && Rb.velocity.magnitude < 5)
        {

            Vector3 theScale = transform.localScale;
            theScale.x = -Mathf.Abs(theScale.x);
            transform.localScale = theScale;
            Rb.AddForce(transform.right * Rb.drag * -3f);
            isWalking = true;
        }

    }

    void SetAnimationParameters()
    {
        Anim.SetBool("IsWalking", isWalking);
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
