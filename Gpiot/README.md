# Controllers

## GPIO Controller

- Will set pin **2** to open with output method

http://192.168.1.150/activatepin?pin=2

- Will close pin **2**

http://192.168.1.150/deactivatepin?pin=2

- Will write **HIGH** to pin **2**

http://192.168.1.150/setpinvalue?pin=2&value=1

- Will write **LOW** to pin **2**

http://192.168.1.150/setpinvalue?pin=2&value=0

- Status of a pin
http://192.168.1.150/pinstatus?pin=2

Return object:
```JSON
{ 
  "Open":"False",
  "PinNumber":2,
  "Value":0
}
```


## Schedule controller

- Add schedule to the device

http://192.168.1.150/schedule
[POST] Body raw:
```JSON
{
    "Name": "Morning",
    "Pin": 2,
    "Start": "08:00", // UTC
    "Interval": "45" // Minutes
}
```