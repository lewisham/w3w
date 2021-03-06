﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Diagnostics;

namespace GameFish
{
    public class SCConfig : MonoBehaviour
    {
        Dictionary<int, string[]> mTimelines;
        Dictionary<int, string[]> mFishPath;
        Dictionary<int, string[]> mFish;

        public string[] GetTimelineUnit(int id)
        {
            return mTimelines[id];
        }

		public string[] GetPath(int id)
		{
			return mFishPath [id];
		}

        public string[] GetFish(int id)
        {
            return mFish[id];
        }

        // Use this for initialization
        void Awake()
        {
            mTimelines = new Dictionary<int, string[]>();
            mFishPath = new Dictionary<int, string[]>();
            mFish = new Dictionary<int, string[]>();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            LoadTimeline();
            LoadPath();
            LoadFish();
            sw.Stop();
            UnityEngine.Debug.Log(string.Format("total: {0} ms", sw.ElapsedMilliseconds));
            //UnityEngine.Debug.Log(mFish[100000001][0]);
        }

        // 加载时间线
        void LoadTimeline()
        {
            TextAsset ret = (TextAsset)Resources.Load("DataBin/timeline");
            string content = ret.text.Replace("\r", "");
            string[] lines = content.Split('\n');
            int id = 0;
            for (int i = 2; i < lines.Length; i++)
            {
                string[] vals = lines[i].Split(',');
                id = int.Parse(vals[0]);
                mTimelines.Add(id, vals);
            }
        }

        // 加载路径
        void LoadPath()
        {
            TextAsset ret = (TextAsset)Resources.Load("DataBin/fishpath");
            string content = ret.text.Replace("\r", "");
            string[] lines = content.Split('\n');
            int id = 0;
            for (int i = 2; i < lines.Length; i++)
            {
                string[] vals = lines[i].Split(',');
                id = int.Parse(vals[0]);
                mFishPath[id] = vals[1].Split(';');
            }
        }

        // 加载鱼的配置数据
        void LoadFish()
        {
            TextAsset ret = (TextAsset)Resources.Load("DataBin/fish");
            string content = ret.text.Replace("\r", "");
            string[] lines = content.Split('\n');
            int id = 0;
            for (int i = 2; i < lines.Length; i++)
            {
                string[] vals = lines[i].Split(',');
                id = int.Parse(vals[0]);
                mFish.Add(id, vals);
            }
        }
    }
}