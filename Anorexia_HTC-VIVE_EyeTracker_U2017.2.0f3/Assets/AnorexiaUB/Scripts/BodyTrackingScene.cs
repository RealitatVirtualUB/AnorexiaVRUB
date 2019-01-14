using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyTrackingScene : MainScene {

    public GameObject bodyPartsButtons;
    private bool _visTouchActive;
    public GameObject therapistController;
    public GameObject mirror;
    public Text statistics;
    public Slider weightSlider;
    private float _fadeDuration = 2f;
    private bool faded = false;
    private float counter = 0;
    
    // Use this for initialization
    void Start() {
        FadeToWhite(0);
        Invoke("FadeFromWhite", _fadeDuration);
        PrintInGameValues();
        GetComponent<AvatarLoader>().model.FixModelCollidersPivotIssue();
        LoadLocalAvatar();
        SetStatistics();
    }

    // Update is called once per frame
    void Update() {

    }

    public void ActivateVisTouch()
    {
        model.GetComponent<ModelColliders>().DisableAll();
        _visTouchActive = !_visTouchActive;
        bodyPartsButtons.SetActive(_visTouchActive);
        therapistController.SetActive(_visTouchActive);
    }

    public override void WeightChanged(Slider s) 
    {
        base.WeightChanged(s);
        Debug.Log("overriding weight function. We need to set the weight factor and paso for the light zones of interest");
        this.GetComponent<LightZonesOfInterest>().SetWeightFactor(s.value/100);
    }
     
    public void DeactivateMirror()
    {
        if (mirror.activeSelf) mirror.SetActive(false);
        else mirror.SetActive(true);
    }

    private void FadeToWhite(float time)
    {
        Color newColor = new Color(0.1f, 0.1f, 0.1f, 1f);
        //set start color
        SteamVR_Fade.Start(Color.clear, 0f);
        //set and start fade to
        SteamVR_Fade.Start(newColor, time);
        faded = true;
    }

    private void FadeFromWhite(float time)
    {
        Color newColor = new Color(0.1f,0.1f,0.1f,1f);
        //set start color
        SteamVR_Fade.Start(newColor, 0f);
        //set and start fade to
        SteamVR_Fade.Start(Color.clear,time);
        faded = false;
    }

    public void switchFade()
    {
        if (!faded) FadeToWhite(_fadeDuration);
        else FadeFromWhite(_fadeDuration);
    }

    public void LoadLocalAvatar()
    {
        int i = 0;
        if (int.TryParse(InGameData.PacientId, out i))
        {
            this.GetComponent<AvatarLoader>().LoadAvatar(InGameData.PacientId);
            float imc = 0;
            float h = 0;
            if (this.GetComponent<AvatarLoader>().model.TryInterpolateIMC(ref imc, ref h))
            {
                Debug.Log("imc: " + imc + " h: " + h);
                InterpolateIMC(imc + InGameData.ImcIncrement, h, weightSlider);
            }
        }
    }

    void SetStatistics()
    {
        List<string> d = new List<string>();
        GetComponent<AvatarLoader>().model.GetData(ref d);
        string newStatistics = ("ID: " + InGameData.PacientId + "\n" +
                                "IMC base: " + d[0] + "\n" +
                                "IMC incremented " + d[1] + "\n" +
                                "height: " + d[2]+ "\n" +
                                "session: "+ d[3]);
        statistics.text = newStatistics;
    }
}
