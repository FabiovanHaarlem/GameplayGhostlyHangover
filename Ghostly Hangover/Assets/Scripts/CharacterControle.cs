using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControle : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;
    private GameObject m_PossessedNPC;
    private GameObject m_CurrentCharacter;

    private float m_MoveSpeed;
	
	void Start ()
    {
        m_CurrentCharacter = m_Player;
        m_MoveSpeed = 4;
	}

    private void SwitchCharacter(GameObject newCharacter)
    {
        m_CurrentCharacter.SetActive(false);

        m_CurrentCharacter = newCharacter;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));

        Physics.Raycast(ray, out hit, Mathf.Infinity);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 50, Color.red);

        m_CurrentCharacter.transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
    }


    void Update ()
    {
		if (Input.GetKey(KeyCode.W)) { m_CurrentCharacter.transform.position = new Vector3(m_CurrentCharacter.transform.position.x,
            m_CurrentCharacter.transform.position.y, m_CurrentCharacter.transform.position.z + m_MoveSpeed * Time.deltaTime); }
        if (Input.GetKey(KeyCode.S)) { m_CurrentCharacter.transform.position = new Vector3(m_CurrentCharacter.transform.position.x,
            m_CurrentCharacter.transform.position.y, m_CurrentCharacter.transform.position.z - m_MoveSpeed * Time.deltaTime); }
        if (Input.GetKey(KeyCode.A)) { m_CurrentCharacter.transform.position = new Vector3(m_CurrentCharacter.transform.position.x - m_MoveSpeed * Time.deltaTime,
            m_CurrentCharacter.transform.position.y, m_CurrentCharacter.transform.position.z); }
        if (Input.GetKey(KeyCode.D)) { m_CurrentCharacter.transform.position = new Vector3(m_CurrentCharacter.transform.position.x + m_MoveSpeed * Time.deltaTime,
            m_CurrentCharacter.transform.position.y, m_CurrentCharacter.transform.position.z); }

        transform.position = m_CurrentCharacter.transform.position;
    }

    private void OnTriggerStay(Collider objectCollider)
    {
        if (objectCollider.CompareTag("NPC"))
        {
            Debug.Log("Trigger");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SwitchCharacter(objectCollider.gameObject);
            }
        }
    }
}
