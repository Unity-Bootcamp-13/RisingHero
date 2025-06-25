
/// <summary>
/// 모든 스킬이 반드시 가져야 할 이름, 쿨타임, 발동 및 레벨업 기능을 정의한 통합 인터페이스입니다.
/// </summary>
public interface ISkill : IActivatableSkill, IUpgradableSkill, IHasCooldown
{
    string SkillName { get; }
}

/// <summary>
/// 직접 발동이 가능한 액티브 스킬을 정의합니다.
/// </summary>
public interface IActivatableSkill
{
    void Activate();
}

/// <summary>
/// 레벨 업 기능을 갖는 스킬에 대한 인터페이스입니다.
/// </summary>
public interface IUpgradableSkill
{
    void Upgrade();
}

/// <summary>
/// 쿨타임 속성과 쿨타임 갱신 메서드를 정의하는 인터페이스입니다.
/// </summary>
public interface IHasCooldown
{
    float Cooldown { get; }
    void UpdateCooldown(float deltaTime);
}