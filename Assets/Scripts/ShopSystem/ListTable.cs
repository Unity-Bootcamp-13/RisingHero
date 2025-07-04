using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ListTableData : ICSVLoadable
{
    public int ListId { get; private set; }
    public int GroupId { get; private set; }
    public string RequiredItem { get; private set; }
    public int RequiredCost { get; private set; }
    public int RewardAmount { get; private set; }

    public void LoadFromDictionary(Dictionary<string, object> dict)
    {
        try
        {
            ListId = Convert.ToInt32(dict["list_id"]);
            GroupId = Convert.ToInt32(dict["group_id"]);
            RequiredItem = dict["required_item"].ToString();
            RequiredCost = Convert.ToInt32(dict["required_cost"]);
            RewardAmount = Convert.ToInt32(dict["reward_amount"]);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError($"[ListTable] LoadFromDictionary 실패: {e.Message}");
        }
    }

    public static List<ListTableData> LoadFromCSV(string resourcePath)
    {
        return CSVLoader.Load<ListTableData>(resourcePath);
    }
}

public class ListTable : MonoBehaviour
{
    private void Start()
    {
        string resourcePath = "CSV/List_Table";

        List<ListTableData> listtabledata = ListTableData.LoadFromCSV(resourcePath);

        foreach (ListTableData item in listtabledata)
        {
           Debug.Log($"ListId: {item.ListId}, GroupId: {item.GroupId}, RequiredItem: {item.RequiredItem}, RequiredCost: {item.RequiredCost}, RewardAmount: {item.RewardAmount}");
        }
    }
}