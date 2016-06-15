using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//George Eisa
//15/06/2015
//TextFormatting
//Puts an underline in front of first letter of tool lables to indicate the shortcut

public class TextFormatting : MonoBehaviour {

	void Start () {
		char und = '\u0332';
		string message = GetComponent<Text> ().text;
		GetComponent<Text>().text = (und.ToString() + message);
	}
}
