using UnityEngine;

public class CannonTower : Tower{
	public Transform m_shootPoint;
	public Transform m_cannon;
	[SerializeField]
	private bool m_isMortar;
	private float g = Physics.gravity.y;
	[SerializeField, Range(0,90)] 
	private float m_angleInDegrees = 45.0f;
	[SerializeField]
	private int m_poolCount = 4;
	private bool m_autoExpand = true;
	private PoolMono<CannonProjectile> m_pool;
	[SerializeField]
	private CannonProjectile m_projectilePrefab;

	

	//создания пула снарядов для оптимизации создания объектов(cм класс PoolMono)
	private void Start()
	{
        
		m_pool = new PoolMono<CannonProjectile>(m_projectilePrefab, m_poolCount, m_shootPoint.transform);
		m_pool.autoExpand = m_autoExpand;
        
	}
	void FixedUpdate()
	{
		Aim();
	}

	//поиск монстра и проверка кд пушки для оптимизации поиск ведем по коллайдеру(см. класс Tower)
	private void Aim()
	{
		if (m_projectilePrefab is null || m_shootPoint is null)
			return;
		
		if (IsAcquireTarget() || IsTargetTracked())
		{
			if (m_lastShotTime + m_shootInterval <= Time.time)
			{
				if (m_isMortar)
				{
					m_cannon.localEulerAngles = new Vector3(-m_angleInDegrees, 0f, 0f);
					mortarShoot(monster);
				}
				else
				{
					Shoot(monster);
				}
				m_lastShotTime = Time.time;
			}
		}
	}
	//тут пушка попадет но немного пока не допер как зависимость от скорости ввести (также вместо создания снаряда просто берем его из пула)
	private void Shoot(Monster target)
	{
		Vector3 lookpos = new Vector3(target.transform.position.x + 0.9f, -4f, target.transform.position.z);
		m_cannon.transform.rotation = Quaternion.LookRotation(lookpos);
		var newProjectile = m_pool.GetFreeElement();
		newProjectile.transform.position = m_shootPoint.transform.position;
		newProjectile.GetComponent<Rigidbody>().useGravity = false;
		newProjectile.GetComponent<Rigidbody>().velocity = m_cannon.forward * newProjectile.GetComponent<CannonProjectile>().m_speed*50;
	}

	//Стрельба на упреждение по параболической траектории
	private void mortarShoot(Monster target)
	{
		var targetPos = target.transform.position;
		
		var targetNorm = (target.m_moveTarget.transform.position - targetPos).normalized *
		                  target.m_speed*50;
		var prediction = targetPos + targetNorm;
		Vector3 lookpos = prediction - m_shootPoint.position;
		Vector3 lookpos_xz = new Vector3(lookpos.x, 0.0f, lookpos.z);
		m_shootPoint.transform.rotation = Quaternion.LookRotation(lookpos_xz);
		float x = lookpos_xz.magnitude;
		float y = lookpos.y;
		float AngleInRadians = m_angleInDegrees * Mathf.PI / 180;
		float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
		float v = Mathf.Sqrt(Mathf.Abs(v2)); 
		var newProjectile = m_pool.GetFreeElement();
		newProjectile.transform.position = m_shootPoint.transform.position;
		newProjectile.GetComponent<Rigidbody>().velocity = m_shootPoint.forward * v;

	}
	
}
