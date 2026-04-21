using UnityEngine;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{
    [SerializeField] private ResourceRankHandler rankHandler;
    [SerializeField] private Image lastCollectedResourceImg;
    [SerializeField] private Image rankProgressBar;

    private void Awake()
    {
        UpdateUI();
        rankHandler.OnRankUpdate += RankHandler_OnRankUpdate;
    }

    private void RankHandler_OnRankUpdate()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        rankProgressBar.fillAmount = (float)rankHandler.CurCollectionStreak / rankHandler.CollectionsToNextRank;
        lastCollectedResourceImg.sprite = rankHandler.LastCollectedResource != null ? rankHandler.LastCollectedResource.Sprite : null;
        lastCollectedResourceImg.gameObject.SetActive(lastCollectedResourceImg.sprite != null);
    }
}
