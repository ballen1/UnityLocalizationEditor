using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(LocalizationManager))]
public class LocalizationManagerEditor : Editor {

	SerializedProperty translationUnit;
	SerializedProperty languageUnit;

	LocalizationManager lm;

	private int selectedIndex = 0;

	void OnEnable() {
		translationUnit = serializedObject.FindProperty ("selectedTranslationUnit");
		lm = (LocalizationManager)target;
	}

	public override void OnInspectorGUI() {

		EditorGUILayout.PropertyField (translationUnit);

		TranslationUnit tu = (TranslationUnit)translationUnit.objectReferenceValue;

		string[] languages = tu.getAvailableLanguages ();

		selectedIndex = EditorGUILayout.Popup (selectedIndex, languages);
		lm.SetLanguageindex (selectedIndex);
	}

}
