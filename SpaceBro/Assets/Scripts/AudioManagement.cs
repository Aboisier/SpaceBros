using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioManagement : MonoBehaviour {


    public AudioMixer master;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        master.SetFloat("LowPass", ComputeLowPass());
	}

    float ComputeLowPass()
    {
        float x = Camera.main.orthographicSize;

        if (x < 10)
            return 22000f;

        return 2 * Mathf.Pow(10, 6) * Mathf.Pow(2.71f, -0.461f*x);
    }
}
