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

        // 路径信息
		struct tagPointInfo
		{
			public float x;
			public float y;
			public float z;
		};

        tagPointInfo mPointInfo;

        // 鱼的信息
        struct tagFishInfo
        {
            public int rotate_type;
            public int show_layer;
        };

        tagFishInfo mFishInfo;
        
        void Awake()
        {
			mPointInfo = new tagPointInfo ();
            mFishInfo = new tagFishInfo();
            mCurPath = null;
            mBornFrame = 0;
            mCurFrame = 0;
            mTotalFrame = 0;
        }

        // 当前路径信息
		void CalcPointInfo()
		{
			mPointInfo.x = float.Parse (mCurPath [mCurFrame]) / 100.0f - 6.4f;
			mPointInfo.y = float.Parse (mCurPath [mCurFrame + 1]) / 100.0f - 3.6f;
			mPointInfo.z = -float.Parse (mCurPath [mCurFrame + 2]) - 90.0f;
		}

        // 移动到
		void MoveTo()
		{
			iTween.MoveTo(gameObject, iTween.Hash("x", mPointInfo.x,"y", mPointInfo.y,"z", 0,"time", 0.15,"easeType", iTween.EaseType.linear));
            if (mFishInfo.rotate_type == 0)
            {
                iTween.RotateTo(gameObject, iTween.Hash("z", mPointInfo.z, "time", 0.01f, "easeType", iTween.EaseType.linear));
            }
		}

        // 跑到第几帧
        public bool GotoFrame(int frame)
        {
            mCurFrame = frame - mBornFrame;
            if (mCurFrame >= mTotalFrame)
            {
                return false;
            }
            GetComponent<SpriteRenderer>().sortingOrder = mFishInfo.show_layer;
			CalcPointInfo ();
			gameObject.transform.position = new Vector3(mPointInfo.x, mPointInfo.y, 0);
            InvokeRepeating("DoMove", 0.01f, 0.15f);
            return true;
        }

        // 鱼的信息
        public void SetInfo(string[] info)
        {
            mFishInfo.rotate_type = int.Parse(info[11]);
            mFishInfo.show_layer = int.Parse(info[12]);
        }

        // 设置游的路径
        public void SetPath(string[] path)
        {
            mCurPath = path;
            mTotalFrame = path.Length;
            mCurFrame = 0;
        }
			
        public void RemoveFromScreen()
        {
			Destroy(gameObject);
        }

        void DoMove()
        {
            if (mCurFrame + 3 > mTotalFrame)
            {
                RemoveFromScreen();
                return;
            }
			CalcPointInfo ();
            mCurFrame = mCurFrame + 3;
			MoveTo ();
        }
    }
}
