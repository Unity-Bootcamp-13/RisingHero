using System;
using System.Collections.Generic;
using UnityEngine;

public class ListItemData : ICSVLoadable
{
    public int GroupId { get; private set; }
    public int ItemId { get; private set; }
    public int Weight { get; private set; }

    /*    public ListItemData(int groupId, int itemId, int weight)
        {
            GroupId = groupId;
            ItemId = itemId;
            Weight = weight;
        }*/

    // ICSVLoadable 구현
    public void LoadFromDictionary(Dictionary<string, object> dict)
    {
        try
        {
            GroupId = Convert.ToInt32(dict["group_id"]);
            ItemId = Convert.ToInt32(dict["item_id"]);
            Weight = Convert.ToInt32(dict["weight"]);
        }
        catch (Exception e)
        {
            Debug.LogError($"[ListItemData] LoadFromDictionary 실패: {e.Message}");
        }
    }

    // CSVLoader 호출을 위한 래퍼 메서드 (선택사항)
    public static List<ListItemData> LoadFromCSV(string resourcePath)
    {
        return CSVLoader.Load<ListItemData>(resourcePath);
    }
}


public class ListItem : MonoBehaviour
{
    private void Start()
    {
        string resourcePath = "CSV/List_Item";

        List<ListItemData> listItemdata = ListItemData.LoadFromCSV(resourcePath);

        foreach (ListItemData item in listItemdata)
        {
            Debug.Log($"ItemId: {item.ItemId}, GroupId: {item.GroupId}, Weight: {item.Weight}");
        }
    }
}
