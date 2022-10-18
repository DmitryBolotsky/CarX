using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField]
    protected float m_shootInterval = 0.5f;
    [SerializeField, Range(6.0f,8.0f)]
    protected float m_range = 6f;

    protected float m_lastShotTime = -0.5f;
    protected Monster monster;
    private const int ENEMY_LAYER_MASK = 1 << 8;
    
    

    protected bool IsAcquireTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.localPosition, m_range,ENEMY_LAYER_MASK);
        if (targets.Length == 0)
        {
            monster = null;
            return false;
        }
        monster = targets[0].GetComponent<Monster>();
        return true;
    }

    protected bool IsTargetTracked()
    {
        if (monster == null)
        {
            return false;
        }

        Vector3 myPos = transform.localPosition;
        Vector3 targetPos = monster.transform.position;
        if (Vector3.Distance(myPos, targetPos) > m_range)
        {
            monster = null;
            return false;
        }

        return true;
    }
}
