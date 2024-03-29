using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;

#endif
using MH;

[System.Serializable]

public class AMTrack : ScriptableObject {

    public bool enabled = true; //if not enabled, the actions will not work in edit/game mode
	public int id;
	public new string name;
	public List<AMKey> keys = new List<AMKey>();
	public List<AMAction> cache = new List<AMAction>();	// action cache
	public bool foldout = true;							// whether or not to foldout track in timeline GUI
	
	public AMTake parentTake;
	
	// set name based on index
	public void setName(int index) {
		name = "Track"+(index+1);
	}
	
	// set name from string
	public void setName(string name) {
		this.name = name;	
	}
	
	// does track have key on frame
	public bool hasKeyOnFrame(int _frame) { 
		foreach(AMKey key in keys) {
			if(key.frame == _frame) return true;	
		}
		return false;
	}
	
	// draw track gizmos
	public virtual void drawGizmos(float gizmo_size) {}
	
	// preview frame
	public virtual void previewFrame(float frame, AMTrack extraTrack = null) { }
	
	// update cache
	public virtual void updateCache() { 
		AMCameraFade.doShouldUpdateStill();
	}
	
	public virtual AnimatorTimeline.JSONInit getJSONInit() {
		Debug.LogWarning("Animator: No override for getJSONInit()");
		return new AnimatorTimeline.JSONInit();
	}
	
	// get key on frame
	public AMKey getKeyOnFrame(int _frame) {
		foreach(AMKey key in keys) {
			if(key.frame == _frame) return key;
		}
		Debug.LogError ("Animator: No key found on frame "+_frame);
		return new AMKey();
	}
	
	// track type as string
	public virtual string getTrackType() {
		return "Unknown";	
	}
	
	public void sortKeys() {
        AMUtil.recordObject(this, "sort keys");
		// sort
		keys.Sort((c,d) => c.frame.CompareTo(d.frame));		
	}
	
	public void deleteKeyOnFrame(int frame) {

		for(int i=0;i<keys.Count;i++) {
            var k = keys[i];
			if(k.frame == frame) {
                AMUtil.recordObject(this, "delete key on frame");
                keys.RemoveAt(i);
                k.destroy();
                --i;
			}
		}
	}
	
	public void deleteDuplicateKeys() {
		sortKeys();
		int lastKey = -1;
		for(int i=0;i<keys.Count;i++) {
			if(keys[i].frame == lastKey) {
				keys[i].destroy();
				keys.RemoveAt (i);
				i--;
			} else {
				lastKey = keys[i].frame;	
			}
		}
	}
	
