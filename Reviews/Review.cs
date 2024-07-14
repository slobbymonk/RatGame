using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Review : MonoBehaviour
{
    public TMP_Text _reviewText;
    private string _lines;
    public float _textSpeed;

    public int _stars;

    [Range(0f, 3f)]
    [HideInInspector] public int _score;

    public GameObject[] _starsImage;
    public float _starGrowingSpeed, _timeBetweenStars;

    public bool _wasReviewed;

    public enum Experiences
    {
        Perfect,
        TooLate,
        Burnt,
        Raw,
        Incomplete
    }
    public Experiences _experience;

    public void LoadText()
    {
        _lines = "Gerome: \n" + _reviewText.text;
        _reviewText.text = "";

        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach (var c in _lines.ToCharArray())
        {
            _reviewText.text += c;
            yield return new WaitForSeconds(_textSpeed);
        }
    }
}
