using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LogicScript : MonoBehaviour
{
    public AudioSource ding;
    public AudioSource lose;
    public AudioSource win;
    public Camera cam;
    public SpriteRenderer bg1;
    public SpriteRenderer bg2;
    public ParticleSystem ps;
    public ParticleSystem.MainModule leftPs;
    public ParticleSystem.MainModule rightPs;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI youWinText;
    public TextMeshProUGUI timeText;
    public int score;
    float delayTime = 0.25f;
    float timer = 0.0f;

    // create dictionary of colors to use for changing background and particle colors
    Dictionary<string, Color> colors = new Dictionary<string, Color>()
    {
        ["softRed"] = new Color(0.85f, 0.39f, 0.35f, 1f),
        ["ogBlue"] = new Color(.35f, .6f, .85f, 1f),
        ["hardBlue"] = new Color(.35f, .46f, .85f, 1f),
        ["purple"] = new Color(.51f, .35f, .85f, 1f),
        ["hardPurple"] = new Color(.51f, .75f, .85f, 1f),
        ["green"] = new Color(.34f, .78f, .53f, 1f),
        ["hardGreen"] = new Color(.34f, .98f, .53f, 1f),
        ["orange"] = new Color(.9f, .58f, .36f, 1f),
        ["hardOrange"] = new Color(.9f, .78f, .36f, 1f),
        ["hardRed"] = new Color(1f, .34f, .34f, 1f),
        ["harderRed"] = new Color(1f, .74f, .34f, 1f)
    };

    // getters and setters for encapsulation sake
    private Color GetColor(string color)
    {
        if (colors.ContainsKey(color))
        {
            return (colors[color]);
        }
        return (Color.black);
    }

    // there's probably another better way of merging these two methods together but idc
    public void SetColor(string color)
    {
        Color c = GetColor(color);
        bg1.color = c;
        bg2.color = c;
    }
    public void SetColors(string color1, string color2)
    {
        Color c1 = GetColor(color1);
        Color c2 = GetColor(color2);

        leftPs.startColor = new ParticleSystem.MinMaxGradient(c1, c2);
        rightPs.startColor = new ParticleSystem.MinMaxGradient(c1, c2);
    }

    void Start()
    {
        ding.time = delayTime; // skips delay to audio clip
        lose.time = delayTime;
        leftPs = GameObject.FindGameObjectWithTag("PS1").GetComponent<ParticleSystem>().main;
        rightPs = GameObject.FindGameObjectWithTag("PS2").GetComponent<ParticleSystem>().main;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void DecreaseScore(int val)
    {
        PlayDingSound();
        score -= val;
        scoreText.text = score.ToString();
    }

    public void YouWinText()
    {
        youWinText.text = "You Win!";
        int timeInt = Mathf.RoundToInt(timer);
        int timeInMin = timeInt / 60;
        int timeInSec = timeInt % 60;
        if (timeInMin > 0)
            timeText.text += timeInMin.ToString() + ":";
        if (timeInSec < 10)
            timeText.text += "0";
        timeText.text += timeInSec.ToString();
        if (timeInMin == 0)
            timeText.text += "s";
    }

    public void PlayDingSound()
    {
        ding.Play();
    }

    public void PlayLoseSound()
    {
        lose.Play();
    }

    public void PlayWinSound()
    {
        win.Play();
    }

    public void ConfettiParty()
    {
        ps.Play();
    }

    public void IncreaseText()
    {
        scoreText.fontSize += 30;
    }

}
