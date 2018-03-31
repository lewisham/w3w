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
        public Dictionary<int, GameObject> mFishDic;
        void Start()
        {
			
        }

        public GameObject CreateFish(int id)
        {
            /*
            GameObject go = Instantiate(mFish) as GameObject;
            return go;
            */
            string path = string.Format("Fish/fishid{0}", id - 100000000);
            Object prefab = Resources.Load(path, typeof(GameObject));
            if (prefab == null)
                return null;
            GameObject go = Instantiate(prefab) as GameObject;
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
			return go;
		}

        public GameObject CreateNet(int id)
        {
            string path = string.Format("Net/net_{0}", id);
            Object prefab = Resources.Load(path, typeof(GameObject));
            if (prefab == null)
                return null;
            GameObject go = Instantiate(prefab) as GameObject;
            return go;
        }
    }
}
