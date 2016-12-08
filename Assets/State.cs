using UnityEngine;
using System.Collections;

public interface State<A> {

	void enter(A agent);

	void execute(A agent);

	void exit(A agent);

}