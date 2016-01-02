using UnityEngine;
using System.Collections;

public class CharacterAI : MonoBehaviour {
    const float RANDOM_OFFSET_FREQUENCY   = 1f;
    const float FOCUS_POINT_PRECISION     = 1f;
    const float FOLLOW_MAIN_CHAR_DISTANCE = 2.5f;

    public Vector3 Position
    {
        get
        {
            return  MoveCharacter.transform.position;
        }
    }
    public Vector3 Rotation
    {
        get
        {
            return MoveCharacter.transform.rotation.eulerAngles;
        }
    }

    enum Behavior { PASSIVE, AGRESSIVE }

    Behavior behavior = Behavior.PASSIVE;
    Character MainCharacter;
    MoveCharacter MoveCharacter;
    float Health = 40;
    float RandomOffsetCountdown { get; set; }
    Weapon weapon { get; set; }

    public Vector3 FocusPoint { get; private set; }
    Vector3 FocusPointRandomOffset { get; set; }

	// Use this for initialization
	void Start () {
        RandomOffsetCountdown = 0;
        MoveCharacter = GetComponent<MoveCharacter>();
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go != null)
            MainCharacter = go.GetComponent<Character>();

        weapon = GetComponentInChildren<Weapon>();
	}

    // Update is called once per frame
    void Update()
    {
        //Checks the health of the NPC
        if (Health < 0)
            Die();

        switch (behavior)
        {
            case Behavior.AGRESSIVE:
                Agressive();
                break;

            case Behavior.PASSIVE:
                Passive();
                break;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void LookAtMainCharacter()
    {
        Vector3 delta = MoveCharacter.transform.position - MainCharacter.Position;
        MoveCharacter.Flip(Vector3.Angle(delta, MoveCharacter.transform.right) < 90 ? Direction.LEFT : Direction.RIGHT);
    }

    void Agressive()
    {
        if (weapon != null)
        {
            RandomOffsetCountdown += Time.deltaTime;
            if (RandomOffsetCountdown > RANDOM_OFFSET_FREQUENCY)
            {
                FocusPointRandomOffset = RandomVector(-FOCUS_POINT_PRECISION, FOCUS_POINT_PRECISION);
                weapon.Shoot(FocusPoint);
                RandomOffsetCountdown = 0;
            }
        }
        FocusPoint = MainCharacter.Position + FocusPointRandomOffset;
        //Debug.DrawLine(MoveCharacter.transform.position, FocusPoint);

        LookAtMainCharacter();

        if ((int)DistanceFromMainChar() > FOLLOW_MAIN_CHAR_DISTANCE)
            MoveCharacter.Move(MoveCharacter.LookDirection, 1 + DistanceFromMainChar() / 4f, 2);
        
    }

    void Passive()
    {

    }

    Vector3 RandomVector(float min, float max)
    {
        return new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
    }

    float DistanceFromMainChar()
    {
        return (MainCharacter.Position - MoveCharacter.transform.position).magnitude;
    }

    public void Hit(float dmg)
    {
        Health -= dmg;
        behavior = Behavior.AGRESSIVE; // Makes the NPC mad cause he's been hit
    }
}
