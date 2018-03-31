using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFish
{
	public class GOBullet : MonoBehaviour {

        public float speed = 1000.0f;
        Vector3 mVec;
        Rect mBox;
		// Use this for initialization
		void Start ()
        {
            float width = Screen.width / 100;
            float height = Screen.height / 100;
            mBox = new Rect(-width / 2, -height / 2, width, height);
        }
		
		// Update is called once per frame
		void Update ()
        {
            Vector3 pos  = transform.position;
            if (pos.x < mBox.xMin)
            {
                //pos.y = pos.y - (pos.x - mBox.xMin) / mVec.x * mVec.y;
                pos.x = mBox.xMin - (pos.x - mBox.xMin);
                mVec.x = -mVec.x;
                RecalcAngle();
            }
            else if(pos.x > mBox.xMax)
            {
                //pos.y = pos.y - (pos.x - mBox.xMax) / mVec.x * mVec.y;
                pos.x = mBox.xMax - (pos.x - mBox.xMax);
                mVec.x = -mVec.x;
                RecalcAngle();
            }
            else if(pos.y < mBox.yMin)
            {
                //pos.x = pos.x - (pos.y - mBox.yMin) / mVec.y * mVec.x;
                pos.y = mBox.yMin - (pos.y - mBox.yMin);
                mVec.y = -mVec.y;
                RecalcAngle();
            }
            else if(pos.y > mBox.yMax)
            {
                //pos.x = pos.x - (pos.y - mBox.yMax) / mVec.y * mVec.x;
                pos.y = mBox.yMax - (pos.y - mBox.yMax);
                mVec.y = -mVec.y;
                RecalcAngle();
            }
            Vector3 dt = mVec * Time.deltaTime;
            pos.x += dt.x;
            pos.y += dt.y;
            transform.position = pos;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            GameObject go = GameObject.Find("GameLoop").GetComponent<SCFactory>().CreateNet(1);
            go.transform.position = transform.position;
            Destroy(gameObject);
        }

        void RecalcAngle()
        {
            float angle = Mathf.Atan2(mVec.y, mVec.x) * Mathf.Rad2Deg - 90;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        public void Launch(Vector3 vec)
		{
            Debug.Log(Screen.width);
            vec.z = 0;
            vec.Normalize();
            mVec = vec * speed / 100;
        }
	}
}
