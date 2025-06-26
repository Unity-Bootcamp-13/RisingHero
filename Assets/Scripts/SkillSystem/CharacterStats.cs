using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float MaxMana = 100f;    // �ӽ÷� �ִ� ���� 100 ����
    public float CurrentMana { get; set; }

    private void Awake()
    {
        CurrentMana = MaxMana;
    }
}
