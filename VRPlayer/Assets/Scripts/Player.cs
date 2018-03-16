using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class Player : MonoBehaviour {

	public GameObject VideoSphere;
	public GameObject Text;

	public void OnPointerDown(PointerEventData eventData) { }


	void Start() {
		
		Text.GetComponent<Text>().text = "Player Control\n" + "Time: 0:00 / 0:00" + '\n' + "PlaySpeed: 1";

	}

	void Update(){
		var videoplayer = VideoSphere.GetComponent<UnityEngine.Video.VideoPlayer> ();
		var frameRate = videoplayer.frameRate;
		string timeInfo = MakeTimecode(videoplayer.frame, frameRate) + "/" + MakeTimecode(videoplayer.frameCount, frameRate);

		Text.GetComponent<Text> ().text = "Player Control\n";
		Text.GetComponent<Text> ().text += ("Time: " + timeInfo +'\n');
		Text.GetComponent<Text> ().text += ("PlaySpeed: " + videoplayer.playbackSpeed.ToString ());

	}

	//Calculate the current time
	string MakeTimecode(long frame, float frameRate){
		if (frameRate == 0) {
			return "0:00";
		}
		float time = frame / frameRate;
		int Second = (int)time % 60;
		int Min =  (int)time/60;
		string time_str = Min.ToString() + ":" + ((Second < 10)?("0"+Second.ToString()):Second.ToString());
		return time_str;
	}
	//Calculete the whole time
	string MakeTimecode(ulong frame, float frameRate){
		if (frameRate == 0) {
			return "0:00";
		}
		float time = frame / frameRate;
		int Second = (int)time % 60;
		int Min =  (int)time/60;
		string time_str = Min.ToString() + ":" + ((Second < 10)?("0"+Second.ToString()):Second.ToString());
		return time_str;
	}
		
}
