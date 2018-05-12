using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour {

    Texture foto;
    int num = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0))
        {
            num++;
            StartCoroutine(captureScreenshot());

        }

    }


    IEnumerator RecordFrame()
    {

        yield return new WaitForEndOfFrame();
        var texture = ScreenCapture.CaptureScreenshotAsTexture();
        // do something with texture
        foto = texture;

        // cleanup
        Object.Destroy(texture);
    }



    IEnumerator captureScreenshot()
    {
        yield return new WaitForEndOfFrame();

        string path = Application.persistentDataPath + "/Assets/Screenshots"
                + "_" + "_" + Screen.width + "X" + Screen.height + "" + ".png";

        Texture2D screenImage = new Texture2D(Screen.width/2, Screen.height/2);
        //Get Image from screen
        screenImage.ReadPixels(new Rect(0, 0, Screen.width/2, Screen.height/2), 0, 0);
        screenImage.Apply();
        //Convert to png
        byte[] imageBytes = screenImage.EncodeToPNG();

        //Save image to file
        System.IO.File.WriteAllBytes("prueba" + num + ".png", imageBytes);
    }


        public void LateUpdate()
    {
        StartCoroutine(RecordFrame());
        //StartCoroutine(captureScreenshot());

    }

    private void OnGUI()
    {
        if(foto != null)
        {

                float width = 600;
                float height = 600;
                GUI.DrawTexture(new Rect((Screen.width / 2) - (width / 2), (Screen.height / 2) - (height / 2), width, height), foto);
                //GUI.DrawTexture(new Rect(10, 10, 60, 60), foto, ScaleMode.ScaleToFit, true, 10.0F);

            //if (Event.current.type.Equals(EventType.Repaint))
              //  Graphics.DrawTexture(new Rect(10, 10, 100, 100), foto);


        }
    }
}
