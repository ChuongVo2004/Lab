using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Thêm dòng này để sử dụng TextMeshPro

public class RaycastExample : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    TextMeshProUGUI destroyedCountText;  // Thêm thành phần TextMeshPro

    private int destroyedCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateDestroyedCountText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 5, layerMask))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.blue);
            Destroy(hit.transform.gameObject);
            destroyedCount++;
            UpdateDestroyedCountText();
        }
        Debug.DrawRay(transform.position, transform.forward * 5, Color.yellow);
    }

    void UpdateDestroyedCountText()
    {
        destroyedCountText.text = "Loot: " + destroyedCount.ToString();
    }
}
