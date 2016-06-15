using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

//George Eisa
//15/06/2015
//PlaceDominoes
//Script 'Line of dominoes' and 'Curve of dominoes' tools

public class PlaceDominoes : MonoBehaviour
{
	public float dominoSpacing;
	public Transform prefab;
	Vector3 startPos = Vector3.zero;
	Vector3 endPos = Vector3.zero;
	Vector3 rotate;
	LineRenderer lineRenderer;

	//set location of line renderer (used to show where dominoes will be placed)
	void Start()
	{
		lineRenderer = GetComponent<LineRenderer> ();
	}

	void Update ()
	{
		if (GameControllerScript.editable && !EventSystem.current.IsPointerOverGameObject()) 
		{
			if (Input.touchCount > 1) {
				startPos = Vector3.zero;
				return;
			}
			//Line tool
			if (ChangedTool.toolNum == 4) {
				if (Input.GetMouseButtonDown (0)) {
					//start line on press
					Vector3 fingerPos = Input.mousePosition;
					fingerPos.z = Camera.main.transform.position.y - 0.5f;
					startPos = Camera.main.ScreenToWorldPoint (fingerPos);
				} else if (Input.GetMouseButtonUp (0)) {
					//create dominoes when release mouse
					if (startPos != Vector3.zero) {
						Vector3 fingerPos = Input.mousePosition;
						fingerPos.z = Camera.main.transform.position.y - 0.5f;
						endPos = Camera.main.ScreenToWorldPoint (fingerPos);
						//draw line of dominoes
						CreateDominoes ();
					}
				} else if (!Input.GetMouseButton (0)) {
					startPos = Vector3.zero;
					lineRenderer.enabled = false;
				} else {
					//update line to stretch from start to mouse
					Vector3 fingerPos = Input.mousePosition;
					fingerPos.z = Camera.main.transform.position.y - 0.5f;
					endPos = Camera.main.ScreenToWorldPoint (fingerPos);
					lineRenderer.enabled = true;
					lineRenderer.SetPosition (0, startPos);
					lineRenderer.SetPosition (1, endPos);
				}
			//curve tool
			} else {
				if (Input.GetMouseButton (0)) {
					//while mouse pressed, draw dominoes
					Vector3 fingerPos = Input.mousePosition;
					fingerPos.z = Camera.main.transform.position.y - 0.5f;
					startPos = Camera.main.ScreenToWorldPoint (fingerPos);
					CreateDominoes ();
				} else {
					startPos = Vector3.zero;
					endPos = Vector3.zero;
				}
			}
		}
	}

	//create dominoes method
	void CreateDominoes ()
	{
		//check if on floor
		float[] range = { -5 * transform.localScale.x,
			5*transform.localScale.x,
			-5*transform.localScale.z,
			5*transform.localScale.z
		};
		float lineLength = (endPos - startPos).magnitude;
		rotate = (endPos - startPos);
		rotate.y = 0;

		if (ChangedTool.toolNum == 4) { //domino line
			float length = 0f;
			float width = endPos.x - startPos.x;
			float height = endPos.z - startPos.z;
			if (lineLength < dominoSpacing) {
				if (Physics.OverlapSphere (startPos, 0.2f).Length == 0 && startPos.x > range [0] && startPos.x < range [1] && startPos.z > range [2] && startPos.z < range [3]) {
					Instantiate (prefab, startPos, Quaternion.identity);
					GameControllerScript.PlaySound(0);
				}
				return;
			}
			//loop though line, creating dominos at a set spacing
			while (lineLength-length >= dominoSpacing) {
				Vector3 pos = new Vector3 (startPos.x + width * (length / lineLength), startPos.y, startPos.z + height * (length / lineLength));
				if (Physics.OverlapSphere (pos, 0.2f).Length == 0 && pos.x > range [0] && pos.x < range [1] && pos.z > range [2] && pos.z < range [3]) {
					Instantiate (prefab, pos, Quaternion.LookRotation (rotate, Vector3.up));
					GameControllerScript.PlaySound(0);
				}
				length += dominoSpacing;
			}
		}
		else //domino curve
		{
			//create domino if space is free and far enough away from last domino
			if (lineLength >= dominoSpacing && Physics.OverlapSphere (startPos, 0.2f).Length == 0 && startPos.x > range [0] && startPos.x < range [1] && startPos.z > range [2] && startPos.z < range [3])
			{
				GameControllerScript.PlaySound(0);
				Instantiate (prefab, startPos, Quaternion.LookRotation (rotate, Vector3.up));
				endPos = startPos;
			}
		}
	}

	void OnDisable()
	{
		lineRenderer.enabled = false;
	}
}
