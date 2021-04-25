using UnityEngine;
using UnityEditor;
using System;

/*
Enemy_ScriptObject
    A scriptable object that holds the initial data for the enemies:
        - Shooter
        - Roller
*/
[CreateAssetMenu(fileName = "New Dialogue Data", menuName = "ScriptableObjects/Dialogue")]
public class Dialogue_ScriptObject : ScriptableObject
{
    public string characterName;
    public Sprite characterImage;
    public string dialogue;

    void OnEnable() => EditorUtility.SetDirty(this);
}

[CustomEditor(typeof(Dialogue_ScriptObject))]
public class Dialogue_ScriptObject_Editor: Editor{

    private string lineWarning = "This might not fit at the dialogue box. Try splitting it by segments and create another Dialogue Data.";

    public override void OnInspectorGUI()
    {
        var s = target as Dialogue_ScriptObject;

        EditorGUILayout.LabelField("Character Info", EditorStyles.boldLabel);
        s.characterName = EditorGUILayout.TextField("Name", s.characterName);
        s.characterImage = (Sprite)EditorGUILayout.ObjectField("Image", s.characterImage, typeof(Sprite), false);
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Dialogue", EditorStyles.label);
        s.dialogue = EditorGUILayout.TextArea(s.dialogue, GUILayout.MinHeight(80));

        if (s.dialogue.Length >= 75){
            EditorGUILayout.HelpBox(lineWarning, MessageType.Warning);
        }
    }
}