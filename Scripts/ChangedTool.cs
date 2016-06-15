using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//George Eisa
//15/06/2015
//ChangedTool
//Methods for changing the tool

public class ChangedTool : MonoBehaviour {
	public static int toolNum = 4;
	public GameObject oldC;
	
	public Sprite[] images= new Sprite[8];
	public GameObject floor;
	float[] sizes = {70,70,70,70,80,80,80,80};

	static GameObject tool;
	static ChangedTool script;

	//set up starter tool (line)
	void Start()
	{
		tool = gameObject;
		script = this;
		UpdateToolImage ();
	}

	//check for keyboard shortcuts
	void Update()
	{
		if (GameControllerScript.editable) {
			if (Input.GetKeyDown (KeyCode.D)) {
				toolNum = 0;
				UpdateToolImage ();
			} else if (Input.GetKeyDown (KeyCode.R)) {
				toolNum = 1;
				UpdateToolImage ();
			} else if (Input.GetKeyDown (KeyCode.P)) {
				toolNum = 2;
				UpdateToolImage ();
			} else if (Input.GetKeyDown (KeyCode.S)) {
				toolNum = 3;
				UpdateToolImage ();
			} else if (Input.GetKeyDown (KeyCode.L)) {
				toolNum = 4;
				UpdateToolImage ();
			} else if (Input.GetKeyDown (KeyCode.C)) {
				toolNum = 5;
				UpdateToolImage ();
			} else if (Input.GetKeyDown (KeyCode.W)) {
				toolNum = 6;
				UpdateToolImage ();
			} else if (Input.GetKeyDown (KeyCode.B)) {
				toolNum = 7;
				UpdateToolImage ();
			}
		}
	}

	//switch image and active script to match current tool
	public static void UpdateToolImage ()
	{
		script.oldC.SetActive(false);
		GameControllerScript.editable = true;
		tool.GetComponent<Image>().sprite = script.images[toolNum];
		tool.GetComponent<Image> ().GetComponent<RectTransform> ().sizeDelta = new Vector2(script.sizes[toolNum], script.sizes[toolNum]);
		switch (toolNum) {
		case 0: case 1:
			script.floor.GetComponent<NonDominoObjectsTools>().DeleteGhost();
			script.floor.GetComponent<DeleteAndRotateTool>().enabled = true;
			script.floor.GetComponent<PlaceDominoes>().enabled = false;
			script.floor.GetComponent<NonDominoObjectsTools>().enabled = false;
			break;
		case 2: case 3: case 6: case 7:
			script.floor.GetComponent<NonDominoObjectsTools>().DeleteGhost();
			script.floor.GetComponent<DeleteAndRotateTool>().enabled = false;
			script.floor.GetComponent<PlaceDominoes>().enabled = false;
			script.floor.GetComponent<NonDominoObjectsTools>().enabled = true;
			script.floor.GetComponent<NonDominoObjectsTools>().CreateGhost();
			break;
		case 4: case 5:
			script.floor.GetComponent<NonDominoObjectsTools>().DeleteGhost();
			script.floor.GetComponent<DeleteAndRotateTool>().enabled = false;
			script.floor.GetComponent<PlaceDominoes>().enabled = false;
			script.floor.GetComponent<PlaceDominoes>().enabled = true;
			script.floor.GetComponent<NonDominoObjectsTools>().enabled = false;
			break;
		}
	 }
}
