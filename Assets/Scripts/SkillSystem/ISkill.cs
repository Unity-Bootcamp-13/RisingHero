
/// <summary>
/// ��� ��ų�� �ݵ�� ������ �� �̸�, ��Ÿ��, �ߵ� �� ������ ����� ������ ���� �������̽��Դϴ�.
/// </summary>
public interface ISkill : IActivatableSkill, IUpgradableSkill, IHasCooldown
{
    string SkillName { get; }
}

/// <summary>
/// ���� �ߵ��� ������ ��Ƽ�� ��ų�� �����մϴ�.
/// </summary>
public interface IActivatableSkill
{
    void Activate();
}

/// <summary>
/// ���� �� ����� ���� ��ų�� ���� �������̽��Դϴ�.
/// </summary>
public interface IUpgradableSkill
{
    void Upgrade();
}

/// <summary>
/// ��Ÿ�� �Ӽ��� ��Ÿ�� ���� �޼��带 �����ϴ� �������̽��Դϴ�.
/// </summary>
public interface IHasCooldown
{
    float Cooldown { get; }
    void UpdateCooldown(float deltaTime);
}