using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUpdater : MonoBehaviour
{
    [SerializeField] private GameObject HeroBtnObject;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private RectTransform RectTransform;

    void Awake()
    {
        GameEventBus.OnUpdateInventoryIU += inventorySlots => UpdateUI(inventorySlots);
    }

    private void UpdateUI(InventoryCell[] inventorySlots)
    {
        for (int btn = 0; btn < RectTransform.childCount; btn++)
        {
            Destroy(RectTransform.GetChild(btn).gameObject);
        }

        for (int slot = 0; slot < inventorySlots.Length; slot++)
        {
            if (inventorySlots[slot].num == 0)
                continue;

            GameObject m_slotObject = Instantiate(HeroBtnObject, RectTransform);

            Button m_btn = m_slotObject.GetComponent<Button>();
            m_btn.transform.GetChild(0).GetComponent<Image>().sprite = inventorySlots[slot].hero.sprite;


            int i = slot;
            m_btn.onClick.AddListener(() =>
            {
                GameEventBus.OnPreviewHero?.Invoke(inventorySlots[i].hero);
            });

            m_btn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventorySlots[slot].num.ToString();
        }

        UpdateLayoutGroup();
    }


    private void UpdateLayoutGroup()
    {
        gridLayoutGroup.CalculateLayoutInputHorizontal();
        gridLayoutGroup.CalculateLayoutInputVertical();
    }
}
