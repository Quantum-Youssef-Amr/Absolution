using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HeroPreview : MonoBehaviour
{
    [SerializeField] private Image HeroImage;
    [SerializeField] private TextMeshProUGUI HeroName;
    [SerializeField] private TextMeshProUGUI HeroClass;
    [SerializeField] private TextMeshProUGUI HeroBloodReturn;

    [SerializeField] private TextMeshProUGUI WeaponName;
    [SerializeField] private TextMeshProUGUI WeaponDamage;
    [SerializeField] private TextMeshProUGUI WeaponRange;
    [SerializeField] private TextMeshProUGUI WeaponFireRate;

    void OnEnable()
    {
        GameEventBus.OnPreviewHero += Hero => PreviewHero(Hero);
    }

    void OnDisable()
    {
        GameEventBus.OnPreviewHero -= Hero => PreviewHero(Hero);
    }

    private void PreviewHero(Hero hero)
    {
        HeroImage.sprite = hero.sprite;

        HeroName.text = $"{hero.name}";
        HeroClass.text = $"{hero.Rank}";
        HeroBloodReturn.text = $"{hero.BloodReturn}";

        WeaponName.text = $"{hero.weapon.name}";
        WeaponDamage.text = $"{hero.weapon.Damage}";
        WeaponRange.text = $"{hero.weapon.range}";
        WeaponFireRate.text = $"{hero.weapon.FiringRate}/s";
    }
}
