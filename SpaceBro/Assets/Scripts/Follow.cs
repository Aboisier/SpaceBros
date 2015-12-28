using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    // Use this for initialization
    public const float SPACESHIP_SIZE = 10;
    public const float CHARACTER_SIZE = 5;
    const float CAMERA_LERPING_SPEED = 0.05f;
    const float MAX_DIST = 5;

    public GameObject go;
    public float size;

    Camera cam;

	void Start () {
        cam = Camera.main;
        cam.orthographicSize = SPACESHIP_SIZE;
        size = SPACESHIP_SIZE;
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

        transform.rotation = Quaternion.Lerp(transform.rotation, trans.rotation, CAMERA_LERPING_SPEED);
        transform.position = new Vector3(transform.position.x, transform.position.y, -5);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, size, CAMERA_LERPING_SPEED);
    }
}
