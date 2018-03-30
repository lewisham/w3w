using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEditor.Animations;
using NUnit.Framework;

public class BuildAnimation : Editor
{

    //生成出的Prefab的路径
    private static string PrefabPath = "Assets/Fish/Prefabs/Fish";
    //生成出的AnimationController的路径
	private static string AnimationControllerPath = "Assets/Fish/AnimationController";
    //生成出的Animation的路径
	private static string AnimationPath = "Assets/Fish/Animation";
    //美术给的原始图片路径
    private static string ImagePath = Application.dataPath + "/Fish/Pic/Fish";

	/*
    [MenuItem("Build/BuildAnimaiton")]
    static void BuildAniamtion()
    {
        DirectoryInfo raw = new DirectoryInfo(ImagePath);
        foreach (DirectoryInfo dictorys in raw.GetDirectories())
        {
            List<AnimationClip> clips = new List<AnimationClip>();
            foreach (DirectoryInfo dictoryAnimations in dictorys.GetDirectories())
            {
                //每个文件夹就是一组帧动画，这里把每个文件夹下的所有图片生成出一个动画文件
                clips.Add(BuildAnimationClip(dictoryAnimations, false));
            }
            //把所有的动画文件生成在一个AnimationController里
            UnityEditor.Animations.AnimatorController controller = BuildAnimationController(clips, dictorys.Name);

            //最后生成程序用的Prefab文件
            BuildPrefab(dictorys, controller, true);
        }
    }
    */

    [MenuItem("Build/BuildFishAnimation")]
    static void BuildAnimaitonOnceAndLoop()
    {
        DirectoryInfo raw = new DirectoryInfo(ImagePath);
		/*
        foreach (DirectoryInfo dictorys in raw.GetDirectories())
        {
			Debug.Log (dictorys.FullName);
            foreach (DirectoryInfo dictoryAnimations in dictorys.GetDirectories())
            {
				Debug.Log (dictoryAnimations.Name);
                List<AnimationClip> clips = new List<AnimationClip>();
                //每个文件夹就是一组帧动画，这里把每个文件夹下的所有图片生成出一个动画文件
                clips.Add(BuildAnimationClip(dictoryAnimations, true));

                //把所有的动画文件生成在一个AnimationController里
                UnityEditor.Animations.AnimatorController controller = BuildAnimationController(clips, dictoryAnimations.Name);
                //最后生成程序用的Prefab文件
                BuildPrefab(dictoryAnimations, controller, false);
            }

        }
        */

		foreach (DirectoryInfo dictorys in raw.GetDirectories())
		{
			Debug.Log (dictorys.Name);
			List<AnimationClip> clips = new List<AnimationClip>();
			//每个文件夹就是一组帧动画，这里把每个文件夹下的所有图片生成出一个动画文件
			clips.Add(BuildAnimationClip(dictorys, true));

			//把所有的动画文件生成在一个AnimationController里
			UnityEditor.Animations.AnimatorController controller = BuildAnimationController(clips, dictorys.Name);
			//最后生成程序用的Prefab文件
			BuildPrefab(dictorys, controller, false);

		}
    }


