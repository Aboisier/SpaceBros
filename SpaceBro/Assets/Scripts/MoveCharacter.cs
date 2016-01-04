using UnityEngine;
public enum Direction { LEFT = -1, RIGHT = 1 }
public class MoveCharacter : MonoBehaviour
{
    const float INITIAL_DRAG = 20;
    const float FALLING_DRAG = 0.1f;
    const int  JUMP_COOLDOWN = 20;
    const float MAX_VELOCITY = 5f;
    const float   WALK_SPEED = 3f;

    Rigidbody2D Rb { get; set; } 
    Animator  Anim { get; set; }
    bool IsWalking { get; set; }
    bool IsJumping { get; set; }
    int  jumpTimer { get; set; }
    public float Health;
    public bool IsAutonomous;   /// If true, the character is controlled by the players input.
    
    public Direction LookDirection { get; private set; }
    public Direction MoveDirection { get; private set; }
    public Planets         Planets { get; private set; }

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Anim.updateMode = AnimatorUpdateMode.AnimatePhysics;
        Planets = FindObjectOfType<Planets>();
    }

    void FixedUpdate()
    {
        // Rotates the character with the closest planet, depending on his position.
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, GetAngle(transform.position - Planets.FindClosestPlanet(transform.position).transform.position) - 90));
        transform.rotation = rot;
        if (IsAutonomous)
        {
            FlipTowardsMouse();

            if (jumpTimer >= 0)
                jumpTimer -= 1;

            if (Rb.IsTouchingLayers())
            {
                Rb.drag = INITIAL_DRAG;
                IsJumping = false;
            }
            else
            {
                Rb.drag = FALLING_DRAG;

            }
            HandleInput();
        }
        else
        {
            if (Rb.velocity.magnitude < 0.1f)
                IsWalking = false;
        }

        SetAnimatorParams();
    }

    ///  @brief Handles the input.
    ///  @return Void.
    void HandleInput()
    {
        IsWalking = false;
        if (Input.GetKey(KeyCode.W))
            Jump();

        if (Input.GetKey(KeyCode.D))
            Move(Direction.RIGHT);

        if (Input.GetKey(KeyCode.A))
            Move(Direction.LEFT);
    }

    ///  @brief Flips the character towards the wanted direction.
    ///  @param dir The direction.
    ///  @return Void.
    public void Flip(Direction dir)
    {
        LookDirection = dir;
        Vector3 scale = transform.localScale;
        scale.x = (int)dir * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    ///  @brief Moves the character in the given direction.
    ///  @param dir The direction.
    ///  @return Void.
    public void Move(Direction dir)
    {
        Move(dir, WALK_SPEED, MAX_VELOCITY);
    }

    public void Move(Direction dir, float speed)
    {
        Move(dir, speed, MAX_VELOCITY);
    }

    public void Move(Direction dir, float speed, float maxVelocity)
    {
        MoveDirection = dir;

        if (Rb.velocity.magnitude < maxVelocity)
        {
            Rb.AddForce((int)dir * transform.right * Rb.drag * speed);
            IsWalking = true;
        }
    }

    ///  @brief Makes the character jump by giving it a big force
    ///         in the up direction. Also initialises a cooldown
    ///         and modifies the drag. Makes the boolean isJumping
    ///         true.
    ///  @return Void.
    void Jump()
    {
        if (!IsJumping && jumpTimer <= 0)
        {
            IsJumping = true;
            Rb.AddForce(transform.up * 140);
            Rb.drag = FALLING_DRAG;
            jumpTimer = JUMP_COOLDOWN;
        }
    }

    ///  @brief Flips the character towards the mouse.
    ///  @return Void.
    void FlipTowardsMouse()
    {
        if (Input.mousePosition.x < Camera.main.WorldToScreenPoint(transform.position).x)
            Flip(Direction.LEFT);
        else
            Flip(Direction.RIGHT);
    }

    ///  @brief Sets the animator parameters.
    ///  @return Void.
    void SetAnimatorParams()
    {
        Anim.SetBool("IsWalking", IsWalking);

        // If the direction the character is moving equals the direction it is looking, then the animation plays normally. Otherwise, it plays backwards.
        Anim.SetFloat("SpeedMultiplier", MoveDirection == LookDirection ? 1 : -1);
    }

    ///  @brief Returns the angle in degrees between the vector and 
    ///         the x axis.
    ///  @param v The vector.
    ///  @return Void.
    float GetAngle(Vector3 v)
    {
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    public void Hit(float dmg)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().Health -= dmg;
    }
}
