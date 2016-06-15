using UnityEngine;
using System.Collections;

//George Eisa
//15/06/2015
//DestroyFallenObjects
//Deletes fallen objects so they dont continuously fall

public class DestroyFallenObjects : MonoBehaviour {

	void OnTriggerEnter (Collider col)
	{
		Destroy (col.gameObject);
	}
}
