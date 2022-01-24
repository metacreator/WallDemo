using DG.Tweening;
using UnityEngine;

public class JackHammerAnimations : MonoBehaviour
{
    [Header("Shake Animation")] 
    [SerializeField] private float shakeStrength;
    [SerializeField] private float shakeDuration;
    [SerializeField] private int shakeRandomness;

    private Tweener currentTweener;

    public void StartShaking()
    {
        currentTweener = transform.DOShakeRotation(shakeDuration, shakeStrength, shakeRandomness, fadeOut: false).SetLoops(-1);
        currentTweener.Play();
    }

    public void StopShaking() => currentTweener.Kill();
}