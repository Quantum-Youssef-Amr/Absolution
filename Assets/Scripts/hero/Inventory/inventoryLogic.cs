using UnityEngine;
using System.Collections;
using System.Linq;

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
        GameEventBus.OnUpdateInventoryIU?.Invoke(inventory.inventoryCells.ToArray());
    }

    private bool IsHeroAvailable(int SlotID)
    {
        return inventory.inventoryCells[SlotID].num > 0;
    }

    private void AddHeroToInventory(Hero hero)
    {
        if (inventory.inventoryCells.Where(cell => cell.hero == hero).ToArray().Length == 0)
        {
            inventory.inventoryCells.Add(new(hero));
        }

        for (int slotIndex = 0; slotIndex < inventory.inventoryCells.Count; slotIndex++)
        {
            if (inventory.inventoryCells[slotIndex].hero == hero)
            {
                inventory.inventoryCells[slotIndex] = new(hero, inventory.inventoryCells[slotIndex].num + 1);
                break;
            }
        }
    }

    private void RemoveHeroFromInventory(Hero hero)
    {
        for (int slotIndex = 0; slotIndex < inventory.inventoryCells.Count; slotIndex++)
        {
            if (inventory.inventoryCells[slotIndex].hero.name == hero.name)
            {
                inventory.inventoryCells[slotIndex] = new(hero, inventory.inventoryCells[slotIndex].num - 1);
                if (inventory.inventoryCells[slotIndex].num <= 0)
                {
                    inventory.inventoryCells.RemoveAt(slotIndex);
                }
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
