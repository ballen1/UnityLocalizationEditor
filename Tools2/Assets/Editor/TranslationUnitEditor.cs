using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TranslationUnitEditor : EditorWindow {

	public TranslationUnit tu;
	private int languagePopupIndex = 0;

	[MenuItem("Localization/Localization Editor")]
	static void Init() {
		EditorWindow.GetWindow<TranslationUnitEditor> ();
	}

	void OnGUI() {

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Translation Unit Editor", EditorStyles.boldLabel);

		if (GUILayout.Button("Open Translation Unit")) {
			OpenTranslationUnit ();
		}

		if (GUILayout.Button ("New Translation Unit")) {
			CreateTranslationUnit ();
		}

		GUILayout.EndHorizontal ();

		if (tu != null) {

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Current Translation Unit:", EditorStyles.boldLabel);
			GUILayout.Label (AssetDatabase.GetAssetOrScenePath (tu), EditorStyles.largeLabel);
			GUILayout.EndHorizontal ();

			EditorGUILayout.Space ();
			EditorGUILayout.Space ();

			tu.updateLanguageFileList ();

			GUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Current Language", EditorStyles.boldLabel);
			CreateLanguagePopup ();


			if (GUILayout.Button ("New Language")) {
				NewLanguageDialog ();
			}

			GUILayout.EndHorizontal ();

			if (tu.languageFiles.Count != 0) {
				displayLanguageUnitEditor ();
			}
		}

	}

	private void OpenTranslationUnit() {

		string path = EditorUtility.OpenFilePanel ("Choose Translation Unit", "", "");

		if (path.Length > 0) {

			if (path.StartsWith (Application.dataPath)) {
				path = pathRelToAssetsDir(path);
				tu = AssetDatabase.LoadAssetAtPath<TranslationUnit> (path);

				if (tu.languageFiles == null) {
					tu.languageFiles = new List<LanguageUnit> ();
				}
			}

		}

	}

	private void CreateTranslationUnit() {

		string path = EditorUtility.OpenFolderPanel ("Choose folder for translation unit", "", "");

		if (path.Length > 0) {
			tu = ScriptableObject.CreateInstance<TranslationUnit> ();
			tu.languageFiles = new List<LanguageUnit> ();

			if (path.StartsWith(Application.dataPath)) {

				path = pathRelToAssetsDir(path);

				AssetDatabase.CreateAsset (tu, path + System.IO.Path.DirectorySeparatorChar + "Translation.asset");
				AssetDatabase.SaveAssets ();

			}
		}
	}

	private void CreateLanguagePopup() {

		if (tu != null) {

			string[] popupOptions;

			if (tu.languageFiles.Count == 0) {
				popupOptions = new string[] { "None" };
			} else {
				popupOptions = tu.getAvailableLanguages();
			}

			EditorGUILayout.Space ();
			languagePopupIndex = EditorGUILayout.Popup (languagePopupIndex, popupOptions);
		}

	}

	private void NewLanguageDialog() {

		string path = EditorUtility.SaveFilePanelInProject ("Save new language asset file", "Language", "asset", "");

		if (!string.IsNullOrEmpty (path)) {
			LanguageUnit lu = ScriptableObject.CreateInstance<LanguageUnit> ();
			lu.keys = new List<string> ();
			lu.values = new List<string> ();

			AssetDatabase.CreateAsset (lu, path);
			AssetDatabase.SaveAssets ();

			tu.updateLanguageFileList ();
		}

	}

	// Returns index of key/value pair to be deleted
	private void displayLanguageUnitEditor() {

		int deletionMark = -1;
		LanguageUnit lu = tu.getLanguageUnit (languagePopupIndex);

		if (lu) {

			EditorGUILayout.BeginHorizontal ();
			lu.language = EditorGUILayout.TextField ("Language", lu.language);
			EditorGUILayout.EndHorizontal ();

			for (int i = 0; i < lu.keys.Count; i++) {
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.LabelField ("Key", GUILayout.MaxWidth(30));
				lu.keys [i] = EditorGUILayout.TextField (lu.keys [i]);
				EditorGUILayout.LabelField ("String", GUILayout.MaxWidth(50));
				lu.values [i] = EditorGUILayout.TextField (lu.values [i]);

				if (GUILayout.Button ("X", GUILayout.MaxWidth(20))) {
					deletionMark = i;
				}

				EditorGUILayout.EndHorizontal ();
			}

			EditorGUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Add New String")) {
				lu.keys.Add ("");
				lu.values.Add ("");
			}
			EditorGUILayout.EndHorizontal ();

			if (deletionMark != -1) {
				GUI.FocusControl (null);
				lu.keys.RemoveAt (deletionMark);
				lu.values.RemoveAt (deletionMark);
				EditorUtility.SetDirty (this);
			}
		}
			
	}

	private string pathRelToAssetsDir(string path) {
		return path.Substring (Application.dataPath.Length - "Assets".Length);
	}
}
