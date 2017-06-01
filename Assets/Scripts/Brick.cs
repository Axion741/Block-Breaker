using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    public static int breakableCount = 0;
    public AudioClip crack;
    public Sprite[] hitSprites;
    private int timesHit;
    private LevelManager levelManager;

    private bool isBreakable; 

    // Use this for initialization
    void Start () {
        isBreakable = (this.tag == "Breakable");
        //keeping track of breakable bricks
        if (isBreakable)
        {
            breakableCount++;
        }

        timesHit = 0;
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        
	}
	
	// Update is called once per frame
	void Update () {
        print(breakableCount);
	}

    void OnCollisionEnter2D(Collision2D hit)
    {//Only run HandleHits if the brick has the tag Breakable
        AudioSource.PlayClipAtPoint(crack, transform.position);
        if (isBreakable)
        {
            HandleHits();
        }
    }

    void HandleHits() {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            breakableCount--;
            levelManager.BrickDestroyed();
            Destroy(gameObject);
        }
        else
        {
            LoadSprites();
        }
    }

    //TODO Remove this method once we can actually win
    void SimulateWin()
    {
        levelManager.LoadNextLevel();
    }

    void LoadSprites()
    {
        //sprite elements start at 0
        int spriteIndex = timesHit - 1;
        //if statement to make obvious if sprite missing - doesn't load invisible sprite
        if (hitSprites[spriteIndex])
        {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }

    }
}
