using UnityEngine;
using System.Collections;

public class ShowInBox : MonoBehaviour {

	//float x;
	float y;
	bool inBox = false;
	Color fadeColor = new Color(0F, 0F, 0F, 0F);
	string choose;
	IncommingCollider box;
	SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		sprite = (SpriteRenderer)gameObject.GetComponent(typeof(SpriteRenderer));
		sprite.renderer.material.color = fadeColor;

	}

	void OnTriggerEnter2D(Collider2D other) {

		GameObject g = GameObject.Find("boxBlue");
		box = g.GetComponent<IncommingCollider>();

		//x = other.transform.position.x;
		y = other.transform.position.y;

		sprite = (SpriteRenderer)gameObject.GetComponent(typeof(SpriteRenderer));
		fadeColor.a = 1;
		sprite.renderer.material.color = fadeColor;

		inBox = true;
	}

	void OnTriggerExit2D(Collider2D other) {

		fadeColor.a = 0;
		sprite.renderer.material.color = fadeColor;

		inBox = false;
	}


	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {

			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Ended && inBox && string.Equals(box.incomming, this.name)) {
				//float deltaX =  transform.position.x - x;
				//transform.parent.position = new Vector3(transform.parent.position.x - deltaX, transform.parent.position.y, 0);

				float deltaY =  transform.position.y - y;
				transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y - deltaY, 0);
			}
		}
	}
}
