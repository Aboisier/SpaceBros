using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    // Use this for initialization
    const float INITIAL_SIZE = 10;
    const float MAX_DIST = 5;

    public GameObject spaceship;
    Gravity goScript;
    public GameObject character;

    bool isInShip = true;

    Camera cam;

	void Start () {
        cam = GetComponent<Camera>();
        cam.orthographicSize = INITIAL_SIZE;
        goScript = spaceship.GetComponent<Gravity>();
	}
	
	// Update is called once per frame
        // Thing to follow
	void FixedUpdate () {
        Vector3 rotOffset = new Vector3(0,0,0);
        Transform trans = spaceship.transform; // Thing to follow

        if (Input.GetKeyDown(KeyCode.E))
            isInShip = !isInShip;

        if (!isInShip)
        {
            trans =  character.transform;
        }

        if ((trans.position- transform.position).magnitude > MAX_DIST)
            transform.position += (trans.position - transform.position).normalized * ((trans.position - transform.position).magnitude - MAX_DIST);

        transform.rotation = Quaternion.Lerp(transform.rotation, trans.rotation, 0.1f);
        transform.position = new Vector3(transform.position.x, transform.position.y, -5);

        cam.orthographicSize = INITIAL_SIZE / Mathf.Max(goScript.GravityMagnitude,1);
    }
}
