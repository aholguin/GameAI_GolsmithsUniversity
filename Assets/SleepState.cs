using UnityEngine;
using System.Collections;

public class SleepState : State<Objective> {

	public void enter(Objective agent) {
		//Debug.Log ("method enter");
	}

	public void execute(Objective agent) {

		//Debug.Log ("Sleppening");

		if (agent.isSphereClose()) {
			
			agent.fsm.changeState (new RunState());
		}

	}

	public void exit(Objective agent) {
		//Debug.Log ("method exit");
	}
}

