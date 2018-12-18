using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionController : MonoBehaviour {

    public GameObject questionPanel;
    public List<string> Questions;
    public ControllerChecker playerController;
    public bool passControlled = true;
    public float timeBtwQuestions = 60.0f;

    private float timer;
    private bool questionActivated = false;
    private int questionCounter = 0;

	// Use this for initialization
	void Awake () {
        if (questionPanel != null) questionPanel.SetActive(false);
        else Debug.Log("question panel isn't setted");
	}
	
	// Update is called once per frame
	void Update () {
        if (!passControlled && !questionActivated)
        {
            timer += Time.deltaTime;
            if(timer > timeBtwQuestions)
            {
                timer = 0;
                ChargeNextQuestion();
            }
        }
	}

    public void ChargeNextQuestion()
    {
        questionPanel.SetActive(true);
        if (Questions.Count != 0) questionPanel.GetComponent<QuestionPanel>().SetQuestion(Questions[questionCounter % (Questions.Count - 1)]);
        else Debug.Log("Any question still setted");
        playerController.inputActivated = true;
        questionActivated = true;
        questionCounter++;
    }

    public bool OnQuestionActivate(){ return questionActivated; }

    public void EndQuestion()
    {
        this.GetComponent<LightZonesOfInterest>().ChangeIntensitySlider(questionPanel.GetComponent<QuestionPanel>().stressBar.value/100);
        questionPanel.SetActive(false);
        questionActivated = false;
        playerController.inputActivated = false;
    }
}
