using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public AnorexiaUB anorexiaUB;
    public PupilControls pupilControls;
    public GameObject therapystView;

    public void connectParticipant(bool male)  {
        //this.anorexiaUB.connectParticipant(male);
    }

    public void pupilDisconnection() {
        this.pupilControls.pupilDisconnection();
    }

    public void pupilConnection() {
        this.pupilControls.pupilConnection();
    }

    public void pupilTerminated() {
        this.pupilControls.pupilTerminated();
    }

    public void resetCamera() {
        //this.anorexiaUB.resetCamera();
    }

    public void pupilCalibrationStarted() {
        this.therapystView.SetActive(false);
        this.anorexiaUB.pupilCalibrationStarted();
    }

    public void pupilCalibrationTerminated() {
        this.therapystView.SetActive(true);
    }

    public void recordPupilData() {
        this.anorexiaUB.recordPupilData();
    }

    public void stopRecordPupilData() {
        this.anorexiaUB.stopRecordPupilData();
    }

    public void toggleReference() {
        this.anorexiaUB.toggleReference();
    }

    public void toggleReference_2()
    {
        this.anorexiaUB.toggleReference_2();
    }
}
