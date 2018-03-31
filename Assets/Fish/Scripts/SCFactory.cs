using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFish
{
    public class SCFactory : MonoBehaviour
    {
		public GameObject mTimeLine;
		public GameObject mBullet;
        void Start()
        {
			
        }

        public GameObject CreateFish(int id)
        {
            string path = string.Format("Assets/Fish/Prefabs/Fish/fishid{0}.prefab", id - 100000000);
            Object prefab = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
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
            string path = string.Format("Assets/Fish/Prefabs/Net/net_{0}.prefab", id);
            Object prefab = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
            if (prefab == null)
                return null;
            GameObject go = Instantiate(prefab) as GameObject;
            return go;
        }
    }
}
