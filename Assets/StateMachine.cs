using UnityEngine;
using System.Collections;

public class StateMachine<A> {

	public A agent;

	public State<A> current;

	public StateMachine(A a) { agent = a; }

	public void changeState(State<A> next) {

		current.exit(agent);

		current = next;

		current.enter(agent);

	}

	public void update() {
		current.execute(agent);

	}

}

