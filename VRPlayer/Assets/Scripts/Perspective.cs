using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class Perspective : MonoBehaviour {

	bool inside;
	public GameObject VideoSphere;
	public void OnPointerDown(PointerEventData eventData) { }

	void Start() {
		inside = true;
		var button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
	}

	private void OnClick() {
		if (inside) {
			VideoSphere.transform.position = new Vector3 (0, 5, 25);
			inside = false;
		} else {
			VideoSphere.transform.position = new Vector3 (0, 0, 0);
			inside = true;
		}
	}

    void Update() {
        if (!inside) {
            VideoSphere.transform.Rotate(new Vector3(0, 0.2f, 0));
        }
    }
}
