using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    // Use this for initialization

    public GameObject go;
    public float maxDist = 1;

	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //transform.position = Vector3.Lerp(go.transform.position, transform.position, 0.97f) - new Vector3(0, 0, 5);  // lerp plz
        if ((go.transform.position - transform.position).magnitude > maxDist)
            transform.position += (go.transform.position - transform.position).normalized * ((go.transform.position - transform.position).magnitude - maxDist);

        transform.rotation = Quaternion.Lerp(transform.rotation, go.transform.rotation, 0.1f);


        transform.position = new Vector3(transform.position.x, transform.position.y, -5);
    }
}
