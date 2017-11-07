using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameHandler : MonoBehaviour
{
    
    public event Action<float> m_LifeEnegryChangeEvent;
    
    public static GameHandler m_Instance;

    private Player m_Player;

    private float m_ChecklifeEnergyTimer;



    void Awake()
    {
        m_Instance = this;
        m_Player = GameObject.Find("Player").GetComponent<Player>();
        m_ChecklifeEnergyTimer = 0f;
	}
	
	
	void Update ()
    {
        m_ChecklifeEnergyTimer += Time.deltaTime;
		if (m_ChecklifeEnergyTimer >= 5)
        {
            PlayerLifeEnergyChanged();
            m_ChecklifeEnergyTimer = 0;
        }
	}
    

    private void PlayerLifeEnergyChanged()
    {
        if (m_LifeEnegryChangeEvent != null)
            m_LifeEnegryChangeEvent(m_Player.LifeEnergy);
           
    }
}
