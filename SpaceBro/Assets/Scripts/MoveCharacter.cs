using UnityEngine;
public enum Direction { LEFT = -1, RIGHT = 1 }
public class MoveCharacter : MonoBehaviour
{
    public Planets planets;
    const float INITIAL_DRAG = 20;
    const float FALLING_DRAG = 0.1f;
    const int JUMP_COOLDOWN = 20;

    Rigidbody2D Rb { get; set; }
    Animator Anim { get; set; }
    bool isWalking { get; set; }
    bool isJumping { get; set; }
    int jumpTimer = 0;

    //Debugging
    public float angle;

    public bool isAutonomous;
    public Direction LookDirection { get; set; }
    public Direction moveDirection { get; set; }

    // Use this for initialization
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Anim.updateMode = AnimatorUpdateMode.AnimatePhysics;
        planets = FindObjectOfType<Planets>();
    }

    void Update()
    {
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, GetAngle(transform.position - planets.FindClosestPlanet(transform.position).transform.position) - 90));
        transform.rotation = rot;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAutonomous)
        {
            SetDirection();

            if (jumpTimer >= 0)
                jumpTimer -= 1;

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
        }
        SetAnimationParameters();
    }

    void HandleInput()
    {
        isWalking = false;
        if (Input.GetKey(KeyCode.W))
            Jump();

        if (Input.GetKey(KeyCode.D))
            Move(Direction.RIGHT);

        if (Input.GetKey(KeyCode.A))
            Move(Direction.LEFT);
    }

    void Flip(Direction dir)
    {
        LookDirection = dir;
        Vector3 scale = transform.localScale;
        scale.x = (int)dir * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    public void Move(Direction dir)
    {
        moveDirection = dir;

        if (Rb.velocity.magnitude < 5)
        {
            Rb.AddForce((int)dir * transform.right * Rb.drag * 3f);
            isWalking = true;
        }
    }

    void Jump()
    {
        if (!isJumping && jumpTimer <= 0)
        {
            isJumping = true;
            Rb.AddForce(transform.up * 140);
            Rb.drag = FALLING_DRAG;
            jumpTimer = JUMP_COOLDOWN;
        }
    }

    void SetDirection()
    {
        if (isAutonomous)
        {
            if (Input.mousePosition.x < Camera.main.WorldToScreenPoint(transform.position).x)
                Flip(Direction.LEFT);
            else
                Flip(Direction.RIGHT);
        }
    }

    void SetAnimationParameters()
    {
        Anim.SetBool("IsWalking", isWalking);

        // If the direction the character is moving equals the direction it is looking, then the animation plays normally. Otherwise, it plays backwards.
        //print(moveDirection.ToString());
        Anim.SetFloat("SpeedMultiplier", moveDirection == LookDirection ? 1 : -1);
    }

    //Gets the angle between the vector and the positive x axis
    float GetAngle(Vector3 v)
    {
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    float GetAngle(Vector3 v1, Vector3 v2)
    {
        return Mathf.Acos(Vector3.Dot(v1.normalized, v2.normalized));
    }

}
