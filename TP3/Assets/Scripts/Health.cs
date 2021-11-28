using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject firstHeart;
    public GameObject heartsContainer;

    // [Range(1, 30)]
    // public int maxHearts = 5;
    // private int remainingHearts;
    
    public Sprite filledHeart;
    public Sprite emptyHeart;

    public static Health instance; 

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        firstHeart = GameObject.Find("Heart");
        heartsContainer = GameObject.Find("HeartsContainer");
        instance = this;
        ResetHearts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Transform getHeartTransform(int index)
    {
        return heartsContainer.transform.GetChild(index);
    }

    Transform getLastHeart()
    {
        return getHeartTransform(heartsContainer.transform.childCount - 1);
    }
    
    public void ResetHearts() {
        if (firstHeart == null) return;

      
        if(LevelManager.instance.nbInitialLives == LevelManager.instance.nbLives) {
            int childCount = heartsContainer.transform.childCount;
            if (childCount > 1)
            {
                for (int i = 0; i < childCount; i++)
                {
                    getHeartTransform(i).GetComponent<Image>().sprite = filledHeart;
                }
            }
            else
            {
            for (int i = 1; i < LevelManager.instance.nbInitialLives; i++) {
                Vector3 position = getLastHeart().transform.localPosition;
                position.x -= getLastHeart().GetComponent<RectTransform>().rect.width + 10;
                GameObject newHeart = Instantiate(firstHeart);
                newHeart.transform.SetParent(heartsContainer.transform);
                newHeart.transform.localScale = firstHeart.transform.localScale;
                newHeart.transform.localPosition = position;
            }
        
            }
        }  
    }

    public void LoseHeart(int amount, int remainingHearts)
    {
        //remainingHearts -= amount;
        for(int i = 0; i < amount; i++)
        {
            if(remainingHearts + i >= 0)
            {
                getHeartTransform(remainingHearts + i).GetComponent<Image>().sprite = emptyHeart;
            }
        }
    }
}
