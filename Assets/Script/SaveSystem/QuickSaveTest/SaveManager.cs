using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// 管理本地文件到游戏内的数据交换。
/// 模块间的数据交换绝对不能用这里的Reader。
/// 每个模块自行编写保存时与加载时的方法，并分别挂在LoadEvent与SaveEvent上。
/// 获取数据可以用LoadData，存入数据可以用SaveData。
/// 
/// 使用了QuickSave插件，它的思路是在读取时创建一个Reader，在写入时创建一个Writer，它俩在内存里的指向*不一样！*。
/// 也就是说，如果你同时创建了一个Reader与一个Writer，在修改了数据并用Writer执行写入后，再使用Reader会发现，读取的依然是旧数据。
/// 所以每当你执行了一次写入，你就需要重新创建一次Reader。
/// 经过我初步考虑后（1.13.2024），决定让Reader常驻，而令Writter作为临时变量，仅在需要保存时创建，并在写入后脏标记Reader。
/// 当使用Reader时，首先检查脏标记，若为脏，则重新加载一遍文件，干净的话便可随意使用。
/// </summary>
public class SaveManager : Singleton<SaveManager>
{
    public UnityEvent LoadEvent;
    public UnityEvent SaveEvent;

    [SerializeField]
    private string currentSaveFileName;

    /// <summary>
    /// 增强鲁棒性设计
    /// </summary>
    [SerializeField]
    private bool isReaderDirty;
    QuickSaveReader reader { get; set; }
    QuickSaveWriter writer { get; set; }

    public static readonly string NODATA = "NoData";

    /// <summary>
    /// 因为这个类基于QuickSave插件搭建，所以方法也是基于其设计进行的拓展
    /// 每当本地的存档文件发生变动时，应当重新调用这个方法
    /// </summary>
    /// <param name="fileName">存档文件的名字，不用加后缀</param>
    public void LoadFromFile(string fileName)
    {
        currentSaveFileName = fileName;
        reader = QuickSaveReader.Create(fileName);
        LoadEvent?.Invoke();
        isReaderDirty = false;
    }


    /// <summary>
    /// 从存档中取得数据，模块间的数据交换绝对不要用这个！
    /// </summary>
    /// <typeparam name="T">数据类型，可以是任何能序列化的玩意</typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception">如果没能加载到数据，会抛出异常</exception>
    public T LoadData<T>(string key)
    {
        T data;
        if (isReaderDirty)
        {
            LoadFromFile(currentSaveFileName);
        }

        if (reader.TryRead<T>(key,out data))
        {
            return data;
        }
        throw new System.Exception(NODATA);
    }

    public void testry()
    {
        try
        {
            LoadData<int>("ss");
        }
        catch (System.Exception e)
        {
            print(e.Message);
        }
    }

    public void GetAllKeys()
    {
        reader.GetAllKeys();
    }

    public void SaveToFile(string fileName)
    {
        currentSaveFileName = fileName;
        writer = QuickSaveWriter.Create(fileName);
        SaveEvent?.Invoke();
        writer.Commit();
        isReaderDirty = true;
    }

    /// <summary>
    /// 更新数据，当被
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">键</param>
    /// <param name="data">任何可以序列化的东西</param>
    public void SaveData<T>(string key, T data)
    {
        writer.Write<T>(key, data);
        isReaderDirty = true;
    }
}
