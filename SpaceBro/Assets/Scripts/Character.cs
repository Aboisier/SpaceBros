using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    const float MIN_DIST = 2.5f;

    public GameObject CharacterPrefab;

    GameObject Spaceship;
    MoveSpaceship MoveSpaceship;
    GameObject Char;
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
    }
}
