using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LanguageUnit : ScriptableObject {

	public string language = "Language Unspecified";
	public List<string> keys;
	public List<string> values;

	public string Get(string key) {
		int index = keys.IndexOf (key);

		if (index == -1) {
			return null;
		} else {
			return values [index];
		}
	}
}
