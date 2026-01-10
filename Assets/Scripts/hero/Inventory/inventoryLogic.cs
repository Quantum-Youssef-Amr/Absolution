using UnityEngine;
using System.Collections;

public class InventoryLogic : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    void Start()
    {
        StartCoroutine(UpdateInventory());
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

    private IEnumerator UpdateInventory()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        UpdateInventoryUI();
    }

}
