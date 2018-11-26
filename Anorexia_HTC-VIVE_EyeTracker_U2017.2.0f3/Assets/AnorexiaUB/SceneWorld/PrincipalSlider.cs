using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrincipalSlider : MonoBehaviour {
    public RigController rigController;
    public RigController mirrorRigController;
    
    public Slider slider;
    public InputField percentageField;


    public void setRigControllers(RigController rigController, RigController mirrorRigController)
    {
        this.rigController = rigController;
        this.mirrorRigController = mirrorRigController;
    }

    public void sliderChanged()
    {
        if (null != this.rigController.meshRenderer)
        {
            float v = this.rigController.riggedArmature.startBlendShape + (100f - this.rigController.riggedArmature.startBlendShape) * this.slider.value;
            this.rigController.meshRenderer.SetBlendShapeWeight(0, v);
        }

        if (null != this.mirrorRigController)
        {
            float v = this.rigController.riggedArmature.startBlendShape + (100f - this.rigController.riggedArmature.startBlendShape) * this.slider.value;

            this.mirrorRigController.meshRenderer.SetBlendShapeWeight(0, v);
        }

        this.percentageField.text = (100f * this.slider.value).ToString("0.0");
    }

    public void inputEdited()
    {
        try
        {
            this.slider.value = float.Parse(this.percentageField.text) / 100f;
            this.sliderChanged();
        } catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
