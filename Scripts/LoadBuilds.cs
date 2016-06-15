using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

//George Eisa
//15/06/2015
//LoadBuilds
//Creates buttons that can be used to load previously made builds

public class LoadBuilds : MonoBehaviour {

	public GameObject buttonPrefab;
	string[] sampleNames = {"Sample Build"};
    void Start()
	{
		Vector3 scale = new Vector3 (1, 1, 1);
		//create button for each sample build
		for (int i = 0; i < sampleNames.Length; i++) {
			GameObject button = Instantiate (buttonPrefab);
			RectTransform rect = button.GetComponent<RectTransform> ();
			rect.SetParent (transform);
			rect.localScale = scale;
			rect.GetComponentInChildren<Text> ().text = sampleNames[i];
			rect.GetComponent<SelectBuild>().txtName =  Application.dataPath + "\\TextFiles\\" + sampleNames[i] + ".dat";
		}
		//create button for each user build
		foreach (string txtName in Directory.GetFiles(Application.persistentDataPath)) {
			GameObject button = Instantiate (buttonPrefab);
			RectTransform rect = button.GetComponent<RectTransform> ();
			rect.SetParent(transform);
			rect.localScale = scale;
			rect.GetComponentInChildren<Text>().text = txtName.Substring(txtName.LastIndexOf("\\")+1, txtName.Length-txtName.LastIndexOf("\\")-5);
			rect.GetComponent<SelectBuild>().txtName =  txtName;
		}
	}
}
