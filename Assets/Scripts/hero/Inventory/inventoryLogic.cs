using System.Linq;
using UnityEngine;

public class InventoryLogic : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    void Awake()
    {
        GameEventBus.OnSelectingHero += HeroID =>
        {
            GameEventBus.OnIsHeroAvailable?.Invoke(IsHeroAvailable(HeroID), inventory.inventoryCells[HeroID].hero ?? null);
        };

        GameEventBus.OnBuildHero += Hero =>
        {
            RemoveHeroFromInventory(Hero);
            UpdateInventoryUI();
        };

        GameEventBus.OnRemoveHero += Hero =>
        {
            AddHeroToInventory(Hero);
            UpdateInventoryUI();
        };
    }

    void Start()
    {
        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        GameEventBus.OnUpdateInventoryIU?.Invoke(inventory.inventoryCells);
    }

    private bool IsHeroAvailable(int SlotID)
    {
        return inventory.inventoryCells[SlotID].num > 0;
    }

    private void AddHeroToInventory(Hero hero)
    {
        for (int slotIndex = 0; slotIndex < inventory.inventoryCells.Length; slotIndex++)
        {
            if (inventory.inventoryCells[slotIndex].hero.name == hero.name)
            {
                inventory.inventoryCells[slotIndex].num++;
                break;
            }
        }
    }

    private void RemoveHeroFromInventory(Hero hero)
    {
        for (int slotIndex = 0; slotIndex < inventory.inventoryCells.Length; slotIndex++)
        {
            if (inventory.inventoryCells[slotIndex].hero.name == hero.name)
            {
                inventory.inventoryCells[slotIndex].num--;
                break;
            }
        }
    }

}
