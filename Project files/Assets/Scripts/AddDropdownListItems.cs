using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class AddDropdownListItems : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Dropdown dropdownList = GetComponent<Dropdown>();
        IPHostEntry IPAddress = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress pos in IPAddress.AddressList)
            dropdownList.options.Add(new Dropdown.OptionData() { text = pos.ToString() });
        dropdownList.value = 0;
    }
}
