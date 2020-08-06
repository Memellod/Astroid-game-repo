using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

namespace GameLogic
{
    public class GameManager : MonoBehaviour
    {
        [Serializable]
        class Score
        {
            public  int points { get; set; }
            public string playerName { get; }
            public Score(string _name, int _points)
            {
                points = _points;
                playerName = _name;
            }
        }

        static GameManager instance;

        [SerializeField] UIHandler uiHandler;
        [SerializeField] GameObject playerPrefab;
        [SerializeField] Text scoreText;
        GameObject spawnPoint;
        AsteroidSpawner asteroidSpawner;
        GameObject player;
        public bool isPlaying = false;
        private int score = 0;
        private Score[] highscores;
        
        [SerializeField] Text highscoresText;
        // Start is called before the first frame update
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(transform.root);
                spawnPoint = GameObject.FindGameObjectWithTag("spawn point");
                asteroidSpawner = FindObjectOfType<AsteroidSpawner>();
                LoadHighscores();
                highscoresText.text = PrintHighscores();
            }
            else
            {
                Destroy(this);
            }
        }

        public void StartGame()
        {
            isPlaying = true;
            Time.timeScale = 1;
            score = 0;
            scoreText.text = "Score: 0";
            player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
        }

        public void GameOver()
        {
            isPlaying = false;
            asteroidSpawner.DestroyAllAsteroids();
            Destroy(player);
            uiHandler.EnableGameUI(false);

            // Dealing with highscores after game over
            bool new_highscore = false;
            for (int i = 0; new_highscore == false && i < highscores.Length; i++)
            {
                if (highscores[i].points < score)
                    new_highscore = true;
            }

            if (new_highscore)
            {
                UpdateHighscores();

            }
            else
            {
                uiHandler.EnableMenuUI(true);
            }
        }

        public void AddPoints(int value)
        {
            score += value;
            scoreText.text = "Score: " + score.ToString();
        }

        public int GetScore()
        {
            return score;
        }

        private void LoadHighscores()
        {
            IFormatter formatter = new BinaryFormatter();
            if (File.Exists("highscores"))
            {
                Stream stream = new FileStream("highscores", FileMode.Open, FileAccess.Read);
                highscores = (Score[])formatter.Deserialize(stream);
                stream.Close();
            }
            else
            {

                CreateDefaultHighscores();
            }
        }

        private void CreateDefaultHighscores()
        {
            highscores = new Score[10];
            for (int i = 0; i < 10; i++)
            {
                highscores[i] = new Score("empty", 0);
            }
        }

        public void SaveHighscores()
        {
            // Get name (made implying only one InputField exist)

            string name = FindObjectOfType<InputField>().text;

            // Set position for a new highscore

            int position = 0;
            for (; position < highscores.Length; position++)
            {
                if (highscores[position].points < score)
                    break;
            }

            // Moving other highscores
            for (int i = highscores.Length - 1; i > position; i--)
            {
                highscores[i] = highscores[i-1];
            }
            highscores[position] = new Score(name, score);
            
            //  Write to file
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("highscores", FileMode.OpenOrCreate, FileAccess.Write);
            formatter.Serialize(stream, highscores);
            stream.Close();

            highscoresText.text = PrintHighscores();
        }

        private void UpdateHighscores()
        {
            uiHandler.EnableHighscoreUI(true);
        }

        // method for receiving highscore as text for the main screen
        public string PrintHighscores()
        {
            string ans = "";

            for (int i = 0; i < highscores.Length; i++)
            {
                ans += (i+1) + ". " + highscores[i].playerName + " - " + highscores[i].points.ToString() + Environment.NewLine;
            }

            return ans;
        }
    

    }
}