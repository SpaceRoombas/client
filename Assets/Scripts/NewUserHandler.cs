using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Network.Interfaces;
using System;
using System.Collections.Generic;

public class NewUserHandler : MonoBehaviour, IRegisterHandler
{
    public TMP_InputField UsernameField;
    public TMP_InputField PasswordField;
    public TMP_InputField EmailField;
    public TMP_Dropdown monthDrop;
    public TMP_Dropdown dayDrop;
    public TMP_Dropdown yearDrop;
    public TMP_Text error;
    private DateTime currentDate = DateTime.Now;

    public void HandleRegister()
    {
        Debug.Log("Good Registration");

        SceneManager.LoadScene("Login");
    }

    public void HandleRegisterFailure(string reason)
    {
        error.SetText(reason);
    }

    public void AttemptRegister()
    {
        string user = UsernameField.text;
        string pass = PasswordField.text;
        string email = EmailField.text;

        if(!verifyAge())
        {
            return;
        }

        StartCoroutine(MatchmakingRequests.PerformRegistrationRequest(user, pass, email, this));
    }

    public bool verifyAge()
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
            return false;
        }

        Debug.Log(age);
        Debug.Log(currentDate.Year);
        Debug.Log(yearDrop.options[yearDrop.value].text);

        return true;
    }

    public void fillYearDrop()
    {
        List<String> years = new List<String>();
        for (int i = 1900; i <= currentDate.Year; i++)
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
