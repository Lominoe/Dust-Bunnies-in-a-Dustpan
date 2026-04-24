using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Serialization;
using UnityEngine.VFX;


[System.Serializable]
public class ImpactFrame
{
    [Header("Particles To Play")]
    public VisualEffect effect;
    public ParticleSystem system;
    
    [Header("Actors")]
    public bool enableCharacters;
    public Animator animation;
    public GameObject toggleOff;
    public bool toggleGameObj;
    
    [Header("Duration")]
    public float frameTime;
    
    [Header("Impact Frames")]
    public bool disableImpactCam = false;
}

public class ImpactFrameController : MonoBehaviour
{
    [FormerlySerializedAs("frameTime")] [SerializeField] private List<ImpactFrame> frames;
    [SerializeField] private Material material;
    [SerializeField] private float animationSpeed;

    private bool _busy = false;
    
    private Camera _camera;
    
    public void Initiate() {
        if (_busy) return;
        _camera = ImpactCamera.Instance.ThisCamera;
        _camera.depth = -10;
        StartCoroutine(Frame());
    }

    private IEnumerator Frame()
    {
        _busy = true;

        bool isBlackFrame = false;

        foreach (ImpactFrame frame in frames)
        {
            ApplyCameraFrame(frame, isBlackFrame);
            ApplyToggle(frame);
            PlayParticleSystem(frame);
            PlayVisualEffect(frame, isBlackFrame);
            RestartAnimation(frame);

            yield return new WaitForSeconds(frame.frameTime);

            isBlackFrame = !isBlackFrame;
        }

        _busy = false;
        DisableImpactCamera();
    }

    private void ApplyCameraFrame(ImpactFrame frame, bool isBlackFrame)
    {
        if (frame.disableImpactCam)
        {
            DisableImpactCamera();
            return;
        }

        _camera.depth = 10;

        Color background = isBlackFrame ? Color.black : Color.white;
        Color foreground = isBlackFrame ? Color.white : Color.black;

        _camera.backgroundColor = background;

        if (!frame.enableCharacters)
            foreground = background;

        material.SetColor("_Color", foreground);
    }

    private void ApplyToggle(ImpactFrame frame)
    {
        if (frame.toggleOff == null)
            return;

        frame.toggleOff.SetActive(frame.toggleGameObj);
    }

    private void PlayParticleSystem(ImpactFrame frame)
    {
        if (frame.system == null)
            return;

        frame.system.Play();
    }

    private void PlayVisualEffect(ImpactFrame frame, bool isBlackFrame)
    {
        if (frame.effect == null)
            return;

        frame.effect.gameObject.SetActive(true);
        frame.effect.SetVector4("Color", isBlackFrame ? Color.white : Color.black);
        frame.effect.Play();
    }

    private void RestartAnimation(ImpactFrame frame)
    {
        if (frame.animation == null)
            return;

        frame.animation.speed = animationSpeed;
        frame.animation.Rebind();
    }

    private void DisableImpactCamera()
    {
        _camera.depth = -10;
    }
}
