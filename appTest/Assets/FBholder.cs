using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;



public class FBholder : MonoBehaviour {

	public GameObject UIFBIsLoggedIn;
	public GameObject UIFBIsNotLoggedIn;
	public GameObject UIFBAvatar;
	public GameObject UIFBUserName;
	public GameObject overAll;
	public GameObject bla;


	public GameObject levelMenu1;
	public GameObject levelMenu2;
	public GameObject back;
	public GameObject forward;

	private Dictionary<string, string> profile = null;

	void Awake(){
		DontDestroyOnLoad(overAll);
		DontDestroyOnLoad(bla);
		if(!gameManager.appStarted2){
			gameManager.score = LoadScoreFile();
			gameManager.appStarted2 = true;
		}
		FB.Init(SetInit, OnHideUnity);
	}

	private void SetInit(){
		Debug.Log ("FB init done");

		if(FB.IsLoggedIn){
			Debug.Log("FB logged in");
			DealWithFBMenus(true);
		}else{
			DealWithFBMenus(false);
		}
	}

	private void OnHideUnity(bool isGameShown){
		if(!isGameShown){
			Time.timeScale = 0;
		}else{
			Time.timeScale = 1;
		}
	}

	public void FBlogin(){
		FB.Login("email", AuthCallback);
	}

	void AuthCallback(FBResult result){
		if(FB.IsLoggedIn){
			Debug.Log("FB login worked");
			DealWithFBMenus(true);
		}else{
			Debug.Log ("FB login fail");
			DealWithFBMenus(false);
		}
	}

	void DealWithFBMenus(bool isLoggedIn){
		if(isLoggedIn){
			UIFBIsLoggedIn.SetActive(true);
			UIFBIsNotLoggedIn.SetActive(false);

			//get profile pickture code
			FB.API(Util.GetPictureURL("me", 128, 128), Facebook.HttpMethod.GET, DealWithProfilePicture);
			//get username code
			FB.API("/me?fields=id,first_name", Facebook.HttpMethod.GET, DealWithUserName);


			if(!gameManager.appStarted){
				Hashtable ht = loadpaths();
				Hashtable httime = loadtime();
				StartCoroutine(SendFile(ht, httime));
				gameManager.score = LoadScoreFile();
				levelMenu2.SetActive(false);
				back.SetActive(false);
				gameManager.appStarted = true;
			}else{
				StartCoroutine(SendFile(gameManager.storePath, gameManager.time));
				SavePaths();
				SaveScoreFile();
				SaveTime();
				switch(gameManager.currentLevelMenu){
				case 1:
					levelMenu1.SetActive(true);
					levelMenu2.SetActive(false);
					forward.SetActive(true);
					back.SetActive(false);
					break;
				case 2:
					levelMenu1.SetActive(false);
					levelMenu2.SetActive(true);
					forward.SetActive(false);
					back.SetActive(true);
					break;
				}
			}


		}else{
			UIFBIsLoggedIn.SetActive(false);
			UIFBIsNotLoggedIn.SetActive(true);
		}
	}

	void DealWithProfilePicture(FBResult result){
		if(result.Error != null){
			Debug.Log("Problem with getting profile picture");

			FB.API(Util.GetPictureURL("me", 128, 128), Facebook.HttpMethod.GET, DealWithProfilePicture);
			return;
		}

		Image UserAvatar = UIFBAvatar.GetComponent<Image>();
		UserAvatar.sprite = Sprite.Create(result.Texture, new Rect(0,0,128,128), new Vector2(0,0));
	}

	void DealWithUserName(FBResult result){
		if(result.Error != null){
			Debug.Log("Problem with getting username");
			
			FB.API("/me?fields=id,first_name", Facebook.HttpMethod.GET, DealWithUserName);
			return;
		}

		profile = Util.DeserializeJSONProfile(result.Text);
		Text UserMsg = UIFBUserName.GetComponent<Text>();
		UserMsg.text = "Hello, " + profile["first_name"];



	}

