using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Video;

public class VideoSequence : MonoBehaviour {

    public double PlayerClipTime {
        get { return videoPlayer.time; }
    }

    public bool videoSequenceIsLooping = true;
    public VideoPlayer videoPlayer;
    public VideoClip[] videoClipsArray;
    System.Random randVid = new System.Random ();
    int currentVideo = 0;

    void Start () {
        videoPlayer = gameObject.GetComponent<VideoPlayer> ();
        videoPlayer.isLooping = false;
        randVid.Shuffle (videoClipsArray);
        for (int i = 0; i < videoClipsArray.Length; i++) {
            print (i + "    " + videoClipsArray[i].name);
        }
    }

    IEnumerator PlayVideoArray () {
        videoPlayer.targetTexture.Release ();
        videoPlayer.clip = videoClipsArray[currentVideo];
        videoPlayer.Play ();
        yield return new WaitForSeconds ((float) PlayerClipTime);
        currentVideo++;
        if (currentVideo > videoClipsArray.Length) {
            if (videoSequenceIsLooping) {
                videoPlayer.Stop ();
            } else {
                currentVideo = 0;
            }
        } else {
            StartCoroutine (PlayVideoArray ());
        }
    }
}

static class RandomExtensions {
    public static void Shuffle<T> (this System.Random rng, T[] array) {
        int n = array.Length;
        while (n > 1) {
            int k = rng.Next (n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}