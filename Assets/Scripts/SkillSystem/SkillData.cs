public class SkillData
{
    // 각 스킬의 기본 데이터(고유 정보) 정의
    public int ID;
    public string Name;
    public SkillType Type;
    public int Power;
    public float Range;
    public string PrefabName;
    public float Duration;
    public float TickInterval;  // 피해 주기 (오라, 범위형 스킬에서 사용 예정)
    public float CooldownTime;
}