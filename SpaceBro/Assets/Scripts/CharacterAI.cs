using UnityEngine;
using System.Collections;

public class CharacterAI : MonoBehaviour {

    enum Behavior { PASSIVE, AGRESSIVE }

    Behavior behavior = Behavior.AGRESSIVE;
    Character MainCharacter;
    MoveCharacter MoveCharacter;
    float Health = 20;
    public float angle;
    public float MainCharAngle;
    public float AIangle;
    public float Result;

	// Use this for initialization
	void Start () {
        MoveCharacter = GetComponent<MoveCharacter>();
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go != null)
            MainCharacter = go.GetComponent<Character>();
	}

    // Update is called once per frame
    void Update()
    {

        
        //MoveCharacter.Move(MoveCharacter.moveDirection);

        //Checks the health of the NPC
        if (Health < 0)
            Destroy(gameObject);

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

    void LookAtMainCharacter()
    {
        GameObject planet = MoveCharacter.planets.FindClosestPlanet(MoveCharacter.transform.position);
        Vector3 vA = MainCharacter.Position - planet.transform.position;
        Vector3 vB = MoveCharacter.transform.position - planet.transform.position;
        angle = Vector3.Angle(vA, vB);
        
        AIangle = Mathf.Atan2(vA.x, vA.y) * Mathf.Rad2Deg;
        MainCharAngle = Mathf.Atan2(vB.x, vB.y) * Mathf.Rad2Deg;

        if(Mathf.Abs(WrapAngle(AIangle + angle - MainCharAngle)) < 1)
            MoveCharacter.LookDirection = MoveCharacter.moveDirection = Direction.LEFT;
        else
            MoveCharacter.LookDirection = MoveCharacter.moveDirection = Direction.RIGHT;
        MoveCharacter.Move(MoveCharacter.moveDirection);
        Result = Mathf.Abs(WrapAngle(AIangle + angle - MainCharAngle));
    }

    float WrapAngle(float angle)
    {
        if (angle > 180)
            angle -= 360;
        if (angle < -180)
            angle += 360;

        return angle;
    }

    void Agressive()
    {
        LookAtMainCharacter();
    }

    void Passive()
    {

    }

    public void Hit(float dmg)
    {
        Health -= dmg;
    }
}
