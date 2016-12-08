using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Priority_Queue;


public class AStarPathAgent : PathAgent {
	
	public override Pathfinder createPathfinder () {
		
		return new AStarPathfinder ();


	}
}


public delegate float Heuristic(int a, int b);

public class AStarPathfinder : Pathfinder {

	protected Heuristic guessCost;

    //public AStarPathfinder(Heuristic h) {
	//	guessCost = h;
	//}

	public override List<int> findPath(int start, int goal) {
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

	protected int heuristic (int a, int b){
		GameObject waypointA;
		GameObject waypointB;
		waypointA = GameObject.Find("waypoint"+ a);
		waypointB = GameObject.Find("waypoint"+ b);
		float Distance = Vector3.Distance (waypointA.transform.position, waypointB.transform.position);
		return (int)Distance;
	}
}