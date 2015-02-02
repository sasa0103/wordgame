using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetArrow : MonoBehaviour {

	public GameObject LetterBox;
	public GameObject arrowUp;
	public GameObject arrowDown;
	public int box;
	public Sprite a;



	void OnMouseUp(){
		print ("letter: " + LetterBox.GetComponent<Image>().sprite.name);
		LetterBox.GetComponent<Image>().sprite = a;
		print ("letter: " + LetterBox.GetComponent<Image>().sprite.name);
		/*
		if(name.Equals("down3") && gameManager.canChange[2] == true){
			if(gameManager.letter[2] <= 0f){
				gameManager.letter[2] =  0f;
			}else{
				gameManager.letter[2] =  gameManager.letter[2] - 1;
			}
			print ("value:" + gameManager.letter[2]);

		}*/

	}
}
