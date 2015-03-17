using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameManager {

	private static readonly gameManager instance = new gameManager();


	static gameManager(){
		//letters = (Object[])Resources.Load("alphabeth.png");
		initVar();
	}

	public static gameManager Instance{
		get{
			return Instance;
		} 
	}


	public static void initVar(){
		letter = new float[] {0f,0f,0f};
		letterEnd = new float[] {0f,0f,0f};
		klickBox = new bool[] {false,false,false};
		ok = false;
		canChange = new bool[] {true,true,true};
		oldLetter = new float[] {0f,0f,0f};
		reset = new bool[] {false, false, false};
		first = new bool[] {true,true,true};
		isFade = new bool[] {false, false, false};
		valid = false;
		level = 0;
		currentLevelMenu = 1;
		currentLetter = 0;

		storePath = new Hashtable();
		appStarted = false;
		appStarted2 = false;
		score = new Hashtable();
		time = new Hashtable();
		oldScore = 0;
		pressed = false;

		LScene = new GameObject();
		GScene = new GameObject();
		WScene = new GameObject();

		choosableLetters = new Hashtable[] {new Hashtable(),new Hashtable(),new Hashtable()};
		allLetter = new Sprite[26];
		setAllLetter = false;

		words = new List<string>();
		textFile = Resources.Load("words_all") as TextAsset;
		readTextFileLines();
	}

	
	public static void readTextFileLines() { 
		string[] linesInFile = textFile.text.Split('\n');
		
		foreach (string line in linesInFile)
		{
			words.Add(line.ToUpper());
		}	
	}

	public static ArrayList getChoosableLetters( char f, char s, char t, int box ){
		ArrayList letters = new ArrayList();

		string x = f.ToString();
		string y = s.ToString();
		string z = t.ToString();
		string start = x + y;
		string end = y + z;


		if (box == 0){
			foreach (string st in words)
			{
				if (st.Trim().EndsWith(end)){
					float fl = (float)(char.ToUpper(st[0]) - 65);
					letters.Add(fl);
					//(float)(char.ToUpper(first) - 65);
				}
			}
		}
		if (box == 2){
			foreach (string st in words)
			{
				if (st.Trim().StartsWith(start)){
					float fl = (float)(char.ToUpper(st[2]) - 65);
					letters.Add(fl);
					//(float)(char.ToUpper(first) - 65);
				}
			}
		}
		if (box == 1){
			foreach (string st in words)
			{
				if (st.Trim().EndsWith(z) && st.Trim().StartsWith(x)){
					float fl = (float)(char.ToUpper(st[1]) - 65);
					letters.Add(fl);
					//(float)(char.ToUpper(first) - 65);
				}
			}
		}
		return letters; 
	}

	public static void setChoosableLetters(){
		char x = (char)(gameManager.letter[0]+ 65f);
		char y = (char)(gameManager.letter[1]+ 65f);
		char z = (char)(gameManager.letter[2]+ 65f);
		
		gameManager.choosableLetters[0].Clear();
		gameManager.choosableLetters[1].Clear();
		gameManager.choosableLetters[2].Clear();
		
		ArrayList aArray = gameManager.getChoosableLetters(x, y, z, 0);
		foreach(float i in aArray)
		{
			int index = (int) i;
			Sprite sprite = gameManager.allLetter[index];
			if(!gameManager.choosableLetters[0].ContainsKey(index)){
				gameManager.choosableLetters[0].Add(index,sprite);
			}
		}
		
		aArray = gameManager.getChoosableLetters(x, y, z, 1);
		foreach(float i in aArray)
		{
			int index = (int) i;
			Sprite sprite = gameManager.allLetter[index];
			if(!gameManager.choosableLetters[1].ContainsKey(index)){
				gameManager.choosableLetters[1].Add(index,sprite);
			}
		}
		
		aArray = gameManager.getChoosableLetters(x, y, z, 2);
		foreach(float i in aArray)
		{
			int index = (int) i;
			Sprite sprite = gameManager.allLetter[index];
			if(!gameManager.choosableLetters[2].ContainsKey(index)){
				gameManager.choosableLetters[2].Add(index,sprite);
			}
		}
	}

	//Variables
	
	public static bool[] klickBox;
	public static float[] letter;
	public static float[] letterEnd;
	

	public static bool ok;
	public static bool[] reset;
	public static bool[] canChange; 
	public static float[] oldLetter;
	public static bool[] first;
	public static bool[] isFade;
	public static bool valid;
	public static int level;
	public static Hashtable score;
	public static int oldScore;
	public static Hashtable time;
	public static int currentLevelMenu;
	public static int currentLetter;

	public static Hashtable storePath;

	public static TextAsset textFile;
	public static List<string> words;

	public static bool appStarted;
	public static bool appStarted2;
	

	public static GameObject LScene;
	public static GameObject GScene;
	public static GameObject WScene;

	public static Hashtable[] choosableLetters;

	public static bool pressed;
	public static Sprite[] allLetter;
	public static bool setAllLetter;
	
}
