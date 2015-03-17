using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetArrow : MonoBehaviour {

	public GameObject LetterBox;
	public GameObject LetterBoxStart;
	public GameObject LetterBoxEnd;
	public GameObject arrowUp;
	public GameObject arrowDown;
	public int box;
	public bool toChange;
	public string name;
	public Sprite a;
	public Sprite b;
	public Sprite c;
	public Sprite d;
	public Sprite e;
	public Sprite f;
	public Sprite g;
	public Sprite h;
	public Sprite i;
	public Sprite j;
	public Sprite k;
	public Sprite l;
	public Sprite m;
	public Sprite n;
	public Sprite o;
	public Sprite p;
	public Sprite q;
	public Sprite r;
	public Sprite s;
	public Sprite t;
	public Sprite u;
	public Sprite v;
	public Sprite w;
	public Sprite x;
	public Sprite y;
	public Sprite z;


	private int current = 0;



	void Start () {
		if(!gameManager.setAllLetter){
			gameManager.allLetter = new Sprite[] {a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z};		

			gameManager.setAllLetter = true;
			gameManager.setChoosableLetters();
		}
		
		LetterBoxStart.GetComponent<Image>().sprite = gameManager.allLetter[(int)gameManager.letter[box]];
		LetterBox.GetComponent<Image>().sprite = gameManager.allLetter[(int)gameManager.letter[box]];
		LetterBoxEnd.GetComponent<Image>().sprite = gameManager.allLetter[(int)gameManager.letterEnd[box]];
	}

	void OnMouseUp(){
		if(!gameManager.pressed){
			gameManager.canChange[box] = true;
			gameManager.canChange[(box+1)%3] = false;
			gameManager.canChange[(box+2)%3] = false;
			gameManager.pressed = true;

			gameManager.currentLetter = (int)gameManager.letter[box];
		}
		if(gameManager.canChange[box]){
			if(name.Equals("up0") || name.Equals("up1") || name.Equals("up2")){
				gameManager.currentLetter++;
				gameManager.currentLetter = gameManager.currentLetter%26;

				while (!gameManager.choosableLetters[box].ContainsKey(gameManager.currentLetter)){
					gameManager.currentLetter = (gameManager.currentLetter+1)%26;
				}

				print ("current: " + gameManager.currentLetter);
				Hashtable h = gameManager.choosableLetters[box];
				LetterBox.GetComponent<Image>().sprite = (Sprite)h[gameManager.currentLetter];
				gameManager.letter[box] = (float)gameManager.currentLetter;
			}
			if(name.Equals("down0") || name.Equals("down1") || name.Equals("down2")){
				gameManager.currentLetter = gameManager.currentLetter + 25;
				gameManager.currentLetter = gameManager.currentLetter%26;

				while (!gameManager.choosableLetters[box].ContainsKey(gameManager.currentLetter)){
					gameManager.currentLetter = (gameManager.currentLetter-1)%26;
					print ("current while -: " + gameManager.currentLetter);
				}

				print ("current: " + gameManager.currentLetter);
				Hashtable h = gameManager.choosableLetters[box];
				LetterBox.GetComponent<Image>().sprite = (Sprite)h[gameManager.currentLetter];
				gameManager.letter[box] = (float)gameManager.currentLetter;
			}
		}

	}

	void FixedUpdate(){
		if(gameManager.ok && gameManager.canChange[box]){
			setBack();
			gameManager.first[0] = true;
			gameManager.first[1] = true;
			gameManager.first[2] = true;

				int fi = ((int)(gameManager.letter[0]));
				int se = ((int)(gameManager.letter[1]));
				int th = ((int)(gameManager.letter[2]));
				switch(box){
				case 0: 
					LetterBoxStart.GetComponent<Image>().sprite = gameManager.allLetter[fi];
					break;
				case 1: 
				    LetterBoxStart.GetComponent<Image>().sprite = gameManager.allLetter[se];
					break;
				case 2: 
				    LetterBoxStart.GetComponent<Image>().sprite = gameManager.allLetter[th];
					break;

				gameManager.oldLetter[box] = gameManager.letter[box];
			}

		}

		if(!gameManager.ok && gameManager.canChange[box] && !gameManager.reset[box]){
			if (gameManager.first[box] && toChange){
				//setChoosableLetters();
				gameManager.first[box] = false;
			}
			gameManager.ok = false;
			//ScrollLetters();
			
		}else if(gameManager.ok && gameManager.canChange[box]){
			setBack();
			gameManager.first[0] = true;
			gameManager.first[1] = true;
			gameManager.first[2] = true;
			//set current word

		}

		if(gameManager.reset[box] && toChange){
			//setBack();
			gameManager.letter[box] = gameManager.oldLetter[box];
			gameManager.reset[box] = false;
			LetterBox.GetComponent<Image>().sprite = gameManager.allLetter[(int)gameManager.oldLetter[box]];
			gameManager.pressed = false;
			print("reset");
		}
	}


	/*void setChoosableLetters(){
		char x = (char)(gameManager.letter[0]+ 65f);
		char y = (char)(gameManager.letter[1]+ 65f);
		char z = (char)(gameManager.letter[2]+ 65f);
		gameManager.choosableLetters[box].Clear();
		ArrayList aArray = gameManager.getChoosableLetters(x, y, z, box);
		foreach(float i in aArray)
		{
			int index = (int) i;
			Sprite sprite = gameManager.allLetter[index];
			//print ("Box: "+box+ ", " +gameManager.allLetter[index]);
			if(!gameManager.choosableLetters[box].ContainsKey(i)){
				gameManager.choosableLetters[box].Add(index,sprite);
			}
		}
	}*/

	void setBack(){
		gameManager.canChange[0] = true;
		gameManager.canChange[1] = true;
		gameManager.canChange[2] = true;
		//anim.SetBool("OKisTouched", gameManager.ok);
		gameManager.ok = false;
		gameManager.pressed = false;
		//gameManager.choosableLetters[box].Clear();
		gameManager.currentLetter = 0;
		print ("setBack");
	}


}
