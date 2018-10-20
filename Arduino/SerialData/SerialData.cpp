#include "Arduino.h"
#include "SerialData.h"
#include "SerialData_config.h"

SerialData::SerialData(HardwareSerial &serial) : Serial(serial) {
	HeaderQueuePosition = 0;
	MessageQueuePosition = 0;
}

void SerialData::Begin() {
	Serial.begin(115200);
}

void SerialData::Update() {
	//
	if(Serial.available() >= (sizeof(SerialHeader)+MESSAGE_SIZE) ) {
	
		//Get message header
		byte headerBuffer[sizeof(SerialHeader)];
				
		Serial.readBytes(headerBuffer, sizeof(SerialHeader));
		
		SerialHeader serialHeader;
		memcpy(&serialHeader, headerBuffer, sizeof(SerialHeader));
		
		//if queue is less than max number of messages and adding this message will not overflow message queue
		if( (HeaderQueuePosition < sizeof(SerialHeader)*MAXIMUM_QUEUE_MESSAGES) && ((MessageQueuePosition+MESSAGE_SIZE) <= MAXIMUM_QUEUE_BYTES) ) {
					
			//put into header queue
			memcpy(HeaderQueue+HeaderQueuePosition, &serialHeader, sizeof(SerialHeader));
			HeaderQueuePosition += sizeof(SerialHeader);
			
			//read number of message bytes
			byte messageBuffer[MESSAGE_SIZE];
			Serial.readBytes(messageBuffer, sizeof(messageBuffer));
			
			//put message bytes into queue
			memcpy(MessageQueue+MessageQueuePosition, messageBuffer, sizeof(messageBuffer));
			MessageQueuePosition += MESSAGE_SIZE;
			
			delay(100);
			//Send a 1 in acknowledgement and successful reading
			SerialHeader ackHeader('@');
			long ack = 1;
			this->Write(ackHeader, &ack, sizeof(ack));
						
		}
		else {
			//read serial bytes and disregard
			byte messageBuffer[MESSAGE_SIZE];
			Serial.readBytes(messageBuffer, sizeof(messageBuffer));

			//delay(100);
			//Send a 0 in acknowledgement and UNsuccessful reading
			//SerialHeader ackHeader('!');
			//long ack = 0;
			//this->Write(ackHeader, &ack, sizeof(ack));
		} 
		
	}
}

uint32_t SerialData::Available() {
	//return number of messages in queue	
	return HeaderQueuePosition/sizeof(SerialHeader);
}

bool SerialData::Peek(SerialHeader &Header) {
	if(this->Available() > 0) {
		//return header for message at front of queue
		memcpy(&Header, HeaderQueue, sizeof(SerialHeader));
		
		//delay(100);
		//SerialHeader ackHeader('!');
		//long ack = 2;
		//this->Write(ackHeader, &ack, sizeof(ack));
	
		return true;
	}
	return false;
}

bool SerialData::Read(SerialHeader &Header, const void* object, byte size) {
	
	
	//set header equal to header at front of queue
	memcpy(&Header, HeaderQueue, sizeof(SerialHeader));
		
	//set object byte array to message bytes at front of queue
	memcpy(&object, MessageQueue, sizeof(object));
	
	//remove message and header from fron of queues and advance queues
	memmove(HeaderQueue, HeaderQueue+sizeof(SerialHeader), sizeof(SerialHeader)*(MAXIMUM_QUEUE_MESSAGES-1));
	HeaderQueuePosition -= sizeof(SerialHeader);
	
	memmove(MessageQueue, MessageQueue+MESSAGE_SIZE, MAXIMUM_QUEUE_BYTES-MESSAGE_SIZE);
	MessageQueuePosition -= MESSAGE_SIZE;
	
	//delay(100);
	//Send a 1 in acknowledgement and successful reading
	//SerialHeader ackHeader('!');
	//long ack = 3;
	//this->Write(ackHeader, &ack, sizeof(ack));
	
	
	
	
	return true;
}
		
bool SerialData::Write(SerialHeader &Header, const void* object, byte size) {
	//Set Header.NumBytes to the size of the passed object
	Header.NumBytes = size;
	
	//create buffer for header and message
	byte writeBuffer[sizeof(SerialHeader)+MESSAGE_SIZE];
	
	//copy header to buffer
	memcpy(writeBuffer, &Header, sizeof(SerialHeader));
	
	//copy message to buffer
	memcpy(writeBuffer+sizeof(SerialHeader), object, MESSAGE_SIZE);
	
	//write buffer to serial
	Serial.write(writeBuffer, sizeof(writeBuffer));
	//Serial.flush();
	return true;
}