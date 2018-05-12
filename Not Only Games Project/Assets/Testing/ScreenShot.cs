﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    [SerializeField] private GameObject m_flash;
    [SerializeField] private float m_scaleSocialMediaPhoto;
    [SerializeField] private Transform m_socialMediaPhoto;
    [SerializeField] private float m_speed;

    [Space(10)]
    [SerializeField] private SpriteRenderer m_renderer;
    [SerializeField] private Transform m_renderCamera;
    [SerializeField] private RenderTexture m_renderTexture;

    public Vector2Int m_photoSize = Vector2Int.zero;

    private Texture2D m_photo;
    private Vector3 m_mousePos = Vector3.zero;

    private int vAux_photoNum = 0;
    private bool vAux_photoReady = false;
    private bool vAux_animation = false;
    private float vAux_normalScale;

    private void Start()
    {
        vAux_normalScale = m_renderer.transform.localScale.x;
    }

    private void Update()
    {
        F_MoveMouse();

        if (vAux_animation)
        {
            m_renderer.transform.position = Vector3.MoveTowards(m_renderer.transform.position, m_socialMediaPhoto.position, m_speed * Time.deltaTime);
            float l_scale = Mathf.Lerp(m_renderer.transform.localScale.x, m_scaleSocialMediaPhoto, m_speed * 0.75f * Time.deltaTime);
            m_renderer.transform.localScale = new Vector3(l_scale, l_scale, l_scale);

            if (Vector3.Distance(m_renderer.transform.position, m_socialMediaPhoto.position) == 0)
            {
                vAux_animation = false;
            }
        }
    }

    private void LateUpdate()
    {
        if (Input.GetButtonDown("TakePhoto") && !vAux_animation && !vAux_photoReady)
            F_TakePhoto();

        if (vAux_photoReady)
            F_ShowPhoto();
    }

    private Vector2 F_GetRenderCameraOrthograficSize()
    {
        Camera l_renderCamera = m_renderCamera.GetComponent<Camera>();
        float l_height = l_renderCamera.orthographicSize * 2;
        float l_width = l_height * l_renderCamera.aspect;

        return new Vector2(l_width, l_height);
    }

    private void F_MoveMouse()
    {
        float l_height = Camera.main.orthographicSize * 2;
        float l_width = l_height * Camera.main.aspect;
        m_mousePos = new Vector3(Camera.main.ScreenToViewportPoint(Input.mousePosition).x * l_width - l_width / 2, Camera.main.ScreenToViewportPoint(Input.mousePosition).y * l_height - l_height / 2, 0);
    }

    private void F_ShowPhoto()
    {
        m_renderer.enabled = true;
        m_renderer.sprite = Sprite.Create(m_photo, new Rect(0, 0, m_photo.width, m_photo.height), new Vector2(0.5f, 0.5f));
        vAux_photoReady = false;

        F_Flash();
        Invoke("F_Flash", 0.5f);
        Invoke("F_StartAnimation", 1.5f);
    }

    public void F_TakePhoto()
    {
        m_renderer.transform.localScale = new Vector3(vAux_normalScale, vAux_normalScale, vAux_normalScale);
        m_renderer.transform.position = new Vector3(0, 0, -2);
        m_renderer.enabled = false;
        m_renderCamera.position = new Vector3(m_mousePos.x, m_mousePos.y, m_renderCamera.position.z);

        StartCoroutine(Coroutine_CaptureScreenShot());
    }

    IEnumerator Coroutine_CaptureScreenShot()
    {
        yield return new WaitForEndOfFrame();

        string l_path = Application.persistentDataPath + "/Assets/Screenshots"
                + "_" + "_" + Screen.width + "X" + Screen.height + "" + ".png";

        m_photo = F_toTexture2D(m_renderTexture);

        //Convert to png
        byte[] l_imageBytes = m_photo.EncodeToPNG();

        //Save image to file
        System.IO.File.WriteAllBytes("prueba" + vAux_photoNum + ".png", l_imageBytes);
        vAux_photoNum++;

        vAux_photoReady = true;
    }

    private Texture2D F_toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(m_photoSize.x, m_photoSize.y, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    private void F_Flash()
    {
        bool l_active = m_flash.activeSelf;
        m_flash.SetActive(!l_active);
    }

    private void F_StartAnimation()
    {
        vAux_animation = true;
    }
}