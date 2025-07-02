using Unity.VisualScripting;
using UnityEngine;

public class StageButton : MonoBehaviour
{
    [SerializeField] private int stageId;

    public void OnClick()
    {
        StageEventBus.MoveStage(stageId);
    }
}
