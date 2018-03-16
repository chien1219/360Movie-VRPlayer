using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class Slower : MonoBehaviour {

	public GameObject VideoSphere;
	public void OnPointerDown(PointerEventData eventData) { }

	void Start() {
		var button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
	}

	private void OnClick() {
		var videoplayer = VideoSphere.GetComponent<UnityEngine.Video.VideoPlayer> ();
		if (videoplayer.playbackSpeed > 0.2f) {
			videoplayer.playbackSpeed -= 0.2f;
		}
	}

}
