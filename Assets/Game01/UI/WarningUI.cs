using UnityEngine;
using TMPro;
using System.Collections;

public class WarningUI : MonoBehaviour
{
    public static WarningUI Instance { get; private set; }
    [SerializeField] private float hideTime = 2f; 

    [SerializeField] private TextMeshProUGUI warningText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void ShowWarning(string message)
    {
        warningText.text = message;
        warningText.gameObject.SetActive(true);
        StartCoroutine(HideWarningAfterDelay(hideTime));
    }
    
    private IEnumerator HideWarningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        warningText.gameObject.SetActive(false);
    }

    // 非表示関数。外部から呼び出し可能。
    public void HideWarning()
    {
        StopAllCoroutines(); // コルーチンを停止
        warningText.gameObject.SetActive(false);  // 即座に非表示にする
    }
}