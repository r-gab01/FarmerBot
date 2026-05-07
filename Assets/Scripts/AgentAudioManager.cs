using UnityEngine;
using System.Collections.Generic;

public class AgentAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // AudioSource per riprodurre i suoni
    private Queue<AudioClip> audioQueue = new Queue<AudioClip>(); // Coda di AudioClip
    private bool isPlaying = false; // Indica se un audio è in riproduzione

    [SerializeField] private AudioClip plantingSound; // Suono per piantare una piantina
    [SerializeField] private AudioClip wateringSound; // Suono per innaffiare una piantina
    [SerializeField] private AudioClip notDrySound; // Suono per rimuovere una piantina secca
    [SerializeField] private AudioClip sickSound; // Suono per sistemare una piantina malata
    [SerializeField] private AudioClip matureSound; // Suono per sistemare una piantina matura
    [SerializeField] private AudioClip placeToSlotSound; // Suono per portare la piantina nell apposito capannone
    [SerializeField] private AudioClip placePlantSound; // Suono per sistemare una piantina
    [SerializeField] private AudioClip movePlantSound; // Suono per segnalare che una piantina è stata in modo improvviso
    [SerializeField] private AudioClip findPlantSound; // Suono pianta trovata
    [SerializeField] private AudioClip searchPlantSound; // Suono per la ricerca della pianta spostata
    [SerializeField] private AudioClip lowBatterySound; // Suono per ricaricare la batteria
    [SerializeField] private AudioClip lowWaterSound; // Suono per ricaricare l'acqua nel serbatoio
    [SerializeField] private AudioClip waitModeSound; // Suono che attiva la modalità attesa


    // Aggiunge un audio alla coda
    public void EnqueueAudio(AudioClip clip)
    {
        if (clip != null)
        {
            audioQueue.Enqueue(clip);
            PlayNextInQueue();
        }
        else
        {
            Debug.LogWarning("Tentativo di aggiungere un AudioClip nullo alla coda!");
        }
    }

    // Riproduciamo il prossimo audio nella coda
    private void PlayNextInQueue()
    {
        if (!isPlaying && audioQueue.Count > 0)
        {
            AudioClip nextClip = audioQueue.Dequeue(); // Ottieniamo il prossimo clip
            audioSource.PlayOneShot(nextClip);        // Riproduciamo l'audio
            isPlaying = true;

            // Avviamo un Coroutine per monitorare la fine del suono
            StartCoroutine(WaitForAudioToEnd(nextClip.length));
        }
    }

    // Coroutine per aspettare la fine del suono
    private System.Collections.IEnumerator WaitForAudioToEnd(float duration)
    {
        yield return new WaitForSeconds(duration);
        isPlaying = false; // Segna che l'audio è terminato
        PlayNextInQueue(); // Riproduciamo il prossimo audio
    }

    // aggiungiamo gli audio alla coda
    public void PlayPlantingSound()
    {
        EnqueueAudio(plantingSound);
    }

    public void PlayWateringSound()
    {
        EnqueueAudio(wateringSound);
    }

    public void PlayNotDrySound()
    {
        EnqueueAudio(notDrySound);
    }

    public void PlaceToSlotSound()
    {
        EnqueueAudio(placeToSlotSound);
    }

    public void PlacePlantSound()
    {
        EnqueueAudio(placePlantSound);
    }

    public void MovePlantSound()
    {
        EnqueueAudio(movePlantSound);
    }

    public void FindPlantSound()
    {
        EnqueueAudio(findPlantSound);
    }

    public void SearchPlantSound()
    {
        EnqueueAudio(searchPlantSound);
    }

    public void PlaySickSound()
    {
        EnqueueAudio(sickSound);
    }

    public void PlayMatureSound()
    {
        EnqueueAudio(matureSound);
    }

    public void LowBatterySound()
    {
        EnqueueAudio(lowBatterySound);
    }

    public void LowWaterSound()
    {
        EnqueueAudio(lowWaterSound);
    }

    public void WaitSound()
    {
        EnqueueAudio(waitModeSound);
    }
    
}

