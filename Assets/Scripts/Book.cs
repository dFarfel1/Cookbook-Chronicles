using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class book : MonoBehaviour
{
    [SerializeField] float pageSpeed = 0.1f;
    [SerializeField] List<Transform> pages;
    int index = 0;
    bool rotate = false;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject forwardButton;

    private void Start()
    {
        InitialState();
    }

    public void InitialState()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        pages[0].SetAsLastSibling();
        backButton.SetActive(false);
    }

    public void RotateForward()
    {
        if (rotate == true) { return; }
        float angle = -180;
        StartCoroutine(Rotate(angle, true));
        forwardButton.SetActive(false);
        backButton.SetActive(false);
    }

    public void ForwardButtonActions()
    {
        if (backButton.activeInHierarchy == false){
            backButton.SetActive(true);
        }
        if (index >= pages.Count - 1){
            forwardButton.SetActive(false);
        } else {
            forwardButton.SetActive(true);
        }
    }

    public void RotateBack(){
        if (rotate == true) { return; }
        float angle = 0;
        StartCoroutine(Rotate(angle, false));
        forwardButton.SetActive(false);
        backButton.SetActive(false);
    }

    public void BackButtonActions(){
        if (forwardButton.activeInHierarchy == false)
        {
            forwardButton.SetActive(true);
        }
        if (index == 0)
        {
            backButton.SetActive(false);
        } else {
            backButton.SetActive(true);
        }
    }

    IEnumerator Rotate(float angle, bool forward){
        float value = 0f;
        while (true){
            int pageToRotate = index;
            if (!forward) {
                pageToRotate = index-1;
            }
            rotate = true;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            value += Time.deltaTime * pageSpeed;
            pages[pageToRotate].localRotation = Quaternion.Slerp(pages[pageToRotate].localRotation, targetRotation, value);
            float angle1 = Quaternion.Angle(pages[pageToRotate].localRotation, targetRotation);
            if (angle1 < 90.0f && forward)  {
                pages[index+1].SetAsLastSibling();    
            }
            if (angle1 < 90.0f && !forward)  {
                pages[index-1].SetAsLastSibling();    
            }
            
            if (angle1 < 0.1f)
            {
                if (forward == false)
                {
                    index--;
                    BackButtonActions();
                } else {
                    index ++;
                    ForwardButtonActions();
                }
                rotate = false;
                break;
            }
            yield return null;
        }
    }
}
