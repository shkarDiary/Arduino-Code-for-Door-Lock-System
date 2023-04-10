#include <SPI.h>
#include <nRF24L01.h>
#include <RF24.h>

RF24 radio(7, 8);

const byte address[6] = "00001";

void setup() {
  Serial.begin(9600);
  radio.begin();
  radio.openReadingPipe(0, 0x7878787878);
  radio.openWritingPipe(address);
  radio.setPALevel(RF24_PA_MAX);
  radio.setDataRate(RF24_250KBPS);
      radio.setAutoAck(true);  // enable auto-acknowledgment
    radio.enableDynamicPayloads();  // enable dynamic payload length
    radio.setChannel(76);  // set the channel to 76
  Serial.println(radio.isChipConnected());
}

void loop() {
  radio.startListening();
  if (radio.available()) {
    char text[32];
    radio.read(text, sizeof(text));
    Serial.println(text);
    memset(text, 0, sizeof(text));
  }
  readSerial();
}

void readSerial() {
  const int MAX_CHARS = 50;
  static char charBuffer[MAX_CHARS] = {'\0'};
  static int numChars = 0;
  
  if (Serial.available() > 0) {    // If there is data available on the serial port
    char myChar = Serial.read();  // Read the incoming byte
    
    if (myChar == '\n' || numChars >= MAX_CHARS-1 || myChar == '0' ) { // If we received a termination character or reached the end of the buffer
      radio.stopListening(); 
      radio.setPayloadSize(10); 
      radio.write(charBuffer, sizeof(charBuffer));// send the entire character array
      Serial.println("Sent: ");
      numChars = 0; // reset the character buffer
      memset(charBuffer, '\0', sizeof(charBuffer)); // clear the character buffer
      return;
    } else {
      charBuffer[numChars] = myChar;
      numChars++;
    }
  }
}

