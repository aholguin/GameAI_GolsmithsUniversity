using UnityEngine;
using System.Collections.Generic;

public class RandomAgent : MonoBehaviour
{

    public GameObject waypointSet;  // Set from inspector

    // Waypoints
    protected Graph navGraph;
    protected List<GameObject> waypoints;
    protected int targetNode;

    public float speed;
    protected static float NEARBY = 0.2f;
    static System.Random rnd = new System.Random();

    void Start()
    {
        waypoints = new List<GameObject>();
        navGraph = new AdjacencyListGraph();
        findWaypoints();
        buildGraph();
    }

    void Update()
	{
        float dist = Vector3.Distance(targetPosition(), transform.position);
        if (dist < NEARBY)
        {
            List<int> nodes = navGraph.neighbours(targetNode);
            targetNode = nodes[rnd.Next(nodes.Count)];
            Debug.Log("Targeted waypoint " + targetNode);
			nodes.Clear ();
        }
    }

    void FixedUpdate()
    {
        Vector3 targetPosn = targetPosition();
        transform.position = Vector3.MoveTowards(transform.position, targetPosn, speed);
    }

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			other.gameObject.SetActive (false);
		}
	}

    Vector3 targetPosition()
    {
        return waypoints[targetNode].transform.position;
    }

    void findWaypoints()
    {
        if (waypointSet != null)
        {
            foreach (Transform t in waypointSet.transform)
            {
                waypoints.Add(t.gameObject);
            }
            Debug.Log("Found " + waypoints.Count + " waypoints.");
        }
        else {
            Debug.Log("No waypoints found.");
        }
    }
    
    void buildGraph()
    {
        for (int i = 0; i < waypoints.Count; i++)
        {
            navGraph.addNode(i);

        }
        // TO DO: add edges


			navGraph.addEdge (0, 1);
			navGraph.addEdge (0, 2);
			navGraph.addEdge (1, 0);
			navGraph.addEdge (1, 2);
		    navGraph.addEdge (1, 3); 
			navGraph.addEdge (2, 1);
		    navGraph.addEdge (2, 0);
			navGraph.addEdge (2, 3);
			navGraph.addEdge (3, 2);
		}
    }

