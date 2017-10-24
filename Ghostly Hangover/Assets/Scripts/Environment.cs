using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    private Renderer m_Renderer;
    private GameHandler m_GameHandler;


	void Start ()
    {
        GameHandler.m_Instance.m_LifeEnegryChangeEvent += ChangeTranparency;
        m_GameHandler = GameHandler.m_Instance;
        m_Renderer = GetComponent<Renderer>();
	}

    private void ChangeTranparency(float playerlifeEnergy)
    {
        Color newBuilingColor = new Color(m_Renderer.material.color.r, m_Renderer.material.color.g, m_Renderer.material.color.b, playerlifeEnergy / 100f);
        m_Renderer.material.color = newBuilingColor;
    }
}
