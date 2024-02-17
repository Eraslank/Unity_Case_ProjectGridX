using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DoTweenExtensions
{
    public static Tween DOBounce(this Transform t, float duration = .1f)
    {
        return t.DOScale(1.3f, duration).OnComplete(() =>
        {
            t.DOScale(.8f, duration * .5f).OnComplete(() =>
            {
                t.DOScale(.9f, duration);
            });
        });
    }
}
