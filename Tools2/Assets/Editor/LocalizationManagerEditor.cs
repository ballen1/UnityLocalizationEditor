using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(LocalizationManager))]
public class LocalizationManagerEditor : Editor {

	SerializedProperty translationUnit;
	SerializedProperty languageUnit;

	void OnEnable() {
		translationUnit = serializedObject.FindProperty ("selectedTranslationUnit");
	}

	public override void OnInspectorGUI() {

		EditorGUILayout.PropertyField (translationUnit);

		TranslationUnit tu = (TranslationUnit)translationUnit.objectReferenceValue;

		string[] languages = tu.getAvailableLanguages ();

		for (int i = 0; i < languages.Length; i++) {
			
		}

	}

}
