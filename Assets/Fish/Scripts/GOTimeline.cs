using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFish
{
    public class GOTimeline : MonoBehaviour
    {
        int mStartID;
        public SCConfig mConfig;
		public SCFactory mFactory;

        struct tagTimelineUnit
        {
            public int fishid;
            public int frame;
            public int pathid;
        };
        tagTimelineUnit mCurUnit;

        void Awake()
        {
            mStartID = 320101000;
            mCurUnit = new tagTimelineUnit();
        }
			

        bool GetUnit(int id)
        {
            string[] vals = mConfig.GetTimelineUnit(id);
            if (vals == null)
            {
                return false;
            }
            mCurUnit.fishid = int.Parse(vals[1]);
            mCurUnit.frame = int.Parse(vals[2]);
            mCurUnit.pathid = int.Parse(vals[3]);
            return true;
        }

        void DoCreateFish()
        {
            if (mCurUnit.fishid == 100)
            {

            }
            else
            {
				GameObject go = mFactory.CreateFish (mCurUnit.fishid);
				if (go == null)
					return;
                GOFish fish = go.GetComponent<GOFish> ();
                fish.SetInfo(mConfig.GetFish(mCurUnit.fishid));
				fish.SetPath (mConfig.GetPath (mCurUnit.pathid + 300000000));
				fish.GotoFrame (0);
            }
        }

        // 设置起始id
        public void SetStartID(int id)
        {
            mStartID = id;
        }

        // 跳转到帧
        public bool GotoFrame(int frame)
        {
            return true;
        }

        // 更新帧
        public void UpdateFrame(int frame)
        {
            while (true)
            {
                // 全部帧都跑完
                if (!GetUnit(mStartID))
                {
                    return;
                }
                // 不属于当前帧的
                if (mCurUnit.frame > frame)
                {
                    return;
                }
                mStartID = mStartID + 1;
                DoCreateFish();
            }
        }
    }
}
