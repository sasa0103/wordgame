using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {

	public GameObject levelMenu1;
	public GameObject levelMenu2;
	public GameObject back;
	public GameObject forward;

	void Start(){
	}

	void OnMouseUp(){
		switch(gameManager.currentLevelMenu){
		case 1:
			levelMenu1.SetActive(false);
			levelMenu2.SetActive(true);
			forward.SetActive(false);
			back.SetActive(true);
			gameManager.currentLevelMenu = 2;
			break;
		case 2:
			levelMenu1.SetActive(true);
			levelMenu2.SetActive(false);
			forward.SetActive(true);
			back.SetActive(false);
			gameManager.currentLevelMenu = 1;
			break;
		}
	}
}
