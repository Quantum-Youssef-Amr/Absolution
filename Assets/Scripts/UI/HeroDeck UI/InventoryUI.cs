using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject HeroBtnObject;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private RectTransform RectTransform;


    void Awake()
    {
    }

    void Start()
    {
        GameEventBus.OnUpdateInventoryIU += Inventory => UpdateUI(Inventory);
    }

    private void UpdateUI(InventoryCell[] inventory)
    {
        for (int btn = 0; btn < RectTransform.childCount; btn++)
        {
            Destroy(RectTransform.GetChild(btn).gameObject);
        }

        for (int slot = 0; slot < inventory.Length; slot++)
        {
            GameObject m_slotObject = Instantiate(HeroBtnObject, RectTransform);

            Button m_btn = m_slotObject.GetComponent<Button>();
            m_btn.transform.GetChild(0).GetComponent<Image>().sprite = inventory[slot].hero.sprite;

            if (inventory[slot].num == 0)
            {
                m_btn.interactable = false;
                continue;
            }

            int i = slot;
            m_btn.onClick.AddListener(() =>
            {
                GameEventBus.OnSelectingHero?.Invoke(i);
                GameEventBus.OnIsHeroAvailable += (IsHeroAvailable, Hero) =>
                {
                    if (IsHeroAvailable)
                        GameEventBus.OnSelectingHeroSec?.Invoke(Hero);
                };
            });
            m_btn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventory[slot].num.ToString();
        }

        UpdateLayoutGroup();
    }

    private void UpdateLayoutGroup()
    {
        gridLayoutGroup.CalculateLayoutInputHorizontal();
        gridLayoutGroup.CalculateLayoutInputVertical();
    }
}
