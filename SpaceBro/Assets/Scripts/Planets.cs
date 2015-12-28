using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class Planets : MonoBehaviour {
    const float DEFAULT_PLANET_RADIUS = 30;
    const float DEFAULT_GRAVITY = 3.25f;
    const float GRAVITY_UNCERTAINTY = 1f;
    const float MAX_SCALE = 1f;
    const float MIN_SCALE = 0.25f;

    public List<Sprite> PlanetPatterns;

    List<Color> Colors = new List<Color> {  new Color( 239/255f,  65/255f,  54/255f), // Orange
                                            new Color( 190/255f,  30/255f,  45/255f), // Darker orange
                                            new Color(   0/255f, 148/255f,  68/255f), // Green
                                            new Color(   1/255f,  65/255f,  54/255f), // Turquoise
                                            new Color(  28/255f, 125/255f, 188/255f), // Got bored here
                                            new Color(  39/255f, 170/255f, 225/255f),
                                            new Color(   0/255f, 104/255f,  56/255f),
                                            new Color( 247/255f, 148/255f,  30/255f),
                                            new Color( 141/255f, 198/255f,  63/255f),
                                            new Color(  96/255f,  57/255f,  19/255f),
                                            new Color( 146/255f,  39/255f, 143/255f),
                                            new Color(  43/255f,  53/255f, 144/255f),
                                            new Color(  93/255f, 130/255f,   0/255f) };
    public List<GameObject> PlanetsList;
    public GameObject TemplatePlanet;

    public float maxDist;
    void Start () {
        PlanetsList = new List<GameObject>();
        AddRandomPlanet();
    }

    public void AddRandomPlanet()
    {
        // Instantiate the planet
        GameObject go = Instantiate(TemplatePlanet, Vector3.zero + new Vector3(0,0,2), Quaternion.identity) as GameObject;

        // Sets the scaling and the mass of the planet
        float scale = Random.Range(MIN_SCALE, MAX_SCALE);
        go.transform.localScale = new Vector3(scale, scale, 1);
        go.GetComponent<Rigidbody2D>().mass = GenerateRandomMass(DEFAULT_PLANET_RADIUS * scale);

        // Picks random colours from the list, chooses a pattern and rotates it.
        int primary = Random.Range(0, Colors.Count);
        int second = Random.Range(0, Colors.Count);
        int pattern = Random.Range(0, PlanetPatterns.Count);

        go.transform.FindChild("base").GetComponent<SpriteRenderer>().color = Colors[primary];
        go.transform.FindChild("halo").GetComponent<SpriteRenderer>().color = Colors[second]/1.5f - new Color(0,0,0,0.3f);
        go.transform.FindChild("spots").GetComponent<SpriteRenderer>().color = Colors[second];
        go.transform.FindChild("spots").transform.Rotate(0, 0, Random.Range(0, 360));
        go.transform.FindChild("spots").GetComponent<SpriteRenderer>().sprite = PlanetPatterns[pattern];

        // Generates a name
        go.name = PlanetNameGenerator.GenerateName();

        // Adds the planet to the list
        PlanetsList.Add(go);
    }



    public float GenerateRandomMass(float radius)
    {
        // Basically generates a mass so that the gravity of the planet is around a median given gravity,
        // with a given uncertainty and a given gravitationnal constant.
        return Random.Range(DEFAULT_GRAVITY - GRAVITY_UNCERTAINTY, DEFAULT_GRAVITY + GRAVITY_UNCERTAINTY) *
               Mathf.Pow(radius, 2) / Gravity.GRAVITATIONNAL_CONSTANT;
    }

	// Update is called once per frame
	void Update () {

	}

    public GameObject FindClosestPlanet(Vector3 pos)
    {
        GameObject closestPlanet = PlanetsList[0];

        foreach (GameObject planet in PlanetsList)
        {
            closestPlanet = (planet.transform.position - pos).magnitude < (closestPlanet.transform.position - pos).magnitude ? 
                             planet : closestPlanet;
        }

        maxDist = (closestPlanet.transform.position - pos).magnitude;

        return closestPlanet;
    }

    //Computes the distance between two given vectors
    float ComputeDist(Vector3 pos1, Vector3 pos2)
    {
        return (pos1 - pos2).magnitude;
    }
}
