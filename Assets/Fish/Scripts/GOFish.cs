using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFish
{
    public class GOFish : MonoBehaviour
    {
        string[] mCurPath;
        int mBornFrame;
        int mTotalFrame;
        int mCurFrame;
        // Use this for initialization
        void Start()
        {
            mCurPath = null;
            mBornFrame = 0;
            mCurFrame = 0;
            mTotalFrame = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }

        // 跑到第几帧
        public bool GotoFrame(int frame)
        {
            mCurFrame = frame - mBornFrame;
            if (mCurFrame >= mTotalFrame)
            {
                return false;
            }
            InvokeRepeating("DoMove", 100.0f, 0.15f);
            return true;
        }

        // 设置游的路径
        public void SetPath(string[] path)
        {
            mCurPath = path;
            mTotalFrame = path.Length / 3;
            mCurFrame = 0;
        }

        public void RemoveFromScreen()
        {

        }

        void DoMove()
        {
            if (mCurFrame >= mTotalFrame)
            {
                RemoveFromScreen();
                return;
            }
            mCurFrame = mCurFrame + 1;
        }
    }
}
