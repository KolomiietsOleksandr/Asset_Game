using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class InSailTiimer : MonoBehaviour
{
    [SerializeField] private TMP_Text Time_Text;

    int days = 0;
    public static int hours = 0;
    public static int minutes = 0;
    int residueH = 0;
    int residueM = 0;

    public void TimeInSail()
    {
        int period_of_hour = Random.Range(0, 3);
        int period_of_minutes = Random.Range(15, 45);

        if (minutes < 60)
        {
            minutes += period_of_minutes;
        }
        if (minutes >= 60)
        {
            hours += 1;
            residueM = minutes - 60;
            minutes = residueM;
        }

        if (hours < 24)
        {
            hours += period_of_hour;
        }

        if (hours >= 24)
        {
            days += 1;
            residueH = hours - 24;
            hours = residueH;
        }

        Time_Text.text = $"IN SAIL: {days} days {hours} hours {minutes} minutes";
    }
}
