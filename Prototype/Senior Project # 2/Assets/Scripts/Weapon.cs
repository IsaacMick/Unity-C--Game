using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate = 0;
    public int damage = 10;
    public LayerMask hittable;

    public Transform bulletTrail;
    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;

    public Transform flashPrefab;

    public Transform hitPrefab;

    float timeToFire = 0;
    Transform firePoint;
    // Start is called before the first frame update
    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("NO FIRE POINT DETECTED");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }

        else
        {
            if(Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPos = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPos, mousePosition - firePointPos, 100, hittable);

        Debug.DrawLine(firePointPos, (mousePosition - firePointPos) * 100, Color.cyan);

        if (hit.collider != null)
        {

            Debug.DrawLine(firePointPos, hit.point, Color.red);
            

            Enemy enemy = hit.collider.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.DamageEnemy(damage);
                Debug.Log("HIT: " + hit.collider.name + "FOR: " + damage);
            }
        }

        if (Time.time >= timeToSpawnEffect)
        {
            Vector3 hitPos;
            Vector3 hitNormal;

            if (hit.collider == null)
            {
                hitPos = (mousePosition - firePointPos) * 30;
                hitNormal = new Vector3(9999, 9999, 9999);
            }

            else
            {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }
            effect(hitPos, hitNormal);
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
    }

    void effect(Vector3 hitPosition, Vector3 hitNormal)
    {
        Transform trail = Transform.Instantiate(bulletTrail, firePoint.position, firePoint.rotation);
        LineRenderer lineRender = trail.GetComponent<LineRenderer>();
        if (lineRender != null)
        {
            lineRender.SetPosition(0, firePoint.position);
            lineRender.SetPosition(1, hitPosition);
        }

        Destroy(trail.gameObject, .02f);

        if (hitNormal != new Vector3(9999, 9999, 9999))
        {
            Transform hitPart = Transform.Instantiate(hitPrefab, hitPosition, Quaternion.FromToRotation(Vector3.right, hitNormal));
            Destroy(hitPart.gameObject, 1);
        }
        Instantiate(bulletTrail, firePoint.position, firePoint.rotation);
        Transform flashInstance = (Transform) Instantiate(flashPrefab, firePoint.position, firePoint.rotation);

        flashInstance.parent = firePoint;
        float size = Random.Range(0.6f, 0.9f);
        flashInstance.localScale = new Vector3(size, size, size);

        Destroy(flashInstance.gameObject, .05f);
    }
}
