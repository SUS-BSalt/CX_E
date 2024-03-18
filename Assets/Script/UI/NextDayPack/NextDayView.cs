using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextDayView : Singleton<NextDayView>
{
    public Text dateShow;
    public CanvasGroup dateShowG;
    private void OnEnable()
    {
        dateShowG.gameObject.SetActive(true);
        var sequence = DOTween.Sequence()
            .PrependInterval(1)
            .Append(dateShowG.DOFade(0f, 2f).SetEase(Ease.InOutQuad))
            .OnComplete(() => { dateShowG.alpha = 1; dateShowG.gameObject.SetActive(false); });
    }
}
