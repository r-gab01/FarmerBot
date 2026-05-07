using UnityEngine;

public class KalmanFilter : MonoBehaviour
{
    // Stato del sistema: posizione e velocità
    private Vector3 estimatedPosition;
    private Vector3 velocity;

    // Covarianza del sistema
    private float P = 1f; // Uncertainty in the prediction (posizione)
    private float R = 0.1f; // Noise in the measurement (misura posizione)
    private float Q = 0.001f; // Noise in the process (velocità)

    //la covarianza misura quanto le previsioni del filtro sono "affidabili" in relazione al rumore che può essere presente nel sistema e nei dati di misurazione.

    // Guadagno di Kalman
    private float K;

    // Tempo passato
    private float deltaTime = 0.1f;

    void Start()
    {
        estimatedPosition = transform.position;
        velocity = Vector3.zero;
    }


    public Vector3 ApplyKalmanFilter(Vector3 measuredPosition)
    {
        Debug.Log($" KalmanFilter attivo");
        // Predizione della posizione basata sulla velocità
        Vector3 predictedPosition = estimatedPosition + velocity * deltaTime;

        // Calcolo del guadagno di Kalman
        K = P / (P + R);

        // Correzione della posizione stimata con il rumore
        estimatedPosition = predictedPosition + K * (measuredPosition - predictedPosition);

        // Aggiorniamo la velocità
        velocity = (estimatedPosition - transform.position) / deltaTime;

        // Aggiorniamo la covarianza
        P = (1 - K) * P + Q;

        // Debug dei valori
        Debug.Log($"Predicted: {predictedPosition}, Measured: {measuredPosition}, Estimated: {estimatedPosition}");

        return estimatedPosition;
    }
}
