using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    // Use this for initialization
    const float INITIAL_SIZE = 10;
    const float FOLLOW_CHARACTer_SIZE = 10;
    const float MAX_DIST = 5;

    GameObject Spaceship;
    public float SpaceshipRotationOffset = 0;
    Gravity SpaceshipGravity;

    GameObject Character;
    public float CharacterRotationOffset = 0;

    bool isInShip = true;

    Camera cam;

	void Start () {
        cam = GetComponent<Camera>();
        cam.orthographicSize = INITIAL_SIZE;

        Spaceship = GameObject.FindGameObjectWithTag("MainSpaceship");
        SpaceshipGravity = Spaceship.GetComponent<Gravity>();

        Character = GameObject.FindGameObjectWithTag("MainCharacter");
	}
	
	// Update is called once per frame
        // Thing to follow
	void FixedUpdate () {
        Transform trans = Spaceship.transform; // Thing to follow

        if (Input.GetKeyDown(KeyCode.E))
            isInShip = !isInShip;

        if (!isInShip)
        {
            trans =  Character.transform;
        }

        FollowTransorm(trans);

        
    }

    void FollowTransorm(Transform trans)
    {
        if ((trans.position - transform.position).magnitude > MAX_DIST)
            transform.position += (trans.position - transform.position).normalized * ((trans.position - transform.position).magnitude - MAX_DIST);

        transform.rotation = Quaternion.Lerp(transform.rotation, trans.rotation, 0.05f);
        transform.position = new Vector3(transform.position.x, transform.position.y, -5);

        cam.orthographicSize = INITIAL_SIZE / Mathf.Max(SpaceshipGravity.GravityMagnitude, 1);
    }
}
