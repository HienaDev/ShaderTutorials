using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class RendererFeatureToggle : MonoBehaviour
{
    [Header("Main Renderer Features")]
    public ScriptableRendererFeature edgeDetection;
    public ScriptableRendererFeature normalColorDither;
    public ScriptableRendererFeature twoToneDither;
    public ScriptableRendererFeature twoToneDitherGreen;
    public ScriptableRendererFeature twoToneDitherYellow;

    [Header("Fullscreen Dither Features")]
    public ScriptableRendererFeature fullscreenDitherA;
    public ScriptableRendererFeature fullscreenDitherB;
    public ScriptableRendererFeature fullscreenDitherC;
    public ScriptableRendererFeature fullscreenDitherD;

    private List<ScriptableRendererFeature> allFeatures;

    void Awake()
    {
        allFeatures = new List<ScriptableRendererFeature>
        {
            edgeDetection,
            normalColorDither,
            twoToneDither,
            twoToneDitherGreen,
            twoToneDitherYellow,
            fullscreenDitherA,
            fullscreenDitherB,
            fullscreenDitherC,
            fullscreenDitherD
        };
    }

    void Update()
    {
        bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (Input.GetKeyDown(KeyCode.Alpha1)) HandleInput(0, shift);
        if (Input.GetKeyDown(KeyCode.Alpha2)) HandleInput(1, shift);
        if (Input.GetKeyDown(KeyCode.Alpha3)) HandleInput(2, shift);
        if (Input.GetKeyDown(KeyCode.Alpha4)) HandleInput(3, shift);
        if (Input.GetKeyDown(KeyCode.Alpha5)) HandleInput(4, shift);
        if (Input.GetKeyDown(KeyCode.Alpha6)) HandleInput(5, shift);
        if (Input.GetKeyDown(KeyCode.Alpha7)) HandleInput(6, shift);
        if (Input.GetKeyDown(KeyCode.Alpha8)) HandleInput(7, shift);
        if (Input.GetKeyDown(KeyCode.Alpha9)) HandleInput(8, shift);
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (shift)
                EnableAll();
            else
                DisableAll();
        }
    }

    private void HandleInput(int index, bool shift)
    {
        if (index < 0 || index >= allFeatures.Count || allFeatures[index] == null)
            return;

        if (shift)
            EnableAdditive(allFeatures[index]);
        else
            EnableOnly(allFeatures[index]);
    }

    public void EnableOnly(ScriptableRendererFeature featureToEnable)
    {
        foreach (var feature in allFeatures)
        {
            if (feature != null)
                feature.SetActive(feature == featureToEnable);
        }
    }

    public void EnableAdditive(ScriptableRendererFeature featureToEnable)
    {
        if (featureToEnable != null)
            featureToEnable.SetActive(true);
    }

    public void DisableAll()
    {
        foreach (var feature in allFeatures)
        {
            if (feature != null)
                feature.SetActive(false);
        }
    }

    public void EnableAll()
    {
        foreach (var feature in allFeatures)
        {
            if (feature != null)
                feature.SetActive(true);
        }
    }
}
