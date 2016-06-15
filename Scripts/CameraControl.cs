using UnityEngine;
using System.Collections;

//George Eisa
//15/06/2015
//CameraControl
//Code for controls of camera

public class CameraControl : MonoBehaviour
{
	public float PanSpeed;
	public float ZoomSpeed ;
	public float pcZoomSpeed;
	public float MaxZoom;
	public float MaxZoomOut;
	public bool pc;//set true for pc, false for android
	Vector2 startPos = Vector3.zero;
	Vector2 endPos;
	
	void Update ()
	{
		if (pc) {
			//zoom in or out with scroll wheel
			if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
				float deltaMagnitudeDiff = Input.GetAxis ("Mouse ScrollWheel") * ZoomSpeed;
				if ((transform.position.y > MaxZoom && deltaMagnitudeDiff > 0) || (transform.position.y < MaxZoomOut && deltaMagnitudeDiff < 0)) {
					transform.Translate (0, 0, deltaMagnitudeDiff * pcZoomSpeed);
				}
			}
			//pan camera with right click and drag
			if (Input.GetMouseButtonDown (1)) {
				startPos = Input.mousePosition;
			} else if (Input.GetMouseButton (1)) {
				if (startPos != Vector2.zero) {
					endPos = Input.mousePosition;
					Vector2 touchDeltaPosition = endPos - startPos;
					transform.Translate (-touchDeltaPosition.x / Screen.width * PanSpeed, -touchDeltaPosition.y / Screen.height * PanSpeed, 0);
					startPos = new Vector2(endPos.x, endPos.y);
				}
			} else if (!Input.GetMouseButton (1)) {
				startPos = Vector3.zero;
			}
		} else {//for android
			if (Input.touchCount == 2) {
				Touch touchZero = Input.GetTouch (0);
				Touch touchOne = Input.GetTouch (1);
				// find the position in the previous frame
				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
				
				// find the magnitude of the distance between the touches
				float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
				float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
				
				// find the difference in the distances
				float deltaMagnitudeDiff = touchDeltaMag - prevTouchDeltaMag;

				//decide whether to pan or zoom
				if (Mathf.Abs (deltaMagnitudeDiff) < 3) {
					Vector2 touchDeltaPosition = touchZero.deltaPosition;
					transform.Translate (-touchDeltaPosition.x / Screen.width* PanSpeed, -touchDeltaPosition.y / Screen.height* PanSpeed, 0);
				} else {
					if ((transform.position.y > MaxZoom && deltaMagnitudeDiff > 0) || (transform.position.y < MaxZoomOut && deltaMagnitudeDiff < 0))
						transform.Translate (0, 0, deltaMagnitudeDiff * ZoomSpeed);
				}
			}
		}
	}
}
