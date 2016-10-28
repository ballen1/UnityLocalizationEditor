using UnityEngine;
using System.Collections;
using UnityEditor;
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
			EditorGUILayout.LabelField ("Current Language", EditorStyles.boldLabel);
			CreateLanguagePopup ();


			if (GUILayout.Button ("New Language")) {
				NewLanguageDialog ();
			}

			GUILayout.EndHorizontal ();
		}

	}

	private void OpenTranslationUnit() {

		string path = EditorUtility.OpenFilePanel ("Choose Translation Unit", "", "");

		if (path.Length > 0) {

			if (path.StartsWith (Application.dataPath)) {
				path = path.Substring (Application.dataPath.Length - "Assets".Length);
				tu = AssetDatabase.LoadAssetAtPath<TranslationUnit> (path);

				if (tu.languageFiles == null) {
					tu.languageFiles = new List<string> ();
				}
				tu.path = path;
			}

		}

	}

	private void CreateTranslationUnit() {

		string path = EditorUtility.OpenFolderPanel ("Choose folder for translation unit", "", "");

		if (path.Length > 0) {
			tu = ScriptableObject.CreateInstance<TranslationUnit> ();
			tu.languageFiles = new List<string> ();

			if (path.StartsWith(Application.dataPath)) {

				path = path.Substring (Application.dataPath.Length - "Assets".Length);
				tu.path = path;

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
				popupOptions = tu.languageFiles.ToArray ();
			}

			EditorGUILayout.Space ();
			languagePopupIndex = EditorGUILayout.Popup (languagePopupIndex, popupOptions);
		}

	}

	private void NewLanguageDialog() {

	}
}
