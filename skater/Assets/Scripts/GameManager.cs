using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
// using GooglePlayGames;
// using GooglePlayGames:BasicApi.SavedGame;
// using UnityEngine.Advertisements;
public class GameManager : MonoBehaviour
{
    private const int COIN_SCORE_AMOUNT = 5;
    public static GameManager Instance { set; get; }
    public bool IsDead { set; get; }

    private bool isGameStarted = false;
    private bool isTapped = false;
    private PlayerMotor motor;

    // ui 
    public TextMeshProUGUI scoreText, coinText, modifierText, highscoreText;
    private float score /*,coinScore*/, modifierScore;
    private int lastScore, coinScore, totalCoin;

    public Animator deathMenuAnim, gameCanvas, menuAnim, coinAnim;
    public TextMeshProUGUI deathScoreText, deathCoinText;
    // public GameObject connectedMenu, disconnectedMenu;
    public GameObject settingsPanel;
    public static int coinScor, diamondScore, healthScore;
    public TextMeshProUGUI coinRewards, diamondRewards, healthRewards;


    // player hats
    /*
    public List<int> hatPrices;
    public Transform hatContainer;
    public Transform hatButtonContainer;
    private int unlockedHats = 1;
    */

    private void Awake()
    {
        Instance = this;
        modifierScore = 1;
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        // UpdateScores();
        scoreText.text = score.ToString("0");
        coinText.text = coinScore.ToString("0");
        modifierText.text = "x" + modifierScore.ToString("0.0");
        highscoreText.text = PlayerPrefs.GetInt("Highscore").ToString();

        // rewards
        coinRewards.text = "x" + coinScor.ToString("0");
        diamondRewards.text = "x" + diamondScore.ToString("0");
        healthRewards.text = "x" + healthScore.ToString("0");
        /*
        // ads

        Advertisement.Initialize("..12..");


        // Google Play Services
        GooglePlayGames BasicApi.PlayGamesClientConfiguration config = new GooglePlayGames.BasicApi.PlayGamesClientConfiguration.Builder().EnableSavedGames().Buiild();
        // PlayGamesPlatform.Activate();
        // OnConnectionResponse(PlayGamesPlatform.Instance.localUser.authenticated);

        */
    }
    /*
    public void RequestRevive(){
    	ShowOptions so = new ShowOptions();
    	//so.resultCallback = HandleShowResult;
    	so.resultCallback = Revive;
    	Advertisement.Show("rewardedVideo", so);
    }
    
    private void HandleShowResult(ShowResult obj){
    		switch(obj)
    		{
    			caseShowResult.Finished
    		}
    ]
    
    public void Revive(){
    	if(sr == ShowResult sr){
    		FindObjectOfTypePlayerMotor<>().Revive();
    		IsDead = false;
    		
    		foreach(GlacierSpawn gs in FindObjectsOfType<GlacierSpawn>())
    			gs.IsScrolling = true;
    		deathMenuAnim.SetTrigger("Alive");
    		gameCanvas.SetTrigger(Show);
    		else{
    				OnPlayButton();
    		}
    	}
    }
    
    */

    private void Update()
    {
        if (isTapped && !isGameStarted)
        {
            isGameStarted = true;
            motor.StartRunning();
            FindObjectOfType<GlacierSpawn>().IsScrolling = true;
            FindObjectOfType<CameraMotor>().IsMoving = true;
            gameCanvas.SetTrigger("Show");
            menuAnim.SetTrigger("Hide");

        }

        if (isGameStarted && !IsDead)
        {
            // bump score up
            score += (Time.deltaTime * modifierScore);
            if (lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = score.ToString("0");
            }
        }
    }

    /*
    
    public void OnConnectClick(){
    		Social.localUser.Authenticate((bool success) =>{
    			OnConnectionResponse(success);
    		});
    }
    
    public void OnConnectionResponse(bool authenticated){
    		if(authenticated){
    		UnlockAchievement(RPGPS.achievement_login);
    			connectedMenu.SetActive(true);
    			disconnectedMenu.SetActive(false);
    			OpenSave(false);
    		}
    		else{
    			connectedMenu.SetActive(false);
    			disconnectedMenu.SetActive(true);
    		}
    }
    
    public void OnAchievementClick(){
    	if(Social.localUser.authenticated){
    		Social.ShowAchievementsUI();
    	}
    }
    
    public void UnlockAchievement(string achievementID){
    			Social.ReportProgress(achievementID, 100.0f, (bool success) =>{
    					Debug.Log("Achievement Unlocked" + success.ToString());
    			});
    }
    
    // leaderboard
    public void OnLeaderboardClick(){
    	if(Social.localUser.authenticated){
    		Social.ShowLeaderboardUI();
    	}
    }
    
    public void ReportScore(int score){
    	Social.ReportScore(score, RPGPS.leaderboard_highscore, (bool success) => {
    		Debug.Log("Score added" + success.ToString());
    	});
    }
    */

    public void CloseMenu()
    {
        settingsPanel.SetActive(false);
        menuAnim.SetTrigger("Show");
    }

    public void GetSettings()
    {
        settingsPanel.SetActive(true);
        menuAnim.SetTrigger("Hide");
    }

