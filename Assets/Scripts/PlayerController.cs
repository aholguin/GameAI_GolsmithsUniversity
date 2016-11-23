//basic code taken from https://unity3d.com/learn/tutorials/projects/roll-ball-tutorial as a starting point 
//Implementation of Waypoints and FSM will be added accordingly

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
// The code below stores variables for different functions that are needed
// for this game to run
	public float speed;
	public Text countText;
	public Text winText;
	private Rigidbody rb;
	private int count;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		winText.text = "";
	}
	void FixedUpdate ()
	//Ball movement
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
	
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter (Collider other)
	//This is what happens when the ball picks up an object
	//The counter is incremented by 1
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		} 
			
	}

	void SetCountText ()
	//The wintext text is displayed once you collect all the pickups
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 12)
			{
				winText.text = "You Win!";
			}
	}

}
