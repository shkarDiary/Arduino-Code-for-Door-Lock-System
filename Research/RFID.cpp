#include "RFID.h"
#include <MFRC522.h>
#include <Arduino.h>

MFRC522 mfrc522(SS_PIN, RST_PIN);  // Create MFRC522 instance.
char arr[32];

void RFID::init() {
  Serial.begin(9600);
  mfrc522.PCD_Init();  // Initiate MFRC522
  Serial.println("Approximate your card to the reader...");
}
char* RFID::getRFIDCode() {
  String content = "";
  for (int i = 0; i < 32; i++) {
    arr[i] = 0;
  }
  // Look for new cards
  if (!mfrc522.PICC_IsNewCardPresent()) {
    return;
  }
  // Select one of the cards
  if (!mfrc522.PICC_ReadCardSerial()) {
    return;
  }
  //Show UID on serial monitor
  for (byte i = 0; i < mfrc522.uid.size; i++) {
    Serial.print(mfrc522.uid.uidByte[i] < 0x10 ? " 0" : " ");
    Serial.print(mfrc522.uid.uidByte[i], HEX);
    content.concat(String(mfrc522.uid.uidByte[i] < 0x10 ? " 0" : " "));
    content.concat(String(mfrc522.uid.uidByte[i], HEX));
  }
  content.toCharArray((char*)arr, content.length() + 1);


  Serial.println(arr);
  return arr;
}