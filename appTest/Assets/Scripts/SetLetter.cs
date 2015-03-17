using UnityEngine;
using System.Collections;
using System.Linq;

public class SetLetter : MonoBehaviour {

	public bool forCheck;
	public GameObject gameScene;

	private string s;
	private float t;

	// Use this for initialization
	void Start () {
	}

	void OnMouseDown (){
		if (forCheck){
			check();
		}else{
			gameManager.reset[0] = true;
			gameManager.reset[1] = true;
			gameManager.reset[2] = true;
		}
	}

	void check(){
		print ("OK");
		gameManager.ok = true;

		gameManager.letter[0] = Mathf.Round(gameManager.letter[0]);
		gameManager.letter[1] = Mathf.Round(gameManager.letter[1]);
		gameManager.letter[2] = Mathf.Round(gameManager.letter[2]);

		s = null;
		s += ((char)(gameManager.letter[0]+ 65f)).ToString();
		s += ((char)(gameManager.letter[1]+ 65f)).ToString();
		s += ((char)(gameManager.letter[2]+ 65f)).ToString();

		if(gameManager.storePath.ContainsKey(gameManager.level)){
			string st = gameManager.storePath[gameManager.level] +  ", " + s;
			gameManager.storePath.Remove(gameManager.level);
			gameManager.storePath.Add(gameManager.level,st);
		}else{
			gameManager.storePath.Add(gameManager.level,s);
		}

		if(gameManager.letter.SequenceEqual(gameManager.letterEnd)){
			gameManager.setAllLetter = false;
			CalcScore();
			t = Time.timeSinceLevelLoad;
			gameManager.time[gameManager.level] = t;
			Application.LoadLevel("WinScene");
		}
		gameManager.setChoosableLetters();		
	}

	void CalcScore(){
		int anzWords = (((gameManager.storePath[gameManager.level].ToString().Length)+2)/5);
		int min = 0;
		int factor = 0;
		int sc = 0;
		
		if(gameManager.level <= 15){
			min = 3;
			//min = 5;
		}
		if(gameManager.level >= 16 && gameManager.level <= 25){
			min = 7;
		}
		if(gameManager.level >= 26 && gameManager.level <= 35){
			min = 9;
		}		
		if(gameManager.level >= 36 && gameManager.level <= 45){
			min = 11;
		}
		if(gameManager.level >= 46 && gameManager.level <= 55){
			min = 13;
		}
		if(gameManager.level >= 56 && gameManager.level <= 65){
			min = 15;
		}
		
		factor = Mathf.CeilToInt((float)anzWords / (float)min);
		sc = 4 - factor;
		gameManager.oldScore = (int)gameManager.score[gameManager.level];
		gameManager.score[gameManager.level] = sc;
	}
	
	
	
	// Update is called once per frame
	void Update () {

	}
}
