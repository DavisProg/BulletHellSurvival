using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int seconds;
    public int minutes;
    [SerializeField] private TMP_Text timer;
    private Coroutine routine;
    
    void Start()
    {
        UnPause();
    }
    public void UnPause()
    {
        routine = StartCoroutine(Tick());
    }
    public void Pause()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
    }
    IEnumerator Tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            seconds++;

            if(seconds >= 60)
            {
                seconds -= 60;
                minutes++;
            }
            string timerText = "";

            if (minutes >= 10)
            {
                timerText += minutes;
            }
            else
            {
                timerText += "0" + minutes;
            }
            timerText += ":";
            if (seconds >= 10)
            {
                timerText += seconds;
            }
            else
            {
                timerText += "0" + seconds;
            }

            timer.text = timerText;
        }
    }
}