    public void GetCoin()
    {
        coinAnim.SetTrigger("Collect");
        coinScore++;

        /*
        // check achievement unlock

        switch(coinScore){
        case 50:
            UnlockAchievement(RPGPS.achievement_collect_50_coins);
            break;
        case 100:
                UnlockAchievement(RPGPS.achievement_collect_100_coins);
                break;
        case 150:
                UnlockAchievement(RPGPS.achievement_collect_150_coins);
                break;
        case 200:
                UnlockAchievement(RPGPS.achievement_collect_200_coins);
                break;
        default:
                break;
        }
        */
        coinText.text = coinScore.ToString("0");
        score += COIN_SCORE_AMOUNT;
        scoreText.text = scoreText.text = score.ToString("0");

    }

    public void GetReward()
    {
        coinAnim.SetTrigger("Collect");

        // rewards
        coinRewards.text = "x" + coinScor.ToString("0");
        diamondRewards.text = "x" + diamondScore.ToString("0");
        healthRewards.text = "x" + healthScore.ToString("0");

    }


    /*
    public void UpdateScores(){
    	scoreText.text = score.ToString();
    	coinText.text = coinScore.ToString();
    	modifierText.text = "x" + modifierScore.ToString("0.0");
  
    }
    
    public void OpenSave(bool saving){
    	Debug.Log("Open Save");
    	
    	if(Social.localUser.autthenticated){
    			isSaving = saving;
    			((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomticConfflictResolution("Skater", GooglePlayGames.BasicApi.DataSource.ReadCacheNetwork,
    			ConflictResolutionStrategy.UseLongestPlaytime, SaveGameOpened);
    	}
    }
    
    private void SaveGameOpened(SavedGameRequestStatus status, ISavedGameMetadata meta){
Debug.Log("SaveGameOpened");
    	if(status == SavedGameRequestStatus.Success){
    		if(isSaving) // writing
    		{
    		byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(GetSaveString());
    		SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder().WithUpdateDescription("Saved at " + DateTime.Now.ToString());
    			((PlayGamePlatform)Social.Active).SavedGame.CommitUpdate(meta, update, *data, SaveUpdate);
    		}
    		else // Reading
    		{
    		((PlayGamesPlatform)Social.Active).savedGame.ReadBinaryData(meta, SaveRead);
    		}
    	}
    }
    
    // load
    private void SaveRead(SavedGameRequestStatus status, byte[] data){
    		if(status == SavedGameRequestStatus.Success){
    			string saveData = System.Text.ASCIIEncoding.ASCII.GetString(data);
    			Debug.Log(saveData);
    			LoadSaveString(saveData);
    		}
    }
    
    private string GetSaveString(){
    		string r = "";
    		r += PlayerPrefs.GetInt("Highscore").ToString();
    		r+= "|";
    		r += totalCoin.ToString();
    		r += unlockedHats.ToString();
    		
    		return r;
    }
    
    private void LoadSaveString(string save){
    		// 100|84
    		string[] data = save.Split('|');
    	
    		PlayerPrefs.SetInt("Highscore", int.Parse(data[0]));
    		totalCoin = int.Parse(data[1]);
    		unlockedHats = int.parse(data[2]);
    		totalCoinText.text = totalCoin.ToString();
    }
    
    // success save
    private void SavedUpdate(SavedGameRequestStatus status, ISavedGameMetadata arg2){
    		Debug.Log(status)
    }
    
    */
    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        //UpdateScores();
        modifierText.text = "x" + modifierScore.ToString("0.0");

    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void StartButton()
    {
        isTapped = true;
    }

    public void OnDeath()
    {
        IsDead = true;
        FindObjectOfType<GlacierSpawn>().IsScrolling = false;
        deathScoreText.text = score.ToString("0");
        deathCoinText.text = coinScore.ToString("0");

        coinRewards.text = coinScore.ToString("0");
        diamondRewards.text = diamondScore.ToString("0");
        healthRewards.text = healthScore.ToString("0");

        deathMenuAnim.SetTrigger("Dead");
        gameCanvas.SetTrigger("Hide");

        // ReportScore((int)score);

        // check for high score
        if (score > PlayerPrefs.GetInt("Highscore"))
        {
            float s = score;
            if (s % 1 == 0)
                s += 1;
            PlayerPrefs.SetInt("Highscore", (int)s);
        }

        // totalCoin += coinScore;
        // OpenSave(true);
    }

    /* 
     public void TryBuyingHat(int index){
             // unlocked
             if((unlockedHats & 1 << index) == 1 << index){
                     // physical change
                     foreach(Transform t in hatContainer)
                         t.gameObject.SetActive(false);
                         if(index == 0)
                             return;
                         hatContainer.GetChild(index - 1).gameObject.SetActive(true);
             }
             else{
                     if(totalCoin >= hatPrices[index]){
                             totalCoin -= hatPrices[index];

                             // unlock in array
                             unlockedHats += 1 << index;

                             // physical Change
                             foreach(Transform t in hatContainer)
                                 t.gameObject.SetActive(false);

                                 if(index == 0)
                                     return;

                         hatContainer.GetChild(index - 1).gameObject.SetActive(true);
                         hatButtonContainer.GetChild(index).GetChild(1).gameObject.SetActive(false);
                         // OpenSave(true);
                     }					
             }
     }

     private void SetHatMenu(){
         // price
         int i = 0;
         foreach(Transform t in hatButtonContainer){
             // unlocked already
             if((unlockedHats & 1 << i) == 1 << i)
                 t.GetChild(1).gameObject.SetActive(false);
             else
                 t.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = hatPrices[i].ToString();
             i++;
         }
     }
     */

}
