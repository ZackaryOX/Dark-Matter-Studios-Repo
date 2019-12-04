//PinNumbers
//Won't change unless I rewire arduino
const int _LED = 13;
const int _LDR = A0;
int _LDRlight;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  //Serial.setTimeout(50);
  pinMode(_LED, OUTPUT); //LED is output (Will send to unity)
  pinMode(_LDR, INPUT); //LDR is input (On/off lights)
}

void loop() {
  // put your main code here, to run repeatedly:
  _LDRlight = analogRead(_LDR); //Check if on or off reads amount of light in room
  //Serial.println(_LDRlight);
  if(_LDRlight <= 300){
  digitalWrite(_LED, HIGH);
  Serial.println("ON");
  }else{
    digitalWrite(_LED, LOW);
    Serial.println("OFF");
  }
 delay(100);
}
