# Unity UI Framework + Automation Ready System

</br>
</br>

## 1. 기술 개요
- **이름:** Unity UI Framework + Automation Ready System  
- **목적:** 확장 가능한 UI 프레임워크 구현  
</br>

## 2. 주요 기능
- **Enum 기반 자동 바인딩:** `UI_Base.Bind<T>()`  
- **이벤트 시스템 추상화:** `UI_EventHandler`  
- **Popup Stack 구조:** `UIManager`  
- **GameObject 확장 메서드:** `UIBindExtension`  
- **UI 계층 분리:** `UI_Scene / UI_Popup`  
</br>

## 3. 기술 포인트
- **재사용성:** 모든 UI를 상속형 구조로 통합 관리  
- **자동화 호환성:** 외부 스크립트에서 UI 이벤트 트리거 가능  
- **유지보수성:** Enum 기반 이름 매칭으로 `Find` 과정 자동화  
</br>

## 4. 구조 요약
- **Core:**  
  - `UI_Base`  
  - `UIManager`  
  - `Popup` / `Scene` 관리  

- **Util:**  
  - UI 탐색  
  - 이벤트 바인딩  
  - 확장 메서드  
</br>

## 5. 프로젝트 적용 예시
- **[UI_Shop]**  
<p align="center">
<img width="445" height="257" alt="image" src="https://github.com/user-attachments/assets/2821e749-0a37-4e2b-a261-5f075ae9d014" />
<img width="445" height="257" alt="image" src="https://github.com/user-attachments/assets/84031931-8123-48c5-9ac6-657ae4809b03" />
</p>

- **[UI_MainScene]**  
<p align="center">
<img width="456" height="258" alt="image" src="https://github.com/user-attachments/assets/cd936c6d-c166-4dea-95d9-ee3813686524" />
<img width="456" height="262" alt="image" src="https://github.com/user-attachments/assets/4a485fa9-877f-4a0b-a610-46135dabfb5e" />
</p>

- **[UI_Game]**  
<p align="center">
<img width="456" height="260" alt="image" src="https://github.com/user-attachments/assets/9f2c5038-7b7a-4389-9c62-9b1abbc29a19" />
<img width="453" height="259" alt="image" src="https://github.com/user-attachments/assets/0b8202cf-dfc4-4091-be57-e5fb51a3797d" />
</p>

</br>

## 6. 활용 가능성
- UI **자동화 툴 개발** 기반으로 확장 가능  
- **대규모 모바일 UI 시스템**에 통합 가능  
