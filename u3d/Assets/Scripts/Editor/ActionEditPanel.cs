using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;



// [ExecuteInEditMode]
public class ActionEditWindow : EditorWindow
{	

	public ActionTable m_ActionTable;
	private string fileName = "NewAction";
	private string path = "";

	// private int m_CurActionIndex = 0;
	// private ActionObject m_CurActionObj = null;
	// private int m_CurEventIndex = 0;
	// private ActionObject.Event m_CurEvent = null;

	private Vector2 scrollPosition;


	[MenuItem("Tool/ActionEditWindow")]
    public static void Init()
    {
       // ActionEditPanel window = EditorWindow.GetWindow<ActionEditPanel>();
    	ActionEditWindow window = (ActionEditWindow)EditorWindow.GetWindow(typeof(ActionEditWindow));
       window.Show();
    }

	void Awake()
	{
		m_ActionTable = new ActionTable();
		m_ActionTable.Init();
	}

	void OnGUI()
	{
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(position.width), GUILayout.Height(position.height));

		EditorGUILayout.BeginHorizontal();
		if ( GUILayout.Button( "Load Scene" ) )
		{
			EditorSceneManager.OpenScene("Assets/Scripts/Editor/CharacterEffectEditor.unity");
			if(!EditorApplication.isPlaying)
				EditorApplication.isPlaying = true;
		}
		if (GUILayout.Button("Clear Action"))
        {
        	this.m_ActionTable = new ActionTable();
            this.path = string.Empty;
			this.fileName = "NewAction";
        }
        if (GUILayout.Button("Load Action"))
        {
        	this.path = EditorUtility.OpenFilePanel("Open Asset...", Application.dataPath + "/App/Art/Resources/Battle/Action/", "asset");
            if (this.path != string.Empty)
            {
                this.m_ActionTable.OnLoad(this.path);
				this.fileName = System.IO.Path.GetFileNameWithoutExtension( this.path );
            }
        }
        if (GUILayout.Button("Save Action"))
        {
        	this.path = EditorUtility.SaveFilePanel("Save Asset...", Application.dataPath + "/App/Art/Resources/Battle/Action/",this.fileName,"asset");
        	if( this.path != string.Empty )
        	{
        		this.m_ActionTable.OnSave(path);
        	}
        }
		EditorGUILayout.EndHorizontal();

		if(this.m_ActionTable != null )
			this.m_ActionTable.Draw();

		GUILayout.EndScrollView();
	}

	void Update()
	{
		//
	}
}
