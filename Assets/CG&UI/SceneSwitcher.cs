using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    // 要切换到的目标场景名称
    public string targetSceneName = "TargetScene";

    // 公共方法供按钮调用
    public void SwitchSceneWithDelay()
    {
        StartCoroutine(DelayAndSwitch());
    }

    // 协程：延迟 2 秒后切换场景
    IEnumerator DelayAndSwitch()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(targetSceneName);
    }
}
