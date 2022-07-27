using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Controls special effects (bloom and vignette) when player gets debuffed.
public class DebuffCameraEffectApplier : MonoBehaviour
{
    public PostProcessVolume volume;

    private Bloom _bloom;
    private Vignette _vignette;

    public float debuffTimer = 3.0f;

    private bool debuffed;
    public bool Debuffed
    { set { debuffed = value; } }
    
    void Start()
    {
        volume.profile.TryGetSettings(out _bloom);
        volume.profile.TryGetSettings(out _vignette);

        _bloom.intensity.value = 0;
        _vignette.intensity.value = 0;

        debuffed = false;
    }        

    void Update()
    {
        if (debuffed)
        {
            FadeIn();

            if (debuffTimer > 0)
            {
                debuffTimer -= Time.deltaTime;
            }
            else
            {
                debuffTimer = 0;
                debuffed = false;
            }
        }
        else
        {            
            FadeOut();
        }
    }

    public void FadeIn()
    {
        _bloom.intensity.value = Mathf.Lerp(_bloom.intensity.value, 2.18f, 2.75f * Time.deltaTime);
        _vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value, 0.476f, 2.75f * Time.deltaTime);
    }

    public void FadeOut()
    {
        _bloom.intensity.value = Mathf.Lerp(_bloom.intensity.value, 0f, 2.75f * Time.deltaTime);
        _vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value, 0f, 2.75f * Time.deltaTime);
    }
}
