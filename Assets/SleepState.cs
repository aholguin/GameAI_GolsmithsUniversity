using UnityEngine;
using System.Collections;

public class SleepState : State<Objective> {

	public void enter(Objective agent) {
		//Debug.Log ("method enter");
	}

	public void execute(Objective agent) {

		//Debug.Log ("Sleppening");

		if (agent.isSphereClose()) {

			string temp  = agent.logText.text;
			agent.logText.text = "The Objetive has changed its state to RUN!!";
			agent.logText.text +=  "\n^" +temp;
			agent.r.material.color= Color.red;
			//agent.sleep_state.text = "RUNNNN!!!!";
			agent.sleep_state.SetActive (false);
			agent.run_state.SetActive (true);
			agent.fsm.changeState (new RunState());

		}

	}

	public void exit(Objective agent) {
		//Debug.Log ("method exit");
	}
}

