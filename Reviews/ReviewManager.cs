using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Review;
using DG.Tweening;

public class ReviewManager : MonoBehaviour
{
    public Review[] _reviews;
    private int _reviewCount;

    public string[] _burntReviews;
    public string[] _rawReviews;
    public string[] _incompleteReviews;
    public string[] _tardyReviews;
    public string[] _perfectReviews;

    public float _timeBetweenReviews;

    public float _moveSpeed;

    private int _finalScore;
    public float _finalStarGrowingSpeed;
    public GameObject[] _finalStars;
    public GameObject _finalStarsHolder;


    private void Update()
    {
        //Debug.Log(_reviewCount);
    }
    public void AddReview(Experiences experience)
    {
        switch (experience)
        {
            case Experiences.Perfect:
                _reviews[_reviewCount]._reviewText.text = _perfectReviews[Random.Range(0, _perfectReviews.Length)];
                Debug.Log("Order Perfect");
                Review(3);
                break;
            case Experiences.TooLate:
                _reviews[_reviewCount]._reviewText.text = _tardyReviews[Random.Range(0, _tardyReviews.Length)];
                Debug.Log("Order TooLate");
                Review(2);
                break;
            case Experiences.Incomplete:
                _reviews[_reviewCount]._reviewText.text = _incompleteReviews[Random.Range(0, _incompleteReviews.Length)];
                Review(0);
                break;
            case Experiences.Burnt:
                _reviews[_reviewCount]._reviewText.text = _burntReviews[Random.Range(0, _burntReviews.Length)];
                Debug.Log("Order Burnt");
                Review(1);
                break;
            case Experiences.Raw:
                _reviews[_reviewCount]._reviewText.text = _rawReviews[Random.Range(0, _rawReviews.Length)];
                Debug.Log("Order Raw");
                Review(1);
                break;
        }
    }

    private void Review(int score)
    {
        _reviews[_reviewCount]._wasReviewed = true;
        _reviews[_reviewCount]._score = score;
        _reviewCount++;
    }

    public void GoThroughReviews()
    {
        StartCoroutine(PlayReview());
    }
    IEnumerator PlayReview()
    {
        CheckIfReviewMade();


        ShowStarScore(0);
        _reviews[0].LoadText();
        yield return new WaitForSeconds(_timeBetweenReviews);

        ShowStarScore(1);
        _reviews[0].GetComponent<RectTransform>().DOAnchorPosY(-1200f, _moveSpeed).SetEase(Ease.InOutSine);
        //_reviews[0].GetComponent<RectTransform>().DOScale(.5f, _moveSpeed).SetEase(Ease.InOutSine);
        _reviews[1].gameObject.SetActive(true);
        _reviews[1].LoadText();

        yield return new WaitForSeconds(_timeBetweenReviews);

        _reviews[1].GetComponent<RectTransform>().DOAnchorPosY(1200f, _moveSpeed).SetEase(Ease.InOutSine);
        //_reviews[1].GetComponent<RectTransform>().DOScale(.5f, _moveSpeed).SetEase(Ease.InOutSine);
        _reviews[2].gameObject.SetActive(true);
        _reviews[2].LoadText();
        ShowStarScore(2);

        yield return new WaitForSeconds(_timeBetweenReviews);

        _reviews[2].GetComponent<RectTransform>().DOAnchorPosX(2600f, _moveSpeed).SetEase(Ease.InOutSine);
        //_reviews[2].GetComponent<RectTransform>().DOScale(.5f, _moveSpeed).SetEase(Ease.InOutSine);

        CalculateFinalScore();

        _finalStarsHolder.SetActive(true);
        _finalStarsHolder.GetComponent<RectTransform>().DOAnchorPosY(0, _moveSpeed).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(.5f);

        for (int i = 0; i < _finalScore; i++)
        {
            _finalStars[i].GetComponent<RectTransform>().DOScale(1f, _finalStarGrowingSpeed).SetEase(Ease.InOutElastic);
            yield return new WaitForSeconds(.5f);
        }
    }
    void CheckIfReviewMade()
    {
        foreach (var review in _reviews)
        {
            if (!review._wasReviewed)
            {
                review._reviewText.text = _tardyReviews[Random.Range(0, _tardyReviews.Length)];
            }
        }
    }
    void CalculateFinalScore()
    {
        _finalScore = (_reviews[0]._score + _reviews[1]._score + _reviews[2]._score)/3;
    }
    void ShowStarScore(int k)
    {
        for (int i = 0; i < _reviews[k]._score; i++)
        {
            _reviews[k]._starsImage[i].GetComponent<RectTransform>().DOScale(1f, _reviews[k]._starGrowingSpeed).SetEase(Ease.InOutElastic);
        }
    }
}
