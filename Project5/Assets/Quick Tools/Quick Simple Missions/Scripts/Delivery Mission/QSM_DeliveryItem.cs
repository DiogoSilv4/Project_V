using UnityEngine;

public class QSM_DeliveryItem : MonoBehaviour
{
    [Tooltip("The name of the Item. Should match the name of an item in the Related mission.")]
    public string ItemName;

    [Tooltip("The amount of Items. Generally 1, but could be more for bundle items.")]
    public int Amount = 1;

    [Tooltip("The mission this item is related to.")]
    public QSM_DeliveryMissionControl RelatedMission;

    [Tooltip("Sound effect to player after the Item is picked up.")]
    //The audioclip we will play after the player picks up the item.
    public AudioClip PickupSound;

    [Tooltip("how loud will we notify the player")]
    //The volume of how loud it will play in the 3d game space
    [Range(1.0f, 10.0f)]
    public float Volume = 2.0f;

    private AudioSource _audio;

    public void OnEnable()
    {
        if (RelatedMission == null)
            Debug.LogWarning("There is no Mission related to this Deliver Area. Please create one or link it in the Inspector!");
    }

    public void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag != "Player")
            return;

        RelatedMission.PickupItem(ItemName, Amount);

        // If an AudioSource is present, and a PickupSound has been set, we play it
        if (_audio != null && PickupSound != null)
            _audio.PlayOneShot(PickupSound);

        //Add to inventory
        RelatedMission.AddItemUI();

        gameObject.SetActive(false);
    }
}
