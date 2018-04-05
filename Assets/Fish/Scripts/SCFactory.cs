using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFish
{
    public class SCFactory : MonoBehaviour
    {
		public GameObject mTimeLine;
		public GameObject mBullet;
        public GameObject mFish;

        public GameObject mFishPool;
        public GameObject mNetPool;
        public GameObject mBulletPool;

        void Start()
        {
			
        }

        public GameObject CreateFish(int id)
        {
            string path = string.Format("Fish/fishid{0}", id - 100000000);
            Object prefab = Resources.Load(path, typeof(GameObject));
            if (prefab == null)
                return null;
            GameObject go = Instantiate(prefab) as GameObject;
            if (mFishPool)
            {
                go.transform.parent = mFishPool.transform;
            }
			return go;
        }

        public GameObject CreateTimeline()
        {
			GameObject go = Instantiate(mTimeLine) as GameObject;
			return go;
        }

		public GameObject CreateBullet()
		{
			GameObject go = Instantiate(mBullet) as GameObject;
            if (mBulletPool)
            {
                go.transform.parent = mBulletPool.transform;
            }
            return go;
		}

        public GameObject CreateNet(int id)
        {
            string path = string.Format("Net/net_{0}", id);
            Object prefab = Resources.Load(path, typeof(GameObject));
            if (prefab == null)
                return null;
            GameObject go = Instantiate(prefab) as GameObject;
            if (mNetPool)
            {
                go.transform.parent = mNetPool.transform;
            }
            return go;
        }
    }
}
