using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    // Ҫ�л�����Ŀ�곡������
    public string targetSceneName = "TargetScene";

    // ������������ť����
    public void SwitchSceneWithDelay()
    {
        StartCoroutine(DelayAndSwitch());
    }

    // Э�̣��ӳ� 2 ����л�����
    IEnumerator DelayAndSwitch()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(targetSceneName);
    }
}
