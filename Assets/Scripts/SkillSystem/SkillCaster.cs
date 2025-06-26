using UnityEngine;
using System.Collections.Generic;

public class SkillCaster : MonoBehaviour
{
    [SerializeField] private CharacterStats characterStats; // 시전자의 스탯 (MP, 공격력 etc)
    [SerializeField] private TargetManager targetManager;   // 현재 타겟 관리

    // 스킬별 쿨타임 관리 Dictionary
    private Dictionary<SkillDefinition, float> skillCooldowns = new Dictionary<SkillDefinition, float>();

    // 스킬별 레벨 관리 Dictionary
    private Dictionary<SkillDefinition, int> skillLevels = new Dictionary<SkillDefinition, int>();

    private SkillDefinition currentCastingSkill;            // 현재 시전 중인 스킬(시전 시간 있는 스킬 용도)
    private float currentCastTimer;

    public CharacterStats CharacterStats => characterStats;
    public TargetManager TargetManager => targetManager;

    private void Awake()
    {
        if (characterStats == null)
            characterStats = GetComponent<CharacterStats>();
        if (targetManager == null)
            targetManager = GetComponent<TargetManager>();

        // 초기 스킬 레벨 설정, 데이터 로드 감안해야 함.
    }

    // Update is called once per frame
    private void Update()
    {
        // 쿨타임 실시간 반영
        List<SkillDefinition> skillsToUpdate = new List<SkillDefinition>(skillCooldowns.Keys);

        foreach (var skillDef in skillsToUpdate)
        {
            if (skillCooldowns[skillDef] > 0)
            {
                skillCooldowns[skillDef] -= Time.deltaTime;

                if (skillCooldowns[skillDef] < 0)
                    skillCooldowns[skillDef] = 0;   // 쿨타임의 최솟값은 0으로 설정
                // 쿨다운 UI 애니메이션 로직 이벤트로 호출
            }
        }

        // 현재 시전 중인 스킬 처리
        if (currentCastingSkill != null)
        {
            currentCastTimer -= Time.deltaTime;

            if (currentCastTimer <= 0)
            {
                // 먼저 사용된 스킬 시전 완료 후 스킬 발동
                currentCastingSkill.ExecuteSkill(this, targetManager.GetCurrentTarget());
                currentCastingSkill = null;     // 시전 완료
            }
        }
    }

    /// <summary>
    /// 스킬 사용 요청
    /// </summary>
    /// <param name="skillToCast">사용할 스킬 정의 Scriptable Object</param>
    /// <param name="targetPosition">범위형 스킬일 경우 시전할 위치</param>
    public bool TryCastSkill(SkillDefinition skillToCast, Vector3? targetPosition = null)
    {
        if (skillToCast == null)
            return false;

        // 1. 현재 다른 스킬 시전 중인지 체크
        if (currentCastingSkill != null)
        {
            Debug.Log($"이미 {skillToCast.SkillName} 스킬이 시전 중입니다!");
            return false;
        }

        // 2. MP 소모 자원 체크 + 쿨타임 체크
        if (!skillToCast.CanCastSkill(characterStats, skillCooldowns))
            return false;

        // 3. 타겟이 유효한가?
        Targetable currentTarget = targetManager.GetCurrentTarget();

        if (skillToCast is ProjectileSkillDefinition projectileSkill)
        {
            // 타겟이 사라진 경우, 예외 처리대로 플레이어의 정면 방향으로 투사체 발사
            if (currentTarget == null)
                Debug.LogWarning($"투사체 스킬 {skillToCast.SkillName}은 타겟이 필요하지만, 타겟이 발견되지 않았습니다.");
        }

        else if (skillToCast is AreaSkillDefinition areaSkill)
        {
            // 범위형 스킬은 타겟이 필수가 아님. (타겟이 사라지더라도 땅에 시전 가능)
            // 특정 위치 클릭 시 시전하는 경우, targetPosition이 중요
        }

        // 다른 스킬 타입 추가 시 이 곳에 if else if 추가

        // 모든 조건 충족 시 스킬 발동 시작
        StartSkillExecution(skillToCast, currentTarget, targetPosition);
        return true;
    }

    private void StartSkillExecution(SkillDefinition skillToCast, Targetable target, Vector3? castPosition)
    {
        characterStats.CurrentMana -= skillToCast.ManaCost;     // 스킬에 정해진 MP량 소모

        // 쿨타임 적용
        skillCooldowns[skillToCast] = skillToCast.CoolDown;
        
        // 여기에 쿨다운 UI 애니메이션 호출 로직 작성할 예정

        // 스킬 시전 시간 있다면 시전 시작, 아니면 즉시 실행
        if (skillToCast.CastTime > 0)
        {
            currentCastingSkill = skillToCast;
            currentCastTimer = skillToCast.CastTime;
        }

        // 즉시 시전 가능 스킬
        else
        {
            skillToCast.ExecuteSkill(this, target, castPosition);
        }
    }

    /// <summary>
    /// 특정 스킬의 현재 레벨 반환 기능
    /// </summary>
    public int GetSkillLevel(SkillDefinition skillDef)
    {
        return skillLevels.ContainsKey(skillDef) ? skillLevels[skillDef] : 1;
    }

    /// <summary>
    /// 스킬 레벨업
    /// </summary>
    public void LevelUpSkill(SkillDefinition skillDef)
    {
        if (skillLevels.ContainsKey(skillDef))
        {
            skillLevels[skillDef]++;
            Debug.Log($"{skillDef.SkillName} 스킬을 레벨업 했습니다.");
            // UI에 스킬 레벨 보여주는 경우, 스킬 정보 업데이트 기능 호출
        }

        else
        {
            skillLevels.Add(skillDef, 1);   // 스킬을 처음 습득 시, 1레벨로 추가
            Debug.Log($"{skillDef.SkillName} 스킬을 획득했습니다!");
        }
    }

    /// <summary>
    /// 특정 스킬의 쿨타임 진행률 반환 (0 ~ 1)
    /// </summary>
    public float GetSkillCooldownProgress(SkillDefinition skillDef)
    {
        if (!skillCooldowns.ContainsKey(skillDef))
            return 1.0f;    // 쿨타임 없으면 사용 가능

        float remaining = skillCooldowns[skillDef];

        if (remaining <= 0)
            return 1.0f;

        return 1.0f - (remaining / skillDef.CoolDown);
    }
}
