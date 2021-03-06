﻿using System.Collections;
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
        int mMoveCnt;

        static Vector3 ShadowOffset = new Vector3(30.0f / 100.0f, -40.0f / 100.0f, 0.0f);

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
            public Vector2[] points;
        };

        tagFishInfo mFishInfo;

        Transform mShadow;
        
        void Awake()
        {
			mPointInfo = new tagPointInfo ();
            mFishInfo = new tagFishInfo();
            mCurPath = null;
            mBornFrame = 0;
            mCurFrame = 0;
            mTotalFrame = 0;
            mMoveCnt = 0;
        }


		void Start()
		{
            PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
            collider.tag = "fish";
            collider.isTrigger = true;
            collider.SetPath(0, mFishInfo.points);

            mShadow = transform.Find("shadow");
        }

        void Update()
		{
			if (mShadow)
            {
                mShadow.position = transform.position + ShadowOffset;
            }
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
            mMoveCnt = 0;
            //InvokeRepeating("DoMove", 0.01f, 0.15f);
            return true;
        }

        // 鱼的信息
        public void SetInfo(string[] info)
        {
            mFishInfo.rotate_type = int.Parse(info[11]);
            mFishInfo.show_layer = int.Parse(info[12]);
            string[] vals = info[14].Split(';');
            int length = vals.Length / 2;
            Vector2[] points = new Vector2[length];
            int idx = 0;
            for (int i = 0; i < length * 2; i += 2)
            {
                points[idx].x = float.Parse(vals[i]) / 100;
                points[idx].y = -float.Parse(vals[i + 1]) / 100;
                idx++;
            }
            mFishInfo.points = points;
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

        // 游动
        public void DoMove()
        {
            if (mMoveCnt > 0)
            {
                mMoveCnt--;
                return;
            }
            mMoveCnt = 3;
            if (mCurFrame + 3 > mTotalFrame)
            {
                RemoveFromScreen();
                return;
            }
			CalcPointInfo ();
            mCurFrame = mCurFrame + 3;
			MoveTo ();
        }

        // 设置动画速度
        public void SetAnimationSpeed(float s)
        {
            GetComponent<Animator>().speed = s;
            if (mShadow)
            {
                mShadow.GetComponent<Animator>().speed = s;
            }
        }

        // 开始冰冻
        public void DoStartFreeze()
        {
            SetAnimationSpeed(0);
        }

        // 结束冰冻
        public void DoEndFreeze()
        {
            SetAnimationSpeed(1);
        }
    }
}
