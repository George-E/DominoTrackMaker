using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//George Eisa
//15/06/2015
//ChangeScene
//Varying methods for changing the scene

public class ChangeScene : MonoBehaviour {

	public GameObject runButton;

	//create new free build
	public void LoadScene ()
	{
		GameControllerScript.PlaySound(4);
		string txtName = GetComponent<InputField> ().text;
		txtName = Application.persistentDataPath + "/" + txtName + ".dat";

		GameControllerScript.fileName = txtName;
		GameControllerScript.LoadAndSetUp();
	}

	//exit build
	public void ReturnToMenu ()
	{
		runButton.GetComponent<Run> ().Save ();
		Application.LoadLevel ("MainMenu");
	}

	//exit level menu
	public void ReturnToMenuWithoutSaving ()
	{
		Application.LoadLevel ("MainMenu");
	}

	//load a level
	public void LoadLevel(int num)
	{
		GameControllerScript.fileName = Application.dataPath + "\\TextFiles\\Level" + num + ".dat";
		//GameControllerScript.fileName = "C:\\Users\\George\\Desktop\\George's Folder\\Level"+num+".dat";
		Application.LoadLevel("Level"+num);
	}
}
