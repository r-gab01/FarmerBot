using UnityEngine;
using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine.AI;

[Action("MyNode/GoToPoint")]
public class GoToPoint : GOAction
{
    [InParam("agentMap")]
    public AgentMap agentMap;

    [InParam("pointName")]
    public string pointName;

    [InParam("agent")]
    public NavMeshAgent agent;

    [InParam("animator", DefaultValue = null)]
    public Animator animator;

    [InParam("agentStatistics", DefaultValue = null)]
    public AgentStatistics agentStatistics;

    [InParam("agentAudioManager")]
    public AgentAudioManager agentAudioManager;

    private Vector3? destination;

    [OutParam("direction")]
    public Transform direction;

    // Parametri per il rumore
    private float noiseIntensity = 0.15f;  // Intensità massima del rumore
    private float noiseFrequency = 0.8f;  // Frequenza del rumore
    private float noiseChance = 0.15f;     // Probabilità di applicare rumore
    private float oscillationOffset = 0f; // Per oscillazioni continue

    private bool applyNoise = false;      // Stato del rumore attivo
    private float noiseTimer = 0f;        // Timer per gestire intervalli casuali

    public override void OnStart()
    {
        if (agentMap == null)
        {
            Debug.LogError("AgentMap non assegnato!");
            return;
        }

        destination = agentMap.GetPosition(pointName);
        direction = agentMap.GetDirection(pointName);

        if (destination.HasValue)
        {
            agent.SetDestination(destination.Value);
            if (pointName == "start")
            {
                if (agentAudioManager != null)
                {
                    agentAudioManager.WaitSound();
                }
            }

            if (animator != null)
                animator.SetBool("Walking", true);

            if (agentStatistics != null)
                agentStatistics.setIsMoving(true);
        }
        else
        {
            Debug.LogWarning($"Destinazione '{pointName}' non valida.");
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (destination.HasValue && agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            if (animator != null)
                animator.SetBool("Walking", false);

            if (agentStatistics != null)
                agentStatistics.setIsMoving(false);

            return TaskStatus.COMPLETED;
        }

        noiseTimer += Time.deltaTime;

        // Ogni 2 secondi, valutiamo se applicare il rumore
        if (noiseTimer > 2f)
        {
            applyNoise = Random.value < noiseChance; 
            noiseTimer = 0f; // Resetta il timer
        }

        // Movimento con rumore applicato solo se attivo
        Vector3 noisyTarget = destination.Value + (applyNoise ? GetVariableNoiseOffset() : Vector3.zero);

        agent.SetDestination(noisyTarget);

        return TaskStatus.RUNNING;
    }


    private Vector3 GetVariableNoiseOffset()
    {
        Debug.Log($" Aggiunta di rumore al movimento del robot");
        // Incrementa l'oscillazione
        oscillationOffset += Time.deltaTime * noiseFrequency;

        float noiseMultiplier = Random.Range(0f, noiseIntensity); // Rumore casuale variabile
        float noiseX = RandomGaussian() * noiseMultiplier;        // Rumore su X
        float noiseZ = RandomGaussian() * noiseMultiplier;        // Rumore su Z

        // Introduci una piccola oscillazione continua
        noiseX += Mathf.Sin(oscillationOffset) * (noiseIntensity / 2f);
        noiseZ += Mathf.Cos(oscillationOffset) * (noiseIntensity / 2f);

        return new Vector3(noiseX, 0f, noiseZ);
    }

    private float RandomGaussian()
    {
        // Generazione del rumore con distribuzione gaussiana
        float u1 = Random.value;
        float u2 = Random.value;

        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
                              Mathf.Sin(2.0f * Mathf.PI * u2);

        return randStdNormal;
    }
}
