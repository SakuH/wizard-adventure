using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{   
    public GameObject playerParent;
    public GameObject skillUi;
    public GameObject gameOverUi;
    public GameObject bossHpUi;
    public float health;
    public bool dead; 
    public float maxHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite halfHeart;
    public Image backgroundImage;
    public float fadeToBlackColor;

    public bool vulnerable = true;
    public float vulnerableCoolDown;
    public float vulnerableCoolDownMax;

    public Image SprintBoostPanel;
    public Image DashBoostPanel;

    public Material normalMat;
   // public Material hatMaterial;
    public Material invulnerableMat;
    public GameObject head;

    public PlayerMovement playerScript;

    public bool isTouchingEnemy;
    public int contactDamage = 1;

    public float knockbackTime;
    public float knockbackTimeMax = 0.15f;

    public float sfxVolume;

    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;
    private Color originalBoostColor;
    private Color originalDashColor;
    
    public AudioClip playerTakeDamageClip;
    public AudioClip playerHpPickUpClip;

    private bool canReturnToMainMenu = false;
    private bool saveDeleted;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = playerParent.GetComponent<PlayerMovement>();
        sfxVolume = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameAudioSettings>().sfxVolume;
        knockbackTimeMax = 0.15f;
        // hatMaterial.SetColor("_Color", Color.white);
        originalBoostColor = SprintBoostPanel.color;
        originalDashColor = DashBoostPanel.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health / 2)
            {
                if (i + 0.5f == health / 2)
                {
                    hearts[i].sprite = halfHeart;

                }
                else
                {
                    hearts[i].sprite = fullHeart;
                }


            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }


            if (i < maxHealth / 2&&!dead)
            {
                hearts[i].enabled = true;
            }else
            {
                hearts[i].enabled = false;
            }

            if (dead)
            {
                hearts[i].enabled = false;
            }
           

        }

        if(health <= 0)
        {

            
            gameOver();
            skillUi.SetActive(false);
            bossHpUi.SetActive(false);
            
                for (int y = 0; y < hearts.Length; y++)
            {
                hearts[y].enabled = false;
            }
                   
            
        }

        if (vulnerableCoolDown > 0)
        {
            if (knockbackTime > 0)
            {
                knockbackTime -= Time.deltaTime;
            }
            vulnerableCoolDown -= Time.deltaTime;
           
            SkinnedMeshRenderer gameObjectRenderer = head.GetComponent<SkinnedMeshRenderer>();
            gameObjectRenderer.material = invulnerableMat;
            if (!dead)
            {
              //  hatMaterial.SetColor("_Color", Color.red);
            }
           

        }
        else if (!vulnerable)
        {
            vulnerable = true;
            SkinnedMeshRenderer gameObjectRenderer = head.GetComponent<SkinnedMeshRenderer>();
            gameObjectRenderer.material = normalMat;
            //hatMaterial.SetColor("_Color", Color.white);
        }

       /*if (isTouchingEnemy)
        {
            takeDamage(contactDamage);
        }
        */
        if(knockbackTime > 0)
        {
            knockbackTime -= Time.deltaTime;
        }
        dashSkillCooldown();
        boostSkillCooldown();
        if (Input.GetKeyDown("space")&&canReturnToMainMenu)
        {

            FindObjectOfType<gameManagement>().restart();
        }
    }


    public void takeDamage(int damage)
    {
        if (vulnerable && playerScript.isDashing == false)
        {
            //takeDamageSound();
            PlayerHealthSound(playerTakeDamageClip);
            health -= damage;
            vulnerableCoolDown = vulnerableCoolDownMax;
            vulnerable = false;

        }
      
        if (health <= 0)
        {
            dead = true;
            
        }

    }
    public void playerReset()
    {
        dead = false;
        health = maxHealth;
    }

    //public void takeDamageSound()
    //{
    //    AudioManager.PlaySound("playerHit");
    //}

    void PlayerHealthSound(AudioClip clip)
    {
        GameObject clipGameObject = new GameObject("Player Health Sound");
        AudioSource source = clipGameObject.AddComponent<AudioSource>();
        clipGameObject.transform.position = transform.position;
        source.clip = clip;
        source.volume = sfxVolume;
        source.pitch = Random.Range(minPitch, maxPitch);
        source.Play();
        Destroy(clipGameObject, clip.length / source.pitch);
    }

    public void knockBack(float force, Vector3 enemyPos)
    {
        //Debug.Log("enemyForce" +force + " enemyPos"+ enemyPos);
        if (vulnerable && playerScript.isDashing == false)
        {
          //  Debug.Log("knockingBACK");

            knockbackTime = knockbackTimeMax;

        Vector3 moveDiretion = enemyPos * force;

            playerScript.knockback(moveDiretion);

        }
    
        
    }

    public void gameOver()
    {
        var tempColor = backgroundImage.color;

        if (fadeToBlackColor < 1)
        {
            fadeToBlackColor += Time.deltaTime;
        }

        tempColor.a = fadeToBlackColor;
        backgroundImage.color = tempColor;
        playerScript.movementSpeed = 0;

        // FindObjectOfType<gameManagement>().endGame();
        if (!saveDeleted)
        {
            SaveSystem.DeletePlayer();
            saveDeleted = true;
        }
        //SaveSystem.DeletePlayer();
        Invoke("showGameOverText", 3);
    }

    public void dashSkillCooldown()
    {
       

        if (playerScript.dashCooldown > 0|| playerScript.isDashing)
        {
            var tempColor = DashBoostPanel.color;
            tempColor = Color.red;
            tempColor.a = 180f;

            DashBoostPanel.color = tempColor;

        }

        if (playerScript.dashCooldown <= 0 && !playerScript.isDashing)
        {            
            DashBoostPanel.color = originalDashColor;
        }
   
    }
    public void boostSkillCooldown()
    {
       
        if (playerScript.isSpeedBoosting)
        {

            var tempColor = SprintBoostPanel.color;
            tempColor.a = 230f;

            SprintBoostPanel.color = tempColor;


        }
        if (playerScript.speedBoostCooldown > 0)
        {
            var tempColor = SprintBoostPanel.color;
            tempColor = Color.red;
            tempColor.a = 180f;
            
            SprintBoostPanel.color = tempColor;

        }
        if(playerScript.speedBoostCooldown <= 0&& !playerScript.isSpeedBoosting)
        {
            SprintBoostPanel.color = originalBoostColor;
        }
    }
    public void showGameOverText()
    {
        gameOverUi.SetActive(true);
        canReturnToMainMenu = true;
    }

    public void HpPickUpSound()
    {
        PlayerHealthSound(playerHpPickUpClip);
    }
}
