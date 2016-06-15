using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

//George Eisa
//15/06/2015
//DeleteAndRotateTool
//Script for both delete tool and rotate tool

public class DeleteAndRotateTool : MonoBehaviour
{

	public float rotateSpeed;

	void Update ()
	{
		//if build is editable
		if (GameControllerScript.editable && !EventSystem.current.IsPointerOverGameObject ()) {
			if (Input.GetMouseButton (0)) {
				//get object pointed at
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					//check if parent or child
					if (hit.collider.gameObject.CompareTag ("Item")) {
						//check to delete or rotate
						if (ChangedTool.toolNum == 0) {
							Destroy (hit.collider.gameObject);
							GameControllerScript.PlaySound (1);
						}
						if (ChangedTool.toolNum == 1) {
							GameControllerScript.PlaySound (2);
							hit.transform.Rotate (Vector3.up * rotateSpeed, Space.World);
						}
					} else if (hit.collider.gameObject.CompareTag ("SubItem")) {
						if (hit.collider.gameObject.transform.parent.CompareTag ("Item")) {
							//check to delete or rotate
							if (ChangedTool.toolNum == 0) {
								Destroy (hit.transform.parent.gameObject);
								GameControllerScript.PlaySound (1);
							}
							if (ChangedTool.toolNum == 1) {
								GameControllerScript.PlaySound (2);
								hit.transform.parent.transform.Rotate (Vector3.up * rotateSpeed, Space.World);
							}
						}
					}
				}
			}
		}
	}
}
