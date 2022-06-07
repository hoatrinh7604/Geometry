using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentController : MonoBehaviour
{
    [SerializeField] GameObject buttonNumberPreFab;
    [SerializeField] Transform content;

    [SerializeField] GameObject[] squares;
    [SerializeField] GameObject[] rectangles;
    [SerializeField] GameObject[] circles;
    [SerializeField] GameObject[] triangles;

    private List<int> list;

    public void SpawButton(List<int> arr)
    {
        ClearContent();

        for(int i = 0; i < arr.Count; i++)
        {
            GameObject item = new GameObject();
            item = Instantiate(buttonNumberPreFab, Vector3.zero, Quaternion.identity, content);
            item.transform.localPosition = Vector3.zero;

            item.GetComponent<ButtonNumberController>().SetInfo(arr[i]);
        }
    }

    private void ClearContent()
    {
        for(int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
}
