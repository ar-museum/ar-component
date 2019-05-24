using System;
using System.Collections;
using Assets.Scripts.AR_TEAM.Http;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class AudioPlayerTests
    {
        private string songPath  = Application.dataPath + "/StreamingAssets/Sound/test.wav"; // or other existing .wav from StreamingAssets/Sound
        // test 1 Lipan Matei
        [UnityTest]
        public IEnumerator AudioPlayer_PlayMusic()
        {
            //Arrange
            SceneManager.LoadScene("PreloadScene");
            while (SceneManager.GetActiveScene().name != "MenuScene")
            {
                yield return new WaitForSeconds(1);
            }

            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);
            
            var audioPlayer = GameObject.Find("Audio Source").GetComponent<AudioPlayer>();
            var audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
            //Act

            audioPlayer.PlayMusic(songPath);
            yield return new WaitForSeconds(1);

            //Assert
            Assert.IsTrue(audioSource.isPlaying);

        }

        // test 2 Lipan Matei
        [UnityTest]
        public IEnumerator AudioPlayer_PauseMusic()
        {
            //Arrange
            SceneManager.LoadScene("PreloadScene");
            while (SceneManager.GetActiveScene().name != "MenuScene")
            {
                yield return new WaitForSeconds(1);
            }

            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);
            
            var audioPlayer = GameObject.Find("Audio Source").GetComponent<AudioPlayer>();
            var audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
            //Act

            audioPlayer.PlayMusic(songPath);
            yield return new WaitForSeconds(1);
            audioPlayer.PauseMusic();

            //Assert
            Assert.IsFalse(audioSource.isPlaying);

        }

        // test 3 Lipan Matei
        [UnityTest]
        public IEnumerator AudioPlayer_ResumeMusic()
        {
            //Arrange
            SceneManager.LoadScene("PreloadScene");
            while (SceneManager.GetActiveScene().name != "MenuScene")
            {
                yield return new WaitForSeconds(1);
            }

            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);

            var audioPlayer = GameObject.Find("Audio Source").GetComponent<AudioPlayer>();
            var audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
            //Act

            audioPlayer.PlayMusic(songPath);
            yield return new WaitForSeconds(1);
            var time_paused = audioSource.time;
            audioPlayer.PauseMusic();
            audioPlayer.PlayMusic();
            yield return new WaitForSeconds(1);
            var time_resumed = audioSource.time - 1;
            //Assert
            Assert.IsTrue(System.Math.Abs(time_paused - time_resumed) < 0.1);

        }

        // test 4 Lipan Matei
        [UnityTest]
        public IEnumerator AudioPlayer_ReplayMusic()
        {
            //Arrange
            SceneManager.LoadScene("PreloadScene");
            while (SceneManager.GetActiveScene().name != "MenuScene")
            {
                yield return new WaitForSeconds(1);
            }

            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);

            var audioPlayer = GameObject.Find("Audio Source").GetComponent<AudioPlayer>();
            var audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
            //Act

            audioPlayer.PlayMusic(songPath);
            yield return new WaitForSeconds(2);
            audioPlayer.PauseMusic();
            audioPlayer.PlayMusic();
            yield return new WaitForSeconds(1);
            var time_1sec_afster_paused = audioSource.time;
            audioPlayer.ReplayMusic();
            yield return new WaitForSeconds(1);
            var time_1sec_afster_restart = audioSource.time;

            //Assert
            Assert.IsFalse(System.Math.Abs(time_1sec_afster_paused-time_1sec_afster_restart) < 0.5);
            Assert.IsTrue(System.Math.Abs(time_1sec_afster_restart - 1) < 0.1);
        }

        // test 5 Lipan Matei
        [UnityTest]
        public IEnumerator AudioPlayer_StopMusic()
        {
            //Arrange
            SceneManager.LoadScene("PreloadScene");
            while (SceneManager.GetActiveScene().name != "MenuScene")
            {
                yield return new WaitForSeconds(1);
            }

            SceneManager.LoadScene("ARScene");
            yield return new WaitForSeconds(1);

            var audioPlayer = GameObject.Find("Audio Source").GetComponent<AudioPlayer>();
            var audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
            //Act

            audioPlayer.PlayMusic(songPath);
            yield return new WaitForSeconds(1);
            audioPlayer.StopMusic();
            var time_after_stop = audioSource.time;
            //Assert
            Assert.IsFalse(audioSource.isPlaying);
            Assert.AreEqual(0, time_after_stop);
        }
    }
}
