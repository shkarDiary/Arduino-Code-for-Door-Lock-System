#ifndef RFID_H
#define RFID_H

#define SS_PIN 10
#define RST_PIN 9

#include <Arduino.h>
  extern char arr[] ;

class RFID {
private:
  String content;
  byte SS_pin;
  byte RST_pin;
public:
  void init();
  char* getRFIDCode();
};

#endif