#ifndef Fingerprint_H
#define Fingerprint_H

#include "Arduino.h"


class Fingerprint {
private:
  uint8_t id;
  uint8_t readnumber(void);
public:
  Fingerprint() {}
  void init();
  int enrollState;
  int enroll(int id);
  uint8_t getFingerprintID();
  // returns -1 if failed, otherwise returns ID #
  int getFingerprintIDez();
  uint8_t fingerprintdelete(uint8_t id);
  uint8_t getFingerprintEnroll();
};

#endif