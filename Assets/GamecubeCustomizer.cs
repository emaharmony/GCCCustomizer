using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UE.Email;

public class GamecubeCustomizer : MonoBehaviour
{
    public static GamecubeCustomizer Instance { get; private set; }
    [SerializeField] ColorPicker colorPicker;
    [SerializeField] Image frontShell;
    [SerializeField] Image backShell;
    [SerializeField] Image[] lBumper;
    [SerializeField] Image rBumper;
    [SerializeField] Image a, b, x, y, z, dir, start, lstick, cstick;
    [SerializeField] InputField emailInput;
    [SerializeField] Text errorMsg;
    [SerializeField] GameObject finish;
    [SerializeField] TMPro.TextMeshProUGUI selectedPart;
    Image[] selected = null;

    Color fshell, bshell, lbumperC, rbumperC, aC, bC, xC, yC, zC, dirC, startC, lstickC, cStickC;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        finish.SetActive(false);
        errorMsg.gameObject.SetActive(false);
        fshell = frontShell.color;
        bshell = backShell.color;
        lbumperC = lBumper[0].color;
        rbumperC = rBumper.color;
        aC = a.color;
        bC = b.color;
        xC = x.color;
        yC = y.color;
        zC = z.color;
        dirC = dir.color;
        startC = start.color;
        lstickC = lstick.color;
        cStickC = cstick.color;

        if (colorPicker != null)
        {
            colorPicker.onValueChanged.AddListener((c) => {
                if (selected[0] != null && c != null)
                {
                    for(int i = 0; i < selected.Length; i++)
                    {
                        if(selected[i] != null)
                            selected[i].color = c;
                    }
                }
            });

            colorPicker.gameObject.SetActive(false);
        }

    }

    public void ResetColors()
    {
        frontShell.color = fshell;
        backShell.color = bshell;
        lBumper[0].color = lBumper[1].color = lbumperC;
        rBumper.color = rbumperC;
        a.color = aC;
        b.color = bC;
        x.color = xC;
        y.color = yC;
        z.color = zC;
        dir.color = dirC;
        start.color = startC;
        lstick.color = lstickC;
        cstick.color = cStickC;

        selected = new Image[3];
        colorPicker.gameObject.SetActive(false);

    }
    
    public void SelectPart(string tag)
    {
        selected = new Image[3];
        selectedPart.text = tag;
        Debug.Log("Tag Selected: " + tag);
        GameObject[] go = GameObject.FindGameObjectsWithTag(tag);
        for(int g = 0; g < go.Length; g++)
        {
            selected[g] = go[g].GetComponent<Image>();
        }

        colorPicker.gameObject.SetActive(selected[0] != null);
    }

    public void FinishController()
    {
        if (emailInput.text == "")
        {
            emailInput.caretColor = Color.red;
            errorMsg.gameObject.SetActive(true);
            return;
        }
        emailInput.caretColor = Color.white;
        errorMsg.gameObject.SetActive(false);
        StartCoroutine(SendMail());
    }

    IEnumerator SendMail()
    {
        yield return new WaitForEndOfFrame();
        Email.SendEmail(emailInput.text, "emaharmony@gmail.com", "Controller Custom Order",
            " Email: " + emailInput.text + 
            "\n---------------------- " +
            "\nFront Shell: " + frontShell.color.ToString() +
            "\nBack Shell: " + backShell.color.ToString() +
            "\nLeft Bumper: " + lBumper[0].color.ToString() +
            "\nRight Bumper: " + rBumper.color.ToString() +
            "\nA: " + a.color.ToString() +
            "\nB: " + b.color.ToString() +
            "\nX: " + x.color.ToString() +
            "\nY: " + y.color.ToString() +
            "\nDir Pad: " + dir.color.ToString() +
            "\nStart: " + start.color.ToString() +
            "\nLeft Stick: " + lstick.color.ToString() +
            "\nc Stick: " + cstick.color.ToString() +
            "\n ---------------------- ", "smtp.gmail.com", "emaharmony@gmail.com", "jcbrllkqjxeyfkkg");

        yield return null;
        finish.SetActive(true);

        yield return new WaitForSeconds(3);
        //Brow.OpenURL("www.harmoniouzCustoms.com");
    }
    /*Application Email sending:
     *         MailMessage mail = new MailMessage();
        mail.To.Add("emaharmony@gmail.com");
        mail.From = new MailAddress(emailInput.text);
        mail.Subject = "Controller Custom Order";
        mail.Body = " ---------------------- \nFront Shell: " + frontShell.color.ToString() +
            "\nBack Shell: " + backShell.color.ToString() +
            "\nLeft Bumper: " + lBumper[0].color.ToString() +
            "\nRight Bumper: " + rBumper.color.ToString() +
            "\nA: " + a.color.ToString() +
            "\nB: " + b.color.ToString() +
            "\nX: " + x.color.ToString() +
            "\nY: " + y.color.ToString() +
            "\nDir Pad: " + dir.color.ToString() +
            "\nStart: " + start.color.ToString() +
            "\nLeft Stick: " + lstick.color.ToString() +
            "\nc Stick: " + cstick.color.ToString() +
            "\n ---------------------- ";

        yield return null;

      SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("emaharmony@gmail.com", "jcbrllkqjxeyfkkg") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        { return true; };
        smtpServer.Send(mail);
    */
}

