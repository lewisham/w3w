using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFish
{
	public class GOBullet : MonoBehaviour {

        public float speed = 1200.0f;
		// Use this for initialization
		void Start () {
            //speed = speed * speed;
        }
		
		// Update is called once per frame
		void Update () {
			
		}

        void OnTriggerEnter2D(Collider2D other)
        {
            GameObject go = GameObject.Find("GameLoop").GetComponent<SCFactory>().CreateNet(1);
            go.transform.position = transform.position;
            Destroy(gameObject);
        }

        public void Launch(Vector3 vec)
		{
            vec.Normalize();
            vec *= 30.0f;
            float duration = vec.magnitude * 100 / speed;
            vec += transform.position;
            iTween.MoveTo(gameObject, iTween.Hash("x", vec.x, "y", vec.y, "z", 0, "time", duration, "easeType", iTween.EaseType.linear));
            Destroy(gameObject, duration);
        }
	}
}
