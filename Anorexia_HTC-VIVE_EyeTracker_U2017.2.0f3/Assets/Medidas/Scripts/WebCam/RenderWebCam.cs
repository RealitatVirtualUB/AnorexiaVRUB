using UnityEngine;
using System.Collections;

public class RenderWebCam : MonoBehaviour
{
    /*
    //Delete script
    private float initialScale;

    void Awake()
    {
        initialScale = transform.localScale.x;
    }

    public void setWebCam(string webCamName)
    {
        Renderer renderer = GetComponent<Renderer>(); 
        if (renderer.material.mainTexture is WebCamTexture)
        {
            WebCamTexture webCamTexture = (WebCamTexture)renderer.material.mainTexture;
            webCamTexture.Stop();
        }
              
        WebCamTexture webcamTexture = new WebCamTexture(webCamName);
        renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();
        
        Vector3 scale = this.transform.localScale;
        scale.x = this.initialScale * webcamTexture.width / webcamTexture.height;
        this.transform.localScale = scale;
    }

    public void togglePlay()
    {
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
    }
    */
}