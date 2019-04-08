using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class WebCamManager : MonoBehaviour {


    public Dropdown webCamDropDown;
    public InputField subjectIdField;
    private Vector3 _initialFrontPhotoScale;
    private Vector3 _initialSidePhotoScale;
    private Renderer _currentPhoto;
    public Renderer frontPhoto;
    public Renderer sidePhoto;
    public Transform sideReference;
    public Transform frontReference;
    private WebCamTexture _webCamTex;    
    private CamView _camView;
    private string filePath;
    private string fileName;
    private string extension;
    public Slider imageRotationSlider;
    public Slider imageScaleSlider;

    void Awake(){
        
        _initialFrontPhotoScale = frontPhoto.transform.localScale;
        _initialSidePhotoScale = sidePhoto.transform.localScale;
        _currentPhoto = frontPhoto;
        filePath = System.IO.Directory.GetCurrentDirectory() + "/Pictures/";
        fileName = _camView.ToString();
        extension = ".png";
        ViewState.VIEW = _camView;
        //webCamDropDown.onValueChanged = WebCamChanged;   
    }

    void Start () {
        for (int i = 0; i < WebCamTexture.devices.Length; i++){
            WebCamDevice webCamDevice = WebCamTexture.devices[i];
            webCamDropDown.options.Add(new Dropdown.OptionData() { text = webCamDevice.name });
        }
        webCamDropDown.value = 0;
        WebCamChanged();
        _initialFrontPhotoScale = SetPhotoDimension(frontPhoto, _initialFrontPhotoScale);
        _initialSidePhotoScale = SetPhotoDimension(sidePhoto, _initialSidePhotoScale);
        ChangePhotoObject(frontPhoto.gameObject, _initialFrontPhotoScale);
    }

    public void WebCamChanged(){
        Dropdown.OptionData optionData = webCamDropDown.options[webCamDropDown.value];
        SetWebCam(optionData.text);
    }

    public void SetWebCam(string webCamName){
        //Renderer photoRenderer = frontPhoto.GetComponent<Renderer>();
        /*if (photoRenderer.material.mainTexture is WebCamTexture){
            WebCamTexture webCamTexture = (WebCamTexture)photoRenderer.material.mainTexture;
            webCamTexture.Stop();
        }*/
        if(!_webCamTex) _webCamTex = new WebCamTexture(webCamName);
        _webCamTex.Play();
        Texture2D savedTexture;
        if(CheckSavedPicture(_camView, out savedTexture)){
            _currentPhoto.material.mainTexture = savedTexture;
            return;
        }

        _currentPhoto.material.mainTexture = _webCamTex;
        _webCamTex.Play();
        /*
        Vector3 scale = _currentPhoto.transform.localScale;
        float initialPhotoScale = (_camView == CamView.FRONT) ? _initialFrontPhotoScale : _initialSidePhotoScale;      
        scale.x = initialPhotoScale * _webCamTex.width / _webCamTex.height;
        _currentPhoto.transform.localScale = scale;*/
    }

    public Vector3 SetPhotoDimension(Renderer pPhoto, Vector3 pInitialPhotoScale){
        Vector3 scale = pPhoto.transform.localScale;
        //float initialPhotoScale = (_camView == CamView.FRONT) ? _initialFrontPhotoScale : _initialSidePhotoScale;
        scale.x = pInitialPhotoScale.x * _webCamTex.width / _webCamTex.height;
        pPhoto.transform.localScale = scale;
        return pPhoto.transform.localScale;
    }

    void ImageRotationChanged(GameObject pPhoto){
        Vector3 angles = pPhoto.transform.eulerAngles;
        angles.z = imageRotationSlider.value;
        pPhoto.transform.eulerAngles = angles;
    }

    void ImageScaleChanged(GameObject pPhoto, Vector3 pInitialScale){

        pPhoto.transform.localScale = pInitialScale * imageScaleSlider.value;
    }

    public void ChangePhotoObject(GameObject pPhoto, Vector3 pInitialScale){
        imageRotationSlider.onValueChanged = new Slider.SliderEvent();
        imageScaleSlider.onValueChanged = new Slider.SliderEvent();
        imageRotationSlider.onValueChanged.AddListener(delegate { ImageRotationChanged(pPhoto); });
        imageScaleSlider.onValueChanged.AddListener(delegate { ImageScaleChanged(pPhoto, pInitialScale); });

    }


    public bool CheckSavedPicture(CamView pCamView, out Texture2D savedTexture){
        if (File.Exists(filePath + subjectIdField.text + "_" + fileName + extension)){
           byte[] savedData = File.ReadAllBytes(filePath + subjectIdField.text + "_" + fileName + extension);
            savedTexture = new Texture2D(1, 1);
            savedTexture.LoadImage(savedData);
            return true;
        }
        savedTexture = null;
        return false;
    }

    public void LoadSubject(){
        Texture2D savedTexture;
        if (CheckSavedPicture(CamView.FRONT, out savedTexture)){
            frontPhoto.material.mainTexture = savedTexture;
        }
        if (CheckSavedPicture(CamView.SIDE, out savedTexture))
        {
            sidePhoto.material.mainTexture = savedTexture;
        }
       
    }

    public void DeletePicture(){
        if (File.Exists(filePath + subjectIdField.text + "_" + fileName + extension)){
            File.Delete(filePath + subjectIdField.text + "_" + fileName + extension);
            _currentPhoto.material.mainTexture = _webCamTex;
            _webCamTex.Play();
        }
    }

    public void DeleteAllPictures()
    {
        //TODO
    }

    public void togglePlay(){
        Renderer renderer = frontPhoto.GetComponent<Renderer>();
        SaveImage();

        if (renderer.material.mainTexture is WebCamTexture){
            _webCamTex = (WebCamTexture)renderer.material.mainTexture;
            if (_webCamTex.isPlaying){
                _webCamTex.Pause();
            }else{
                _webCamTex.Play();
            }
        }
    }
    
    public void SaveImage()
    {
        Texture2D snap = new Texture2D(_webCamTex.width, _webCamTex.height);
        snap.SetPixels(_webCamTex.GetPixels());
        snap.Apply();
        //string filePath = Application.dataPath;
        //string fileName = "/Pictures/" + _camView.ToString();
        System.IO.File.WriteAllBytes(filePath + subjectIdField.text + "_" + fileName + extension, snap.EncodeToPNG());
        print("File Saved on: " + filePath + subjectIdField.text + "_" + fileName + extension);
        _currentPhoto.material.mainTexture = snap; //("_MainTex", snap);
    }
    public void togglePLayCam(){
        togglePlay();
    }

    public void SwitchView(){
        _currentPhoto.gameObject.SetActive(false);
        if (_camView == CamView.FRONT){
            _camView = CamView.SIDE;
            Camera.main.transform.position = sideReference.position;
            Camera.main.transform.rotation = sideReference.rotation;
            _currentPhoto = sidePhoto;  
             ChangePhotoObject(sidePhoto.gameObject, _initialSidePhotoScale);
        }else{
            _camView = CamView.FRONT;
            Camera.main.transform.position = frontReference.position;
            Camera.main.transform.rotation = frontReference.rotation;
            _currentPhoto = frontPhoto;
            ChangePhotoObject(frontPhoto.gameObject, _initialFrontPhotoScale);

        }
        //frontSlides.SetActive(!frontSlides.activeInHierarchy);
        //sideSlides.SetActive(!sideSlides.activeInHierarchy);
        ViewState.VIEW = _camView;
        fileName = _camView.ToString();
        _currentPhoto.gameObject.SetActive(true);
        WebCamChanged();
    }

    public void DisableCamObjects(){
        _camView = CamView.FRONT;
        Camera.main.transform.position = frontReference.position;
        Camera.main.transform.rotation = frontReference.rotation;
    
        ViewState.VIEW = _camView;
        fileName = _camView.ToString();
        _currentPhoto.gameObject.SetActive(false);
        _currentPhoto = frontPhoto;
        _currentPhoto.gameObject.SetActive(false);
        WebCamChanged();
    }

    public void EnableCamObjects(){
        _currentPhoto.gameObject.SetActive(true);
    }

}
public enum CamView
{
    FRONT,
    SIDE
}

public class ViewState
{
    public static CamView VIEW;
}
