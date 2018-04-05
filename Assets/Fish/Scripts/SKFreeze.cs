using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFish
{
    public class SKFreeze : MonoBehaviour
    {

        float mDuration; // 冰冻剩余时间

        void Start()
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            StartCoroutine(ReleaseSkillImpl());
        }


        IEnumerator ReleaseSkillImpl()
        {
            yield return null;
            mDuration = 10.0f;
            GameObject.Find("GameLoop").GetComponent<SCGameLoop>().FreezeAllFish(true);
            GetComponent<AudioSource>().Play();
            float dt = 0.2f;
            iTween.FadeTo(gameObject, 1.0f, 1.0f);
            while (mDuration > 0)
            {
                mDuration -= dt;
                yield return new WaitForSeconds(dt);
                Debug.Log(mDuration);
            }
            GameObject.Find("GameLoop").GetComponent<SCGameLoop>().FreezeAllFish(false);
            Destroy(gameObject);
        }
    }
}
