using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explode : MonoBehaviour {


    public List<GameObject> Items;
    public float Force;

	void Start () {
        foreach (GameObject go in Items)
        {
            go.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (GameObject go in Items)
            {
                Vector3 direction = go.transform.position - transform.position;
                Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
                go.GetComponent<BoxCollider2D>().enabled = true;
                rb.isKinematic = false;
                rb.WakeUp();
                rb.AddForce(direction * Force * 10);
            }
        }
	}
}