    static AnimationClip BuildAnimationClip(DirectoryInfo dictorys, bool isLoop)
    {
        string animationName = dictorys.Name;
        //查找所有图片，因为我找的测试动画是.jpg 
        FileInfo[] images = dictorys.GetFiles("*.png");
        AnimationClip clip = new AnimationClip();
        //AnimationUtility.SetAnimationType(clip, ModelImporterAnimationType.Generic);
        EditorCurveBinding curveBinding = new EditorCurveBinding();
        curveBinding.type = typeof(SpriteRenderer);
        curveBinding.path = "";
        curveBinding.propertyName = "m_Sprite";
        ObjectReferenceKeyframe[] keyFrames = new ObjectReferenceKeyframe[images.Length];
        //动画长度是按秒为单位，1/10就表示1秒切10张图片，根据项目的情况可以自己调节
        float frameTime = 1 / 10f;
        for (int i = 0; i < images.Length; i++)
        {
			//Debug.Log (images[i].FullName);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(DataPathToAssetPath(images[i].FullName));
            keyFrames[i] = new ObjectReferenceKeyframe();
            keyFrames[i].time = frameTime * i;
            keyFrames[i].value = sprite;
        }
        //动画帧率，30比较合适
        clip.frameRate = 12;

        //有些动画我希望天生它就动画循环
        if (isLoop)
        {
            //设置idle文件为循环动画
            SerializedObject serializedClip = new SerializedObject(clip);
            AnimationClipSettings clipSettings = new AnimationClipSettings(serializedClip.FindProperty("m_AnimationClipSettings"));
            clipSettings.loopTime = true;
            serializedClip.ApplyModifiedProperties();
        }
        string parentName = System.IO.Directory.GetParent(dictorys.FullName).Name;
        System.IO.Directory.CreateDirectory(AnimationPath + "/" + parentName);
        AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keyFrames);
        AssetDatabase.CreateAsset(clip, AnimationPath + "/" + parentName + "/" + animationName + ".anim");
        AssetDatabase.SaveAssets();
        return clip;
    }

    static UnityEditor.Animations.AnimatorController BuildAnimationController(List<AnimationClip> clips, string name)
    {
        UnityEditor.Animations.AnimatorController animatorController = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath(AnimationControllerPath + "/" + name + ".controller");
        UnityEditor.Animations.AnimatorControllerLayer layer = animatorController.layers[0];

        AnimatorStateMachine sm = layer.stateMachine;	
        foreach(AnimationClip newClip in clips)	{	
            AnimatorState state = sm.AddState(newClip.name);	
            state.motion = newClip;
//             if(newClip.name == "idle")
//             {	
//                 sm.defaultState = state;	
//             }	
        }	
        AssetDatabase.SaveAssets();	return animatorController;	
    }


    static void BuildPrefab(DirectoryInfo dictorys, UnityEditor.Animations.AnimatorController animatorCountorller, bool isDirectory)
    {
        //生成Prefab 添加一张预览用的Sprite
        FileInfo images;
        if(isDirectory)
        {
            images = dictorys.GetDirectories()[0].GetFiles("*.png")[0];
        }
        else
            images = dictorys.GetFiles("*.png")[0];

        GameObject go = new GameObject();
        go.name = dictorys.Name;
        SpriteRenderer spriteRender = go.AddComponent<SpriteRenderer>();
        spriteRender.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(DataPathToAssetPath(images.FullName));
        Animator animator = go.AddComponent<Animator>();
        animator.runtimeAnimatorController = animatorCountorller;
		go.AddComponent<GameFish.GOFish> ();


        GameObject shadow = new GameObject();
        shadow.name = "shadow";
        SpriteRenderer spriteRender1 = shadow.AddComponent<SpriteRenderer>();
        spriteRender1.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(DataPathToAssetPath(images.FullName));
        spriteRender1.color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
        Animator animator1 = shadow.AddComponent<Animator>();
        animator1.runtimeAnimatorController = animatorCountorller;

        shadow.transform.parent = go.transform;
        shadow.transform.position = new Vector3(30.0f / 100.0f, -40.0f / 100.0f, 0.0f);



        PrefabUtility.CreatePrefab(PrefabPath + "/" + go.name + ".prefab", go);
        DestroyImmediate(go);
    }


    public static string DataPathToAssetPath(string path)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
            return path.Substring(path.IndexOf("Assets\\"));
        else
            return path.Substring(path.IndexOf("Assets/"));
    }


    class AnimationClipSettings
    {
        SerializedProperty m_Property;

        private SerializedProperty Get(string property) { return m_Property.FindPropertyRelative(property); }

        public AnimationClipSettings(SerializedProperty prop) { m_Property = prop; }

        public float startTime { get { return Get("m_StartTime").floatValue; } set { Get("m_StartTime").floatValue = value; } }
        public float stopTime { get { return Get("m_StopTime").floatValue; } set { Get("m_StopTime").floatValue = value; } }
        public float orientationOffsetY { get { return Get("m_OrientationOffsetY").floatValue; } set { Get("m_OrientationOffsetY").floatValue = value; } }
        public float level { get { return Get("m_Level").floatValue; } set { Get("m_Level").floatValue = value; } }
        public float cycleOffset { get { return Get("m_CycleOffset").floatValue; } set { Get("m_CycleOffset").floatValue = value; } }

        public bool loopTime { get { return Get("m_LoopTime").boolValue; } set { Get("m_LoopTime").boolValue = value; } }
        public bool loopBlend { get { return Get("m_LoopBlend").boolValue; } set { Get("m_LoopBlend").boolValue = value; } }
        public bool loopBlendOrientation { get { return Get("m_LoopBlendOrientation").boolValue; } set { Get("m_LoopBlendOrientation").boolValue = value; } }
        public bool loopBlendPositionY { get { return Get("m_LoopBlendPositionY").boolValue; } set { Get("m_LoopBlendPositionY").boolValue = value; } }
        public bool loopBlendPositionXZ { get { return Get("m_LoopBlendPositionXZ").boolValue; } set { Get("m_LoopBlendPositionXZ").boolValue = value; } }
        public bool keepOriginalOrientation { get { return Get("m_KeepOriginalOrientation").boolValue; } set { Get("m_KeepOriginalOrientation").boolValue = value; } }
        public bool keepOriginalPositionY { get { return Get("m_KeepOriginalPositionY").boolValue; } set { Get("m_KeepOriginalPositionY").boolValue = value; } }
        public bool keepOriginalPositionXZ { get { return Get("m_KeepOriginalPositionXZ").boolValue; } set { Get("m_KeepOriginalPositionXZ").boolValue = value; } }
        public bool heightFromFeet { get { return Get("m_HeightFromFeet").boolValue; } set { Get("m_HeightFromFeet").boolValue = value; } }
        public bool mirror { get { return Get("m_Mirror").boolValue; } set { Get("m_Mirror").boolValue = value; } }
    }

}