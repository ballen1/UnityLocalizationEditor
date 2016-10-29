using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(LocalizationManager))]
public class LocalizationManagerEditor : Editor {

	SerializedProperty translationUnit;
	SerializedProperty languageUnit;

	private int selectedIndex = 0;

	void OnEnable() {
		translationUnit = serializedObject.FindProperty ("selectedTranslationUnit");
	}

	public override void OnInspectorGUI() {

		EditorGUILayout.PropertyField (translationUnit);

		TranslationUnit tu = (TranslationUnit)translationUnit.objectReferenceValue;

		string[] languages = tu.getAvailableLanguages ();

		selectedIndex = EditorGUILayout.Popup (selectedIndex, languages);
		LocalizationManager.SetLanguageIndex (selectedIndex);
	}

}
