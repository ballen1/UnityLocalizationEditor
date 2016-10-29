﻿using UnityEngine;
using System.Collections;

public class LocalizationManager : MonoBehaviour {

	public static LocalizationManager Instance;
	public TranslationUnit selectedTranslationUnit;
	private LanguageUnit selectedLanguageUnit;
	public static int languageIndex = 0;

	void Awake() {
		
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}

		Instance.selectedTranslationUnit = selectedTranslationUnit;
		SelectLanguage ();
	}

	public static void SetLanguageindex (int index) {
		languageIndex = index;
	}

	private void SelectLanguage() {

		Instance.selectedLanguageUnit = Instance.selectedTranslationUnit.getLanguageUnit (languageIndex);

	}

	void Update() {
		SelectLanguage ();
	}

	public static string Get(string key) {
		return Instance.selectedLanguageUnit.Get (key);
	}

}
