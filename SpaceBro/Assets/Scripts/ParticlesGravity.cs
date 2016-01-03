using UnityEngine;
using System.Collections;

public class ParticlesGravity : MonoBehaviour {

    ParticleSystem Pe;
    ParticleSystem.Particle[] particles;
    // Use this for initialization
    void Start () {
        Pe = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[Pe.maxParticles];
	}
	
	// Update is called once per frame
	void Update () {
        int nb = Pe.GetParticles(particles);

        for (int i = 0; i < nb; ++i)
        {
            particles[i].velocity += Gravity.GravityAt(particles[i].position, 0.1f);
        }

        Pe.SetParticles(particles, nb);
	}
}
