using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextScript : MonoBehaviour {

	Text textBox;
	string textKey;
	int dialogIndex = 0;
	string[] dialog = {"Rick.Hurry", "Rick.Walmart", "Rick.Flip", "Rick.LimitedEdition", "Rick.Hurry2"};

	// Use this for initialization
	void Start () {

		textBox = GetComponent<Text> ();
		textKey = dialog [dialogIndex];
		textBox.text = LocalizationManager.Get (textKey);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown(0)) {
			dialogIndex = ++dialogIndex % dialog.Length;
		}

		textKey = dialog [dialogIndex];
		textBox.text = LocalizationManager.Get (textKey);

	}
}
