using UnityEngine;
using System.Collections;

public class SetWords : MonoBehaviour {
	public int level;
	public GameObject UIFBIsLoggedIn;
	public GameObject Fooo2;

	public GameObject s1;
	public GameObject s2;
	public GameObject s3;


	char[,] words;

	// Use this for initialization
	void Start () {
		words = new char[3,6] {{'C','O','P','B','A','T'},{'B','E','T','C','A','P'},{'T','A','P','N','E','T'}};
		gameManager.LScene = Fooo2;
		gameManager.level = level;
		SetScore();
	}

	void OnMouseUp() {
		initWords();
		Fooo2.SetActive(false);
		Application.LoadLevel("GameScene");
	}


	void initWords(){
		//Test set ARE
		/*char first = 'A';
		float f = (float)(char.ToUpper(first) - 65);
		char second = 'R';
		float s = (float)(char.ToUpper(second) - 65);
		char third = 'E';
		float t = (float)(char.ToUpper(third) - 65);

        char firstEnd = 'A';
		float fEnd = (float)(char.ToUpper(firstEnd) - 65);
		char secondEnd = 'G';
		float sEnd = (float)(char.ToUpper(secondEnd) - 65);
		char thirdEnd = 'O';
		float tEnd = (float)(char.ToUpper(thirdEnd) - 65);*/


		char first = words[(level-1),0];
		float f = (float)(char.ToUpper(first) - 65);
		char second = words[(level-1),1];
		float s = (float)(char.ToUpper(second) - 65);
		char third = words[(level-1),2];
		float t = (float)(char.ToUpper(third) - 65);

		char firstEnd = words[(level-1),3];
		float fEnd = (float)(char.ToUpper(firstEnd) - 65);
		char secondEnd = words[(level-1),4];
		float sEnd = (float)(char.ToUpper(secondEnd) - 65);
		char thirdEnd = words[(level-1),5];
		float tEnd = (float)(char.ToUpper(thirdEnd) - 65);


		gameManager.letter[0] = f;
		gameManager.letter[1] = s;
		gameManager.letter[2] = t;

		gameManager.oldLetter[0] = f;
		gameManager.oldLetter[1] = s;
		gameManager.oldLetter[2] = t;

		gameManager.letterEnd[0] = fEnd;
		gameManager.letterEnd[1] = sEnd;
		gameManager.letterEnd[2] = tEnd;

		gameManager.level = level;
		if(gameManager.storePath.ContainsKey(level)){
			print("already played.");
			gameManager.storePath.Remove(level);
		}
	}

	public void SetScore(){
		if(gameManager.score.ContainsKey(level)){
			int i = (int)gameManager.score[level];
		switch(i){
			case 1:
				s1.SetActive(true);
				s2.SetActive(false);
				s3.SetActive(false);
				break;
			case 2:
				s1.SetActive(true);
				s2.SetActive(true);
				s3.SetActive(false);
				break;
			case 3:
				s1.SetActive(true);
				s2.SetActive(true);
				s3.SetActive(true);
				break;
			}
		}else{
			s1.SetActive(false);
			s2.SetActive(false);
			s3.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
	
	public void HideThings(bool b){
		UIFBIsLoggedIn.SetActive(!b);
	}
}
