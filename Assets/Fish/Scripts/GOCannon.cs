using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFish
{
	public class GOCannon : MonoBehaviour {

        Transform mGun;
        Transform mBasePos;
        Transform mLancherPos;
		// Use this for initialization
		void Start () {
            mGun = transform.Find("Gun");
            mBasePos = mGun.transform.Find("BasePos");
            mLancherPos = mGun.transform.Find("LancherPos");
            //InvokeRepeating("FirePre", 0.01f, 1.05f);
        }

		// Update is called once per frame
		void Update ()
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))//鼠标或触摸事件  
            {
                if (!IsInvoking("FirePre"))
                {
                    InvokeRepeating("FirePre", 0.00f, 0.2f);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                CancelInvoke("FirePre");
            }
        }


		void FirePre()
		{
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 vec = worldPoint - mBasePos.position;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg - 90;
            mGun.eulerAngles = new Vector3(0, 0, angle);

            GameObject go = GameObject.Find ("GameLoop").GetComponent<SCFactory> ().CreateBullet();
			GOBullet bullet = go.GetComponent<GOBullet> ();
            go.transform.rotation = mGun.rotation;
            go.transform.position = mLancherPos.position;
            bullet.Launch (vec);
		}

		void CalcFireVec()
		{

		}
	}
}
