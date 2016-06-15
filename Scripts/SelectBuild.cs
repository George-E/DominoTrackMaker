using UnityEngine;
using System.Collections;

//George Eisa
//15/06/2015
//SelectBuild
//Opens a previously made build

public class SelectBuild : MonoBehaviour {

	public string txtName;

	public void BuildSelected()
	{
		GameControllerScript.PlaySound(4);
		GameControllerScript.fileName = txtName;
		GameControllerScript.LoadAndSetUp();
	}
}
