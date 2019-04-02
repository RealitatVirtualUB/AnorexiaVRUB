using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour {

    public GameObject questionPanel;
    public List<string> Questions;
    public ControllerChecker playerController;
    public bool passControlled = false;
    public float timeBtwQuestions = 60.0f;
    public GameObject automataMenu;
    public GameObject controlMenu;
    private float timer;
    private bool questionActivated = false;
    private int questionCounter = 0;

	// Use this for initialization
	void Awake () {
        if (questionPanel != null) questionPanel.SetActive(false);
        else Debug.Log("question panel isn't setted");
        if (passControlled) ChangeControlSystem();

    }
	
	// Update is called once per frame
	void Update () {
        if (!passControlled && !questionActivated)
        {
            timer += Time.deltaTime;
            if(timer > timeBtwQuestions)
            {
                timer = 0;
                ChargeNextQuestion(false);
            }
        }
	}

    public void ChargeNextQuestion(bool firstOrLast)
    {
        if (questionActivated) return;
        questionPanel.SetActive(true);
        //if (Questions.Count != 0 && !firstOrLast) questionPanel.GetComponent<QuestionPanel>().SetQuestion(Questions[questionCounter % (Questions.Count)]);
        if (Questions.Count != 0 && !firstOrLast) questionPanel.GetComponent<QuestionPanel>().SetQuestion(Questions[1]);
        else if(Questions.Count != 0 && firstOrLast) questionPanel.GetComponent<QuestionPanel>().SetQuestion(Questions[0]);
        else Debug.Log("Any question still setted");
        questionPanel.GetComponent<QuestionPanel>().ResetStressBarValue();
        playerController.inputActivated = true;
        questionActivated = true;
        questionCounter++;
    }

    public bool OnQuestionActivate(){ return questionActivated; }

    public void EndQuestion()
    {
        //this.GetComponent<LightZonesOfInterest>().ChangeIntensitySlider(questionPanel.GetComponent<QuestionPanel>().stressBar.value/100);
        this.GetComponent<CsvReadWrite>().AddData(questionPanel.GetComponent<QuestionPanel>().stressBar.value);
        this.GetComponent<LightZonesOfInterest>().ChangeObjectiveIntensity(questionPanel.GetComponent<QuestionPanel>().stressBar.value / 100);
        questionPanel.SetActive(false);
        questionActivated = false;
        playerController.inputActivated = false;
    }

    public void ChangeControlSystem()
    {
        if (automataMenu.activeSelf)
        {
            automataMenu.SetActive(false);
            controlMenu.SetActive(true);
            passControlled = true;
            timer = 0;
        }
        else
        {
            automataMenu.SetActive(true);
            controlMenu.SetActive(false);
            passControlled = false;
        }
    }

    public void SetAutomataTimeBtwQuestions(Text text)
    {
        float.TryParse(text.text,out timeBtwQuestions);
        Debug.Log("time btw questions are seted");
    }
}
