using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    // Use this for initialization

    public GameObject go;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = go.transform.position - new Vector3(0, 0, 5); 
        transform.rotation = go.transform.rotation;

    }
}
