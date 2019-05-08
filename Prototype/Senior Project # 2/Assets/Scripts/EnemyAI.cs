using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof(Seeker))]
public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float updateRate = 2f;

    private Seeker seeker;
    private Rigidbody2D rb;

    //The calculated path
    public Path path;

    //AI SPEED
    public float AISpeed= 300f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;

    public float nextWaypointDistance = 3;

    private int currentWaypoint = 0;

    private bool playerFound = true;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            if (playerFound)
            {
                playerFound = false;
                StartCoroutine(searchForPlayer());
            }
            return;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    IEnumerator searchForPlayer()
    {
        GameObject objectSearch = GameObject.FindGameObjectWithTag("Player");
        if(objectSearch == null)
        {
            yield return new WaitForSeconds(.5f);
            StartCoroutine(searchForPlayer());
        }

        else
        {
            target = objectSearch.transform;
            playerFound = true;
            StartCoroutine(UpdatePath());

            yield return false;
        }
       
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("Path detected. Error: " + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            if (playerFound)
            {
                playerFound = false;
                StartCoroutine(searchForPlayer());
            }
            yield return false;
        }

        else
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
            yield return new WaitForSeconds(1f / updateRate);
            StartCoroutine(UpdatePath());
        }
        
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            if (playerFound)
            {
                playerFound = false;
                StartCoroutine(searchForPlayer());
            }
            return;
        }

        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
            {
                return;
            }
            Debug.Log("End of path");
            pathIsEnded = true;
            return;
        }

        pathIsEnded = false;

        //Direction to next point
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= AISpeed * Time.fixedDeltaTime;

        //Move the AI
        rb.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }
}
