using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class HomeScreen : MonoBehaviour
{
   void OnEnable(){
       SceneManager.LoadScene("HomeScreen", LoadSceneMode.Single);
   }
}
