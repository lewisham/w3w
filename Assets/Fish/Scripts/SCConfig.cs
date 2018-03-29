using System.Collections;
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

        public string[] GetTimelineUnit(int id)
        {
            return mTimelines[id];
        }

        // Use this for initialization
        void Start()
        {
            mTimelines = new Dictionary<int, string[]>();
            mFishPath = new Dictionary<int, string[]>();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            LoadTimeline();
            LoadPath();
            sw.Stop();
            UnityEngine.Debug.Log(string.Format("total: {0} ms", sw.ElapsedMilliseconds));
            UnityEngine.Debug.Log(mFishPath[300012022][0]);
        }

        // 加载时间线
        void LoadTimeline()
        {
            string path = Application.dataPath + "/Fish/DataBin/timeline.txt";
            StreamReader sr;
            sr = File.OpenText(path);
            string content = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();

            content = content.Replace("\r", "");
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
            string path = Application.dataPath + "/Fish/DataBin/fishpath.txt";
            StreamReader sr;
            sr = File.OpenText(path);
            string content = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();

            content = content.Replace("\r", "");
            string[] lines = content.Split('\n');
            int id = 0;
            for (int i = 2; i < lines.Length; i++)
            {
                string[] vals = lines[i].Split(',');
                id = int.Parse(vals[0]);
                mFishPath[id] = vals[1].Split(';');
            }
        }
    }
}