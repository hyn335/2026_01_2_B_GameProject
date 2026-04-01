using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;

public class DialogManager : MonoBehaviour
{
   public static DialogManager Instance { get; private set; }

    [Header("Dialog References")]
    [SerializeField] private DialogDatabaseSO dialogDatabase;

    [Header("UI References")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private Image portraitImage;  
    [SerializeField] private TextMeshProUGUI characteNameText;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private Button NextButton;

    [Header("Dialog settings")]
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private bool useTypewriterEffect = true;

    private bool isTyping = false;
    private Coroutine typingCoroutine;

    private DialogSO currentDialog;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
            return;

        }
        
        if (dialogDatabase != null)
        { 
            dialogDatabase.Initailize();     //초기화
        }

        else
        {
            Debug.LogError("Dialog Database is not assinged to Dialog Manager");

        }

        if ( NextButton != null)
        {
            NextButton.onClick.AddListener(NextDialog);   //버튼 리스너 등록
        }
        else
        {
            Debug.LogError("Next Button is Not assigned!");
        }
    }

    //ID로 대화 시작 
    public void StartDialog(int dialogId)
    {
        DialogSO Dialog = dialogDatabase.GetDialongByld(dialogId);
        if(Dialog != null)
        {
            StartDialog(Dialog);
        }
        else
        {
            Debug.LogError($"Dialog with ID {dialogId} not found!");
        }
    }

    //DialogSO로 대화 시작 
    public void StartDialog(DialogSO dialog)
    {
        if (dialog == null) return;

        currentDialog = dialog;
        ShowDialog();
        dialogPanel.SetActive(true);
    }
    
    public void ShowDialog()
    {

        Debug.Log(currentDialog.portraitPath);

        if (currentDialog == null) return;
        characteNameText.text = currentDialog.characterName;   //캐릭터 이름 설정 
       
        if(useTypewriterEffect)                //대화 텍스트 설정 부분 수정
        {
            StartTypingEffect(currentDialog.text);
        }
        else
        {
            dialogText.text = currentDialog.text;         // 대화 텍스트 설정
        }


        //초상화 설정 ( 새로운 부분)
        if (currentDialog.portrait != null)
        {
            portraitImage.sprite = currentDialog.portrait;
            portraitImage.gameObject.SetActive(true);
        }
        else if (!string.IsNullOrEmpty(currentDialog.portraitPath))
        {
            Sprite portrait = Resources.Load<Sprite>(currentDialog.portraitPath);
            if (portrait != null)
            {
                portraitImage.sprite = portrait;
                portraitImage.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning($"Portrait not found at path : {currentDialog.portraitPath}");
                portraitImage.gameObject.SetActive(false);

            }
        }
        else
        {
            portraitImage.gameObject.SetActive(false);
        }
    }

    public void CloseDialog()
    {
        dialogPanel.SetActive(false);
        currentDialog = null;
        StopTypingEffect(); 
    }



    public void NextDialog()
    {

        if (isTyping)
        {
            StopTypingEffect();
            dialogText.text = currentDialog.text;
            isTyping = false;
            return;
        }
            
        {
            
        }
        if (currentDialog != null && currentDialog.nextId > 0)
        {
            DialogSO nextDialog = dialogDatabase.GetDialongByld(currentDialog.nextId);
            if (nextDialog != null)
            {
                currentDialog = nextDialog;
                ShowDialog();
            }
            else
            {
                CloseDialog();
            }
        }
        else
        {
            CloseDialog();
        }
    }

    private IEnumerator TypeText(string text)
    {
        dialogText.text = "";
        foreach(char c in text)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(typingSpeed);

        }
        isTyping = false;
    }

    private void StopTypingEffect()
    {
        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
    }

    private void StartTypingEffect(string text)
    {
        isTyping = true;
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(text));
    }

    void Start()
    {
        CloseDialog();
        StartDialog(1);      //자동으로 첫 번째 대화 시작
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
