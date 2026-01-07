using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Data.SqlTypes;

public class HeroAI : MonoBehaviour, IPointerClickHandler
{
    private Hero _param;
    private Weapon _weapon;

    private Coroutine _defending;
    private StageData _sd;

    public void SetHeroParams(Hero hero)
    {
        _param = hero;
        _weapon = _param.weapon;
        _sd = StageManager.Instance.getStageData();
    }

    public void BeginDefending()
    {
        if (_defending != null)
        {
            StopCoroutine(_defending);
            _defending = null;
        }

        _defending ??= StartCoroutine(Defending());
    }


    private IEnumerator Defending()
    {
        Vector2 m_GridPos = GameMath.FromIsometricToGrid(transform.position, _param.spriteOffset, _sd.MapStepSize);
        while (true)
        {
            RaycastHit2D[] m_raycastHit = new RaycastHit2D[0];

            for (int x = -(int)_weapon.range; x <= (int)_weapon.range; x++)
            {
                for (int y = -(int)_weapon.range; y <= (int)_weapon.range; y++)
                {
                    m_raycastHit = Physics2D.CircleCastAll(GameMath.FromGridToIsometric(m_GridPos, _param.spriteOffset, _sd.MapStepSize), 1f, GameMath.FromGridToIsometric(new Vector2(x, y) - m_GridPos, Vector2.zero, _sd.MapStepSize), (int)_weapon.range, LayerMask.GetMask("Enemies"));
                }
            }

            if (m_raycastHit.Length > 0)
            {
                GameObject m_target = m_raycastHit[Random.Range(0, m_raycastHit.Length)].collider.gameObject;
                GameObject m_bullet = Instantiate(_weapon.bullet, transform.position, Quaternion.identity);

                m_bullet.GetComponent<Bullet>().SetProps(_weapon.Damage, (m_target.transform.position - transform.position).normalized);
            }
            yield return new WaitForSeconds(1f / _weapon.FiringRate);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right)
            return;

        GameEventBus.OnRemoveHero?.Invoke(_param);
        Destroy(gameObject);
    }
}
