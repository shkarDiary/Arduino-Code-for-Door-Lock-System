#include "RFID.h"
#include "Fingerprint.h"
#include "NRF2401Code.h"


#define doorLockPin 1;
Fingerprint fingerprint;
RFID rfid;
NRF2401Code nrf2401code;

void setup() {
  Serial.begin(9600);

  nrf2401code.init();
  rfid.init();
  fingerprint.init();
}
int secondToLastF(char* recivedData) {
  int arrayLength = strlen(recivedData);   // Get the length of the char array
  char subCharArray[arrayLength];          // Create a new char array with the same length as the original
  for (int i = 0; i < arrayLength; i++) {  // take the second character to the last character
    subCharArray[i] = recivedData[i + 1];
  }
  return atoi(subCharArray);
}
bool doorLockApprove = false;
void loop() {
  char* recivedData = nrf2401code.recive();
  char* cpyData = recivedData;
  uint8_t secondToLast = secondToLastF(recivedData);
  if (cpyData[0] == '1') {
    String enrollState = String(fingerprint.enroll(secondToLast));  //return the state of enrollment and make the subCharArray intiger
    nrf2401code.send(enrollState);                                  //send the enroll state via nrf2401
  } else if (cpyData[0] == '2') {
    String p = String(fingerprint.fingerprintdelete(secondToLast));
    nrf2401code.send(p);  //send the enroll state via nrf2401
    p = "";
  }
  int fid = fingerprint.getFingerprintID();  //get the finger id
  if (fid > 0) {
    nrf2401code.send(String(fid));
  }
  char* rfidContent = rfid.getRFIDCode();
  fid = 0;
  if (rfidContent[0] != '\0'){
    nrf2401code.send(rfidContent);
    delay(500);
  }  // if the RFID is not null send it
  memset(rfidContent, '\0', sizeof(rfidContent));  // Clear the CharArray
  memset(cpyData, '\0', sizeof(cpyData));          // Clear the CharArray
  memset(recivedData, '\0', sizeof(recivedData));  //Clear the Char Array
}
