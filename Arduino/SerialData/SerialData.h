#include "Arduino.h"
#include "SerialData_config.h"

struct SerialHeader {
	char Type;
	byte NumBytes;
	
	SerialHeader() {}
	
	SerialHeader(char type): Type(type), NumBytes(0) {}
};

class SerialData {
	private:
		HardwareSerial&	Serial;
		
		byte HeaderQueue[sizeof(SerialHeader)*MAXIMUM_QUEUE_MESSAGES];
		uint32_t HeaderQueuePosition;
		
		byte MessageQueue[MAXIMUM_QUEUE_BYTES];
		uint32_t MessageQueuePosition;
		
	public:
		SerialData(HardwareSerial &serial);
		
		void Begin();
		
		void Update();
		
		uint32_t Available();
		
		bool Peek(SerialHeader &Header);
		
		bool Read(SerialHeader &Header, const void* object, byte size);
		
		bool Write(SerialHeader &Header, const void* object, byte size);
	
};

