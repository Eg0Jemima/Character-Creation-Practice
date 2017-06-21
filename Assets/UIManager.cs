using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public RectTransform buttonPrefab;
    private FeatureManager manager;
    private Text descriptionText;
    private List<Button> buttons;

	// Use this for initialization
	void Start () {
        manager = FindObjectOfType<FeatureManager>();
        
        transform.Find("Navigation").GetChild(0).GetComponent<Button>().onClick.AddListener(() => manager.PreviousChoice());
        transform.Find("Navigation").GetChild(1).GetComponent<Button>().onClick.AddListener(() => manager.NextChoice());
        descriptionText = transform.Find("Navigation").GetChild(2).GetComponent<Text>();
        InitializeFeatureButtons();
        UpdateFeatureButtons();
    }

    void InitializeFeatureButtons() {
        buttons = new List<Button>();
        float height = buttonPrefab.rect.height;
        float width = buttonPrefab.rect.width;

        for(int i = 0; i < manager.features.Count; i++) {
            RectTransform temp = Instantiate(buttonPrefab);
            temp.name = i.ToString();
            temp.SetParent(transform.GetChild(1).GetComponent<RectTransform>()); //Features Game Object
            temp.localScale = new Vector3(1, 1, 1);
            temp.localPosition = new Vector3(0, 0, 0);
            temp.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, width);
            temp.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * height, height);

            Button b = temp.GetComponent<Button>();
            b.onClick.AddListener(() => manager.SetCurrent(int.Parse(temp.name)));
            buttons.Add(b);
        }
    }
    void UpdateFeatureButtons() {
        for(int i = 0; i < manager.features.Count; i++) {
            buttons[i].transform.GetChild(0).GetComponent<Image>().sprite = manager.features[i].renderer.sprite;
        }
    }
	
	// Update is called once per frame
	void Update () {
        UpdateFeatureButtons();
        EventSystem.current.SetSelectedGameObject(buttons[manager.currentFeature].gameObject);
        descriptionText.text = manager.features[manager.currentFeature].id + " #" + (manager.features[manager.currentFeature].currentIndex + 1).ToString();
	}
}
