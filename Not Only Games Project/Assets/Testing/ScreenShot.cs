using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{

    [SerializeField] private SpriteRenderer m_renderer;
    [SerializeField] private RectInt m_section;

    private Texture2D m_photo;
    private int vAux_num = 0;
    private bool vAux_photoReady = false;

    public void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
            F_TakePhoto();

        if (vAux_photoReady)
            F_ShowPhoto();
    }

    private void F_ShowPhoto()
    {
        m_renderer.sprite = Sprite.Create(m_photo, new Rect(0, 0, m_photo.width, m_photo.height), new Vector2(0.5f, 0.5f));
        vAux_photoReady = false;
    }

    public void F_TakePhoto()
    {
        StartCoroutine(Coroutine_CaptureScreenShot());

        //Texture2D l_photo = ScreenCapture.CaptureScreenshotAsTexture();
        //m_renderer.sprite = Sprite.Create(l_photo, new Rect(0, 0, l_photo.width, l_photo.height), new Vector2(0.5f, 0.5f));
    }

    IEnumerator Coroutine_CaptureScreenShot()
    {
        yield return new WaitForEndOfFrame();

        string l_path = Application.persistentDataPath + "/Assets/Screenshots"
                + "_" + "_" + Screen.width + "X" + Screen.height + "" + ".png";

        Vector2 pos = new Vector2(m_section.x, m_section.y);
        Vector2 size = Camera.main.WorldToScreenPoint(new Vector2(m_section.width, m_section.height));
        Rect l_section = new Rect(pos, size);
        print(pos + ", " + size);

        m_photo = new Texture2D((int)l_section.width, (int)l_section.height);
        //Get Image from screen
        m_photo.ReadPixels(new Rect(l_section.x, l_section.y, l_section.width, l_section.height), 0, 0);
        m_photo.Apply();
        //Convert to png
        byte[] l_imageBytes = m_photo.EncodeToPNG();

        //Save image to file
        System.IO.File.WriteAllBytes("prueba" + vAux_num + ".png", l_imageBytes);
        vAux_num++;

        vAux_photoReady = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(m_section.x, m_section.y), new Vector3(m_section.width, m_section.height));
    }
}