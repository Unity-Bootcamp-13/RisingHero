public class GachaWeightEntry : ICSVParsable
{
    public int groupId;
    public int itemId;
    public int weight;

    public void Parse(string[] values)
    {
        groupId = int.Parse(values[0]);
        itemId = int.Parse(values[1]);
        weight = int.Parse(values[2]);
    }
}