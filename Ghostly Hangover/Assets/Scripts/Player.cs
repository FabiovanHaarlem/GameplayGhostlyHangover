using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private NPC m_NPCScript;
    private GameObject m_Camera;
    private Renderer m_Renderer;

    private float m_MoveSpeed;
    [SerializeField]
    private float m_LifeEnergy;
    public float LifeEnergy
    {
        get
        {
            return m_LifeEnergy;
        }
    }
    private float m_CheckLifeEnergyTimer;

    void Start ()
    {
        m_MoveSpeed = 4f;
        m_NPCScript = null;
        m_LifeEnergy = 100f;
        m_Renderer = GetComponent<Renderer>();
        m_CheckLifeEnergyTimer = 0f;
    }
	
	
	void Update ()
    {
        Movement();
        m_CheckLifeEnergyTimer += Time.deltaTime;

        if (m_NPCScript != null)
        {
            TakeLifeFromNPC();
        }
        else if (m_NPCScript == null)
        {
            LoseLifeEnergy();
        }

        if (m_CheckLifeEnergyTimer >= 5f)
        {
            Color newBuilingColor = new Color(m_Renderer.material.color.r, m_Renderer.material.color.g, m_Renderer.material.color.b, m_LifeEnergy / 100f);
            m_Renderer.material.color = newBuilingColor;
            m_CheckLifeEnergyTimer = 0f;
        }

    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(transform.position.x,
            transform.position.y, transform.position.z + m_MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x,
            transform.position.y, transform.position.z - m_MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - m_MoveSpeed * Time.deltaTime,
            transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + m_MoveSpeed * Time.deltaTime,
            transform.position.y, transform.position.z);
        }

        transform.position = transform.position;
    }

    private void  PossesNPC(GameObject targetNPC)
    {
        targetNPC.layer = 10;
        transform.position = targetNPC.transform.position;
        transform.rotation = targetNPC.transform.rotation;
        targetNPC.transform.parent = transform;
        m_NPCScript = targetNPC.GetComponent<NPC>();
    }

    private void TakeLifeFromNPC()
    {
        float npcLifeLost = m_NPCScript.LoselifeEnergy();

        if (npcLifeLost > 0)
        {
            m_LifeEnergy += npcLifeLost;
        }   
        else
        {
            Destroy(m_NPCScript.gameObject);
            m_NPCScript = null;
        }
    }

    private void DestroyCurrentPossesedNPC()
    {
        Destroy(m_NPCScript.gameObject);
    }

    private void LoseLifeEnergy()
    {
        m_LifeEnergy -= Time.deltaTime;
    }

    private void SetCamera()
    {
        m_Camera.transform.position = transform.position;
    }

    private void OnTriggerStay(Collider objectCollider)
    {
        if (objectCollider.CompareTag("NPC"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {   
                if (m_NPCScript != null)
                {
                    DestroyCurrentPossesedNPC();
                }

                PossesNPC(objectCollider.gameObject);

                Color newBuilingColor = new Color(m_Renderer.material.color.r, m_Renderer.material.color.g, m_Renderer.material.color.b, 0.5f);
                m_Renderer.material.color = newBuilingColor;
            }
        }
    }
}
