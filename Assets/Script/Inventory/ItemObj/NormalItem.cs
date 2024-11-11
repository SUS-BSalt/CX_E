using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class NormalItem : ItemBase
{
    public override int ItemID { get; set; }

    public override bool AreTheySame(ItemBase _otherItem)
    {
        return _otherItem.ItemID == this.ItemID;
    }

    public override ItemBase GetADeepCopy()
    {
        return ItemFactory.CreateItem(ItemID, GetProfileJson());
    }

    public override GameObject GetInstance(string _Profile)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefab/Inventory/ItemOBJ/物品图标");
        GameObject instance = Object.Instantiate(prefab);

        string filePath = Path.Combine(Application.streamingAssetsPath, ICON_PATH);
        byte[] imageData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2); // 临时大小，稍后会被覆盖
        texture.LoadImage(imageData);
        texture.Apply();
        instance.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        return instance;
    }

    public override string GetProfileJson()
    {
        return "{}";
    }

    public override void SetProfileFromJson(string _JsonString)
    {
        
    }

}
