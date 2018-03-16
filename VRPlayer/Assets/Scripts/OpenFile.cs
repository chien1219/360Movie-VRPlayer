using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;

[RequireComponent(typeof(Button))]
public class OpenFile : MonoBehaviour, IPointerDownHandler
{
    public string Title = "";
    public string FileName = "";
    public string Directory = "";
    public ExtensionFilter[] Extension = {
    new ExtensionFilter("Movie Files", "mp3", "wav" , "mp4", "mov"),
    new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
    new ExtensionFilter("All Files", "*" ),
    };
    public GameObject VideoSphere;
    public GameObject prefabButton;
    public RectTransform ParentPanel;
    public GameObject fileList;
    public GameObject UIParentPanel;
    public RectTransform UISVPanel;
    public GameObject PreButton;
    public GameObject NextButton;
    public GameObject UIPreButton;
    public GameObject UINextButton;
    public static bool firstTimeFlag;
    public int itemCount;
    public int currentItem;

    public struct fileInfo_
    {
        string file;
        string path;

        public void setFileInfo(string fileName, string filePath)
        {
			file = fileName;
            path = filePath;
        }
        public string getFileName()
        {
            return file;
        }
        public string getPathName()
        {
            return path;
        }
        public bool isExisted()
        {
            return (file == null) ? false : true; 
        }
    };

    fileInfo_[] fileInfo = new fileInfo_[50]; 
    
#if UNITY_WEBGL && !UNITY_EDITOR
	//
	// WebGL
	//
	[DllImport("__Internal")]
	private static extern void UploadFile(string id);

	public void OnPointerDown(PointerEventData eventData) {
	UploadFile(gameObject.name);
	}

	// Called from browser
	public void OnFileUploaded(string url) {
	StartCoroutine(OutputRoutine(url));
	}
#else
    //
    // Standalone platforms & editor
    //
    public void OnPointerDown(PointerEventData eventData) { }

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        firstTimeFlag = true;
        itemCount = 0;
    }
    
    private void OnClick()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel(Title, Directory, Extension, true);

        for (int i = 0; i < paths.Length; i++)
        {
        	string[] fileParse = paths[i].Split('\\', '.');
        	FileName = fileParse[fileParse.Length - 2]; //get the file name
        	FileName = FileName.Length >= 10 ? FileName.Substring(0, 10) : FileName;
			
        	if (!isExistedInList(FileName))
        	{
        	    fileInfo[itemCount].setFileInfo(FileName, paths[i]);
        	    if (!firstTimeFlag)
        	        createButton(fileInfo[itemCount].getFileName(), fileInfo[itemCount].getPathName(), itemCount);
        	    ++itemCount;
                currentItem = 0;

            }
        }

		if (paths.Length > 0)
		{
			if (firstTimeFlag && itemCount > 1)
			{
				GameObject filelist = (GameObject)Instantiate(fileList);

				filelist.SetActive(true);
				UIParentPanel.SetActive(true);
				filelist.transform.SetParent(ParentPanel, false);

                initPreNextButton();
                initButtonList();

				firstTimeFlag = false;
			}
		}

		StartCoroutine(OutputRoutine("file://"+paths[0]));
    }
#endif

    void initPreNextButton()
    {
        PreButton.SetActive(true);
        NextButton.SetActive(true);
        UIPreButton.SetActive(true);
        UINextButton.SetActive(true);

        Button preButton = PreButton.GetComponent<Button>();
        preButton.onClick.AddListener(() => ButtonClicked(currentItem - 1));
        Button nextButton = NextButton.GetComponent<Button>();
        nextButton.onClick.AddListener(() => ButtonClicked(currentItem + 1));

        preButton = UIPreButton.GetComponent<Button>();
        preButton.onClick.AddListener(() => ButtonClicked(currentItem - 1));
        nextButton = UINextButton.GetComponent<Button>();
        nextButton.onClick.AddListener(() => ButtonClicked(currentItem + 1));
    }


    bool isExistedInList(string fileName)
    {
        if (itemCount == 0)
            return false;

        for (int j = 0; j <  itemCount; j++)    //check whether button exists
        { 
            if (fileInfo[j].getFileName().Equals(fileName))             //////TOCHECK
                return true;
        }
        return false;
    }

    void initButtonList()
    {
        for (int i = 0; i < itemCount; i++)
            createButton(fileInfo[i].getFileName(), fileInfo[i].getPathName(), i);
    }

    void createButton(string fileName, string path, int index)
    {
        GameObject goButton = initButton(fileName, false);
        Button tempButton = goButton.GetComponent<Button>();
        tempButton.onClick.AddListener(() => ButtonClicked(index));
        tempButton.GetComponent<OpenFile>().enabled = false;

        goButton = initButton(fileName, true);
        tempButton = goButton.GetComponent<Button>();
        tempButton.onClick.AddListener(() => ButtonClicked(index));
        tempButton.GetComponent<OpenFile>().enabled = false;
    }
    
    GameObject initButton(string fileName, bool is_UI)
    {
        GameObject goButton = (GameObject)Instantiate(prefabButton);
        if (!is_UI) {
            goButton.transform.SetParent(ParentPanel, false);
            goButton.transform.localScale = new Vector3(1, 1, 1);
        }
        else {
            goButton.transform.SetParent(UISVPanel, false);    
        }

        goButton.name = fileName;
        goButton.GetComponentInChildren<Text>().text = fileName;

        return goButton;
    }

    void ButtonClicked(int index)
    {
        if (index > -1 && fileInfo[index].isExisted())
        {
            currentItem = index;
            StartCoroutine(OutputRoutine("file://" + fileInfo[index].getPathName()));
        }
    }


    private IEnumerator OutputRoutine(string url)
    {
        Debug.Log("URL: " + url);
        var loader = new WWW(url);
        yield return loader;

        string[] fileParse = url.Split('\\', '.');
        string fileType = fileParse[fileParse.Length - 1]; //get the file type

        bool isVideo = true;
        if (fileType.Equals("jpg") || fileType.Equals("png"))
            isVideo = false;

        if (isVideo)
        {
            var videoplayer = VideoSphere.GetComponent<UnityEngine.Video.VideoPlayer>();
            videoplayer.url = url;
            videoplayer.Play();
            videoplayer.isLooping = true;
        }
        else
        {
            var videoplayer = VideoSphere.GetComponent<UnityEngine.Video.VideoPlayer>();
            videoplayer.Stop();
            Texture2D texTmp = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            loader.LoadImageIntoTexture(texTmp);
            VideoSphere.GetComponent<UnityEngine.Renderer>().material.SetTexture("_MainTex", texTmp);
        }

        //var loader = new WWW(url);
        //yield return loader;
        //output.text = loader.text;
    }
}