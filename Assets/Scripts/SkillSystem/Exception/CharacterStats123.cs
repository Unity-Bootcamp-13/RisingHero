using UnityEngine;

public class CharacterStats123 : MonoBehaviour
{
    public float MaxMana = 100f;    // �ӽ÷� �ִ� ���� 100 ����
    public float CurrentMana { get; set; }

    private void Awake()
    {
        CurrentMana = MaxMana;
    }
}
