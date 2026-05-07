using UnityEngine;
using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;     // TaskStatus

[Action("Custom/CallScriptFunction")]
public class CallScriptFunction : GOAction
{
    public MonoBehaviour targetScript; // Script a cui chiamare la funzione
    public string methodName;         // Nome della funzione da chiamare
    public string param;

    public override void OnStart()
    {
        if (targetScript == null || string.IsNullOrEmpty(methodName))
        {
            Debug.LogError("Target script o metodo non specificati!");
            return;
        }

        // Usa Reflection per chiamare il metodo specificato
        var method = targetScript.GetType().GetMethod(methodName);
        if (method != null)
        {
            method.Invoke(targetScript, null);
            Debug.Log($"Metodo {methodName} eseguito su {targetScript.name}");
        }
        else
        {
            Debug.LogWarning($"Metodo {methodName} non trovato in {targetScript.name}");
        }
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.COMPLETED; // Il nodo termina immediatamente
    }
}
