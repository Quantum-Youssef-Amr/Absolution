using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Roll : MonoBehaviour
{
    [SerializeField] private int HeroesPerRoll = 2;
    [SerializeField]
    private HeroRankToPrice[] heroRankToPrice = new HeroRankToPrice[]
    {
        new(HeroRank.S, 5000),
        new(HeroRank.A, 2000),
        new(HeroRank.B, 1000),
        new(HeroRank.C, 500)
    };

    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject RollScreen, RollResults, RollResultsContainer, HeroCard;
    [SerializeField] private Hero[] GameHeroes;

    void OnEnable()
    {
        GameEventBus.OnUpdateInventoryIU?.Invoke(inventory.inventoryCells.ToArray());
        inventory.Blood++;
    }

    public void ShowRollScreen()
    {
        RollScreen.SetActive(true);
    }

    public void HideRollScreen()
    {
        RollScreen.SetActive(false);
    }

    public void HideRollResults()
    {
        RollResults.SetActive(false);
        RemoveRollResults();
    }

    public void RollHero(int heroRank)
    {
        HeroRank m_heroRank = (HeroRank)heroRank;
        int m_heroPrice = heroRankToPrice.Where(heroToPrice => heroToPrice.rank == m_heroRank).First().Price;
        if (inventory.Blood < m_heroPrice)
            return;


        inventory.Blood -= m_heroPrice;
        Hero[] m_heroesRank = GameHeroes.Where(hero => hero.Rank == m_heroRank).ToArray();

        for (int i = 0; i < HeroesPerRoll; i++)
        {
            Hero m_hero = GetRandomHero(m_heroesRank);
            GameObject m_heroCard = Instantiate(HeroCard, RollResultsContainer.transform);

            m_heroCard.transform.GetChild(0).GetComponent<Image>().sprite = m_hero.sprite;
            m_heroCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{m_hero.name}";

            GameEventBus.OnRemoveHero?.Invoke(m_hero);
        }

        HideRollScreen();
        RollResults.SetActive(true);
    }


    private void RemoveRollResults()
    {
        for (int child = 0; child < RollResultsContainer.transform.childCount; child++)
        {
            Destroy(RollResultsContainer.transform.GetChild(child).gameObject);
        }
    }

    private Hero GetRandomHero(Hero[] heroes)
    {
        return heroes[Random.Range(0, heroes.Length)];
    }
}


[System.Serializable]
public struct HeroRankToPrice
{
    public HeroRank rank;
    public int Price;

    public HeroRankToPrice(HeroRank rank, int Price)
    {
        this.rank = rank;
        this.Price = Price;
    }
}