using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neo;
using System;

namespace Neo
{
    /// <summary>
    /// 
    /// </summary>
    public class NeoItemBase : ScriptableObject
    {
        public string prefabPath;

        public Type type = typeof(NeoItemBase);

        public bool IsUnique;//当它为真时，表明这个物品有额外的数据需要加载
                             //利用Unity自带的Resources.Load方法获取的是一个引用，在任何地方使用它都会引用到同一个实例，直接修改会造成全局的数据变化，而特殊物品意味着需要深度复制
                             //而当有些物品是独特的，比如有耐久度设定的武器，有创建时间标记的奖牌，这种东西时，加载出来的SO实际上是它的引用

        public string ItemDataJson;//所有所需要额外保存的数据全部转成json字符存进去，按照


        //以下的字段为便于本项目添加的字段
        public int value;

        public virtual GameObject GetInstance()//在数据实例化到仓库后，仓库的ui可以调用该方法实例出物品的图形元素
        {
            var prefab = Resources.Load<GameObject>(prefabPath);
            GameObject obj = Instantiate(prefab);
            return obj;
        }
        public virtual void OnSave()
        {
            //将追加的数据序列化成json后放进ItemAdditionalJsonData这里
        }
        public virtual void OnLoad()
        {

        }
    }
}
