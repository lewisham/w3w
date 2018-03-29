using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFish
{
    public class GOTimeline : MonoBehaviour
    {
        int mStartID;
        int mCurFrame;
        public SCConfig mConfig;

        struct tagTimelineUnit
        {
            public int fishid;
            public int frame;
            public int pathid;
        };
        tagTimelineUnit mCurUnit;

        void Start()
        {
            mStartID = 320101000;
            mCurFrame = 0;
            mCurUnit = new tagTimelineUnit();
            InvokeRepeating("TestFrame", 1.0f, 0.05f);
        }

        void TestFrame()
        {
            mCurFrame = mCurFrame + 1;
            UpdateFrame(mCurFrame);
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
                Debug.Log(mCurUnit.fishid);
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
