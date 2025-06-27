using UnityEngine;

public class CharacterStats123 : MonoBehaviour
{
    public float MaxMana = 100f;    // 임시로 최대 마나 100 설정
    public float CurrentMana { get; set; }

    private void Awake()
    {
        CurrentMana = MaxMana;
    }
}
