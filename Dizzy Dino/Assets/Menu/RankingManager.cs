using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour {

	private static List<PlayerData> ScoreList;

	public Text NameText;

	public Text ScoreText;

	private static String[] names = {"Alice", "Bob", "Carol", "David", "Erin"};

	private static int[] scores = {0, 0, 0, 0, 0};


	// Use this for initialization
	void Start () {
		
		// Initial the ranking board
		if (!PlayerPrefs.HasKey("name0")) {
			for (int i = 0; i < 5; i++) {
				PlayerPrefs.SetString("name" + i, names[i]);
				PlayerPrefs.SetInt("score" + i, scores[i]);
			}
		}

        RenderList();
	}

    public static int GetWorstScore() {
        if (!PlayerPrefs.HasKey("name0")) {
            return PlayerPrefs.GetInt("score4");
        }
        return 0;
    }

    public void RenderList() {
        String dName = "";
        String dScore = "";

        // Display the top 5 in ranking board
        for (int i = 0; i < 5; i++) {
            if (i != 4) {
                dName += ScoreList[i].Name + "\n";
                dScore += ScoreList[i].Score.ToString() + "\n";
            } else {
                dName += ScoreList[i].Name;
                dScore += ScoreList[i].Score.ToString();
            }
        }

        NameText.text = dName;
        ScoreText.text = dScore;
    }

	public static void UpdateList (String name, int score) {
		ScoreList = new List<PlayerData>();
		ScoreList.Add(new PlayerData(name, score));

		String tName = "name";
		int tScore = 0;

		for (int i = 0; i < 5; i++) {
			// Check key exists in the preference
			tName = PlayerPrefs.GetString("name" + i);
			tScore = PlayerPrefs.GetInt("score" + i);

			ScoreList.Add(new PlayerData(tName, tScore));
		}
		
		// Sort the list in descending order
		ScoreList.Sort();

		for (int i = 0; i < 5; i++) {
			PlayerPrefs.SetString("name" + i, ScoreList[i].Name);
			PlayerPrefs.SetInt("score" + i, ScoreList[i].Score);
		}

		// Save the player preference
		PlayerPrefs.Save();

	}

}

public class PlayerData : IComparable<PlayerData> {

	public int Score;
	public String Name;

	public PlayerData(String name, int score) {
		Score = score;
		Name = name;
	}

	public int CompareTo(PlayerData other) {

		int diff = other.Score.CompareTo(Score);
		
		if (diff == 0) {
			return Name.ToLower().CompareTo(other.Name.ToLower());
		}

		return diff;
	}

}
