using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    const float MIN_DIST = 2.5f;

    public GameObject CharacterPrefab;

    GameObject Spaceship;
    GameObject Char;
    Follow Follow;
    bool isInShip = true;

	// Use this for initialization
	void Start () {
        Spaceship = GameObject.FindGameObjectWithTag("MainSpaceship");
        Follow = Camera.main.GetComponent<Follow>();
        Follow.go = Spaceship;
    }
	
	// Update is called once per frame
	void Update () {
        
        HandleInput();
	}

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInShip)
            {
                Char = Instantiate(CharacterPrefab, Spaceship.transform.position + Spaceship.transform.right * 2, Quaternion.identity) as GameObject;
                isInShip = false;
                Follow.go = Char;
            }
            else if((Char.transform.position - Spaceship.transform.position).magnitude < MIN_DIST)
            {
                Destroy(Char);
                isInShip = true;
                Follow.go = Spaceship;
            }
        }
    }
}
