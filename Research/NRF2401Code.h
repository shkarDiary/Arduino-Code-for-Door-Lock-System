#ifndef NRF2401_H
#define NRF2401_H
#include <SPI.h>
#include "HardwareSerial.h"

#include <RF24.h>
#include <nRF24L01.h>
#include <Arduino.h>

RF24 radio(7, 8);
const byte address[6] = "00001";

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
    radio.openReadingPipe(1, address);
    radio.setPALevel(RF24_PA_MAX);
    radio.setDataRate(RF24_250KBPS);
    radio.setAutoAck(true);  // enable auto-acknowledgment
    radio.enableDynamicPayloads();  // enable dynamic payload length
    radio.setChannel(76);  // set the channel to 76
  }
  void send(String message) {
    radio.stopListening();
    char charArray[message.length() + 1];
    message.toCharArray(charArray, message.length() + 1);
    radio.write(charArray, message.length() + 1);
  }
  void send(int message) {
    radio.stopListening();
    radio.setPayloadSize(3);
    char charArray[2];
    sprintf(charArray, "%d", message);
    radio.write(&charArray, sizeof(charArray));
  }

  char text[32];
  char* recive() {
    radio.startListening();
    if (radio.available()) {
      radio.read(text, sizeof(text));
      text[radio.getPayloadSize()] = '\0';
    }
    return text;
  }
};

#endif