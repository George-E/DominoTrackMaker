using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//George Eisa
//15/06/2015
//SelectTool
//Changes tool on button press

public class SelectTool : MonoBehaviour {
	public GameObject tool;

	public void ToolSelected(int num)
	{
			GameControllerScript.PlaySound (4);
			ChangedTool.toolNum = num;
			ChangedTool.UpdateToolImage ();
	}
}
