using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;
using System.Text;
using System.Linq;

public class ApplicationController : MonoBehaviour
{
    public static bool isMouseControl = false;
    public static bool canOpenHighScorePanel = false;
    [SerializeField]
    private GameObject highScorePanel;
    [SerializeField]
    private TMP_InputField inputNickName;
    [SerializeField]
    private Image controlButtonImage;
    [SerializeField]
    private Sprite mouseIcon;
    [SerializeField]
    private Sprite keyboardIcon;

    //FileIO
    private static string Path;
    private static StringBuilder fileText;

    private void Start() {
        LoadFile();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return))
            LoadScene(1);

        if (Input.GetKeyDown(KeyCode.Escape) && highScorePanel.activeSelf)
            highScorePanel.SetActive(false);
    }

    private void OnLevelWasLoaded() {
        SetControlImage();
        if (canOpenHighScorePanel)
            HighScoresClick();
        inputNickName.text = GameController.NickName;
        Cursor.visible = true;
    }

    public void LoadScene(int sceneIndex) 
    {
        switch (sceneIndex)
        {
            case 0:
                SceneManager.LoadScene("Menu");
                break;
            case 1:
                SceneManager.LoadScene("Game");
                highScorePanel.SetActive(false);
                GameController.NickName = ValidateNickName();
                Cursor.visible = false;
                break;
        }    
    }

    private void ClearScoreRecords() {
        foreach (var item in highScorePanel.GetComponentsInChildren<ScoreRecord>()) {
            if (item.gameObject.activeSelf)
                Destroy(item.gameObject);
        }
    }

    public void HighScoresClick() {

        ClearScoreRecords();
        canOpenHighScorePanel = false;
        highScorePanel.SetActive(true);
        highScorePanel.GetComponentInChildren<ScoreBoxController>().SetHighScores(GetSortedListOfPlayerScores());
    }

    public void ClearHighScores() {
        StartCoroutine(ClearAllScores());
        ClearScoreRecords();
    }

    public void ExitButtonClick() 
    {
        Application.Quit();
    }

    public void ChangeControl() {
        isMouseControl = !isMouseControl;
        SetControlImage();
    }

    private string ValidateNickName() {
        string text_orig = inputNickName.text;

        string text_v1 = text_orig.Replace(';', ' ');
        string text_v2 = text_v1.Replace('#', ' ');

        return text_v2;
    }

    private void SetControlImage() {
        if (isMouseControl)
            controlButtonImage.sprite = mouseIcon;
        else
            controlButtonImage.sprite = keyboardIcon;
    }

    public static void SaveScore(PlayerScore playerScore) {

        if (File.Exists(Path) && playerScore.Score != 0) {
            fileText.Append(playerScore.ToString());
            string write = fileText.ToString();
            File.WriteAllText(Path, write);
        }
    }

    private void LoadFile() {

        Path = Application.dataPath + "/Resources/scores.txt";

        fileText = new StringBuilder("");

        if (File.Exists(Path)) {

            string read = File.ReadAllText(Path);
            fileText.Append(read);
        }
        else File.Create(Path);
    }

    private List<PlayerScore> GetSortedListOfPlayerScores() {

        List<PlayerScore> listOfPlayerScores = new List<PlayerScore>();

        LoadFile();

        string allText = fileText.ToString();
        string[] dataRecord = allText.Split('#');

        for (int i = 0; i < dataRecord.Length - 1; i++) {
            if (dataRecord[i] != null || dataRecord[i] != "") {
                string[] scoreRecordText = dataRecord[i].Split(';');
                listOfPlayerScores.Add(new PlayerScore(scoreRecordText[0], scoreRecordText[1], scoreRecordText[2]));
            }
        }
        listOfPlayerScores.Sort((p, q) => p.Score.CompareTo(q.Score));
        var sortedlist = listOfPlayerScores.OrderByDescending(x => x.Score).ToList();

        return sortedlist;
    }

    private IEnumerator ClearAllScores() {
        yield return null;

        if (File.Exists(Path)) {
            File.WriteAllText(Path, "");
        }
    }
}
