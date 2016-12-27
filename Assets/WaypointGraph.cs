using UnityEngine;
using System.Collections.Generic;
using Priority_Queue;

public class WaypointGraph {

	public Graph navGraph;
	protected List<GameObject> waypoints;
	public GameObject this[int i] {
		get { return waypoints [i]; }
		set { waypoints [i] = value; }
	}

	public WaypointGraph(GameObject waypointSet) {

		waypoints = new List<GameObject> ();
		navGraph = new AdjacencyListGraph ();

		findWaypoints (waypointSet);
		buildGraph ();
	}
		
	private void findWaypoints(GameObject waypointSet) {

		if (waypointSet != null) {
			foreach (Transform t in waypointSet.transform) {
				waypoints.Add (t.gameObject);
			}
			Debug.Log("Found " + waypoints.Count + " waypoints.");

		} else {
			Debug.Log ("No waypoints found.");

		}
	}

	private void buildGraph() {

		int n = waypoints.Count;

		navGraph = new AdjacencyListGraph ();
		for (int i = 0; i < n; i++) {
			navGraph.addNode (i);
		}

		// ADD APPROPRIATE EDGES
		navGraph.addEdge (0, 1);
		navGraph.addEdge (1, 2);
		navGraph.addEdge (2, 3);
		navGraph.addEdge (3,4);
		navGraph.addEdge (4,5);
		navGraph.addEdge (5,6);
		navGraph.addEdge (6,7);
		navGraph.addEdge (7,0);

	

	}


	public int? findNearest(Vector3 here) {
		int? nearest = null;
		//Debug.Log ("number" + waypoints.Count);
		if (waypoints.Count > 0) {
			nearest = 0;
			Vector3 there = waypoints [0].transform.position;
			float minDistance = Vector3.Distance (here, there);

			for (int i = 1; i < waypoints.Count; i++) {
				there = waypoints[i].transform.position;
				float distance = Vector3.Distance (here, there);
				if (distance < minDistance) {
					nearest = i;
					minDistance = distance;
				}
			}
		}
		return nearest;
	}

	public int? findFurthest(Vector3 here){ //this function is going to find the furthest waypoint from the sphere
		int? furthest = null;
		//Debug.Log ("number" + waypoints.Count);
		if (waypoints.Count > 0) {
			furthest = 0;
			Vector3 there = waypoints [0].transform.position;
			float maxDistance = Vector3.Distance (here, there);
			//Debug.Log ("distance to 0 --" + maxDistance);
			for (int i = 1; i < waypoints.Count; i++) {
				there = waypoints[i].transform.position;

				float distance = Vector3.Distance (here, there);
				//Debug.Log ("distance to " + i + " --" + distance);
				if (distance > maxDistance) {
					furthest = i;
					maxDistance = distance;
				}
			}
		}
		return furthest;
	}



	public  List<int> findPath(int start, int goal) {
		// Use guessCost(a,b) to implement A*
		SimplePriorityQueue<int> frontier = new SimplePriorityQueue<int>();
		Dictionary<int, int> cameFrom = new Dictionary<int, int>();
		Dictionary<int, int> costSoFar = new Dictionary<int, int>();
		frontier.Enqueue(start, 0);
		cameFrom[start] = -1;
		costSoFar[start] = 0;

		while (frontier.Count > 0) {

			int current = frontier.Dequeue();

			if (current == goal) break;

			List<int> neighbours = navGraph.neighbours(current);

			foreach (int next in neighbours) {

				int nextCost = costSoFar[current] + navGraph.cost(current, next);



				if (!costSoFar.ContainsKey(next) || nextCost < costSoFar[next]) {

					frontier.Enqueue(next, nextCost + heuristic(next,goal));

					cameFrom[next] = current;

					costSoFar[next] = nextCost;

				}

			}
			neighbours.Clear ();

		}

		List<int> route = new List<int>();
		route.Add (goal);
		int reverse = goal;
		while (reverse != start) {
			route.Add (cameFrom [reverse]);
			reverse = cameFrom [reverse];
		}
		route.Reverse ();

		return route;


	}


	public int? findneighboursFar(int objective, Vector3 player) {
		List<int> neighbours = navGraph.neighbours(objective);

		float maxDistance = 0;
		int? wp  = null;
		foreach (int next in neighbours) {
			GameObject nextO = waypoints [next];
			float newDistance = Vector3.Distance (nextO.transform.position, player);
			if (newDistance > maxDistance) {
				maxDistance = newDistance;
				wp = next;
			}
		}
		//Debug.Log ("the far is "+ wp);
		return wp;
	}


	protected int heuristic (int a, int b){
		GameObject waypointA;
		GameObject waypointB;
		waypointA = GameObject.Find("Waypoint"+ a);
		waypointB = GameObject.Find("Waypoint"+ b);
		float Distance = Vector3.Distance (waypointA.transform.position, waypointB.transform.position);
		return (int)Distance;
	}


}
