using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    const float MIN_DIST = 2.5f;

    public GameObject CharacterPrefab;

    public Vector3 Position
    {
        get
        {
            return Health > 0 ? (isInShip ? MoveSpaceship.transform.position : MoveCharacter.transform.position) : Vector3.zero;
        }
    }
    public Vector3 Rotation
    {
        get
        {
            return Health > 0 ? (isInShip ? MoveSpaceship.transform.rotation.eulerAngles : MoveCharacter.transform.rotation.eulerAngles) : Vector3.zero;
        }
    }
    GameObject Spaceship;
    MoveSpaceship MoveSpaceship;
    GameObject Char;
    MoveCharacter MoveCharacter;
    SpriteRenderer CaptnBroHead;
    Animator GlassAnim;
    public AudioClip OpenSAS;
    public AudioClip CloseSAS;
    AudioSource AS;
    AudioListener AL;
    public float Health { get; set; }
    public ParticleSystem DeathEffect;
    Follow Follow;
    bool isInShip = true;
    bool dead = false;
	// Use this for initialization
	void Start () {
        Spaceship = GameObject.FindGameObjectWithTag("MainSpaceship");
        MoveSpaceship = Spaceship.GetComponent<MoveSpaceship>();
        Follow = Camera.main.GetComponent<Follow>();
        Follow.go = Spaceship;
        CaptnBroHead = Spaceship.transform.FindChild("CaptnBroHead").GetComponent<SpriteRenderer>();
        GlassAnim = Spaceship.transform.FindChild("Glass").GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
        AL = GetComponent<AudioListener>();
        Health = 100;
    }

	// Update is called once per frame
	void Update () {
        if (Health <= 0)
            Die();
        else
        {
            HandleInput();

            AL.transform.position = Position;
        }
	}

    void Die()
    {
        if (!dead)
        {
            ParticleSystem deathEffect = Instantiate(DeathEffect);
            deathEffect.transform.position = MoveCharacter.transform.position;
            Destroy(deathEffect.GetComponent<ParticlesGravity>(), deathEffect.startLifetime);
            Destroy(deathEffect.gameObject, deathEffect.startLifetime);
            Destroy(Char);
            dead = true;
            Follow.go = gameObject;
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInShip)
            {
                AS.transform.position = Spaceship.transform.position;
                AS.pitch = 1;
                AS.PlayOneShot(OpenSAS);

                Char = Instantiate(CharacterPrefab, Spaceship.transform.position, Spaceship.transform.rotation) as GameObject;
                MoveCharacter = Char.GetComponent<MoveCharacter>();
                isInShip = false;
                Follow.go = Char;
                Follow.size = Follow.CHARACTER_SIZE;
                CaptnBroHead.enabled = false;
                GlassAnim.SetBool("Opened", true);
                MoveSpaceship.enabled = false;
            }
            else if ((Char.transform.position - Spaceship.transform.position).magnitude < MIN_DIST)
            {
                AS.transform.position = Spaceship.transform.position;
                AS.pitch = -1;
                AS.PlayOneShot(OpenSAS);

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
