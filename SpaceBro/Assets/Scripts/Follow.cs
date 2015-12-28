using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    // Use this for initialization
    const float INITIAL_SIZE = 10;
    const float FOLLOW_CHARACTer_SIZE = 10;
    const float MAX_DIST = 5;

    public GameObject go;

    Camera cam;

	void Start () {
        cam = Camera.main;
        cam.orthographicSize = INITIAL_SIZE;
	}
	
	// Update is called once per frame
        // Thing to follow
	void FixedUpdate () {
        FollowTransorm(go.transform);        
    }

    void FollowTransorm(Transform trans)
    {
        if ((trans.position - transform.position).magnitude > MAX_DIST)
            transform.position += (trans.position - transform.position).normalized * ((trans.position - transform.position).magnitude - MAX_DIST);

        transform.rotation = Quaternion.Lerp(transform.rotation, trans.rotation, 0.05f);
        transform.position = new Vector3(transform.position.x, transform.position.y, -5);

        cam.orthographicSize = INITIAL_SIZE / Mathf.Max(1, 1);
    }
}
