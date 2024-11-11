using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FrameCounter : MonoBehaviour
{
    private float deltaTime = 0f;

    [SerializeField] private int size = 25;
    [SerializeField] private Color color = Color.red;
    [SerializeField] private TextMeshProUGUI frameText;
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        if (frameText != null)
            CheckFrame();
    }

    public void CheckFrame()
    {
        float ms = deltaTime * 1000f;
        float fps = 1.0f / deltaTime;
        frameText.text = string.Format("{0:0.} FPS ({1:0.0} ms)", fps, ms);
    }
    //private void OnGUI()
    //{
    //    GUIStyle style = new GUIStyle();

    //    Rect rect = new Rect(30, 30, Screen.width, Screen.height);
    //    style.alignment = TextAnchor.UpperLeft;
    //    style.fontSize = size;
    //    style.normal.textColor = color;

    //    float ms = deltaTime * 1000f;
    //    float fps = 1.0f / deltaTime;
    //    string text = string.Format("{0:0.} FPS ({1:0.0} ms)", fps, ms);

    //    GUI.Label(rect, text, style);
    //}
}