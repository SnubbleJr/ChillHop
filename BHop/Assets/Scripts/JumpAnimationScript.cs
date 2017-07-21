using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAnimationScript : MonoBehaviour
{
    public Vector2 offsetPos = Vector3.zero;
    public float lerpSpeed;

    public AnimationCurve curve;

    private Vector2 destinationPos;

    private RectTransform rect;
    private bool animating = false;

    private float t = 0;

    void OnEnable()
    {
        BHopBehaviour.playerJumped += startAnimation;
    }

    void OnDisable()
    {
        BHopBehaviour.playerJumped -= startAnimation;
    }

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        destinationPos = rect.anchoredPosition;
    }

    public void startAnimation()
    {
        animating = true;
        t = 0;
    }

    public void stopAnimation()
    {
        animating = false;
    }

    void Update()
    {
        t += Time.deltaTime;

        if (animating)
        {
            float curveValue = curve.Evaluate(t * lerpSpeed);

            rect.anchoredPosition = Vector2.Lerp((destinationPos + offsetPos), destinationPos, curveValue);
            if (curveValue >= 1f)
                stopAnimation();
        }
    }
}
