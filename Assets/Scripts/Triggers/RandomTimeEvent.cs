using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTimeEvent : MonoBehaviour
{

    [Header("PlayingSoundSettings")]
    public SoundBoard SB;
    public float ChoosenFloat;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomSound());
    }


    IEnumerator RandomSound()
    {
        while (true)
        {
            if (SB.source.isPlaying == false)
            {
                int wait_time = Random.Range(15, 50);
                yield return new WaitForSeconds(wait_time);

                ChooseNummber();

                if (ChoosenFloat == 0)
                {
                    SB.setVolume(0.3f);
                    SB.PlayClip(ChoosenFloat);
                }
                else
                {
                    SB.setVolume(0.9f);
                    SB.PlayClip(ChoosenFloat);
                }




            }
            yield return null;
        }
    }

    public void ChooseNummber()
    {
        ChoosenFloat = Random.Range(0, SB.clips.Length);
    }


}
