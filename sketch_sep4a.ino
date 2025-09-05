#define BLYNK_TEMPLATE_ID "TMPL6CjK7BLjh"
#define BLYNK_TEMPLATE_NAME "FANCONTROLLED"
#define BLYNK_AUTH_TOKEN  "btnB_MKx5eEheXvW96IvTnxxWhGWr6Cf"

#include <WiFi.h>
#include <WiFiClient.h>
#include <BlynkSimpleEsp32.h>

// WiFi
char ssid[] = "Warinthorn";
char pass[] = "00000000";

// ขารีเลย์
#define RELAY1_PIN 4   // G4  -> IN1
#define RELAY2_PIN 0   // G0  -> IN2 (ระวังเรื่องบูต)
#define RELAY3_PIN 2   // G2  -> IN3 (ระวังเรื่องบูต)

// helper: เปิด/ปิดตาม Active-LOW
void setRelay(uint8_t pin, int on) {
  // Active-LOW: ON = LOW, OFF = HIGH
  digitalWrite(pin, on ? LOW : HIGH);
}

// Blynk write handlers
BLYNK_WRITE(V1) { setRelay(RELAY1_PIN, param.asInt()); }
BLYNK_WRITE(V2) { setRelay(RELAY2_PIN, param.asInt()); }
BLYNK_WRITE(V3) { setRelay(RELAY3_PIN, param.asInt()); }

void setup() {
  Serial.begin(115200);

  // ตั้งค่า I/O
  pinMode(RELAY1_PIN, OUTPUT);
  pinMode(RELAY2_PIN, OUTPUT);
  pinMode(RELAY3_PIN, OUTPUT);

  // เริ่มต้นให้ "ปิด" ทั้งหมดก่อน (HIGH สำหรับ Active-LOW)
  digitalWrite(RELAY1_PIN, HIGH);
  digitalWrite(RELAY2_PIN, HIGH);
  digitalWrite(RELAY3_PIN, HIGH);

  // สำคัญ: ช่วงบูตอย่าให้รีเลย์ดึง G0/G2 ลง
  // ถ้าบอร์ดรีเลย์ดึงลง ให้พิจารณาเปลี่ยนขา/วงจรตามคำแนะนำด้านบน

  Blynk.begin(BLYNK_AUTH_TOKEN, ssid, pass);
}

void loop() {
  Blynk.run();
}
