#include <SerialData.h>

SerialData serialData(Serial);

void setup() {
  serialData.Begin();

}

void loop() {
  serialData.Update();

  while(serialData.Available()) {
    SerialHeader header;
    serialData.Peek(header);

    byte messageBuffer[MESSAGE_SIZE];
    serialData.Read(header, messageBuffer, sizeof(messageBuffer));

    delay(100);
    serialData.Write(header, messageBuffer, sizeof(messageBuffer));
  }

}
