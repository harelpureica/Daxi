using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class shopButtons : MonoBehaviour
{
    public GameObject SV_Skin;
    public GameObject SV_follower;
    public GameObject SV_life;
    public GameObject SV_power;

    public Button skinOnBT;
    public Button followerOnBT;
    public Button lifeOnBT;
    public Button powerOnBT;

    public TextMeshProUGUI subTitle; 

    private void Start()
    {
        skinOn();
    }
    public void skinOn()
    {
        SV_Skin.SetActive(true);
        SV_follower.SetActive(false);
        SV_life.SetActive(false);
        SV_power.SetActive(false);

        skinOnBT.gameObject.SetActive(true);
        followerOnBT.gameObject.SetActive(false);
        lifeOnBT.gameObject.SetActive(false);
        powerOnBT.gameObject.SetActive(false);

        subTitle.text = "The main character";
    }
    public void followerOn()
    {
        SV_Skin.SetActive(false);
        SV_follower.SetActive(true);
        SV_life.SetActive(false);
        SV_power.SetActive(false);

        skinOnBT.gameObject.SetActive(false);
        followerOnBT.gameObject.SetActive(true);
        lifeOnBT.gameObject.SetActive(false);
        powerOnBT.gameObject.SetActive(false);

        subTitle.text = "The flying pet";
    }
    public void lifeOn()
    {
        SV_Skin.SetActive(false);
        SV_follower.SetActive(false);
        SV_life.SetActive(true);
        SV_power.SetActive(false);

        skinOnBT.gameObject.SetActive(false);
        followerOnBT.gameObject.SetActive(false);
        lifeOnBT.gameObject.SetActive(true);
        powerOnBT.gameObject.SetActive(false);

        subTitle.text = "lifes";
    }
    public void powerOn()
    {
        SV_Skin.SetActive(false);
        SV_follower.SetActive(false);
        SV_life.SetActive(false);
        SV_power.SetActive(true);

        skinOnBT.gameObject.SetActive(false);
        followerOnBT.gameObject.SetActive(false);
        lifeOnBT.gameObject.SetActive(false);
        powerOnBT.gameObject.SetActive(true);

        subTitle.text = "powers";
    }
}
