using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberContentController : MonoBehaviour
{
    [SerializeField] GameObject[] shadowPrefab;
    [SerializeField] Transform content;
    [SerializeField] List<GameObject> list = new List<GameObject>();

    public void Spaw(int index)
    {
        ClearContent();
        list.Clear();
        list = new List<GameObject>();

        GameObject item = Instantiate(shadowPrefab[index], Vector3.zero, Quaternion.identity, content);
        item.transform.localPosition = Vector3.zero;
        list.Add(item);
        GamePlayController.Instance.currentTriangle = item.GetComponent<ShadowInfo>().GetTriangle();
    }

    public void UpdateInfo(int index, int value)
    {
        list[index].GetComponent<ShowNumberController>().SetInfo(value);
    }

    private void ClearContent()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
}
