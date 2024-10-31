using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlideMenu : MenuBase
{
    
    private RectTransform rectTransform;
    public float SlideDistanceX = 400;
    public float SlideDistanceY = 0;
    public float Duration = 1f;
    public Vector2 DefaultPosition;
    public bool toggleV;
    public bool EnterAnimePlaying;
    public bool ExitAnimePlaying;

    public override void OnToggleEnter()
    {
        base.OnToggleEnter();
        toggleV = true;
        EnterAnimePlaying = true;
        //ExitAnimePlaying = false;
        gameObject.SetActive(toggleV);
        DOTween.Kill(this);
        ToSlidePos();
        var sequenct = DOTween.Sequence();
        sequenct.Append(rectTransform.DOLocalMove(new Vector2(DefaultPosition.x, DefaultPosition.y), Duration).SetEase(Ease.InOutQuad));
        sequenct.OnComplete(() => { if (!ExitAnimePlaying) { gameObject.SetActive(toggleV); } EnterAnimePlaying = false; });
    }
    public override void OnToggleExit()
    {
        base.OnToggleExit();
        toggleV = false;
        //EnterAnimePlaying = false;
        ExitAnimePlaying = true;
        DOTween.Kill(this);
        ToDefaultPos();
        var sequenct = DOTween.Sequence();
        sequenct.Append(rectTransform.DOLocalMove(new Vector2(DefaultPosition.x + SlideDistanceX, DefaultPosition.y + SlideDistanceY), Duration).SetEase(Ease.InOutQuad));
        sequenct.OnComplete(() => { if (!EnterAnimePlaying) { gameObject.SetActive(toggleV); } ExitAnimePlaying = false; });

    }
 
    public void Awake()
    {
        Init();
    }
    public void Init()
    {
        gameObject.SetActive(toggleV);
        rectTransform = GetComponent<RectTransform>();
        DefaultPosition = new(rectTransform.localPosition.x, rectTransform.localPosition.y);
    }
    public void ToDefaultPos()
    {
        rectTransform.localPosition = DefaultPosition;
    }
    public void ToSlidePos()
    {
        rectTransform.localPosition = new Vector3(DefaultPosition.x + SlideDistanceX, DefaultPosition.y + SlideDistanceY, rectTransform.localPosition.z);
    }
}
