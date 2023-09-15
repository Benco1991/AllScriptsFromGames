using System.Collections;
using UnityEngine;

    public enum NumberOfLVL
    {
        One, Two, Three
    }
public class LVL : MonoBehaviour
{
    public NumberOfLVL numberOfLVL;
    Vector2[] collectionsPos { get; set; }
    public GameObject[] collections;

    public int score; //{ get; private set; }
    public int maxScore; //{ get; private set; }
    BckLitMovement litMovement;
    public int newLevelNumber { get; private set; }
    public GameManager manager { get; private set; }

    private void Start()
    {

        //StartWaiting();
    }
    public void CheckScore(int score)
    {
        this.score += score;
        if (this.score == maxScore)
        {
            //this.score = 0;
            manager.ActualLVL++;
            if (manager.ActualLVL == 2) { manager.StopTiming(); }
            manager.GenerateLVL(this, null, manager.player, false);
            //manager.player.col.enabled = false;
            Debug.Log("new level");
        }
            
    }
    private void GeneratePosLVL3()
    {
        float number = Random.value;
        if (number < 0.5f)
            collectionsPos = StaticData.OK;
        else collectionsPos = StaticData.Smiley;
    }
    private void SpawnCollections()
    {
        Collections[] findAll = FindObjectsOfType<Collections>();
        foreach (Collections found in findAll)
        {
            Destroy(found.gameObject);
        }

        switch (numberOfLVL)
        {
            case NumberOfLVL.One:
                collectionsPos = StaticData.CollectionsPos[NumberOfLVL.One];
                break;

            case NumberOfLVL.Two:
                collectionsPos = StaticData.CollectionsPos[NumberOfLVL.Two];
                break;

            case NumberOfLVL.Three:
                GeneratePosLVL3();
                break;
            default:
                break;
        }

        if (collectionsPos != null)
        {
            for (int i = 0; i < collectionsPos.Length; i++)
            {
                int randomCollection = Random.Range(0, collections.Length);
                GameObject collection = Instantiate(collections[randomCollection], new Vector2(collectionsPos[i].x, collectionsPos[i].y), Quaternion.identity);
                collection.transform.parent = transform;
            }
        }
    }
    public void Initialize(int maxScore, BckLitMovement bckLitMovement, GameManager manager, int newLevel, bool restart)
    {
        this.maxScore = maxScore;
        this.litMovement = bckLitMovement;
        this.manager = manager;
        this.newLevelNumber = newLevel;
        if (restart) { score = 0; }
        SpawnCollections();
    }
    /*private void StartWaiting()
    {
        StartCoroutine(Wait());
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        litMovement.StartMovement();
    }*/
}