	public void deleteAllKeys()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
            deleteAllKeys_nonUndo();
        else
            deleteAllKeys_undo();
#else
        deleteAllKeys_nonUndo();
#endif
	}

    private void deleteAllKeys_undo()
    {
        AMUtil.recordObject(this, "destroy keys");
        var copyKeys = keys;
        keys = new List<AMKey>();
        foreach (var k in copyKeys)
        {
            k.destroy();
        }
        // destroy cache
        destroyCache();
    }

    private void deleteAllKeys_nonUndo()
    {
        foreach (AMKey key in keys)
        {
            key.destroy();
        }
        // destroy cache
        destroyCache();
        keys = new List<AMKey>();
    }
	
	public void deleteKeysAfter(int frame) {
		
		for(int i=0;i<keys.Count;i++) {
			if(keys[i].frame > frame) {
				keys[i].destroy();
				keys.RemoveAt(i);
				i--;
			}	
		}
	}
	
	public void destroy() {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
            destroy_nonUndo();
        else
            destroy_undo();
#else
        destroy_nonUndo();
#endif        
	}

    private void destroy_undo()
    {
#if UNITY_EDITOR
        deleteAllKeys_undo();

        AMUtil.destroyObjImm(this);
#endif
    }

    private void destroy_nonUndo()
    {
        // destroy keys
        foreach (AMKey key in keys)
        {
            key.destroy();
        }
        // destroy cache
        destroyCache();
        // destroy track
        //Object.DestroyImmediate(this);
        AMUtil.destroyObjImm(this);
    }
	
	public virtual List<GameObject> getDependencies()
	{
		return new List<GameObject>();	
	}
	
	public virtual List<GameObject> updateDependencies(List<GameObject> newReferences, List<GameObject> oldReferences) {
		return new List<GameObject>();
	}
	
	public void destroyCache() {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
            _destroyCache_nonUndo();
        else
            _destroyCache_undo();
#else
        _destroyCache_nonUndo();
#endif
        
	}

    private void _destroyCache_undo()
    {
#if UNITY_EDITOR
        if (cache == null) return;

        AMUtil.recordObject(this, "destroy cache");
        var copyCache = cache;
        cache = null;

        foreach (AMAction action in copyCache)
        {
            if (action == null) continue;
            action.destroy();
        }
#endif
    }

    private void _destroyCache_nonUndo()
    {
        if (cache == null) return;
        foreach (AMAction action in cache)
        {
            if (action == null) continue;
            action.destroy();
        }
        cache = null;
    }
	
	public void offsetKeysFromBy(int frame, int amount) {
		if(keys.Count <= 0) return;
		for(int i=0;i<keys.Count;i++) {
            if (frame <= 0 || keys[i].frame >= frame)
            {
                AMUtil.recordObject(keys[i], "update key");
                keys[i].frame += amount;
            }
		}
		updateCache();
	}
	
	// returns the offset
	public int shiftOutOfBoundsKeys() {
		if(keys.Count<=0) return 0;
		sortKeys();
		if(keys[0].frame>=1) return 0;
		int offset = 0;
		offset = Mathf.Abs(keys[0].frame)+1; // calculate shift: -1 = 1+1 etc
		foreach(AMKey key in keys) {
			key.frame += offset;	
		}
		updateCache();
		return offset;
	}
	
	// get action for frame from cache
	public AMAction getActionContainingFrame(int frame) {
		for(int i=cache.Count-1;i>=0;i--) {
			if(frame >= cache[i].startFrame) return cache[i];	
		}
		if(cache.Count > 0) return cache[0];	// return first if not greater than any action
		Debug.LogError ("Animator: No action found for frame "+frame);
		AMAction a = ScriptableObject.CreateInstance<AMAction>();
		return a;
	}
	
	// get action for frame from cache
	public AMAction getActionForFrame(int startFrame) {
		foreach(AMAction action in cache) {
			if(action.startFrame == startFrame) return action;	
		}
		Debug.LogError ("Animator: No action found for frame "+startFrame);
		AMAction a = ScriptableObject.CreateInstance<AMAction>();
		return a;
	}
	
	// get index of action for frame
	public int getActionIndexForFrame(int startFrame) {
		for(int i=0;i<cache.Count;i++) {
			if(cache[i].startFrame == startFrame) return i;	
		}
		return -1;
	}
	
	// is there any action with the start frame
	public bool hasActionOnFrame(int _frame) { 
		foreach(AMAction action in cache) {
			if(action.startFrame == _frame) return true;	
		}
		return false;
	}
	
	// if whole take is true, when end frame is reached the last keyframe will be returned. This is used for the playback controls
	public int getKeyFrameAfterFrame(int frame, bool wholeTake = true) {
		foreach(AMKey key in keys) {
			if(key.frame > frame) return key.frame;
		}
		if(!wholeTake) return -1;
		if(keys.Count > 0) return keys[0].frame;
		Debug.LogError ("Animator: No key found after frame "+frame);
		return -1;
	}
	
	// if whole take is true, when start frame is reached the last keyframe will be returned. This is used for the playback controls
	public int getKeyFrameBeforeFrame(int frame, bool wholeTake = true) {

		for(int i=keys.Count-1;i>=0;i--) {
			if(keys[i].frame < frame) return keys[i].frame;
		}
		if(!wholeTake) return -1;
		if(keys.Count > 0) return keys[keys.Count-1].frame;
		Debug.LogError ("Animator: No key found before frame "+frame);
		return -1;
	}
	
	public AMKey[] getKeyFramesInBetween(int startFrame, int endFrame) {
		List<AMKey> lsKeys = new List<AMKey>();
		if(startFrame <= 0 || endFrame <= 0 || startFrame >= endFrame || !hasKeyOnFrame(startFrame) || !hasKeyOnFrame(endFrame)) return lsKeys.ToArray();
		sortKeys();
		foreach(AMKey key in keys) {
			if(key.frame >= endFrame) break;
			if(key.frame > startFrame) lsKeys.Add(key);
		}
		return lsKeys.ToArray();
	}
	
	public float[] getKeyFrameRatiosInBetween(int startFrame, int endFrame) {
		List<float> lsKeyRatios = new List<float>();
		if(startFrame <= 0 || endFrame <= 0 || startFrame >= endFrame || !hasKeyOnFrame(startFrame) || !hasKeyOnFrame(endFrame)) return lsKeyRatios.ToArray();
		sortKeys();
		foreach(AMKey key in keys) {
			if(key.frame >= endFrame) break;
			if(key.frame > startFrame) lsKeyRatios.Add((float)(key.frame-startFrame)/(float)(endFrame-startFrame));
		}
		return lsKeyRatios.ToArray();
	}


    #region "rebind"
    // "rebind" 
		
	public virtual void rebind(RebindOption opt) { }
		
	public virtual void unbind(RebindOption opt) { }

    protected void rebind4Keys(RebindOption opt)
    {
        for (var ie = keys.GetEnumerator(); ie.MoveNext(); )
        {
            var k = ie.Current;
            k.rebind(opt);
        }
    }

    protected void unbind4Keys(RebindOption opt)
    {
        for (var ie = keys.GetEnumerator(); ie.MoveNext(); )
        {
            var k = ie.Current;
            k.unbind(opt);
        }
    }
		
	protected void rebind4Actions(RebindOption opt)
	{
		for (var ie = cache.GetEnumerator(); ie.MoveNext(); )
		{
		    var act = ie.Current;
		    act.rebind(opt);
		}
	}

    protected void unbind4Actions(RebindOption opt)
    {
        for (var ie = cache.GetEnumerator(); ie.MoveNext(); )
        {
            var act = ie.Current;
            act.unbind(opt);
        }
    }

	#endregion "rebind"

    /// <summary>
    /// called by AMTake.SaveAsset,
    /// the subclasses will override this to save into the asset
    /// </summary>
#if UNITY_EDITOR
    public virtual void SaveAsset(AnimatorData mb, AMTake take)
    {
        AMTakeSav.AddObjectToAsset(this, take);

        foreach (var key in keys)
        {
            key.SaveAsset(mb, take);
        }
        foreach (var act in cache)
        {
            act.SaveAsset(mb, take);
        }

    }
#endif

}
