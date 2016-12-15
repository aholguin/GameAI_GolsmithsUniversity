using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Objective : MonoBehaviour {

	public StateMachine<Objective> fsm;
	// Set from inspector

	public WaypointGraph waypoints;
	public GameObject waypointSet;
	public GameObject Player;
	public List<int> path;
	public float speed;
	public float NEARBY = 0.2f;
	public int? current;
	//public Graph navGraph;



	// Use this for initialization
	void Start () {
		
		waypoints = new WaypointGraph (waypointSet);

		fsm = new StateMachine<Objective>(this);
		fsm.current = new SleepState();


	}
	
	// Update is called once per frame
	void Update () {
		fsm.update ();
	}

	void FixedUpdate() {
		if (path.Count > 0) {
			GameObject next = waypoints[path[0]];
			Vector3 position = next.transform.position;
			transform.position = Vector3.MoveTowards (transform.position, position, speed);
		}
	}
		


	public bool isSphereClose() {
		

		Vector3 there = Player.transform.position;
		Vector3 here = transform.position;

		// Are we there yet?
		float distance = Vector3.Distance (here, there);
		if (distance < NEARBY + 2) {
			return true;
		} else {
			return false;
		}

	}

	public bool isObjectiveInTarget() { 
		if (Input.GetKey ("2")) {
			return true;
		} else {
			return false;
		}
	}






}
