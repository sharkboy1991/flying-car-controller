using UnityEngine;
using UnityEngine.UI;

public class Telemetry : MonoBehaviour
{
    public Text hAxis;
    public Text vAxis;

    public Image gasPedal;
    public Image brakePedal;

    public Text kph;
    public Text mph;

    public void Axis(float v, float h)
    {
        hAxis.text = "Hor_Axis = " + h;
        vAxis.text = "Ver_Axis = " + v;
    }

    public void CarSpeed(float k, float m)
    {
        kph.text = "KPH = " + k;
        mph.text = "MPH = " + m;
    }

    public void GasPedal(bool gas)
    {
        if (gas)
        {
            gasPedal.enabled = true;
        }
        else
        {
            gasPedal.enabled = false;
        }
    }

    public void BrakePedal(bool brake)
    {
        if (brake)
        {
            brakePedal.enabled = true;
        }
        else
        {
            brakePedal.enabled = false;
        }
    }
}
