using UnityEngine;
using System.Collections.Generic;

public class AdjacencyListGraph : Graph
{
	List<int> node = new List<int> ();
	List<int> neighbour = new List<int> ();

	List <List<int>> edge = new List<List<int>> ();

    public bool  addNode(int a) {
		this.node.Add (a);
        return true;
    }          // true if node added
    public bool addEdge(int a, int b){

		List<int> lst = new List<int> ();
		lst.Add (a);
		lst.Add (b);
		this.edge.Add (lst);

        return true;
    }


    public List<int> nodes()
	{
		return this.node;
	}

	
    public List<int> neighbours (int a)
    {
		for (int i = 0; i < this.edge.Count; i++) {
			if (this.edge [i] [0] == a) {
				this.neighbour.Add (this.edge [i] [1]);
			} else if (this.edge [i] [1] == a) {
				this.neighbour.Add (this.edge [i] [0]);
			}
		}
		return this.neighbour;

    }

	public int cost(int a, int b){
		GameObject waypointA;
		GameObject waypointB;
		waypointA = GameObject.Find ("Waypoint" + a);
		waypointB = GameObject.Find ("Waypoint" + b);
		float Distance = Vector3.Distance (waypointA.transform.position, waypointB.transform.position);
		//Debug.Log (Distance);
		return (int)Distance;
	}
}