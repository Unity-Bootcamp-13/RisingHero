using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        var listTable = ListTableData.LoadFromCSV("CSV/List_Table");
        var itemList = ListItemData.LoadFromCSV("CSV/List_Item");

        listTableMap = new();
        foreach (var entry in listTable)
            listTableMap[entry.ListId] = entry;

        itemGroupMap = new();
        foreach (var item in itemList)
        {
            if (!itemGroupMap.ContainsKey(item.GroupId))
                itemGroupMap[item.GroupId] = new();
            itemGroupMap[item.GroupId].Add(item);
        }
    }

    /// <summary>
    /// ListId에 해당하는 가챠 설정 데이터를 반환. 없으면 예외 발생.
    /// </summary>
    public ListTableData GetListTable(int listId)
    {
        if (!listTableMap.TryGetValue(listId, out var data))
            throw new KeyNotFoundException($"ListId {listId}에 해당하는 ListTableData가 없습니다.");
        return data;
    }

    /// <summary>
    /// 특정 그룹 ID에 해당하는 ListItemData 목록을 반환. 없으면 예외 발생.
    /// </summary>
    public List<ListItemData> GetItemsForGroup(int groupId)
    {
        if (!itemGroupMap.TryGetValue(groupId, out var items))
            throw new KeyNotFoundException($"GroupId {groupId}에 해당하는 ListItemData가 없습니다.");

        return items;
    }
}

