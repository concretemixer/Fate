using UnityEngine;
using System.Collections;
using MH;

[System.Serializable]
public class AMAudioKey : AMKey {

	public AudioClip audioClip;
	public bool loop;
	
	public bool setAudioClip(AudioClip audioClip) {
		if(this.audioClip != audioClip) {
            AMUtil.recordObject(this, "set audio clip");
			this.audioClip = audioClip;
			return true;
		}
		return false;
	}
	
	public bool setLoop(bool loop) {
		if(this.loop != loop) {
            AMUtil.recordObject(this, "set audio loop");
            this.loop = loop;
			return true;
		}
		return false;
	}
	
	// copy properties from key
	public override AMKey CreateClone ()
	{
		
		AMAudioKey a = ScriptableObject.CreateInstance<AMAudioKey>();
		a.frame = frame;
		a.audioClip = audioClip;
		a.loop = loop;
		
		return a;
	}
}
