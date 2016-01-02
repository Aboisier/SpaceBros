using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    const float MIN_DIST = 2.5f;

    public GameObject CharacterPrefab;

    public Vector3 Position
    {
        get
        {
            return isInShip ? MoveSpaceship.transform.position : MoveCharacter.transform.position;
        }
    }
    public Vector3 Rotation
    {
        get
        {
            return isInShip ? MoveSpaceship.transform.rotation.eulerAngles : MoveCharacter.transform.rotation.eulerAngles;
        }
    }
    GameObject Spaceship;
    MoveSpaceship MoveSpaceship;
    GameObject Char;
    MoveCharacter MoveCharacter;
    SpriteRenderer CaptnBroHead;
    Animator GlassAnim;

    Follow Follow;
    bool isInShip = true;

	// Use this for initialization
	void Start () {
        Spaceship = GameObject.FindGameObjectWithTag("MainSpaceship");
        MoveSpaceship = Spaceship.GetComponent<MoveSpaceship>();
        Follow = Camera.main.GetComponent<Follow>();
        Follow.go = Spaceship;
        CaptnBroHead = Spaceship.transform.FindChild("CaptnBroHead").GetComponent<SpriteRenderer>();
        GlassAnim = Spaceship.transform.FindChild("Glass").GetComponent<Animator>();
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
                Char = Instantiate(CharacterPrefab, Spaceship.transform.position , Spaceship.transform.rotation) as GameObject;
                MoveCharacter = Char.GetComponent<MoveCharacter>();
                isInShip = false;
                Follow.go = Char;
                Follow.size = Follow.CHARACTER_SIZE;
                CaptnBroHead.enabled = false;
                GlassAnim.SetBool("Opened", true);
                MoveSpaceship.enabled = false;
            }
            else if((Char.transform.position - Spaceship.transform.position).magnitude < MIN_DIST)
            {
                Destroy(Char);
                isInShip = true;
                Follow.go = Spaceship;
                Follow.size = Follow.SPACESHIP_SIZE;
                CaptnBroHead.enabled = true;
                GlassAnim.SetBool("Opened", false);
                MoveSpaceship.enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Follow.size == Follow.CHARACTER_SIZE || Follow.size == Follow.SPACESHIP_SIZE)
                Follow.size = Follow.UNZOOMED_SIZE;
            else if (isInShip)
                Follow.size = Follow.SPACESHIP_SIZE;
            else
                Follow.size = Follow.CHARACTER_SIZE;
        }

    }
}
