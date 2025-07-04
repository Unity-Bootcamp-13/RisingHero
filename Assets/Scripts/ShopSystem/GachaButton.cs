using UnityEngine;

public class GachaButton : MonoBehaviour
{
    [SerializeField]private int listId;

    public void OnClick()
    {
        GachaEventBus.RaiseGachaRoll(listId);
    }

}
