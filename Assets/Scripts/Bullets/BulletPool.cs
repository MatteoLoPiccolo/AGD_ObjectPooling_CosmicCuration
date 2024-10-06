using System.Collections.Generic;

namespace CosmicCuration.Bullets
{
    public class BulletPool
    {
        private BulletView bulletView;
        private BulletScriptableObject bulletScriptableObject;
        private List<PooledBullet> pooledBullets = new List<PooledBullet>();

        public BulletPool(BulletView bulletView, BulletScriptableObject bulletScriptableObject)
        {
            this.bulletView = bulletView;
            this.bulletScriptableObject = bulletScriptableObject;
        }

        public BulletController GetBullet()
        {
            if (pooledBullets.Count > 0)
            {
                PooledBullet pooledBullet = pooledBullets.Find(item => !item.isUsed);
                if (pooledBullet != null)
                {
                    pooledBullet.isUsed = true;
                    return pooledBullet.Bullet;
                }
            }

            return CreateNewPoolBullet();
        }

        public void ReturnToBulletPool(BulletController returnBullet)
        {
            PooledBullet pooledBullet = pooledBullets.Find(item => item.Bullet.Equals(returnBullet));
            pooledBullet.isUsed = false;
        }

        private BulletController CreateNewPoolBullet()
        {
            PooledBullet poolBullet = new PooledBullet();
            poolBullet.Bullet = new BulletController(bulletView, bulletScriptableObject);
            poolBullet.isUsed = true;
            pooledBullets.Add(poolBullet);

            return poolBullet.Bullet;
        }

        public class PooledBullet
        {
            public BulletController Bullet;
            public bool isUsed;
        }
    }
}