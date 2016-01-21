using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;

public class AnimatorTool : MonoBehaviour
{

    /// <summary>
    /// 菜单方法，遍历文件夹创建Animation Controller
    /// </summary>
    [MenuItem("Tools/CreateAnimator")]
    static void CreateAnimationAssets()
    {
        string rootFolder = "Assets/Resources/fbx/";
        if (!Directory.Exists(rootFolder))
        {
            Directory.CreateDirectory(rootFolder);
            return;
        }
        // 遍历目录，查找生成controller文件
        var folders = Directory.GetDirectories(rootFolder);
        foreach (var folder in folders)
        {
            DirectoryInfo info = new DirectoryInfo(folder);
            string folderName = info.Name;
            // 创建animationController文件
            AnimatorController aController =
                AnimatorController.CreateAnimatorControllerAtPath(string.Format("{0}/animation.controller", folder));
            // 得到其layer
            var layer = aController.layers[0];
            // 绑定动画文件
            AddStateTranstion(string.Format("{0}/{1}_model.fbx", folder, folderName), layer);
            // 创建预设
            GameObject go = LoadFbx(folderName);
            PrefabUtility.CreatePrefab(string.Format("{0}/{1}.prefab", folder, folderName), go);
            DestroyImmediate(go);
        }


    }

    /// <summary>
    /// 添加动画状态机状态
    /// </summary>
    /// <param name="path"></param>
    /// <param name="layer"></param>
    private static void AddStateTranstion(string path, AnimatorControllerLayer layer)
    {
        AnimatorStateMachine sm = layer.stateMachine;
        // 根据动画文件读取它的AnimationClip对象
        var datas = AssetDatabase.LoadAllAssetsAtPath(path);
        if (datas.Length == 0)
        {
            Debug.Log(string.Format("Can't find clip in {0}", path));
            return;
        }
        // 先添加一个默认的空状态
        var emptyState = sm.AddState("empty");
        sm.AddAnyStateTransition(emptyState);
        // 遍历模型中包含的动画片段，将其加入状态机中
        foreach (var data in datas)
        {
            if (!(data is AnimationClip))
                continue;
            var newClip = data as AnimationClip;
            if (newClip.name.StartsWith("__"))
                continue;
            // 取出动画名字，添加到state里面
            var state = sm.AddState(newClip.name);
            state.motion = newClip;
            // 把State添加在Layer里面
            sm.AddAnyStateTransition(state);
        }

    }

    /// <summary>
    /// 生成带动画控制器的对象
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject LoadFbx(string name)
    {
        var obj = Instantiate(Resources.Load(string.Format("fbx/{0}/{0}_model", name))) as GameObject;
        obj.GetComponent<Animator>().runtimeAnimatorController =
            Resources.Load<RuntimeAnimatorController>(string.Format("fbx/{0}/animation", name));
        return obj;
    }
}
