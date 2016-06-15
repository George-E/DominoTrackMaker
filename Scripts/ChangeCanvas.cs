using UnityEngine;
using System.Collections;

//George Eisa
//15/06/2015
//ChangeCanvas
//Varying methods for changing the canvas

public class ChangeCanvas : MonoBehaviour
{

	public GameObject oldC;
	public GameObject newC;

	//standard switch of canvas
	public void LoadScene ()
	{
		GameControllerScript.PlaySound (4);
		newC.SetActive (true);
		oldC.SetActive (false);
	}

	//toggle open/close tool select menu
	public void OpenTools ()
	{
		if (GameControllerScript.editable && !newC.activeSelf) {
			GameControllerScript.PlaySound (4);
			GameControllerScript.editable = false;
			newC.SetActive (true);
		} else if (!GameControllerScript.editable && newC.activeSelf) {
			GameControllerScript.PlaySound (4);
			GameControllerScript.editable = true;
			newC.SetActive (false);
		}
	}
}
