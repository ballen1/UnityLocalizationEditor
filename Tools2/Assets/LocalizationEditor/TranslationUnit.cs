using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TranslationUnit : ScriptableObject {

	public List<LanguageUnit> languageFiles;

	void Awake() {
		updateLanguageFileList ();
	}

	void OnEnable() {
		updateLanguageFileList ();
	}

	// Language Assets must be stored in the same directory as the Translation Unit
	// When it is asked to update the language file list, the TU will search its
	// directory for language files that are not tracked and add them to the list
	public void updateLanguageFileList() {


		string path = UnityEditor.AssetDatabase.GetAssetPath (this);

		if (!string.IsNullOrEmpty (path)) {
			string assetName = System.IO.Path.GetFileName (path);

			string containingFolder = path.Substring (0, path.Length - (assetName.Length + 1));
		
			string[] assets = UnityEditor.AssetDatabase.FindAssets ("t:LanguageUnit", new string[] { containingFolder });

			languageFiles.Clear ();

			for (int i = 0; i < assets.Length; i++) {
				LanguageUnit lu = UnityEditor.AssetDatabase.LoadAssetAtPath<LanguageUnit> (UnityEditor.AssetDatabase.GUIDToAssetPath (assets [i]));
				if (!languageFiles.Contains (lu)) {
					languageFiles.Add (lu);
				}
			}
		}
	}

	public string[] getAvailableLanguages() {
		List<string> languages = new List<string> ();

		int repeats = 1;

		for (int i = 0; i < languageFiles.Count; i++) {
			if (languages.Contains (languageFiles [i].language)) {
				languages.Add (languageFiles [i].language + repeats++);
			} else {
				languages.Add (languageFiles [i].language);
			}
		}

		return languages.ToArray ();
	}

	public LanguageUnit getLanguageUnit(int index) {
		if (languageFiles.Count > index) {
			return languageFiles [index];
		} else {
			return null;
		}
	}

}
