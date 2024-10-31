using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class Utils 
{
    public static readonly WaitForFixedUpdate m_WaitForFixedUpdate = new WaitForFixedUpdate();
    public static readonly WaitForEndOfFrame m_WaitForEndOfFrame = new WaitForEndOfFrame();
    private static readonly Dictionary<float, WaitForSeconds> m_WaitForSecondsdict = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds WaitForSecond(float pWaitTime)
    {
        WaitForSeconds wfs;

        if (m_WaitForSecondsdict.TryGetValue(pWaitTime, out wfs))
        {
            return wfs;
        }
        else
        {
            wfs = new WaitForSeconds(pWaitTime);
            m_WaitForSecondsdict.Add(pWaitTime, wfs);
            return wfs;
        }
    }

    #region ��巹���� �ε��ϱ�
    public static void LoadAssetAndHandle<T>(string address, System.Action<T> onLoaded)
    {
        Addressables.LoadAssetAsync<T>(address).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                T loadedAsset = handle.Result;
                onLoaded(loadedAsset);
            }
            else
            {
                Debug.LogError("�ڻ� �ε� ����: " + handle.OperationException);
            }
        };
    }

    public static T OnLoadComplete<T>(T param)
    {
        return param;
    }

    #endregion

    #region ��巹���� �󺧷� �ε��ϱ�
    public static void LoadAssetsByLabelAndCache<T>(string label, System.Action<List<T>> onLoaded)
    {
        Addressables.LoadAssetsAsync<T>(label, null).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                List<T> loadedAssets = new List<T>(handle.Result);
                onLoaded(loadedAssets);
            }
            else
            {
                Debug.LogError("�ڻ� �ε� ����: " + handle.OperationException);
            }
        };
    }
    #endregion

    #region ���ӿ�����Ʈ ������ ���� (���߿� �ʿ����� ����..)
    public static GameObject Istantiate(GameObject pPrefab, Transform parent = null)
    {
        GameObject tempPrefab = GameObject.Instantiate(pPrefab, parent);

        if(tempPrefab == null)
        {
            Debug.Log("������ �������� �������� �ʽ��ϴ�.");
        }
        return tempPrefab;
    }

    #endregion
}
