using System;
using System.Collections.Generic;

public interface IGachaDataService
{
    ListTableData GetListTable(int listId);
    List<ListItemData> GetItemsForGroup(int groupId);
}

public class GachaDataService : IGachaDataService
{
    private Dictionary<int, ListTableData> listTableMap;
    private Dictionary<int, List<ListItemData>> itemGroupMap;

    public GachaDataService()
    {
        List<ListTableData> listTable = ListTableData.LoadFromCSV("CSV/List_Table");
        List<ListItemData> itemList = ListItemData.LoadFromCSV("CSV/List_Item");

        InitializeListTableMap(listTable);
        InitializeItemGroupMap(itemList);
    }

    /// <summary>
    /// listTable 데이터를 Dictionary에 매핑
    /// </summary>
    private void InitializeListTableMap(List<ListTableData> listTable)
    {
        listTableMap = new Dictionary<int, ListTableData>();

        foreach (ListTableData entry in listTable)
        {
            listTableMap[entry.ListId] = entry;
        }
    }

    /// <summary>
    /// itemList 데이터를 GroupId 기준으로 Dictionary에 분류
    /// </summary>
    private void InitializeItemGroupMap(List<ListItemData> itemList)
    {
        itemGroupMap = new Dictionary<int, List<ListItemData>>();

        foreach (ListItemData item in itemList)
        {
            if (!itemGroupMap.ContainsKey(item.GroupId))
            {
                itemGroupMap[item.GroupId] = new List<ListItemData>();
            }

            itemGroupMap[item.GroupId].Add(item);
        }
    }

    /// <summary>
    /// ListId에 해당하는 가챠 설정 데이터를 반환. 없으면 예외 발생.
    /// </summary>
    public ListTableData GetListTable(int listId)
    {
        if (!listTableMap.TryGetValue(listId, out ListTableData data))
        {
            throw new KeyNotFoundException($"ListId {listId}에 해당하는 ListTableData가 없습니다.");
        }

        return data;
    }

    /// <summary>
    /// 특정 그룹 ID에 해당하는 ListItemData 목록을 반환. 없으면 예외 발생.
    /// </summary>
    public List<ListItemData> GetItemsForGroup(int groupId)
    {
        if (!itemGroupMap.TryGetValue(groupId, out List<ListItemData> items))
        {
            throw new KeyNotFoundException($"GroupId {groupId}에 해당하는 ListItemData가 없습니다.");
        }

        return items;
    }
}