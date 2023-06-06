using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotAndShare : MonoBehaviour
{
    [SerializeField] private GameObject UIScreenshot;
    [SerializeField] private GameObject UIPreview;

    private Texture2D screenshot;
    private Sprite screenshotSprite;
    private Rect screenRect;

    void Start()
    {
        screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
    }

    private IEnumerator Screenshoot()
    {
        yield return new WaitForEndOfFrame();
        
        // Take Screenshot
        screenshot.ReadPixels(screenRect, 0, 0);
        screenshot.Apply();

        // Set filename
        string name = "Aruvana_Exercise" + System.DateTime.Now.ToString("dd-mm-yyyy_HH-mm-ss");

        // Save to Android
        NativeGallery.SaveImageToGallery(screenshot, "Aruvana_Exercise_Screenshots", name);

        // Change UIPreview Image to Screenshot
        screenshotSprite = Sprite.Create(screenshot, screenRect, Vector2.zero);
        UIPreview.GetComponentInChildren<Image>().sprite = screenshotSprite;

        // Display Preview
        UIPreview.SetActive(true);
    }

    private IEnumerator ShareImage()
    {
        yield return new WaitForEndOfFrame();

        // Share Image
        new NativeShare().AddFile(screenshot).Share();
    
        UIPreview.SetActive(false);
        UIScreenshot.SetActive(true);
    }

    public void TakeScreenshot()
    {
        // Make sure canvas are all off
        UIScreenshot.SetActive(false);
        UIPreview.SetActive(false);

        StartCoroutine(Screenshoot());
    }

    public void ShareButtonPressed()
    {
        StartCoroutine(ShareImage());
    }

}
