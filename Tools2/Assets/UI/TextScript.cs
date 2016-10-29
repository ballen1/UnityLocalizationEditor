using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextScript : MonoBehaviour {

	Text textBox;
	string textKey = "Test.Hello";

	// Use this for initialization
	void Start () {

		textBox = GetComponent<Text> ();
		textBox.text = LocalizationManager.Get (textKey);
	}
	
	// Update is called once per frame
	void Update () {
	
		textBox.text = LocalizationManager.Get (textKey);

	}
}
