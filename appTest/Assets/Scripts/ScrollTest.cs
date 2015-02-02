using UnityEngine;
using System.Collections;


public class ScrollTest : MonoBehaviour {
	public int box;
	public bool toChoose;

	SpriteRenderer sprite;

	ArrayList aArray = new ArrayList();

	Animator anim;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.SetFloat("SelectLetter", gameManager.letter[box]);
		sprite = (SpriteRenderer)anim.GetComponent(typeof(SpriteRenderer));
	}
	

	void ScrollLetters(){
		if (Input.touchCount == 1) {
			
			Touch touch = Input.GetTouch (0);
			
			Vector3 wp = Camera.main.ScreenToWorldPoint(touch.position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (collider2D == Physics2D.OverlapPoint(touchPos) && toChoose){
				gameManager.canChange[(box+1)%3] = false;
				gameManager.canChange[(box+2)%3] = false;
				gameManager.klickBox[box] = true;

				float delta = (2f*touch.deltaPosition.y/Screen.width*(Mathf.Abs(touch.deltaPosition.y)/2+1));
				float newLetter = gameManager.letter[box] - delta;
				if (newLetter < 0f) {
					newLetter = 0f;
				}else if (newLetter > 25f) {
					newLetter = 25f;
				}
				gameManager.letter[box] = newLetter;
				anim.SetFloat("SelectLetter", gameManager.letter[box]);

				gameManager.valid = true;
				if(!aArray.Contains(Mathf.Round(gameManager.letter[box]))){
					anim.SetFloat("fade", 1f);
					gameManager.isFade[box] = true;
				}
				else{
					anim.SetFloat("fade", 0f);
					gameManager.isFade[box] = false;
				}

			}
		}
	}

	void setBack(){
		gameManager.canChange[0] = true;
		gameManager.canChange[1] = true;
		gameManager.canChange[2] = true;
		gameManager.klickBox[0] = false;
		gameManager.klickBox[1] = false;
		gameManager.klickBox[2] = false;
		anim.SetBool("OKisTouched", gameManager.ok);
		gameManager.ok = false;
	}

	void setChoosableLetters(){
		char x = (char)(gameManager.letter[0]+ 65f);
		char y = (char)(gameManager.letter[1]+ 65f);
		char z = (char)(gameManager.letter[2]+ 65f);

		aArray = gameManager.getChoosableLetters(x, y, z, box);
	}


	// Update is called once per frame
	void FixedUpdate () {
		if(gameManager.reset[box] && toChoose){
			setBack();

			gameManager.letter[box] = gameManager.oldLetter[box];
			anim.SetFloat("SelectLetter", gameManager.letter[box]);
			anim.SetFloat("fade", 0f);
			gameManager.isFade[box] = false;
			gameManager.reset[box] = false;
		}
		if(!gameManager.ok && gameManager.canChange[box] && !gameManager.reset[box]){
			if (gameManager.first[box] && toChoose){
				setChoosableLetters();
				gameManager.first[box] = false;
			}
			ScrollLetters();

		}else if(gameManager.ok && gameManager.canChange[box]){
			setBack();
			gameManager.first[0] = true;
			gameManager.first[1] = true;
			gameManager.first[2] = true;
			if (!toChoose) {
				anim.SetFloat("SelectLetter", gameManager.letter[box]);
				gameManager.oldLetter[box] = gameManager.letter[box];
			}
		}
	}
}
	
