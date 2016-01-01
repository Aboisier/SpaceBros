using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    // Use this for initialization
    public const float SPACESHIP_SIZE = 10;
    public const float CHARACTER_SIZE = 5;
    public const float UNZOOMED_SIZE = 20;
    const float CAMERA_POS_LERPING_SPEED = 0.05f;
    const float CAMERA_ROT_LERPING_SPEED = 0.15f;
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

        transform.rotation = Quaternion.Lerp(transform.rotation, trans.rotation, CAMERA_ROT_LERPING_SPEED);
        transform.position = new Vector3(transform.position.x, transform.position.y, -5);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, size, CAMERA_POS_LERPING_SPEED);
    }
}
