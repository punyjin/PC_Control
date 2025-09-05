// ====== ใส่ค่าจาก Blynk ของคุณ ======
#define BLYNK_TEMPLATE_ID   "TMPLxxxxxx"
#define BLYNK_TEMPLATE_NAME "ESP32 Relay"
#define BLYNK_AUTH_TOKEN    "YOUR_BLYNK_AUTH_TOKEN"
// ====================================

#define BLYNK_PRINT Serial  // เปิด log ทาง Serial Monitor (ไม่ใส่ก็ได้)

#include <WiFi.h>
#include <WiFiClient.h>
#include <BlynkSimpleEsp32.h>

// ---- Wi-Fi ของคุณ ----
char ssid[] = "YOUR_WIFI_SSID";
char pass[] = "YOUR_WIFI_PASSWORD";

// ---- ตั้งค่าขารีเลย์ ----
const uint8_t RELAY_PIN = 26;

// รีเลย์บางรุ่น "Active LOW" (สั่งติดด้วย LOW) บางรุ่น "Active HIGH"
// ตั้งค่านี้ให้ตรงกับโมดูลของคุณ:
const bool RELAY_ACTIVE_LEVEL = LOW; // ถ้ารีเลย์ของคุณติดเมื่อเขียน LOW ให้ใช้ LOW (ค่าเริ่มต้น)
                                     // ถ้าติดเมื่อเขียน HIGH ให้เปลี่ยนเป็น HIGH

// ฟังก์ชันช่วยให้เขียนสถานะรีเลย์ได้ง่าย
void relayWrite(bool on) {
  digitalWrite(RELAY_PIN, on ? RELAY_ACTIVE_LEVEL : !RELAY_ACTIVE_LEVEL);
}

// เมื่ออุปกรณ์ต่อกับ Blynk แล้ว ให้ sync ค่าปุ่มจากคลาวด์
BLYNK_CONNECTED() {
  Blynk.syncVirtual(V0);
}

// รับค่าจากปุ่มบนแอป/แดชบอร์ด Blynk (V0)
BLYNK_WRITE(V0) {
  int state = param.asInt(); // 1 = ON, 0 = OFF
  relayWrite(state == 1);
  // (ถ้าต้องการสะท้อนสถานะกลับไปยังวิดเจ็ตอื่น เช่น V1 ก็ทำได้)
  // Blynk.virtualWrite(V1, state);
}

void setup() {
  Serial.begin(115200);

  pinMode(RELAY_PIN, OUTPUT);
  // ตั้งค่าเริ่มต้น: ปิดรีเลย์ไว้ก่อน
  relayWrite(false);

  // เชื่อมต่อ Blynk Cloud
  Blynk.begin(BLYNK_AUTH_TOKEN, ssid, pass);
}

void loop() {
  Blynk.run();
}