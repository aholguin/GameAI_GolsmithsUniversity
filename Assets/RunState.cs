using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class RunState : State<Objective> {

	public void enter(Objective agent) {
		//Find the  farthest waypont
		int?  furthest = agent.waypoints.findFurthest(agent.transform.position);
		int?  nearest = agent.waypoints.findNearest(agent.transform.position);

		//Debug.Log ("this is the furthest wp --" + furthest.Value);
		Debug.Log ("this is the nearest wp --" + nearest.Value);
		agent.path = agent.waypoints.findPath (nearest.Value,furthest.Value);

		Debug.Log(writePath (agent.path));

		//check if the player is between two waypoits of the path
		int inthepath = 0;
		foreach(int t in agent.path) {
			GameObject wp = agent.waypoints [t];
			float distance = Vector3.Distance (agent.Player.transform.position, wp.transform.position);
			if (distance < 7 ){
				inthepath++; 
				if (inthepath==2)
					break;
			}else {
				inthepath = 0;
			}
			Debug.Log ("Distance between the player and the WP ["+ t +"] --" + distance);
		} 

		//the player is in the route, the objective has to move to one of the neighbours 
		if (inthepath == 2) {

			int? wp;
			wp = agent.waypoints.findneighboursFar (agent.path[0],agent.Player.transform.position);
			int origin = agent.path[0]; 

			agent.path.Clear ();
			agent.path.Add (origin);
			agent.path.Add (wp.Value);
			Debug.Log("New route  "+ writePath (agent.path));
		}

		//Find the best route with A*




	}

	public void execute(Objective agent) {

		if (agent.path.Count > 0) {
			GameObject next = agent.waypoints [agent.path [0]];
			Vector3 there = next.transform.position;
			Vector3 here = agent.transform.position;

			// Are we there yet?
			float distance = Vector3.Distance (here, there);
			if (distance < agent.NEARBY) {
				// We're here
				agent.current = agent.path [0];
				agent.path.RemoveAt (0);
				Debug.Log ("Arrived at waypoint " + agent.current);

				//fsm//

			}
		} else {
		//if (agent.isObjectiveInTarget()) {

			agent.fsm.changeState (new SleepState());
		}

	}

	public void exit(Objective agent) {
		//Debug.Log ("method exit");
	}

	public static string writePath(List<int> path) {
		var s = new StringBuilder();
		bool first = true;
		foreach(int t in path) {
			if(first) {
				first = false;
			} else {
				s.Append(", ");
			}
			s.Append(t);
		}    
		return s.ToString();
	}
}
