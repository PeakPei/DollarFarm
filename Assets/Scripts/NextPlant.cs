﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NextPlant : MonoBehaviour
{

    ShedManager shedManager;

    public GameObject[] Models;
    private int i = 0;
    public GameObject RainCloud;

    void Start() {
        shedManager = GameObject.FindObjectOfType<ShedManager>();
        //since at the start no objects are active (and calling Models[i] will activate redflower, call next and prev to set i to 0 correctly 
        Next();
        Prev();
    }

    void Update() {

    }

    public void WaterPlant() {
        StartCoroutine(makeCloud());


    }

    //Display cloud watering for 4 seconds
    public IEnumerator makeCloud() {
        RainCloud.SetActive(true);
        yield return new WaitForSeconds(4);
        RainCloud.SetActive(false);

        //add experience
        string currentPlant = Models[i].name;
        int currentExperience = shedManager.GetPlantFeature(currentPlant, "experience") + 5;
        shedManager.SetPlantFeature(currentPlant, "experience", currentExperience);
        shedManager.Save();

        Models[i].SetActive(false);
        Models[i + 1].SetActive(false);
        Models[i + 2].SetActive(false);

        if (currentExperience < 15) {
            //play small version animation
            Models[i + 1].SetActive(true);
        }
        else if (currentExperience >= 15 && currentExperience < 25) {
            //play medium animation
            Models[i + 2].SetActive(true);
        }
        else {
            //play full grown plant animation
            Models[i].SetActive(true);
        }


    }

    public void Next() {
        if (i < 9) {

            int oldModel = i;
            Models[i].SetActive(false);
            Models[i + 1].SetActive(false);
            Models[i + 2].SetActive(false);

            //move 3 indices to the next plant and find that object's name
            i = i + 3;
            string currentPlant = Models[i].name;
            Debug.LogError("now i is " + i);
            Debug.LogError("current plant is " + currentPlant);

            //check if that object is in the shedPlants array, if not then keep moving down the object list.
            while (shedManager.shedPlants.ContainsKey(currentPlant) == false && i < 9) {
                i = i + 3;
                currentPlant = Models[i].name;
                Debug.Log(currentPlant);
            }

            
            //Either we've reached the end of the user's plants or we are at another possible option
            //If we have another option, activate the next
            if (shedManager.shedPlants.ContainsKey(currentPlant)) {
                //Check to see what level the plant is at (for which animation to play)
                int experience = shedManager.GetPlantFeature(currentPlant, "experience");
                if(experience < 15) {
                    //play small version animation
                    Models[i + 1].SetActive(true);
                } else if(experience >= 15 && experience < 25) {
                    //play medium animation
                    Models[i + 2].SetActive(true);
                } else {
                    //play full grown plant animation
                    Models[i].SetActive(true);
                }
            
                
            } else {
                //If there are no other options, reactivate the old model we have just deactivated
                i = oldModel;
                currentPlant = Models[i].name;
                int experience = shedManager.GetPlantFeature(currentPlant, "experience");
                if (experience < 15)
                {
                    //play small version animation
                    Models[i + 1].SetActive(true);
                }
                else if (experience >= 15 && experience < 25)
                {
                    //play medium animation
                    Models[i + 2].SetActive(true);
                }
                else
                {
                    //play full grown plant animation
                    Models[i].SetActive(true);
                }
            }

            Debug.Log("" + i);
        }
    }

    public void Prev() {

        //Same logic as with "Next()"
        if (i > 0) {

            int oldModel = i;
            Models[i].SetActive(false);
            Models[i + 1].SetActive(false);
            Models[i + 2].SetActive(false);
            Debug.LogError("[revious" + i);

            i = i - 3;
            string currentPlant = Models[i].name;
            Debug.Log("now its" + i);
            Debug.Log(currentPlant);

            while (shedManager.shedPlants.ContainsKey(currentPlant) == false && i > 0) {
                i = i - 3;
                currentPlant = Models[i].name;
                Debug.LogError(currentPlant);
            }
            
            if (shedManager.shedPlants.ContainsKey(currentPlant)) {
                int experience = shedManager.GetPlantFeature(currentPlant, "experience");
                if (experience < 15)
                {
                    //play small version animation
                    Models[i + 1].SetActive(true);
                }
                else if (experience >= 15 && experience < 25)
                {
                    //play medium animation
                    Models[i + 2].SetActive(true);
                }
                else
                {
                    //play full grown plant animation
                    Models[i].SetActive(true);
                }
            }
            else {
                //If there are no other options, reactivate the old model we have just deactivated
                i = oldModel;
                currentPlant = Models[i].name;
                int experience = shedManager.GetPlantFeature(currentPlant, "experience");
                if (experience < 15)
                {
                    //play small version animation
                    Models[i + 1].SetActive(true);
                }
                else if (experience >= 15 && experience < 25)
                {
                    //play medium animation
                    Models[i + 2].SetActive(true);
                }
                else
                {
                    //play full grown plant animation
                    Models[i].SetActive(true);
                }
            }
        }
    }
}