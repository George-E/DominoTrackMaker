using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//George Eisa
//15/06/2015
//FinishScriptLvl3
//Checks if the user has beaten a level (for level 3 only)

public class FinishScriptLvl3 : MonoBehaviour
{
	
	public GameObject finishPanel;
	public GameObject display;
	public Sprite star;
	public Sprite emptyStar;
	public GameObject[] panels;
	public GameObject[] starHolders;
	public int threeStars = 80;
	public int twoStars = 100;
	public int levelNum = 1;
	
	void Update ()
	{
		//if already won..
		if (finishPanel.activeSelf) {
			return;
		}
		//if a single panel is not moving...
		for (int i=0; i<panels.Length; i++) {
			if (panels [i].GetComponent<Rigidbody> ().isKinematic) {
				return;
			}
		}
		//you win!
		finishPanel.SetActive (true);
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("Item");
		display.GetComponent<Text> ().text = "Number of Objects Used: " + objects.Length;
		int score;
		//calculate score and # stars
		if (objects.Length <= threeStars) {
			score = 3;
			starHolders [1].GetComponent<Image> ().sprite = star;
			starHolders [2].GetComponent<Image> ().sprite = star;
		} else if (objects.Length <= twoStars) {
			score = 2;
			starHolders [1].GetComponent<Image> ().sprite = star;
			starHolders [2].GetComponent<Image> ().sprite = emptyStar;
		} else {
			score = 1;
			starHolders [1].GetComponent<Image> ().sprite = emptyStar;
			starHolders [2].GetComponent<Image> ().sprite = emptyStar;
		}
		starHolders [0].GetComponent<Image> ().sprite = star;
		if (PlayerPrefs.GetInt ("Level" + levelNum) < score) {
			PlayerPrefs.SetInt ("Level" + levelNum, score);
		}


	}
}
