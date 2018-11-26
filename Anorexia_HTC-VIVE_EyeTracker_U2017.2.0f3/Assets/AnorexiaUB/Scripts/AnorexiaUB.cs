using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnorexiaUB : MonoBehaviour {
    public MainMenu mainMenu;
    public SceneWorld sceneWorld;

    public void pupilDisconnection()
    {
        this.mainMenu.pupilDisconnection();        
    }

    public void pupilConnection()
    {
        this.mainMenu.pupilConnection();
    }

    public void pupilTerminated()
    {
        this.mainMenu.pupilTerminated();
    }

    public void pupilCalibrationStarted()
    {
        this.sceneWorld.pupilCalibrationStarted();
    }

    public void pupilCalibrationTerminated()
    {
        this.mainMenu.pupilCalibrationTerminated();
        this.sceneWorld.pupilCalibrationTerminated();
    }

    public void recordPupilData()
    {
        this.sceneWorld.recordPupilData();
    }

    public void stopRecordPupilData()
    {
        this.sceneWorld.stopRecordPupilData();
    }

    public void toggleReference()
    {
        this.sceneWorld.toggleReference();
    }

    public void toggleReference_2()
    {
        this.sceneWorld.toggleReference_2();
    }
}
