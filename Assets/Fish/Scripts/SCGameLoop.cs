using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFish
{
    public class SCGameLoop : MonoBehaviour
    {
        List<GOTimeline> mTimelineList;
        List<GOFish> mFishList;
		SCFactory mFactory;
		SCConfig mConfig;
		int mCurFrame;
        void Start()
        {
			mCurFrame = 0;
            mTimelineList = new List<GOTimeline>();
            mFishList = new List<GOFish>();
			mFactory = GetComponent<SCFactory>();
			mConfig = GetComponent<SCConfig> ();
			AddTimeline (Random.Range(1, 6));
			StartUpdate ();
        }

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

        void StartUpdate()
        {
            InvokeRepeating("UpdateFrame", 0.01f, 0.05f);
        }

        void UpdateFrame()
        {
            foreach (GOTimeline timeline in mTimelineList)
            {
				timeline.UpdateFrame (mCurFrame);
            }

            foreach (GOFish fish in mFishList)
            {

            }
			mCurFrame += 1;
        }
    }
}
