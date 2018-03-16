using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class StopButton: MonoBehaviour {

	public GameObject VideoSphere;
	public void OnPointerDown(PointerEventData eventData) { }

	void Start() {
		var button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
	}

	private void OnClick() {
		var videoplayer = VideoSphere.GetComponent<UnityEngine.Video.VideoPlayer> ();
		videoplayer.Stop ();
		videoplayer.playbackSpeed = 1.0f;
	}
}
