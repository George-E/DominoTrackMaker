using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//George Eisa
//15/06/2015
//LoadStars
//Loads the stars youve already earned on levels

public class LoadStars : MonoBehaviour {

	public Sprite star;
	public GameObject[] starHolders;

	void Start () {

		if (PlayerPrefs.HasKey ("Level2")) {
			//if stats exist, colour in the right number of stars
			for (int i=0; i<4;i++)
			{
				int a = PlayerPrefs.GetInt("Level"+(i+1));
				if (a > 0)
				{
					starHolders[i*3].GetComponent<Image>().sprite = star;
				}
				if (a > 1)
				{
					starHolders[1+i*3].GetComponent<Image>().sprite = star;
				}
				if (a > 2)
				{
					starHolders[2+i*3].GetComponent<Image>().sprite = star;
				}
			}
		} else {
			//if stats dont exist, create them
			PlayerPrefs.SetInt("Level1",0);
			PlayerPrefs.SetInt("Level2",0);
			PlayerPrefs.SetInt("Level3",0);
			PlayerPrefs.SetInt("Level4",0);
		}
	}
}
