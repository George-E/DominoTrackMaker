using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

//George Eisa
//15/06/2015
//NonDominoObjectsTools
//Script for Panel, Wheel, Bridge, and Sphere tools

public class NonDominoObjectsTools : MonoBehaviour {

	public Transform[] objects = new Transform[4];
	public Transform[] ghostObjects = new Transform[4];
	Vector3 startPos = Vector3.zero;
	Transform ghostObject;

	//create faint version of object to see where youre placing it
	public void CreateGhost()
	{
		Vector3 fingerPos = Input.mousePosition;
		fingerPos.z = Camera.main.transform.position.y;
		startPos = Camera.main.ScreenToWorldPoint (fingerPos);
		ghostObject = ((Transform)(Instantiate (((ChangedTool.toolNum <4)?ghostObjects[ChangedTool.toolNum-2]:ghostObjects[ChangedTool.toolNum-4]), startPos, Quaternion.identity)));
	}

	void Update ()
	{
		if (GameControllerScript.editable && !EventSystem.current.IsPointerOverGameObject ()) {
			//update loaction of 'ghost'
			ghostObject.gameObject.SetActive(true);
			Vector3 fingerPos = Input.mousePosition;
			fingerPos.z = Camera.main.transform.position.y;
			startPos = Camera.main.ScreenToWorldPoint (fingerPos);
			float[] range = { -5 * transform.localScale.x,
			5 * transform.localScale.x,
			-5 * transform.localScale.z,
			5 * transform.localScale.z
			};
			if (startPos.x > range [0] && startPos.x < range [1] && startPos.z > range [2] && startPos.z < range [3]) {
				ghostObject.position = startPos;
			}
			//create object on click
			if (Input.GetMouseButtonDown (0)) {
				if (startPos.x > range [0] && startPos.x < range [1] && startPos.z > range [2] && startPos.z < range [3]) {
					GameControllerScript.PlaySound (0);
					Instantiate (((ChangedTool.toolNum < 4) ? objects [ChangedTool.toolNum - 2] : objects [ChangedTool.toolNum - 4]), startPos, Quaternion.identity);
				}
			} 
		} else {
			if (ghostObject != null)
			{
				ghostObject.gameObject.SetActive(false);
			}
		}
	}


	//get rid of faint version
	public void DeleteGhost()
	{
		if (ghostObject != null) {
			Destroy (ghostObject.gameObject);
		}
	}

}
