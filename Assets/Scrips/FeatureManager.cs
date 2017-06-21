using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FeatureManager : MonoBehaviour {
    public List<Feature> features;
    public int currentFeature;

    void LoadFeatures() {
        features = new List<Feature>();
        features.Add(new Feature("Face", transform.Find("Face").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("Hair", transform.Find("Face").GetChild(0).GetComponent<SpriteRenderer>()));
        features.Add(new Feature("Eyes", transform.Find("Face").GetChild(1).GetComponent<SpriteRenderer>()));
        features.Add(new Feature("Mouth", transform.Find("Face").GetChild(3).GetComponent<SpriteRenderer>()));
        features.Add(new Feature("Tops", transform.Find("Top").GetComponent<SpriteRenderer>()));

        for (int i = 0; i < features.Count; i++) {
            string key = "FEATURE_" + i;
            if (!PlayerPrefs.HasKey(key)) {
                PlayerPrefs.SetInt(key, features[i].currentIndex);
            }
            features[i].currentIndex = PlayerPrefs.GetInt(key);
            features[i].UpdateFeature();
        }
    }

    void SaveFeatures() {
        for (int i = 0; i < features.Count; i++) {
            string key = "FEATURE_" + i;
            PlayerPrefs.SetInt(key, features[i].currentIndex);
        }
        PlayerPrefs.Save();
    }

    private void OnEnable() {
        LoadFeatures();
    }

    private void OnDisable() {
        SaveFeatures();
    }

    public void SetCurrent(int index) {
        if(features == null) {
            return;
        }

        currentFeature = index;
    }

    public void NextChoice() {
        if(features == null) {
            return;
        }
        features[currentFeature].currentIndex++;
        features[currentFeature].UpdateFeature();
    }

    public void PreviousChoice() {
        if (features == null) {
            return;
        }
        features[currentFeature].currentIndex--;
        features[currentFeature].UpdateFeature();
    }
    
}

[System.Serializable]
public class Feature {
    public string id;
    public int currentIndex;
    public Sprite[] choices;
    public SpriteRenderer renderer;

    public Feature(string id, SpriteRenderer rend)
    {
        this.id = id;
        this.renderer = rend;
        UpdateFeature();
    }

    public void UpdateFeature()
    {
        choices = Resources.LoadAll<Sprite>("Textures/" + id);

        if(choices == null || renderer == null){
            return;
        }

        if(currentIndex < 0) {
            currentIndex = choices.Length - 1;
        }

        if(currentIndex >= choices.Length) {
            currentIndex = 0;
        }

        renderer.sprite = choices[currentIndex];
        Debug.Log(choices[currentIndex]);
    }
}

