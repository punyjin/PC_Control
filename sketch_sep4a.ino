#define BLYNK_TEMPLATE_ID   "TMPL6CjK7BLjh"
#define BLYNK_TEMPLATE_NAME "FANCONTROLLED"
#define BLYNK_AUTH_TOKEN    "btnB_MKx5eEheXvW96IvTnxxWhGWr6Cf"

#include <WiFi.h>
#include <WiFiClient.h>
#include <BlynkSimpleEsp32.h>

char ssid[] = "Warinthorn";
char pass[] = "00000000";

#define RELAY1 18
#define RELAY2 25
#define RELAY3 26
#define BUTTON_PIN 19 // ปุ่ม physical
#define VPIN_MAIN_SWITCH V0
#define VPIN_MODE_SELECT V5
#define VPIN_TERMINAL   V10
#define RELAY_ON  LOW
#define RELAY_OFF HIGH

WidgetTerminal terminal(VPIN_TERMINAL);

bool mainSwitch = false;
int currentMode = 1; // default 1

void debugLog(String msg) {
  Serial.println(msg);
  terminal.println(msg);
  terminal.flush();
}

void setRelays(int mode) {
  if (!mainSwitch || mode == 0) { // ป้องกัน mode=0
    digitalWrite(RELAY1, RELAY_OFF);
    digitalWrite(RELAY2, RELAY_OFF);
    digitalWrite(RELAY3, RELAY_OFF);
    debugLog("⏹ Switch หลักปิดหรือ mode=0 → ปิดการใช้งานทั้งหมด");
  } else if (mode == 1) {
    digitalWrite(RELAY1, RELAY_ON);
    digitalWrite(RELAY2, RELAY_OFF);
    digitalWrite(RELAY3, RELAY_OFF);
    debugLog("▶ เบอร์ 1: Relay1 ON");
  } else if (mode == 2) {
    digitalWrite(RELAY1, RELAY_OFF);
    digitalWrite(RELAY2, RELAY_ON);
    digitalWrite(RELAY3, RELAY_OFF);
    debugLog("▶ เบอร์ 2: Relay2 ON");
  } else if (mode == 3) {
    digitalWrite(RELAY1, RELAY_OFF);
    digitalWrite(RELAY2, RELAY_OFF);
    digitalWrite(RELAY3, RELAY_ON);
    debugLog("▶ เบอร์ 3: Relay3 ON");
  } else {
    digitalWrite(RELAY1, RELAY_OFF);
    digitalWrite(RELAY2, RELAY_OFF);
    digitalWrite(RELAY3, RELAY_OFF);
    debugLog("▶ ไม่ทราบความเร็ว → ปิดทั้งหมด");
  }
  debugLog("สถานะหลัง set: Relay1=" + String(digitalRead(RELAY1)) + ", Relay2=" + String(digitalRead(RELAY2)) + ", Relay3=" + String(digitalRead(RELAY3)));
}

BLYNK_CONNECTED() {
  Blynk.syncVirtual(VPIN_MAIN_SWITCH, VPIN_MODE_SELECT);
  if (currentMode == 0) {
    currentMode = 1;
    Blynk.virtualWrite(VPIN_MODE_SELECT, 1);
    debugLog("Force mode=1 to prevent invalid mode");
  }
  debugLog("✅ เชื่อมต่อ Blynk สำเร็จ!");
}

BLYNK_WRITE(VPIN_MAIN_SWITCH) {
  mainSwitch = param.asInt();
  debugLog("Main Switch => " + String(mainSwitch ? "ON" : "OFF"));
  setRelays(currentMode);
}

BLYNK_WRITE(VPIN_MODE_SELECT) {
  currentMode = param.asInt();
  debugLog("Mode Selected => " + String(currentMode));
  if (currentMode == 0) { // ป้องกัน mode=0
    currentMode = 1;
    Blynk.virtualWrite(VPIN_MODE_SELECT, 1);
    debugLog("Invalid mode=0, force to mode=1");
  }
  setRelays(currentMode);
}

void setup() {
  Serial.begin(115200);
  pinMode(RELAY1, OUTPUT);
  pinMode(RELAY2, OUTPUT);
  pinMode(RELAY3, OUTPUT);
  pinMode(BUTTON_PIN, INPUT_PULLUP);
  digitalWrite(RELAY1, RELAY_OFF);
  digitalWrite(RELAY2, RELAY_OFF);
  digitalWrite(RELAY3, RELAY_OFF);
  
  Blynk.begin(BLYNK_AUTH_TOKEN, ssid, pass);
  debugLog("🚀 ESP32 Booting...");
  debugLog("PIN: " + String(RELAY1) + "," + String(RELAY2) + "," + String(RELAY3));
}

void loop() {
  Blynk.run();
  static unsigned long lastButtonTime = 0;
  if (digitalRead(BUTTON_PIN) == LOW && millis() - lastButtonTime > 300) {
    lastButtonTime = millis();
    mainSwitch = !mainSwitch;
    currentMode = mainSwitch ? 1 : 0;
    Blynk.virtualWrite(VPIN_MAIN_SWITCH, mainSwitch);
    Blynk.virtualWrite(VPIN_MODE_SELECT, currentMode);
    setRelays(currentMode);
    debugLog("Physical Button: Main Switch => " + String(mainSwitch ? "ON" : "OFF"));
  }
}