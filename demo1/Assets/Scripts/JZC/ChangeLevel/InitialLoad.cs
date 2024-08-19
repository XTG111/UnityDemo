using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class InitialLoad : MonoBehaviour
{
    public AssetReference emptyBase;

    private void Awake()
    {
        Addressables.LoadSceneAsync(emptyBase);
    }
}
