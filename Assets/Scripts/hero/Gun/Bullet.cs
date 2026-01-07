using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float BulletSpeed;
    [SerializeField] private float DeleteTimer = 10;
    private float _damage;
    private Vector2 _heading;
    private Transform _t;

    public void SetProps(float damage, Vector2 _headingUnitVector)
    {
        _damage = damage;
        _heading = _headingUnitVector;
    }

    void Start()
    {
        _t = transform;
        StartCoroutine(Fire());
        StartCoroutine(DeleteAfterTimer(DeleteTimer));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemies"))
            return;

        collision.gameObject.GetComponent<IHealth>().TakeDamage(_damage);

        Destroy(gameObject);
    }

    private IEnumerator Fire()
    {
        yield return new WaitUntil(() =>
        {
            _t.position += BulletSpeed * Time.deltaTime * (Vector3)_heading;
            return false;
        });
    }

    private IEnumerator DeleteAfterTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}
