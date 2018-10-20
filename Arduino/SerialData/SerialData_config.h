#define MESSAGE_SIZE 50 //Ssize in bytes of message (does not include header)

#define MAXIMUM_QUEUE_MESSAGES 5 //Maximum number of messages to be held in queue
#define MAXIMUM_QUEUE_BYTES 200 //Maximum number of bytes for data queue
//NOTE: Total number of bytes used by queue will be = (sizeof(SerialHeader)* QUEUE_SIZE) + MAXIMUM_QUEUE_BYTES