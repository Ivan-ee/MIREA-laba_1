using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class FrogController : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private float _scaleSize;
    [SerializeField] private float _scaleDuration;
    
    [SerializeField] private GameObject frog;
    [SerializeField] private GameObject frogsBody;
    [SerializeField] private Material blue;
    [SerializeField] private Material balckOnRedSpot;
    [SerializeField] private Material orangeBlackBlue;
    [SerializeField] private Material redGreenBlack;
    [SerializeField] private Material yellow;
    [SerializeField] private Material yellowOnBlack;
    [SerializeField] private GameObject guts;
    
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private Animator anim;
    private GameObject gutsEx;
    private bool smashed = false;

    private Sequence _jumpSequence;
    private Sequence _scaleSequence;
    private float _jumpDuration = 1.2f;
    private void Awake()
    {
        anim = frog.GetComponent<Animator>();
        skinnedMeshRenderer = frogsBody.GetComponent<SkinnedMeshRenderer>();
        
        _jumpSequence = DOTween.Sequence();
        _scaleSequence = DOTween.Sequence();
        
        StartCoroutine(Jumping());
    }
    private void Update()
    {
        Move();
        Remove();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Damage();
            Destroy(this.gameObject);
        }
    }
    void IncreaseScale()
    {
        _scaleSequence.Append(transform.DOScale(new Vector3(_scaleSize, _scaleSize, _scaleSize), _scaleDuration));
        _scaleSequence.SetEase(Ease.Linear);
        _scaleSequence.Kill();
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_scaleDuration);
        Destroy(this.gameObject);
    }
    void Move()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Crawl"))
        {
            transform.position += new Vector3(0, 0, -_speed * Time.deltaTime);
        }
    }
    void Remove()
    {
        if (transform.position.z < -100)
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator Jumping()
    {
        int jumpDelay = Random.Range(2, 10);
        yield return new WaitForSeconds(_jumpDuration * jumpDelay);
        Jump();
        StartCoroutine(Jumping());
    }
    void Jump()
    {
        JumpAnim();
        Vector3 position = transform.position;
        _jumpSequence.Append(transform.DOJump(new Vector3(position.x, position.y, position.z - 300f), 15, 1, _jumpDuration));
        _jumpSequence.SetEase(Ease.OutBack);
        _jumpSequence.AppendCallback(CrawlAnim);
    }
    public void IdleAnim()
    {
        print("IDLE!!!");
        RootMotion();
        DestroyGuts();
        anim.SetTrigger("Idle");
    }
    public void JumpAnim()
    {
        RootMotion();
        DestroyGuts();
        anim.SetTrigger("Jump");
    }
    public void CrawlAnim()
    {
        RootMotion();
        DestroyGuts();
        anim.SetTrigger("Crawl");
    }
    public void TongueAnim()
    {
        RootMotion();
        DestroyGuts();
        anim.SetTrigger("Tongue");
    }
    public void SwimAnim()
    {
        RootMotion();
        DestroyGuts();
        anim.SetTrigger("Swim");
    }
    public void SmashedAnim()
    {
        RootMotion();
        DestroyGuts();
        anim.SetTrigger("Smashed");
        GutsAnim();
    }
    public void TurnLeftAnim()
    {
        anim.applyRootMotion = true;
        DestroyGuts();
        anim.SetTrigger("TurnLeft");
    }
    public void TurnRightAnim()
    {
        anim.applyRootMotion = true;
        DestroyGuts();
        anim.SetTrigger("TurnRight");
    }
    public void GutsAnim()
    {
        Invoke("SpreadGuts", 0.1f);
    }
    void SpreadGuts()
    {
        smashed = false;
        if (!smashed)
        {
            Instantiate(guts, frog.transform.position, frog.transform.rotation);
            smashed = true;
        }
    }
    void RootMotion()
    {
        if (anim.applyRootMotion)
        {
            anim.applyRootMotion = false;
        }
    }
    void DestroyGuts()
    {
        gutsEx = GameObject.FindGameObjectWithTag("Guts");
        if (gutsEx != null)
        {
            Destroy(gutsEx);
            smashed = false;
        }
    }
    public void Blue() 
    {
        skinnedMeshRenderer.material = blue;
    }
    public void BlackOnRedSpot()
    {
        skinnedMeshRenderer.material = balckOnRedSpot;
    }
    public void OrangeBlackBlue()
    {
        skinnedMeshRenderer.material = orangeBlackBlue;
    }
    public void RedGreenBlack()
    {
        skinnedMeshRenderer.material = redGreenBlack;
    }
    public void Yellow()
    {
        skinnedMeshRenderer.material = yellow;
    }
    public void YellowOnBlack()
    {
        skinnedMeshRenderer.material = yellowOnBlack;
    }

}