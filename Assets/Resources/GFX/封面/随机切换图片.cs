using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 随机切换图片 : MonoBehaviour
{
    public List<Sprite> sprites;
    public int currentSpriteIndex;
    public SpriteRenderer target;
    public float timeCycle;
    private float timeCount;
    // Update is called once per frame
    private void Start()
    {
        target.sprite = sprites[0];
        currentSpriteIndex = 0;
    }
    void Update()
    {
        timeCount += Time.deltaTime;
        if (timeCount > timeCycle)
        {
            timeCount -= timeCycle;
            currentSpriteIndex += Random.Range(1, sprites.Count - 2);
            if(currentSpriteIndex>= sprites.Count)
            {
                currentSpriteIndex -= sprites.Count;
            }
            target.sprite = sprites[currentSpriteIndex];
        }
    }
}
