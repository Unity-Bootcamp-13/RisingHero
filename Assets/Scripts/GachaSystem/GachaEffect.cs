using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaEffect : MonoBehaviour
{
    [SerializeField] private GameObject rewardPrefab;
    [SerializeField] private Transform rewardParentPanel;
    [SerializeField] private GameObject[] buttonsToDisable;

    public IEnumerator SpawnRewards(List<int> itemIds)
    {
        SetButtons(false);

        foreach (Transform child in rewardParentPanel)
            Destroy(child.gameObject);

        yield return new WaitForSeconds(0.1f);

        foreach (int id in itemIds)
        {
            var obj = Instantiate(rewardPrefab, rewardParentPanel);
            var image = obj.GetComponent<Image>();
            var sprite = Resources.Load<Sprite>($"Icons/{id}");
            if (sprite != null)
            {
                image.sprite = sprite;
                image.enabled = true;
            }
            else
            {
                Debug.LogWarning($"[GachaEffect] ID {id}에 해당하는 Sprite를 찾을 수 없습니다.");
            }
            yield return new WaitForSeconds(0.1f);
        }

        SetButtons(true);
    }

    private void SetButtons(bool active)
    {
        foreach (var btn in buttonsToDisable)
            btn.SetActive(active);
    }
}
