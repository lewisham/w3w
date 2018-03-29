using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFish
{
    public class SCGameLoop : MonoBehaviour
    {
        List<GOTimeline> mTimelineList;
        List<GOFish> mFishList;
        void Start()
        {
            mTimelineList = new List<GOTimeline>();
            mFishList = new List<GOFish>();
        }

        void StartUpdate()
        {
            InvokeRepeating("UpdateFrame", 0.01f, 0.05f);
        }

        void UpdateFrame()
        {
            foreach(GOTimeline timeline in mTimelineList)
            {

            }

            foreach (GOFish fish in mFishList)
            {

            }
        }
    }
}
