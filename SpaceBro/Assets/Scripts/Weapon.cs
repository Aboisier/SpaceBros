using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

    public float FireRate = 0; // 0 equals single burst
    public LayerMask WhatToHit;
    Transform FirePoint;
    public float Damage = 10;
    public bool isAutonomous;
    public float BetweenShots = 0.6f;
    public ParticleSystem ImpactEffect;
    float timer;

    float LaserBeamTimer { get; set; }
    float LaserBeamDuration { get; set; }

    LineRenderer lineRenderer;
    public GameObject muzzleFlash;
    // Use this for initialization
    void Start()
    {
        FirePoint = transform.FindChild("FirePoint");
        if (FirePoint == null)
            Debug.LogError("No FirePoint");
        timer = 0;
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer != null)
        {
            lineRenderer.useWorldSpace = true;
            lineRenderer.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (lineRenderer != null)
        {
            LaserBeamTimer += Time.deltaTime;
            if (LaserBeamTimer > LaserBeamDuration)
                lineRenderer.enabled = false;
        }
        

        if (isAutonomous && Input.GetMouseButtonDown(0))
        {
            Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    public void Shoot(Vector3 target)
    {
        if (timer > BetweenShots)
        {
            timer = 0;
            Vector2 mouse = new Vector2(target.x, target.y);
            Vector2 firePoint = new Vector2(FirePoint.position.x, FirePoint.position.y);

            Vector3 begin = FirePoint.position;
            Vector3 end = mouse + (mouse - firePoint) * 100;

            RaycastHit2D hit = Physics2D.Raycast(firePoint, mouse - firePoint, 100, WhatToHit);

            //DrawImpact(firePoint, end - begin, 0.1f);

            if (hit.collider != null)
            {
                end = hit.point;
                if (hit.collider.GetComponent<CharacterAI>() != null)
                    hit.collider.GetComponent<CharacterAI>().Hit(Damage);
                DrawImpact(hit.point, hit.normal);
            }

            DrawLaserBeam(begin, end, 0.02f);
        }
    }
    
    void DrawImpact(Vector2 pos, Vector2 dir, float duration)
    {
        if (ImpactEffect != null)
        {
            ParticleSystem impact = Instantiate(ImpactEffect);
            impact.Play();
            
            impact.transform.position = pos;
            impact.transform.rotation = Quaternion.LookRotation(dir);
            Destroy(impact.GetComponent<ParticlesGravity>(), duration);
            Destroy(impact, duration);
        }

        if (muzzleFlash != null)
        {
            GameObject muzzle = Instantiate(muzzleFlash);
            muzzle.transform.position = pos;
            muzzle.transform.rotation = Quaternion.LookRotation(dir);
            
            Destroy(muzzle, duration);
        }
    }

    void DrawImpact(Vector2 pos, Vector2 dir)
    {
        if (ImpactEffect != null)
        {
            DrawImpact(pos, dir, ImpactEffect.startLifetime);
        }
    }

    void DrawLaserBeam(Vector3 begin, Vector3 end, float duration)
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetVertexCount(2);
            lineRenderer.SetPosition(0, begin);
            lineRenderer.SetPosition(1, end);
            LaserBeamDuration = duration + 5;
            LaserBeamTimer = 0;
        }
    }
}
