using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipticalOrbit : MonoBehaviour
{
    public GameObject sol;
    public float[] semiMajorAxes = { 57.91f, 108.21f, 149.60f, 227.92f, 778.57f, 1433.5f, 2872.46f, 4495.06f };
    public float fi = 0f; // Fase inicial
    public float omega = 1f; // Velocidade angular do Planeta
    public float[] planetAngularVelocities = new float[] { 6.14f, 1.85f, 1f, 0.5f, 0.8f, 0.3f, 0.1f, 0.06f }; // Velocidades angulares dos planetas em graus/s
    public float[] planetRotationSpeeds = new float[] { 30f, 20f, 10f, 15f, 25f, 35f, 40f, 45f }; // Velocidades de rotação dos planetas em graus/s
    public int planetIndex = 0; // Índice do planeta atual

    void Start()
    {
        // Divide o semieixo maior por 2 para ficar mais próximo do objeto "sol"
        float a = semiMajorAxes[planetIndex] / 2f;
        // Define a velocidade angular inicial com base na tabela
        omega = planetAngularVelocities[planetIndex] * Mathf.Deg2Rad;
    }

    void Update()
    {
        // Calcula a posição atual da órbita com base no tempo
        float t = Time.time;
        float a = semiMajorAxes[planetIndex] / 2f;
        float x = a * Mathf.Cos(omega * t + fi);
        float y = 0f; // Mantém a posição y fixa para ficar no mesmo plano horizontal
        float z = a * Mathf.Sin(omega * t + fi);
        t = t + 1;

        // Atualiza a posição do objeto na cena
        transform.position = sol.transform.position + new Vector3(x, y, z);

        // Verifica se o planeta atual chegou ao fim de sua órbita
        if (t >= 2 * Mathf.PI / omega)
        {
            // Incrementa o índice do planeta atual
            planetIndex++;
            planetIndex %= planetAngularVelocities.Length;

            // Divide o semieixo maior por 2 para ficar mais próximo do objeto "sol"
            a = semiMajorAxes[planetIndex] / 2f;
            // Define a nova velocidade angular com base na tabela
            omega = planetAngularVelocities[planetIndex] * Mathf.Deg2Rad;

            // Zera o tempo
            t = 0;
        }

        // Rotaciona o planeta em torno de seu próprio eixo (horizontalmente)
        float rotationSpeed = planetRotationSpeeds[planetIndex];
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
