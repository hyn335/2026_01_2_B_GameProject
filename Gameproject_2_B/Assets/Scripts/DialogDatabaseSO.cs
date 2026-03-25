using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogDatabaseSO", menuName = "Dialog System/DialogDatabaseSO")]
public class DialogDatabaseSO : ScriptableObject
{
    public List<DialogSO> dialogs = new List<DialogSO>();

    private Dictionary<int, DialogSO> dialogsByld; // Ä³½ÌÀ» À§ÇÑ µñ¼Å³Ê¸® »ç¿ë

    // ÂüÁ¶ 0°³
    public void Initailize()
    {
        dialogsByld = new Dictionary<int, DialogSO>();

        foreach (var dialog in dialogs)
        {
            if (dialog != null)
            {
                dialogsByld[dialog.id] = dialog;
            }
        }
    }

    public DialogSO GetDialongByld( int id)
    {
        if (dialogsByld == null)
            Initailize();

        if(dialogsByld.TryGetValue(id, out DialogSO dialog))
        { 
            return dialog; 
        }
        return null;

    }
}
