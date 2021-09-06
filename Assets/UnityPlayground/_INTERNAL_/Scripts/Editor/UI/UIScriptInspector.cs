using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(UIScript))]
public class UIScriptInspector : InspectorBase {
	private string explanation = "Use the UI to visualise points and health for the players.";
	private string lifeReminder = "Don't forget to use the script HealthSystemAttribute on the player(s)!";

	private int gameType = 0;
	private string[] readableGameTypesEnum = new string[] { "Score", "Life", "Endless" };

	public override void OnInspectorGUI() {
		GUILayout.Space(10);
		EditorGUILayout.HelpBox(explanation, MessageType.Info);

		gameType = serializedObject.FindProperty("gameType").intValue;
		gameType = EditorGUILayout.Popup("Game type", gameType, readableGameTypesEnum);

		EditorGUILayout.PropertyField(serializedObject.FindProperty("scoreForZero"));

		if (gameType == 0) { //score game
			EditorGUILayout.PropertyField(serializedObject.FindProperty("scoreToWin"));
		}

		if (gameType == 1) { //life
			EditorGUILayout.HelpBox(lifeReminder, MessageType.Info);
		}

		//write all the properties back
		serializedObject.FindProperty("gameType").intValue = gameType;

		if (GUI.changed) serializedObject.ApplyModifiedProperties();
	}
}
