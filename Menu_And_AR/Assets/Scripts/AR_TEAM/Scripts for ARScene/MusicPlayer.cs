using UnityEngine;

namespace Assets.Scripts.AR_TEAM {
    public class MusicPlayer : MonoBehaviour {
        private AudioClip MusicClip { get; set; }
        private AudioSource MusicSource { get; set; }
        private bool HasStartedPlaying { get; set; }

        public void Set(string path, AudioSource source)
        {
            using (var www = new WWW("file:///" + path)) {
                MusicClip = www.GetAudioClip();
            }
            MusicSource = source;
            MusicSource.clip = MusicClip;
        }

        public void Start() 
        {
            MusicSource.Play();
            HasStartedPlaying = true;
        }

        public void Play()
        {
            if (!MusicSource.isPlaying)
            {
                MusicSource.UnPause();
            }
        }

        public void Pause()
        {
            if (MusicSource.isPlaying)
            {
                MusicSource.Pause();
            }
        }

        public void Toggle()
        {
            if (HasStartedPlaying)
            {
                if (MusicSource.isPlaying)
                {
                    Pause();
                } 
                else
                {
                    Play();
                }
            }
            else
            {
                Start();
            }
        }
    }
}