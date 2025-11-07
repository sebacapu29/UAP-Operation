using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickUp : MonoBehaviour
{
    public KeyCode pickupKey = KeyCode.E;
    public Transform player;
    public Transform ObjectPickArea;
    public Transform handPosition;
    public float pickUpRange=3f;
    //public float fallingSpeed=2f;
    private bool isPicking=false;
    Vector3 velocity;

    private bool isDeposit = false; // controla que el objeto haya sido dejado en el lugar

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown(pickupKey)){
    //         if(Vector3.Distance(player.position, transform.position)<=pickUpRange){
    //             if (isPicking==false){
    //                 PickupObject();
    //             }else{
    //                 DropObject();
    //             }
    //         }
    //     }
    // }

    void Update()
    {
        if (Input.GetKeyDown(pickupKey)){
            if (!isDeposit && Vector3.Distance(player.position, transform.position) <= pickUpRange){
                if (!isPicking){
                    PickupObject();
                }else{
                    DropObject();
                }
            }
        }
    }


    void PickupObject(){
        isPicking=true;
        transform.position=handPosition.position - (ObjectPickArea.position - transform.position);
        transform.parent=player;
        transform.position=handPosition.position;
        transform.rotation=handPosition.rotation;
        velocity=Vector3.zero;
        //Cambiar animacion del Player a "CarryObject"
    }
    void DropObject(){
        isPicking=false;
        transform.parent=null;
        //Cambiar animacion del Player a "Walk"

    }

    public bool IsPicking()
    {
        return isPicking;
    }

    public void Drop()
    {
        DropObject();
    }

    public void IsDeposited()
    {
        isDeposit = true;
    }


}
