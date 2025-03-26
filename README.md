# READ ME
포트폴리오 PDF: [https://github.com/user-attachments/files/19460360/_.pdf]

WEB UI : [http://52.79.235.205/](https://52.79.235.205/) 

- WEB UI에서 엔진 실행 가능



시뮬레이션의 주요 로직에 관한 설명은 위 PDF를 참조해주시기 바랍니다. 

아래에는 인프라 구성, 코드 레벨의 프로젝트 구조,  객체나 함수의 역할 등에 대해 기술하겠습니다.  

# 프로젝트 구성

![image.png](attachment:3d89aedd-c885-4c44-9f3d-2e8d67746dfb:image.png)

# 1. 시뮬레이션

# **1.1 Data Mart**

           C#,  .Net Framework 4.7, Library 

## Input Mart

: 시뮬레이션시 사용할 Input 데이터를 정의하여 사용합니다.  DB로 부터 입력 받고 데이터 유효성 검사 등을 하여, 시뮬레이션 전 데이터 전처리를 할 수 있습니다.   

## Output Mart

: 시뮬레이션을 진행하면서 각종 통계 정보를 Output Mart에 수집합니다. 수집한 Output Mart는 엔진 종료시 DB에 INSERT 합니다. 

## **1.2 Simulation Engine**

         C#, .Net Framework 4.7, Library

: 시뮬레이션 엔진은 확장성과 재사용성을 고려한 모듈식 구조로 설계되어 다양한 공정 환경에 유연하게 적용될 수 있습니다.

## 주요 구성 요소

- **Base Entity:** 엔진에서 관리되는 기본 데이터 (모델에서 필수 구현 필요)
    - Equipment, Lot, Process, Product, Step
- **Simulation Entity:** Simulation 시 발생하는 추가 정보들 처리. Base Entity를 wrapping하여 사용
    - SimFactory : 시뮬레이션의 가장 상위 계층을 가지는 객체. Manager객체를 모두 가지고 있다.
    - SimEquipment, SimLot : 시뮬레이션 진행 중 동적인 상태 변화, 계획 정보 등을 가지게 되는 객체.
- **Manager Layer**: 설비, 작업물, 공정 등 주요 객체의 행동 관리
    - Schedule Manager: 이벤트 큐 관리 및 시간 진행 제어
    - Route Manager: 작업물의 공정 흐름 제어
    - Equipment Manager: 장비의 상태 변화 및 작업 수행 관리
    - Lot Manager: 작업물의 상태 관리
    - Process Manager: 공정 및 SETUP 작업의 시작/종료 처리
    - Offtime Manager: 유휴시간 관리
    - Dispatching Agent: 설비-작업물 매칭 관리
- **Simulation Interface:** 업종, 공장에 따라 모델에서 구현해야 하는 로직에 대한 인터페이스 제공

## 핵심 기능

- **이벤트 기반 시간 관리**: 다음 이벤트 시점으로 시간을 도약시켜 효율적 시뮬레이션
- **동적 상태 변화**: 이벤트 발생에 따른 객체 상태 변화 처리
- **병렬 처리**: 동시에 발생하는 여러 이벤트의 적절한 처리

## 1.3 Car Aps Model

          C#, .Net Framework 4.7, 콘솔 앱 

: 완성차 공장의 시뮬레이션 대상 요소 추출 및 커스텀 로직 구현. 로컬에서 디버깅 가능한 콘솔 앱. 

## 주요 모델링 요소

- LOT 20개
- 제품 2개
- 공정 : 프레스(Stamping), 차체조립(Welding), 도장(Paint), 의장(Assembly), 검수(Inspection)
- 설비: 프레스 3대, 차체조립 3대, 도장 3대, 의장 2대, 검수 2대

## 기타 커스텀 로직 구현

- 도장(Paint) 공정의 SETUP
- 일요일 마다 OFF 발생

## 1.4 Engine Trigger

         C#, .ASP .NET CORE 

: 웹에서 엔진을 실행시키기 위한 End Point. HTTP로 호출하면 엔진을 실행시킬 수 있다. 

# 2. WEB UI

: 기준정보 확인, 엔진 실행 후 생산 계획 결과 분석까지 가능한 WEB UI 

AWS Lambda를 사용한 Serverless 아키텍쳐 

![image.png](attachment:ac91e666-417a-4d5f-afbf-7ba427cadd94:image.png)

## Frontend

Vue3(typescript), Quasar(오픈소스 디자인 컴포넌트)  

## Backend

AWS Lamdbda (Node.js), API Gateway
