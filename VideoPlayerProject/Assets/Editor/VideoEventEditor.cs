using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VideoEvent))]
public class VideoEventEditor : Editor {

	VideoEvent data;

	void OnEnable() {
		data = (VideoEvent)target;
	}

	public override void OnInspectorGUI() {
		serializedObject.Update();
		EditorGUILayout.PropertyField(serializedObject.FindProperty("eventType"));
		switch (data.eventType) {
			case VideoEvent.EventType.Play:
			case VideoEvent.EventType.Pause:
			case VideoEvent.EventType.Replay:
				EditorGUILayout.PropertyField(serializedObject.FindProperty("playImage"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("pauseImage"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("restartImage"));
				break;
			case VideoEvent.EventType.Load:
				EditorGUILayout.PropertyField(serializedObject.FindProperty("videoName"));
				break;
			case VideoEvent.EventType.LoopOn:
			case VideoEvent.EventType.LoopOff:
				EditorGUILayout.PropertyField(serializedObject.FindProperty("toggleOnColor"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("toggleOffColor"));
				break;
			default:break;
		}
		serializedObject.ApplyModifiedProperties();
	}
}
