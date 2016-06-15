using UnityEngine;
using System.Collections;

//George Eisa
//15/06/2015
//GameControllerScript
//Various functions needed throughout game

public class GameControllerScript : MonoBehaviour {

	public static string fileName;
	public AudioSource[] music = new AudioSource[1];
	public AudioSource[] sources = new AudioSource[5];
	public static GameControllerScript me;
	public static bool editable = true;

	//quit on escape key
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	//play msuic on loop
	void Start () {
		if (me == null) {
			me = this;
			DontDestroyOnLoad (this);
			music [0].Play ();
		} else if (me != this) {
			Destroy(gameObject);
		}
	}

	//called by other scripts to open build
	public static void LoadAndSetUp()
	{
		Application.LoadLevel("Build");
	}

	//called by other scripts to play sound
	public static void PlaySound(int index)
	{
		me.sources [index].Play();
	}
}
