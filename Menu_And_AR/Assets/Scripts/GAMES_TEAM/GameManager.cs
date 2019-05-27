using System.Collections;
using System.Collections.Generic;
using manageQuestions;
using UnityEngine;
using UnityEngine.UI;
using meniu;
using UnityEngine.SceneManagement;
using GameRequest;
using TMPro;

namespace Trivia
{
    [System.Serializable]
    public class GameManager : MonoBehaviour
    {
        //dificulty easy=1,medium1.5,hard =2
        public static List<Question> questions;
        public static string language;
        public static string difficulty;
        public static float scoreDifficulty;
        private string music;
        //static string language;//s-ar putea sa nu avem nevoie de el
        public static List<JsonFile> jsonFiles;

        bool wasClicked = false;
        public static float score;
        [SerializeField]//to be setted from unity
        private static float mainTimer;

        private float startTime;
        private float fixedTime;
        [SerializeField]
        private Text timer;

        private Question currentQuestion;
        [SerializeField]
        private TextMeshProUGUI Score;
        [SerializeField]
        private TextMeshProUGUI toBeDisplayed;
        [SerializeField]
        public Button b1, b2, b3;
        public Button bb;
        [SerializeField]
        private float timeBetweenQ = 1f;
        [SerializeField]
        public Button quit;
        public bool flag;
        public void sendJSFiles()
        {
            Apelare apel = new Apelare();
            StartCoroutine(apel.StartTakingJsonNames());
            jsonFiles = Apelare.JsonFiles;

        }

        public void Start()
        {
            sendJSFiles();
            wasClicked = false;
            language = MenuManager.getLanguage();
            Debug.Log(language);
            music = MenuManager.getMusic();
            difficulty = MenuManager.getDifficulty();
            Debug.Log(difficulty);
            if (questions == null)
            {
                QuestionArray quests;

                JsonToObject deserializer = new JsonToObject();
                quests = deserializer.loadJson();
                questions = quests.questions;

                Score.text = "Score: ";
                score = 0;
            }

            difficulty = MenuManager.getDifficulty();
            language = MenuManager.getLanguage();
            if (difficulty == "Easy") { mainTimer = 20.04f; scoreDifficulty = 1; }//setam dificultate +score+timp pe easy
            else if (difficulty == "Medium") { mainTimer = 16.54f; scoreDifficulty = 2; }//setam pentru mediu
            else { mainTimer = 10.04f; scoreDifficulty = 3; }

            startTime = mainTimer;
            timer.text = startTime.ToString();
            setCurrentQuestion();
            setCurrentButtons();

            Score.text = "Score: " + score.ToString();
            //Debug.Log(currentQuestion.getQuestion());
        }
        public static string getDifficulty()
        {
            return difficulty;
        }
        public static string getLanguage()
        {
            return language;
        }
        public void setCurrentQuestion()
        {
            // Debug.Log(questions.Count);
            if (questions != null)
            {
                int randomIndexQuestion = Random.Range(0, questions.Count);

                currentQuestion = questions[randomIndexQuestion];
                toBeDisplayed.text = currentQuestion.question;
            }

        }
        public void setCurrentButtons()
        {

            if (questions != null)
            {
                //Debug.Log(b1.GetComponentInChildren<TextMeshPro>().text);
                b1.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.getAllAnswers()[0];
                b2.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.getAllAnswers()[1];
                b3.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.getAllAnswers()[2];
            }
        }
        public Button getB1()
        {
            return b1;
        }

        public Question getCurrentQuestion()
        {
            return this.currentQuestion;
        }
        IEnumerator nextQuestion()
        {
            questions.Remove(currentQuestion);
            yield return new WaitForSeconds(timeBetweenQ);
            if (questions.Count != 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            else
            {
                questions = null;
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                MenuManager.setDifficulty(difficulty);
                MenuManager.setLanguage(language);
                MenuManager.setMusic(music);
                SceneManager.LoadScene("GameOverScene");
            }
        }

        public void userSelect(Button b)
        {
            if (wasClicked == false)
            {
                fixedTime = startTime;
                timer.text = "Timer: " + ((int)fixedTime).ToString();

                if (b.GetComponentInChildren<TextMeshProUGUI>().text == currentQuestion.getRightAnswer())
                {
                    bb = b;
                    flag = true;

                    b.GetComponentInChildren<TextMeshProUGUI>().text = "Correct!";
                    if ((difficulty == "Hard" && startTime > (5.04f / 2)) || (difficulty == "Medium" && startTime > (7.54f / 2)) || (difficulty == "Easy" && startTime > (7.54f / 2)))
                        score++;

                    timer.color = Color.green;
                    score += scoreDifficulty;
                    Score.text = "Score:" + score.ToString();


                }
                else
                {
                    bb = b;
                    flag = false;
                    b.GetComponentInChildren<TextMeshProUGUI>().text = "Wrong!";
                    timer.color = Color.red;
                    getRightOption().GetComponent<Image>().color = Color.green;
                }

                wasClicked = true;
            }
        }
        private Button getRightOption()
        {
            Debug.Log(currentQuestion.getRightAnswer());
            if (b1.GetComponentInChildren<TextMeshProUGUI>().text == currentQuestion.getRightAnswer())
            {
                return b1;
            }
            else if (b2.GetComponentInChildren<TextMeshProUGUI>().text == currentQuestion.getRightAnswer())
            {
                return b2;
            }
            else if (b3.GetComponentInChildren<TextMeshProUGUI>().text == currentQuestion.getRightAnswer())
            {
                return b3;
            }
            return null;
        }
        private void Update()
        {
            if (startTime >= 0.0f && wasClicked == false)
            {
                if (!(startTime > 2f))
                    timer.color = Color.red;
                startTime -= Time.deltaTime;
                timer.text = "Timer: " + ((int)startTime).ToString();
            }
            else if (wasClicked == false)
            {
                timer.text = "Timer: 0";
                getRightOption().GetComponent<Image>().color = Color.green;
                StartCoroutine(nextQuestion());

            }
            else if (wasClicked == true)
            {
                if (flag == true)
                {
                    bb.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
                    bb.GetComponentInChildren<TextMeshProUGUI>().faceColor = Color.green;
                }
                else
                {
                    bb.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                    bb.GetComponentInChildren<TextMeshProUGUI>().faceColor = Color.red;
                    getRightOption().GetComponent<Image>().color = Color.green;
                }
                StartCoroutine(nextQuestion());
            }
        }

        public static float getMainTimer()
        {
            return mainTimer;
        }
        public void quits()
        {
            questions = null;
            SceneManager.LoadScene("Menu");
        }
    }


}