using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private float m_LifeEnergy;
	
	void Start ()
    {
        m_LifeEnergy = 20f;
	}
	
	
	void Update ()
    {
		
	}

    public float LoselifeEnergy()
    {
        m_LifeEnergy -= Time.deltaTime;

        if (m_LifeEnergy <= 0)
        {
            return 0;
        }
        else
        {
            return Time.deltaTime;
        }
    }
}
