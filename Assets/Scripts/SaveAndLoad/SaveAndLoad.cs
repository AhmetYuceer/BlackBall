using UnityEngine;

namespace SaveAndLoad
{
    public static class SaveAndLoad
    {
        public static void SaveScore(int score)
        {
            PlayerPrefs.SetInt("Score", score);
        }

        public static int LoadScore()
        {
            return PlayerPrefs.GetInt("Score");
        }
    }
}