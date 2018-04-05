using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFish
{
    public class SCGameLoop : MonoBehaviour
    {
        List<GOTimeline> mTimelineList;
        GameObject mFishPool;
		SCFactory mFactory;
		SCConfig mConfig;
		int mCurFrame;

        bool mbFreeze; // 是否冰冻

        void Start()
        {
			mCurFrame = 0;
            mbFreeze = false;
            mTimelineList = new List<GOTimeline>();
			mFactory = GetComponent<SCFactory>();
			mConfig = GetComponent<SCConfig> ();
            mFishPool = GameObject.Find("FishPool");
            AddTimeline (Random.Range(1, 6));
			StartUpdate ();
        }

        // 添加时间线
		void AddTimeline(int idx)
		{
            int roomid = 1;
            int id = 320000000 + 100000 * roomid + idx * 1000;
			GOTimeline client = mFactory.CreateTimeline ().GetComponent<GOTimeline>();
            client.mConfig = mConfig;
            client.mFactory = mFactory;
            client.SetStartID(id);
            mTimelineList.Add(client);

            id = 320000000 + 100000 * roomid + idx * 1000 + 90000;
            GOTimeline server = mFactory.CreateTimeline().GetComponent<GOTimeline>();
            server.mConfig = mConfig;
            server.mFactory = mFactory;
            server.SetStartID(id);
            mTimelineList.Add(server);
		}

        // 开如更新
        void StartUpdate()
        {
            InvokeRepeating("UpdateFrame", 0.01f, 0.05f);
        }

        // 更新一帧
        void UpdateFrame()
        {
            if (mbFreeze) return;
            foreach (GOTimeline timeline in mTimelineList)
            {
				timeline.UpdateFrame (mCurFrame);
            }

            GOFish[] fishes = mFishPool.GetComponentsInChildren<GOFish>();
            foreach (GOFish fish in fishes)
            {
                fish.DoMove();
            }
			mCurFrame += 1;
        }

        // 冰冻所有的鱼(开关)
        public void FreezeAllFish(bool bo)
        {
            if (bo)
            {
                mbFreeze = true;
                GOFish[] fishes = mFishPool.GetComponentsInChildren<GOFish>();
                foreach (GOFish fish in fishes)
                {
                    fish.DoStartFreeze();
                }
            }
            else
            {
                mbFreeze = false;
                GOFish[] fishes = mFishPool.GetComponentsInChildren<GOFish>();
                foreach (GOFish fish in fishes)
                {
                    fish.DoEndFreeze();
                }
            }
        }
    }
}
