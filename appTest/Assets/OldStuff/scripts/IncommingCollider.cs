using UnityEngine;
using System.Collections;

public class IncommingCollider : MonoBehaviour {

	GameObject g;
	public string incomming = "foo";
	string incommingLast;
	

	void OnTriggerEnter2D(Collider2D other) {
		g = other.gameObject;
		incommingLast = incomming;
		incomming = g.name;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
