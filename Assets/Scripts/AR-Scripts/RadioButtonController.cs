using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RadioButtonController : MonoBehaviour
{
    ToggleGroup ToggleGroup;
    private bool IncrementToggle, DecrementToggle;
    [HideInInspector] public WorldSetup WorldSetup;
    [SerializeField] private float ScaleModifier = 0.01f;
    [SerializeField] private float PositionModifier = 0.01f;
    [SerializeField] private int RotationModifier = 10;
    private GameObject InstantiatedPrefab;
    private Vector3 localScale;
   
    // Start is called before the first frame update
    void OnEnable()
    {
        ToggleGroup = GetComponent<ToggleGroup>();
        IncrementToggle = false;
        DecrementToggle = false;
        InstantiatedPrefab = WorldSetup.ARImageTracker._instantiatedPrefab;
        localScale = InstantiatedPrefab.transform.localScale;
    }

    private void Update()
    {
        Increment();
        Decrement();
        
    }

    void ToggleEvent(Toggle toggle, int sign)
    {
        switch (toggle.tag)
        {
            case "PositionRadioButton":
                switch (toggle.name)
                {
                    case "X":
                        InstantiatedPrefab.transform.Translate(PositionModifier * sign, 0, 0, Space.World);
                        break;
                    case "Y":
                        InstantiatedPrefab.transform.Translate(0, PositionModifier * sign, 0);
                        break;
                    case "Z":
                        InstantiatedPrefab.transform.Translate(0, 0, PositionModifier * sign, Space.World);
                        break;
                }
                break;

            case "ScaleRadioButton":
                switch (toggle.name)
                {
                    case "X":
                        InstantiatedPrefab.transform.localScale += new Vector3(ScaleModifier * sign, 0, 0);
                        break;
                    case "Y":
                        localScale += new Vector3(0, ScaleModifier * sign, 0);
                        InstantiatedPrefab.transform.localScale = localScale;
                       
                        break;
                    case "Z":
                        InstantiatedPrefab.transform.localScale += new Vector3(0, 0, ScaleModifier * sign);
                        break;
                }
                break;
            case "RotationRadioButton":
                InstantiatedPrefab.transform.Rotate(new Vector3(0, RotationModifier*sign, 0) * Time.deltaTime);
                Debug.Log(InstantiatedPrefab.transform.rotation);
                break;
        }
            
    }
    void Increment()
    {
        if (IncrementToggle == true)
        {
            Toggle toggle = ToggleGroup.ActiveToggles().FirstOrDefault();
            ToggleEvent(toggle, 1);
        }
    }

    void Decrement()
    {
        if (DecrementToggle == true)
        {
            Toggle toggle = ToggleGroup.ActiveToggles().FirstOrDefault();
            ToggleEvent(toggle, -1);
        }
    }

    public void toggleIncrement()
    {
        IncrementToggle = !IncrementToggle;
    }

    public void toggleDecrement()
    {
        DecrementToggle = !DecrementToggle;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        Deploy();
        gameObject.SetActive(false);
    }
    
    void Deploy()
    {
        NotifyWorldYScale(localScale.y);
        InstantiatedPrefab.GetComponent<PSController>().enabled = true;
        InstantiatedPrefab.transform.Find("PlayerMarble").gameObject.SetActive(true);
    }
    private void NotifyWorldYScale(float y)
    {
        Agent.Instance.WorldYScale = y;
       // Agent.Instance.OnWorldScaleChangeEvent(Agent.Instance.WorldYScale);
    }
}   
