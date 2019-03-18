using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour {

    public Model model;
    public bool activateSplashScreen;
    public GameObject splashImage;
    //public GameObject bodyPartsButtons;
    //private bool _visTouchActive;
    //public GameObject therapistController;
    //public Toggle touch;
    

    
	// Use this for initialization
	void Start () {
        ActivateSplashImage();
        PrintInGameValues();
    }

    void ActivateSplashImage(){
        if (activateSplashScreen) splashImage.SetActive(true); 
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ThinChanged(Slider s){
        model.avatarComponents.SetThin(s);
    }

    public void FatChanged(Slider s){
        model.avatarComponents.SetFat(s);
    }

    virtual public void WeightChanged(Slider s)
    {
        if (s.value >= 0) model.avatarComponents.SetWeightPorcentage(s.value, BlendShape.FAT);
        else model.avatarComponents.SetWeightPorcentage(-s.value, BlendShape.THIN);
    }

    public void InterpolateIMC(float imc, float height, Slider s, int age, int gender)
    {
        // de moment estem entrant directament el imc per o que sa d'entrar es la relacio de pes 
        float newW = imc * height * height;
        Debug.Log("imc is: " + imc + " weight is: " + newW + " height is: " + height + " age is: " + age + " and gender is: "+ gender);
        model.CalculateMaxMinWeightRelation(height, age, gender);
        //by imc
        //if(imc >= model.midIMC)
        //{
        //    if (imc > model.maxIMC) model.maxIMC = imc;
        //    float newValue = Mathf.InverseLerp(model.midIMC, model.maxIMC, imc);
        //    s.value = newValue * 100;
        //}
        //else
        //{
        //    if (imc < model.maxIMC) model.minIMC = imc;
        //    float newValue = Mathf.InverseLerp(model.midIMC, model.minIMC, imc);
        //    s.value = newValue * -100;
        //}

        //by weight 
        if (newW >= model.midW)
        {
            if (newW > model.maxW) model.maxW = newW;
            float newValue = Mathf.InverseLerp(model.midW, model.maxW, newW);
            s.value = (newValue * 100);
        }
        else
        {
            if (newW < model.minW) model.minW = newW;
            float newValue = Mathf.InverseLerp(model.midW, model.minW, newW);
            s.value = (newValue * -100);
        }
        Debug.Log("slider value is: " + s.value);
    }

    protected void PrintInGameValues()
    {
        Debug.Log("printing pacient values. id: " + InGameData.PacientId +
                " imc increment: " + InGameData.ImcIncrement + 
                " number of the session: " + InGameData.Sn);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ChargeDebreafingScene()
    {
        SceneManager.LoadScene(3);
    }

    public void ChargeAmputationScene()
    {
        SceneManager.LoadScene(4);
    }

}
