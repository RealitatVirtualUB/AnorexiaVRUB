using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyTrackingScene : MainScene {

    public GameObject bodyPartsButtons;
    private bool _visTouchActive;
    public GameObject therapistController;
    public GameObject mirror;
    public Slider weightSlider;
    // Use this for initialization
    void Start() {
        PrintInGameValues();
        if (InGameData.PacientId != "")
        {
            this.GetComponent<AvatarLoader>().LoadAvatar(InGameData.PacientId);
            float imc = 0;
            float h = 0;
            if (this.GetComponent<AvatarLoader>().model.TryInterpolateIMC(ref imc, ref h))
            {
                InterpolateIMC(imc + InGameData.ImcIncrement, h, weightSlider);
            }
        }
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
}
