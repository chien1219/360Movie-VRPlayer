using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class Pause : MonoBehaviour {

	public GameObject VideoSphere;
	public GameObject Text;

	public void OnPointerDown(PointerEventData eventData) { }

	void Start() {
		var button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
	}

	void Update(){
		var videoplayer = VideoSphere.GetComponent<UnityEngine.Video.VideoPlayer> ();
		if (videoplayer.isPlaying) {
			Text.GetComponent<Text>().text = "Pause";
		} else {
			Text.GetComponent<Text>().text = "Play";
		}
	}

	private void OnClick() {
		var videoplayer = VideoSphere.GetComponent<UnityEngine.Video.VideoPlayer> ();
		if (videoplayer.isPlaying) {
			videoplayer.Pause ();
		} else {
			videoplayer.Play ();
		}
	}
}
