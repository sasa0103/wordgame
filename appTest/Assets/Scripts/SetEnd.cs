using UnityEngine;
using System.Collections;

public class SetEnd : MonoBehaviour {

	Animator anim;
	public int box;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.SetFloat("SelectLetter", gameManager.letterEnd[box]);
	}
}
