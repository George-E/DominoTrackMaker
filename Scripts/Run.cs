using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//George Eisa
//15/06/2015
//Run
//Script for saving, loading, and running the build

public class Run : MonoBehaviour
{
	public int level3or4 = 0;

	public Transform[] prefabs = new Transform[5];
	public Transform starter;
	[SerializeField] 
	ObjectProps[] objects;
	public GameObject toolButton;
	public GameObject exitButton;
	public GameObject toolPanel;
	public GameObject finishPanel;

	//load build on start
	void Start ()
	{
		Load ();
	}

	public void Save ()
	{
		//open data file
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.OpenWrite (GameControllerScript.fileName);
		GameObject[] instances = GameObject.FindGameObjectsWithTag ("Item");
		//create array of object properties class
		objects = new ObjectProps[instances.Length];
		for (int i = 0; i < instances.Length; i++) {
			objects [i] = new ObjectProps ();
			if (instances [i].name.Substring (0, 5) == "Panel") {
				objects [i].type = 1;
			} else if (instances [i].name.Substring (0, 6) == "Sphere") {
				objects [i].type = 2;
			} else if (instances [i].name.Substring (0, 5) == "Wheel") {
				objects [i].type = 3;
			} else if (instances [i].name.Substring (0, 6) == "Bridge") {
				objects [i].type = 4;
			} else {
				objects [i].type = 0;
			}
			objects [i].posX = instances [i].transform.position.x;
			objects [i].posY = instances [i].transform.position.y;
			objects [i].posZ = instances [i].transform.position.z;
			objects [i].rotX = instances [i].transform.rotation.x;
			objects [i].rotY = instances [i].transform.rotation.y;
			objects [i].rotZ = instances [i].transform.rotation.z;
			objects [i].rotW = instances [i].transform.rotation.w;
		}
		//serialize the array and store it
		bf.Serialize (file, objects);
		file.Close ();
	}
	
	public void Load ()
	{
		try {
			//read and deserialize data file
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (GameControllerScript.fileName, FileMode.Open);
			objects = (ObjectProps[])bf.Deserialize (file);
			//create objects from deserialed array
			for (int i = 0; i < objects.Length; i++) {
				Instantiate (prefabs [objects [i].type], new Vector3 (objects [i].posX, objects [i].posY, objects [i].posZ), 
			             new Quaternion (objects [i].rotX, objects [i].rotY, objects [i].rotZ, objects [i].rotW));
			}
			file.Close ();
		} catch (FileNotFoundException e) {
			//if file doesnt exist, create a new one
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.OpenWrite (GameControllerScript.fileName);
			ObjectProps[] objects = new ObjectProps[0];
			bf.Serialize (file, objects);
			file.Close ();
		} finally
		{
			Instantiate (starter);
		}
	}

	public void Begin ()//called by Start button press
	{
		GameControllerScript.PlaySound(4);
		if (GetComponentInChildren<Text> ().text == "Start") {
			//if text in button is 'start', save and turn on physics
			Save ();
			GameControllerScript.editable = false;
			toolButton.SetActive(false);
			exitButton.SetActive(false);
			toolPanel.SetActive(false);
			foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item")) {
				if (item.GetComponent<Rigidbody> () != null) {
					item.GetComponent<Rigidbody> ().isKinematic = false;
				}
			}
			foreach (GameObject item in GameObject.FindGameObjectsWithTag("SubItem")) {
				if (item.name.Length >=6 && item.name.Substring(0,6) == "Domino") {
					item.GetComponent<Rigidbody> ().isKinematic = false;
				}
			}
			foreach (GameObject item in GameObject.FindGameObjectsWithTag("Player")) {
				item.GetComponent<Rigidbody> ().isKinematic = false;
			}
			GetComponentInChildren<Text> ().text = "Restart";
		} else {
			//if text in button is 'restart', remove items and load build
			if (level3or4 > 0)
			{
				Application.LoadLevel("Level" + level3or4);
				return;
			}
			foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item")) {
				Destroy (item);
			}
			foreach (GameObject item in GameObject.FindGameObjectsWithTag("Player")) {
				Destroy (item);
			}
			Load ();
			GameControllerScript.editable = true;
			toolButton.SetActive(true);
			exitButton.SetActive(true);
			if (finishPanel != null)
			{
				finishPanel.SetActive(false);
			}
			GetComponentInChildren<Text> ().text = "Start";
		}
	}
	
}

//class to store object properties
[Serializable]
public class ObjectProps
{
	public int type;
	public float posX;
	public float posY;
	public float posZ;
	public float rotX;
	public float rotY;
	public float rotZ;
	public float rotW;
}