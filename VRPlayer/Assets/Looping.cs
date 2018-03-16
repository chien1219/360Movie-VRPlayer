using UnityEngine;
using UnityEngine.UI;

public class Looping : MonoBehaviour {

    public GameObject VideoSphere;
    public GameObject Text;
    // Use this for initialization
    void Start () {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        var videoplayer = VideoSphere.GetComponent<UnityEngine.Video.VideoPlayer>();
        if (videoplayer.isLooping)
        {
            Text.GetComponent<Text>().text = "Looping";
        }
        else
        {
            Text.GetComponent<Text>().text = "Non-Looping";
        }
    }

    private void OnClick()
    {
        var videoplayer = VideoSphere.GetComponent<UnityEngine.Video.VideoPlayer>();
        if (videoplayer.isLooping)
        {
            videoplayer.isLooping = false;
        }
        else
        {
            videoplayer.isLooping = true;
        }
    }
}
