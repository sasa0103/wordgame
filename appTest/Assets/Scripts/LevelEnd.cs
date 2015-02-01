using UnityEngine;
using System.Collections;


public class LevelEnd : MonoBehaviour {
	public GameObject s1;
	public GameObject s2;
	public GameObject s3;


	// Use this for initialization
	void Start () {
	}
	
	void OnMouseUp() {
		//gameManager.LScene.SetActive(true);
		SetScore();
		Application.LoadLevel("LevelScene");
	}

	public void SetScore(){
		if(gameManager.score.ContainsKey(gameManager.level)){
			int i = (int)gameManager.score[gameManager.level];
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
		gameManager.score[gameManager.level] = Mathf.Max((int)gameManager.score[gameManager.level],(int)gameManager.oldScore);
		gameManager.oldScore = 0;
	}
}
