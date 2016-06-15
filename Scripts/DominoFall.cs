using UnityEngine;
using System.Collections;

//George Eisa
//15/06/2015
//DominoFall
//Decides what to do when Domino falls

public class DominoFall : MonoBehaviour {

	void OnCollisionEnter (Collision col)
	{
		GameControllerScript.PlaySound(3);
		//if its the ball in level4, roll it
		if (col.gameObject.name == "lvl4ball") {
			col.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
			//gameObject.GetComponent<Collider> ().isTrigger = true;
			col.gameObject.GetComponent<Rigidbody> ().AddForce (gameObject.GetComponent<Rigidbody> ().velocity.normalized * 700);
			return;
		}
		/*
		if (col.gameObject.name.Length >= 6 && col.gameObject.name.Substring (0, 6) == "Sphere") {
			col.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
			Vector3 ve = (-transform.position + col.gameObject.transform.position);//gameObject.GetComponent<Rigidbody> ().velocity;
			ve.y = 0;
			col.gameObject.GetComponent<Rigidbody> ().AddForce (ve * 60);
			return;
		}
		*/
		//if its a subitem, add a force to its rigidbody
		if (col.gameObject.CompareTag ("SubItem")) {
			if (col.gameObject.name.Length >= 6 && col.gameObject.name.Substring (0, 6) == "Domino") {
				col.gameObject.GetComponent<Rigidbody> ().AddForce (gameObject.GetComponent<Rigidbody> ().velocity.normalized * 10);
			} else if (col.gameObject.GetComponent<Rigidbody> () != null) {
				if (col.gameObject.GetComponent<Rigidbody> ().isKinematic == true) {
					col.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
					if (col.gameObject.name.Substring (0, 5) == "Panel") {
						col.gameObject.GetComponent<Rigidbody> ().AddForce (gameObject.GetComponent<Rigidbody> ().velocity.normalized * 10);
					} else if (col.gameObject.name.Substring (0, 6) == "Sphere") {
						col.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
						Vector3 ve = (-transform.position + col.gameObject.transform.position - gameObject.GetComponent<Rigidbody> ().velocity.normalized);//gameObject.GetComponent<Rigidbody> ().velocity;
						ve.y = 0;
						col.gameObject.GetComponent<Rigidbody> ().AddForce (ve * 90);
					} else if (col.gameObject.name.Substring (0, 8) == "Cylinder") {
						gameObject.GetComponent<Collider> ().isTrigger = true;
						col.gameObject.GetComponent<Rigidbody> ().AddRelativeForce (Vector3.left * 30);
					}
				}
			}
		} else if (gameObject.CompareTag("SubItem") && col.gameObject.name.Length >= 6 && col.gameObject.name.Substring (0, 6) == "Domino") {
			col.gameObject.GetComponent<Rigidbody> ().AddForce (gameObject.GetComponent<Rigidbody> ().velocity.normalized * 10);
		}
	}
}
