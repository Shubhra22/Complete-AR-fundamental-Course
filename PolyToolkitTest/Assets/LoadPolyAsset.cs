using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyToolkit;
using UnityEngine.UI;
using System;
public class LoadPolyAsset : MonoBehaviour
{
    public int pageSize;
    public Text statusText;
    public Transform buttonParent;

    string pageination = null;

    List<Transform> buttons;

    int thumbnailCount;
    // Use this for initialization
    void Start()
    {
        buttons = new List<Transform>();
        foreach (Transform button in buttonParent)
        {
            buttons.Add(button);
        }
    }

    public void LoadAssets()
    {
        PolyListAssetsRequest polyRequest = PolyListAssetsRequest.Featured();
        polyRequest.pageSize = pageSize;
        polyRequest.curated = true;
        polyRequest.orderBy = PolyOrderBy.BEST;
        polyRequest.pageToken = pageination;
        
        PolyApi.ListAssets(polyRequest, LoadAssetsCallback);
    }

    void LoadAssetsCallback(PolyStatusOr<PolyListAssetsResult> result)
    {
        thumbnailCount = 0;
        if (!result.Ok)
        {
            Debug.LogError("Failed to get assets. Reason: " + result.Status);
            statusText.text = "ERROR: " + result.Status;
            return;
        }
        foreach (var item in result.Value.assets)
        {
            PolyApi.FetchThumbnail(item, FetchThumbnailCallback);
        }
        pageination = result.Value.nextPageToken;
    }


    void FetchThumbnailCallback(PolyAsset asset, PolyStatus status)
    {
        if(!status.ok)
        {
            Debug.LogError("Failed to Fetch Thumbnail. Reason: " + status.errorMessage);
            statusText.text = "ERROR: " + status.errorMessage;
            return;
        }
        buttons[thumbnailCount].GetComponent<RawImage>().texture = asset.thumbnailTexture;
        buttons[thumbnailCount].GetComponent<Button>().onClick.AddListener(()=> OnClickImportAsset(asset));
        thumbnailCount++;
        //print(asset.displayName);
    }

    void OnClickImportAsset(PolyAsset asset)
    {
        PolyApi.Import(asset, PolyImportOptions.Default(), ImportAssetCallback);
    }

    private void ImportAssetCallback(PolyAsset asset, PolyStatusOr<PolyImportResult> result)
    {
        if(!result.Ok)
        {
            Debug.LogError("Failed to Fetch Thumbnail. Reason: " + result.Status);
            statusText.text = "ERROR: " + result.Status;
        }

        buttonParent.gameObject.SetActive(false);

        result.Value.gameObject.transform.position = new Vector3(0,0,30);
        //result.Value.gameObject.AddComponent<Rotate>();

        GameObject outputObject = result.Value.gameObject;
        InputController.instance.ControlModel(outputObject, asset.displayName);
    }
}
