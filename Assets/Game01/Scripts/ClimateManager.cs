using UnityEditor.ShaderGraph;
using UnityEngine;

public class ClimateManager : MonoBehaviour
{
    private int Hp = 100;
    private int Speed = 10;
    private float currentTime = 0f;
    [SerializeField] public enum Climate
        {
            normal,     //通常
            heat,       //猛暑
            cold        //寒冷
        }

    [SerializeField] private Climate CurrentClimate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //気候の判定
        switch(CurrentClimate)
        {
            case Climate.normal:
                Climate_Nomal();
                break;

            case Climate.heat:
                Climate_Heat();
                break;

            case Climate.cold:
                Climate_Cold();
                break;
        }
    }

    private void Climate_Nomal()
    {
        //Debug.Log("normal");
    }

    private void Climate_Heat()
    {
        //Debug.Log("heat");
        //前のフレームから経過した秒数を加算
        currentTime += Time.deltaTime;
        //毎秒HP減少
        if (currentTime >= 1.0f)
        {
            Hp -= 1;
            Debug.Log(Hp);
            currentTime = 0;
        }

    }

    private void Climate_Cold()
    {
        //Debug.Log(Speed);
        //前のフレームから経過した秒数を加算
        currentTime += Time.deltaTime;
        //3秒経つと死亡
        if (currentTime >= 3.0f)
        {
            Debug.Log("死亡");
            currentTime = 0;
        }
    }
}
