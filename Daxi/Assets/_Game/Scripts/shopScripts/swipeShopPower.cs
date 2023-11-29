using UnityEngine;
using UnityEngine.UI;

public class swipeShopPower : MonoBehaviour
{
    public GameObject scroolbar;
    public float current = 1f;
    public float others = 0.7f;
    public TMPro.TextMeshProUGUI title;
    public TMPro.TextMeshProUGUI coast;
    public TMPro.TextMeshProUGUI selectOrBuy;
    float scrool_pos = 0;
    float[] pos;

    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 0.99f / (pos.Length - 1);

        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        if (Input.GetMouseButton(0))
        {
            scrool_pos = scroolbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scrool_pos < pos[i] + (distance / 2) && scrool_pos > pos[i] - (distance / 2))
                {
                    scroolbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scroolbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            transform.GetChild(i).GetChild(1).gameObject.SetActive(false);

            if (scrool_pos < pos[i] + (distance / 2) && scrool_pos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(current, current), 0.1f);
                //change the title and the coast to the current 
                title.GetComponent<TMPro.TextMeshProUGUI>().text = transform.GetChild(i).GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text;
                coast.GetComponent<TMPro.TextMeshProUGUI>().text = transform.GetChild(i).GetChild(1).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text;

                if (coast.GetComponent<TMPro.TextMeshProUGUI>().text == "free")
                {
                    selectOrBuy.GetComponent<TMPro.TextMeshProUGUI>().text = "Select";
                }
                else
                {
                    selectOrBuy.GetComponent<TMPro.TextMeshProUGUI>().text = "Buy";
                }
                for (int a = 0; a < pos.Length; a++)
                {
                    if (a != i)
                    {
                        transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(others, others), 0.1f);

                    }
                }
            }
        }

    }
}



