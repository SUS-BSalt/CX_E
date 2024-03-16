using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlideMenu : MenuBase
{
    public RectTransform rectTransform;
    public float SlideDistanceX = 400;
    public float SlideDistanceY = 0;
    public float Duration = 1f;
    public Vector2 DefaultPosition;
    public bool toggleV;
    public bool EnterAnimePlaying;
    public bool ExitAnimePlaying;
    public override void OnEnter(MenuBase initiateMenu)
    {
        gameObject.SetActive(true);
        transform.DOMove(new Vector2(transform.position.x + SlideDistanceX, transform.position.y + SlideDistanceY), Duration).From();
    }
    public override void OnExit(MenuBase targetMenu)
    {
        transform.DOMove(new Vector2(transform.position.x + SlideDistanceX, transform.position.y + SlideDistanceY), Duration);
        gameObject.SetActive(false);
        targetMenu.OnEnter(this);
    }
    public override void OnToggleEnter()
    {
        toggleV = true;
        EnterAnimePlaying = true;
        //ExitAnimePlaying = false;
        gameObject.SetActive(toggleV);
        DOTween.Kill(this);
        var sequenct = DOTween.Sequence();
        sequenct.Append(rectTransform.DOMove(new Vector2(DefaultPosition.x, DefaultPosition.y), Duration).SetEase(Ease.InOutQuad));
        sequenct.OnComplete(() => { if (!ExitAnimePlaying) { gameObject.SetActive(toggleV); } EnterAnimePlaying = false; });
    }
    public override void OnToggleExit()
    {
        toggleV = false;
        //EnterAnimePlaying = false;
        ExitAnimePlaying = true;
        DOTween.Kill(this);
        var sequenct = DOTween.Sequence();
        sequenct.Append(rectTransform.DOMove(new Vector2(DefaultPosition.x + SlideDistanceX, DefaultPosition.y + SlideDistanceY), Duration).SetEase(Ease.InOutQuad));
        sequenct.OnComplete(() => { if (!EnterAnimePlaying) { gameObject.SetActive(toggleV); } ExitAnimePlaying = false; });

    }
 
    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        DefaultPosition = new(rectTransform.position.x, rectTransform.position.y);
        rectTransform.position = new(DefaultPosition.x + SlideDistanceX, DefaultPosition.y + SlideDistanceY);
    }

}
