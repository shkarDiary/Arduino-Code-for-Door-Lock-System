#include "HardwareSerial.h"
#ifndef NRF2401_H
#define NRF2401_H
#include <SPI.h>

#include <RF24.h>
#include <nRF24L01.h>
#include <Arduino.h>

RF24 radio(7, 8);
const byte address[6] = "0x7878787878";
char text[32];
class NRF2401Code {
private:

public:
  NRF2401Code() {}
  void init() {
    radio.begin();
    SPI.begin();  // Initiate  SPI bus
    Serial.begin(9600);
    Serial.print(radio.isChipConnected());
    radio.openWritingPipe(0x7878787878);
    radio.setPALevel(RF24_PA_MIN);
    radio.setDataRate(RF24_250KBPS);
  }
  void send(String message) {
    radio.stopListening();
    char charArray[message.length() + 1];
    message.toCharArray(charArray, message.length() + 1);
    radio.write(charArray, message.length() + 1);
  }
  void send(int message) {
    radio.stopListening();
    char charArray[2];
    sprintf(charArray, "%d", message);
    radio.write(&charArray, sizeof(charArray));
  }
  char* recive() {
    radio.startListening();
    if (radio.available()) {
      radio.read(text, sizeof(text));
      Serial.println(text);
    }
    return text;
  }
};

#endif