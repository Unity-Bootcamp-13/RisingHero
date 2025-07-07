using System;

public enum SkillType
{
    AOE,
    Aura
}

public class SkillData : ICSVParsable
{
    public int ID;
    public string Name;
    public SkillType Type;
    public int ManaCost;
    public float Range;
    public int Power;
    public float Duration;
    public int TickDamage;
    public int PowerPerLevel;
    public string PrefabName;
    public float Cooldown;

    public void Parse(string[] values)
    {
        ID = int.Parse(values[0]);
        Name = values[1];
        Type = Enum.Parse<SkillType>(values[2]);
        ManaCost = int.Parse(values[3]);
        Range = float.Parse(values[4]);
        Power = int.Parse(values[5]);
        Duration = float.Parse(values[6]);
        TickDamage = int.Parse(values[7]);
        PowerPerLevel = int.Parse(values[8]);
        PrefabName = values[9];
        Cooldown = float.Parse(values[10]);
    }

    public int GetPowerWithLevel(int level)
    {
        return Power + PowerPerLevel * (level - 1);
    }

    public int GetTickDamageWithLevel(int level)
    {
        return TickDamage; // 추후 TickDamagePerLevel 추가시 계산 가능
    }
}
