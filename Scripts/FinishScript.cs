using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//George Eisa
//15/06/2015
//FinishScript
//Checks if the user has beaten a level (used in every level except 3)

public class FinishScript : MonoBehaviour {

	public GameObject finishPanel;
	public GameObject display;
	public Sprite star;
	public Sprite emptyStar;
	public GameObject[] starHolders;
	public int threeStars = 80;
	public int twoStars = 100;
	public int levelNum = 1;
	public bool level4 = false;

	void OnTriggerStay (Collider col)
	{
		//if already won..
		if (finishPanel.activeSelf) {
			return;
		}
		//if this is level 4, and the collision is not with the ball...
		if (level4 && col.gameObject.name != "lvl4ball") {
			return;
		}
		//if the coolision object is moving, you won!
		Vector3 vol = col.gameObject.GetComponent<Rigidbody> ().velocity;
		if (vol.x > 0.4f || vol.y>0.4f || vol.z>0.4f) {
			finishPanel.SetActive(true);
			GameObject[] objects = GameObject.FindGameObjectsWithTag("Item");
			display.GetComponent<Text>().text = "Number of Objects Used: " + objects.Length;
			int score;
			//calculate score and # stars
			if (objects.Length <= threeStars)
			{
				score = 3;
				starHolders[1].GetComponent<Image>().sprite = star;
				starHolders[2].GetComponent<Image>().sprite = star;
			}
			else if  (objects.Length <= twoStars)
			{
				score = 2;
				starHolders[1].GetComponent<Image>().sprite = star;
				starHolders[2].GetComponent<Image>().sprite = emptyStar;
			}
			else{
				score =1;
				starHolders[1].GetComponent<Image>().sprite = emptyStar;
				starHolders[2].GetComponent<Image>().sprite = emptyStar;
			}
			starHolders[0].GetComponent<Image>().sprite = star;
			if (PlayerPrefs.GetInt("Level"+levelNum) < score)
			{
				PlayerPrefs.SetInt("Level"+levelNum, score);
			}

		}
	}
}
