using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AgeVerification : MonoBehaviour
{
    public TMP_Dropdown monthDrop;
    public TMP_Dropdown dayDrop;
    public TMP_Dropdown yearDrop;

    public TMP_Text error;



    private DateTime currentDate = DateTime.Now;

    public void verifyAge()
    {
        string currentYear = currentDate.Year.ToString();
        string birthYear = yearDrop.options[yearDrop.value].text;

        int age = Int32.Parse(currentYear) - Int32.Parse(birthYear);
        string month = monthDrop.options[monthDrop.value].text;
        string day = dayDrop.options[dayDrop.value].text;
        int birthMonth = int.Parse(month);
        int birthDay = int.Parse(day);

        if (currentDate.Month < birthMonth || ((currentDate.Month == birthMonth) && (currentDate.Day < birthDay)))
        {
            age--;
        }

        if (age < 13)
        {
            error.text = "Registration is only allowed for users 13 years or older";
        }
        else
        {
            SceneManager.LoadScene("Game Area");
        }

        Debug.Log(age);
        Debug.Log(currentDate.Year);
        Debug.Log(yearDrop.options[yearDrop.value].text);

    }

    public void fillYearDrop()
    {
        List<String> years = new List<String>();
        for (int i = 1900; i<=currentDate.Year; i++)
        {
            years.Add(i.ToString());
        }
        yearDrop.ClearOptions();
        yearDrop.AddOptions(years);
    }

    private void Start()
    {
        fillYearDrop();
  
    }

}
