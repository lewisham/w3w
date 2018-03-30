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
			AddTimeline ();
			StartUpdate ();
        }

		void AddTimeline()
		{
			GOTimeline timeline = mFactory.CreateTimeline ().GetComponent<GOTimeline>();
			timeline.mConfig = mConfig;
			timeline.mFactory = mFactory;
			mTimelineList.Add (timeline);
		}

        void StartUpdate()
        {
            InvokeRepeating("UpdateFrame", 0.01f, 0.05f);
        }

        void UpdateFrame()
        {
            foreach(GOTimeline timeline in mTimelineList)
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
