using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

	Vector2 touchPos;
	Vector2 wp;
	Vector2 target;
	bool stop = false;
	string start;


	void Start (){

	}

	// Update is called once per frame
	void FixedUpdate () {

		if (Input.touchCount == 1) {

			Touch touch = Input.GetTouch (0);

			Vector3 wp = Camera.main.ScreenToWorldPoint(touch.position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (collider2D == Physics2D.OverlapPoint(touchPos))
			{
				float y = transform.position.y + 6f* touch.deltaPosition.y/ Screen.width;
				transform.position = new Vector3(transform.position.x, y, 0);
			}

		}
	}
}
