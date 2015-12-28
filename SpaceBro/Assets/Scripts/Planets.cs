using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class Planets : MonoBehaviour {
    const float DEFAULT_PLANET_RADIUS = 30;
    const float DEFAULT_GRAVITY = 3.25f;
    const float GRAVITY_UNCERTAINTY = 1f;

    List<Color> Colors =  new List<Color> { new Color(239/255f,  65/255f,  54/255f), // Orange
                                            new Color(190/255f,  30/255f,  45/255f), // Darker orange
                                            new Color(  0/255f, 148/255f,  68/255f), // Green
                                            new Color(1/255f,  65/255f,  54/255f)};  // Turquoise

    public List<GameObject> PlanetsList;
    public GameObject GenericPlanet;
    public float maxDist;
    void Start () {
        PlanetsList = new List<GameObject>();
        AddRandomPlanet();
    }

    public void AddRandomPlanet()
    {
        GameObject go = Instantiate(GenericPlanet, Vector3.zero, Quaternion.identity) as GameObject;
        float scale = Random.Range(0.10f, 2f);
        go.transform.localScale = new Vector3(scale, scale, 1);

        go.GetComponent<Rigidbody2D>().mass = GenerateRandomMass(DEFAULT_PLANET_RADIUS * scale);

        go.GetComponent<SpriteRenderer>().color = Colors[Random.Range(0, Colors.Count)];
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