	public void ShareWithFriends(){
		FB.Feed(
			linkCaption: "I'm playing this awsom game",
			picture: "http://blog.exiconglobal.com/corp/wp-content/uploads/2012/10/App-Icon-copy.png",
			linkName: "Check out this game.",
			link: "http://apps.facebook.com/" + FB.AppId + "/?challenge_brag=" + (FB.IsLoggedIn ? FB.UserId : "guest")
			);
	}

	public void InviteFriends(){
		FB.AppRequest(
			message: "This game is soooo awsome, join me.",
			title: "Invite your friends to join you"
			);
	}


	private Hashtable LoadScoreFile(){
		BinaryFormatter bf = new BinaryFormatter();
		if(!File.Exists(Application.persistentDataPath + "/score.txt")) {
			return new Hashtable();
		}
		FileStream file = File.Open(Application.persistentDataPath + "/score.txt", FileMode.Open);
		var x = (Hashtable)bf.Deserialize(file);
		file.Close();
		Debug.Log("SaveFile loaded.");
		return x;
	}

	private void SaveScoreFile(){
		if(gameManager.level != 0){
			if(File.Exists(Application.persistentDataPath + "/score.txt")) {
				File.Delete(Application.persistentDataPath + "/score.txt");
			}
			FileStream file = File.Create (Application.persistentDataPath + "/score.txt");
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(file, gameManager.score);
			file.Close();
			Debug.Log ("SaveFile saved.");
		}	
	}
	

	public void SavePaths(){
		if(gameManager.level != 0){
			if(File.Exists(Application.persistentDataPath + "/paths.txt")) {
				File.Delete(Application.persistentDataPath + "/paths.txt");
			}
			FileStream file = File.Create (Application.persistentDataPath + "/paths.txt");
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(file, gameManager.storePath);
			file.Close();
			Debug.Log ("File saved.");
			//StartCoroutine(SendFile(gameManager.storePath));
		}	
	}
	

	private Hashtable loadpaths(){
		BinaryFormatter bf = new BinaryFormatter();
		if(!File.Exists(Application.persistentDataPath + "/paths.txt")) {
			return new Hashtable();
		}
		FileStream file = File.Open(Application.persistentDataPath + "/paths.txt", FileMode.Open);
		var x = (Hashtable)bf.Deserialize(file);
		file.Close();
		Debug.Log("Paths loaded.");
		return x;
	}

	public void SaveTime(){
		if(gameManager.level != 0){
			if(File.Exists(Application.persistentDataPath + "/time.txt")) {
				File.Delete(Application.persistentDataPath + "/time.txt");
			}
			FileStream file = File.Create (Application.persistentDataPath + "/time.txt");
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(file, gameManager.time);
			file.Close();
			Debug.Log ("File saved.");
		}	
	}

	private Hashtable loadtime(){
		BinaryFormatter bf = new BinaryFormatter();
		if(!File.Exists(Application.persistentDataPath + "/time.txt")) {
			return new Hashtable();
		}
		FileStream file = File.Open(Application.persistentDataPath + "/time.txt", FileMode.Open);
		var x = (Hashtable)bf.Deserialize(file);
		file.Close();
		Debug.Log("Time loaded.");
		return x;
	}
	
	public IEnumerator SendFile(Hashtable file, Hashtable fileTime){
		string url = "http://www.sarahdossinger.de:7552/wordgame/data";
		WWWForm form = new WWWForm();
		foreach(int key in file.Keys){
			form.AddField(key.ToString(), file[key].ToString());
		}
		foreach(int key in fileTime.Keys){
			form.AddField(("timeFrom"+key.ToString()), fileTime[key].ToString());
		}
		form.AddField("FBid", FB.UserId.ToString());
		WWW www = new WWW(url, form);
		Debug.Log("Sending data to the server.");
		//headers["Authorization"] = "Basic " + System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("username:password"));
		yield return www;

		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.data);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}  
	}
	
	
}
