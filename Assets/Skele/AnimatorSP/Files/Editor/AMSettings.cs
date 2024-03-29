using UnityEngine;
using UnityEditor;
using System.Collections;
using MH;

public class AMSettings : EditorWindow {
	public static AMSettings window = null;
	
	public AMOptionsFile oData;
	public AnimatorData aData;	
	
	private int numFrames;
	private int frameRate;
	private bool saveChanges = false;
	// skins
	private GUISkin skin = null;
	private string cachedSkinName = null;
	
	void OnEnable() {
		window = this;
		this.titleContent = new GUIContent("Settings");
		this.minSize = new Vector2(125f,115f);
		this.maxSize = this.minSize;
		
		oData = AMOptionsFile.loadFile();
		loadAnimatorData();

	}
	void OnDisable() {
		window = null;
		if((aData)&& saveChanges) {
            AMUtil.regUndoSelectedTake(aData, "Modify Settings");
            bool saveNumFrames = true;
			if((numFrames < aData.getCurrentTake().numFrames) && (aData.getCurrentTake().hasKeyAfter(numFrames))) {
				if(!EditorUtility.DisplayDialog("Data Will Be Lost","You will lose some keys beyond frame "+numFrames+" if you continue.", "Continue Anway","Cancel")) {
					saveNumFrames = false;
				}
			}
			if(saveNumFrames) {
				// save numFrames
				aData.getCurrentTake().numFrames = numFrames;
				aData.getCurrentTake().deleteKeysAfter(numFrames);
		
				// save data
				foreach(AMTrack track in aData.getCurrentTake().trackValues) {
						EditorUtility.SetDirty(track);
				}
			}
			// save frameRate
			aData.getCurrentTake().frameRate = frameRate;
			EditorWindow.GetWindow (typeof (AMTimeline)).Repaint();
			// save data
			EditorUtility.SetDirty(aData);
		}
	}
	void OnGUI() {
        Event e = Event.current;
        AMTimeline.loadSkin(oData, ref skin, ref cachedSkinName, position);
		if(!aData) {
			AMTimeline.MessageBox("Animator requires an AnimatorData component in your scene. Launch Animator to add the component.",AMTimeline.MessageBoxType.Warning);
			return;
		}
		GUIStyle styleArea = new GUIStyle(GUI.skin.scrollView);
		styleArea.padding = new RectOffset(4,4,4,4);
		GUILayout.BeginArea(new Rect(0f,0f,position.width,position.height),styleArea);
		GUILayout.Label("Number of Frames");
		GUILayout.Space(2f);
		numFrames = EditorGUILayout.IntField(numFrames,GUI.skin.textField,GUILayout.Width(position.width-10f-12f));
		if(numFrames <= 0) numFrames = 1;
		GUILayout.Space(2f);
		GUILayout.Label("Frame Rate (Fps)");
		GUILayout.Space(2f);
		frameRate = EditorGUILayout.IntField(frameRate,GUI.skin.textField,GUILayout.Width(position.width-10f-12f));
		if(frameRate <= 0) frameRate = 1;
		GUILayout.Space(7f);
		GUILayout.BeginHorizontal();
			if(GUILayout.Button("Apply")) {
				saveChanges = true;
				this.Close();	
			}
			if(GUILayout.Button ("Cancel")) {
				saveChanges = false;
				this.Close();	
			}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();

        if (e.type == EventType.KeyDown && (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.Escape))
        {
            saveChanges = (e.keyCode == KeyCode.Return);
            this.Close();	
        }
	}
	void OnHierarchyChange()
	{
		if(!aData) loadAnimatorData();
	}
	public void reloadAnimatorData() {
		aData = null;
		loadAnimatorData();
	}
	void loadAnimatorData()
	{
        aData = AMTimeline.GetAnimatorData();
        if (aData != null)
        {
            numFrames = aData.getCurrentTake().numFrames;
            frameRate = aData.getCurrentTake().frameRate;
        }

        //GameObject go = GameObject.Find ("AnimatorData");
        //if(go) {
        //    aData = (AnimatorData) go.GetComponent<AnimatorData>();
        //    numFrames = aData.getCurrentTake().numFrames;
        //    frameRate = aData.getCurrentTake().frameRate;
        //}
	}
}
