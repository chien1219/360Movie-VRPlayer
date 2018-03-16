using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class VideoPlayBar : MonoBehaviour
{

    public GameObject VideoSphere;
    public GameObject Text;
    public Slider slider;
    int counter;

    public void OnPointerDown(PointerEventData eventData) { }


    void Start()
    {
        Text.GetComponent<Text>().text = "Time: 0:00 / 0:00" + '\n' + "PlaySpeed: 1";
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        slider.value = 0;
        counter = 0;
    }

    void Update()
    {
        var videoPlayer = VideoSphere.GetComponent<UnityEngine.Video.VideoPlayer>();
        var frameRate = videoPlayer.frameRate;

        string timeInfo = MakeTimecode(videoPlayer.frame, frameRate) + "/" + MakeTimecode(videoPlayer.frameCount, frameRate);

        Text.GetComponent<Text>().text = ("Time: " + MakeTimecode(videoPlayer.frame, frameRate) + '\n');
        Text.GetComponent<Text>().text += ("PlaySpeed: " + videoPlayer.playbackSpeed.ToString());

        if (counter == 500)
        {
            counter = 0;
            slider.value = getCurrentRatio(videoPlayer.frame, videoPlayer.frameCount);
        }
        else counter++;
    }

    void ValueChangeCheck()
    {
        var videoPlayer = VideoSphere.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.frame = ratioToFrame(slider.value, videoPlayer.frameCount, videoPlayer.frameRate);
    }

    float getCurrentRatio(long frame, ulong totalFrame)
    {
        return (float)frame / (float)totalFrame;
    }

    long ratioToFrame(float ratio, ulong frameCount, float frameRate)
    {
        float frame = (float)frameCount * ratio;
        return (long)frame;
    }

    int getTotalinSec(ulong frame, float frameRate)
    {
        float time = frame / frameRate;
       return (int)time % 60;
    }

    //Calculate the current time
    string MakeTimecode(long frame, float frameRate)
    {
        if (frameRate == 0)
        {
            return "0:00";
        }
        float time = frame / frameRate;
        int Second = (int)time % 60;
        int Min = (int)time / 60;
        string time_str = Min.ToString() + ":" + ((Second < 10) ? ("0" + Second.ToString()) : Second.ToString());
        return time_str;
    }
    //Calculete the whole time
    string MakeTimecode(ulong frame, float frameRate)
    {
        if (frameRate == 0)
        {
            return "0:00";
        }
        float time = frame / frameRate;
        int Second = (int)time % 60;
        int Min = (int)time / 60;
        string time_str = Min.ToString() + ":" + ((Second < 10) ? ("0" + Second.ToString()) : Second.ToString());
        return time_str;
    }

}