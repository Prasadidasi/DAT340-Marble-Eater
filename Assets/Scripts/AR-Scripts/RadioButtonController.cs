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
    // Start is called before the first frame update
    void OnEnable()
    {
        ToggleGroup = GetComponent<ToggleGroup>();
        IncrementToggle = false;
        DecrementToggle = false;
        InstantiatedPrefab = WorldSetup.ARImageTracker._instantiatedPrefab;
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
                        InstantiatedPrefab.transform.localScale += new Vector3(0, ScaleModifier * sign, 0);
                        NotifyWorldYScale(InstantiatedPrefab.transform.localScale.y);
                        break;
                    case "Z":
                        InstantiatedPrefab.transform.localScale += new Vector3(0, 0, ScaleModifier * sign);
                        break;
                }
                break;
            case "RotationRadioButton":
                InstantiatedPrefab.transform.Rotate(new Vector3(0, RotationModifier, 0) * Time.deltaTime);
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
            //Debug.Log("Increment " + toggle.name);
        }
    }

    void Decrement()
    {
        if (DecrementToggle == true)
        {
            Toggle toggle = ToggleGroup.ActiveToggles().FirstOrDefault();
            ToggleEvent(toggle, -1);
            //Debug.Log("Decrement " + toggle.name);
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
        gameObject.SetActive(false);
    }

    private void NotifyWorldYScale(float y)
    {
        Agent.Instance.WorldYScale = y;
        Agent.Instance.OnWorldYScaleChangeEvent(Agent.Instance.WorldYScale);
    }
}   
