using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpWebCam : MonoBehaviour {

    private float initialScale;

    void Awake()
    {
        this.initialScale = this.transform.localScale.x;
    }

    public void setWebCam(string webCamName)
    {

    }

    void FixedUpdate()
    {
        if (!this.geting)
        {
            this.StartCoroutine(this.getTexture());
        }
    }

    bool geting = false;
    IEnumerator getTexture()
    {
        this.geting = true;

        WWW www = new WWW("http://192.168.0.129:8080/shot.jpg?rnd=351385");

        // Wait for download to complete
        yield return www;

        // assign texture
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = www.texture;
        this.geting = false;
    }

    public void togglePlay()
    {
        /*
        Renderer renderer = GetComponent<Renderer>();
        if (renderer.material.mainTexture is WebCamTexture)
        {
            WebCamTexture webCamTexture = (WebCamTexture)renderer.material.mainTexture;
            if (webCamTexture.isPlaying)
            {
                webCamTexture.Pause();
            }
            else
            {
                webCamTexture.Play();
            }
        }
        */
    }
}
