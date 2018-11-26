using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;

public class PupilControls : StatusControls {
    public InputField pupilPortField;
    public PupilGazeTracker pupilGazeTracker;
    public GameObject pupilConnectButton;
    public GameObject pupilDisconnectButton;
    public Button calibrateButton;
    public Button recordButton;
    public Button stopRecordButton;
    public ValidationResultImage validationResultImage;

    void Start() {
        if (PlayerPrefs.HasKey("PupilPort")) {
            this.pupilPortField.text = PlayerPrefs.GetString("PupilPort");
        }
    }

    void OnApplicationQuit() {
        this.disconnectPupilClicked();
    }
    
    public void startPupilClicked() {
#if UNITY_EDITOR
        string pupilPath = "..\\Anorexia_HTC-VIVE_MoCap_EyeTracker_windows_x64\\pupil_v096_windows_x64\\pupil_capture_v0.9.6\\pupil_capture.exe";
#else
        string pupilPath = "pupil_v096_windows_x64\\pupil_capture_v0.9.6\\pupil_capture.exe";
#endif
        if (null == this.process || this.process.HasExited)
        {
            try
            {
                this.process = System.Diagnostics.Process.Start(pupilPath);
                this.calibrateButton.enabled = true;
                this.recordButton.enabled = true;
            }
            catch (Exception e)
            {
                this.statusText.color = this.errorColor;
                this.statusText.text = "Can't instantiate:\nPupil Capture";
            }
        }
    }

    public void connectPupilClicked() {
        if (null != this.process && !this.process.HasExited) {
            string port = this.pupilPortField.text;
            if (!string.IsNullOrEmpty(port)) {
                PlayerPrefs.SetString("PupilPort", port);
                this.pupilGazeTracker.ServicePort = int.Parse(port);
                this.pupilGazeTracker.gameObject.SetActive(true);
            }
        } else {
            this.statusText.color = this.errorColor;
            this.statusText.text = "Pupil is not running ...";
        }
    }

    public void pupilDisconnection() {
        this.statusText.color = this.errorColor;
        this.statusText.text = "Can't connect to server:\n" + this.pupilGazeTracker.ServerIP + ":" + this.pupilGazeTracker.ServicePort + "\ndisconnected ...";
        this.pupilConnectButton.SetActive(true);
        this.pupilDisconnectButton.SetActive(false);
        this.pupilGazeTracker.gameObject.SetActive(false);
    }

    public void pupilConnection() {
        this.statusText.color = Color.white;
        this.statusText.text = "OK : Connected to:\n" + this.pupilGazeTracker.ServerIP + ":" + this.pupilGazeTracker.ServicePort;
        this.pupilConnectButton.SetActive(false);
        this.pupilDisconnectButton.SetActive(true);
    }

    public void disconnectPupilClicked() {
        this.pupilGazeTracker.disconnect();
        this.pupilGazeTracker.gameObject.SetActive(false);
    }

    public void pupilTerminated() {
        this.statusText.color = Color.white;
        this.statusText.text = "disconnected ...";

        this.pupilGazeTracker.gameObject.SetActive(false);
        this.pupilConnectButton.SetActive(true);
        this.pupilDisconnectButton.SetActive(false);
    }

    public void calibrateClicked() {
        this.mainMenu.pupilCalibrationStarted();
        this.pupilGazeTracker.StartCalibration();
    }

    public void validateClicked() {
        this.validationResultImage.gameObject.SetActive(true);

        this.mainMenu.pupilCalibrationStarted();
        PupilValidation pupilValidation = this.pupilGazeTracker.GetComponent<PupilValidation>();
        pupilValidation.validate();
    }

    public void recordClicked() {
#if UNITY_EDITOR
        // System.Diagnostics.Process.Start("del /Q ..\\Anorexia_HTC-VIVE_MoCap_EyeTracker_windows_x64\\ScreenshotFolder\\hghghg.txt");
        // System.Diagnostics.Process.Start("..\\Anorexia_HTC-VIVE_MoCap_EyeTracker_windows_x64\\CScript Record.vbs");
        // System.Diagnostics.Process.Start("del");

        try {
            Process myProcess = new Process();
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = "C:\\Windows\\system32\\cmd.exe";
            string path = "..\\Anorexia_HTC-VIVE_MoCap_EyeTracker_windows_x64\\Record.bat";
            myProcess.StartInfo.Arguments = "/c" + path;
            myProcess.EnableRaisingEvents = true;
            myProcess.Start();
            myProcess.WaitForExit();
            int ExitCode = myProcess.ExitCode;
            UnityEngine.Debug.Log("" + ExitCode);
        } catch (Exception e) {
            UnityEngine.Debug.LogError(e);
        }

#else
        // System.Diagnostics.Process.Start("del ScreenshotFolder\\*");
        System.Diagnostics.Process.Start("CScript Record.vbs");
         try {
            Process myProcess = new Process();
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = "C:\\Windows\\system32\\cmd.exe";
            string path = "Record.bat";
            myProcess.StartInfo.Arguments = "/c" + path;
            myProcess.EnableRaisingEvents = true;
            myProcess.Start();
            myProcess.WaitForExit();
            int ExitCode = myProcess.ExitCode;
            UnityEngine.Debug.Log("" + ExitCode);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e);
        }
#endif
    }

    public void recordDataClicked() {
        this.recordButton.gameObject.SetActive(false);
        this.stopRecordButton.gameObject.SetActive(true);
        this.mainMenu.recordPupilData();
    }

    public void stopRecordDataClicked() {
        this.recordButton.gameObject.SetActive(true);
        this.stopRecordButton.gameObject.SetActive(false);
        this.mainMenu.stopRecordPupilData();
    }

    public void tooglealidationImage() {
        this.validationResultImage.gameObject.SetActive(!this.validationResultImage.gameObject.activeSelf);
    }

    public void toggleReference() {
        this.mainMenu.toggleReference();
    }

    public void toggleReference_2()
    {
        this.mainMenu.toggleReference_2();
    }
}
