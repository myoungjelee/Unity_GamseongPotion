# GamseongPotion
 
# 🧪 Unity 감성 포션 (GamseongPotion)

![Unity](https://img.shields.io/badge/Unity-2022.x-black.svg?style=flat-square&logo=unity)
![VR](https://img.shields.io/badge/Platform-VR-red.svg?style=flat-square)
![URP](https://img.shields.io/badge/Render%20Pipeline-URP-blue.svg?style=flat-square)
![Development Status](https://img.shields.io/badge/Status-Completed-green.svg?style=flat-square)

마법사의 작업실에서 감성적인 포션을 제조하는 VR 인터랙티브 체험 게임입니다.

## 📋 프로젝트 개요

Unity를 기반으로 제작된 VR 포션 제작 시뮬레이터로, 플레이어가 마법사가 되어 다양한 재료를 조합하고 신비로운 포션을 만드는 몰입형 체험을 제공합니다.

### 🎯 주요 특징

- **🧙‍♀️ 마법사 체험**: 포션 제작과 연금술 시뮬레이션
- **🎮 VR 인터랙션**: 자연스러운 손동작으로 재료 조합
- **✨ 시각적 효과**: 포션 제작 시 화려한 파티클 이펙트
- **🎵 감성적 분위기**: 몰입도 높은 사운드와 음악
- **🏰 중세 판타지**: 마법사의 작업실 분위기 연출

## 🛠️ 기술 스택

### 엔진 & 렌더링
- **Unity 2022.x**
- **Universal Render Pipeline (URP)**
- **XR Interaction Toolkit**

### VR 지원
- **OpenXR** (크로스 플랫폼)
- **Meta Quest 시리즈**
- **SteamVR 호환 헤드셋**

### 주요 에셋 & 플러그인
- `AllIn1SpriteShader` - 2D 스프라이트 쉐이더
- `FluXY` - 유체 시뮬레이션
- `Hovl Studio` - 마법 이펙트
- `Card3D` - 3D 카드 시스템
- `WitchesCauldron` - 마녀의 가마솥

## 📁 프로젝트 구조

```
Assets/
├── 📂 Animations/       # 애니메이션 클립
├── 📂 Audios/          # 배경음악 및 효과음
├── 📂 Card3D/          # 3D 카드 시스템
├── 📂 Effects/         # 파티클 효과
├── 📂 FluXY/           # 유체 시뮬레이션
├── 📂 HovlStudio/      # 마법 이펙트 패키지
├── 📂 Materials/       # 머티리얼
├── 📂 Models/          # 3D 모델
├── 📂 Prefabs/         # 프리팹
├── 📂 Resources/       # 리소스 파일
├── 📂 Scenes/          # 게임 씬
├── 📂 Scripts/         # C# 스크립트
├── 📂 Textures/        # 텍스처 이미지
├── 📂 WitchesCauldron/ # 가마솥 시스템
├── 📂 XR/              # VR 관련 설정
└── 📂 XRI/             # XR Interaction
```

## 🎮 주요 기능

### 포션 제작 시스템
- 다양한 재료를 가마솥에 투입
- 실시간 유체 시뮬레이션
- 조합에 따른 다른 포션 생성
- 포션 완성 시 특별한 시각 효과

### VR 인터랙션
- 자연스러운 손 추적
- 물리 기반 객체 조작
- 직관적인 재료 조합
- 몰입감 있는 공간 이동

### 감성적 연출
- 마법사 작업실 분위기
- 부드러운 조명과 그림자
- 감성적 배경음악
- 포션 제작 시 만족감 있는 피드백

### 시각적 효과
- 가마솥 내부 유체 애니메이션
- 마법 파티클 시스템
- 포션별 고유한 색상과 이펙트
- 중세 판타지 UI/UX

## 🚀 시작하기

### 시스템 요구사항
- **OS**: Windows 10/11 (64-bit)
- **Unity**: 2022.x 이상
- **VR**: Meta Quest, Oculus Rift, SteamVR 지원 헤드셋
- **RAM**: 8GB 이상
- **GPU**: GTX 1060 이상 권장

### 설치 방법
1. 레포지토리 클론
```bash
git clone https://github.com/myoungjelee/Unity_GamseongPotion.git
```

2. Unity Hub에서 프로젝트 열기
3. VR 헤드셋 연결 및 설정
4. Build Settings에서 Android (Quest) 또는 Windows (PC VR) 선택
5. 빌드 후 실행

### 개발 환경 설정
```bash
# Unity 패키지 매니저에서 필요한 패키지 설치
- XR Interaction Toolkit
- Universal RP
- TextMeshPro
```

## 🎯 게임플레이

1. **작업실 둘러보기**: VR로 마법사의 작업실 탐험
2. **재료 수집**: 선반과 서랍에서 포션 재료 찾기
3. **가마솥 점화**: 마법의 불로 가마솥 가열
4. **재료 투입**: 순서와 타이밍에 맞춰 재료 넣기
5. **포션 완성**: 마법의 포션 완성 후 효과 감상

## 📊 개발 진행상황

- [x] VR 기본 시스템 구축
- [x] 포션 제작 시스템 완성
- [x] 유체 시뮬레이션 구현
- [x] 마법 이펙트 시스템
- [x] UI/UX 완성
- [x] 오디오 시스템 구현
- [x] 최적화 완료

## 📸 주요 씬

### 🏰 마법사의 작업실
- 중세 판타지 분위기의 작업실
- 각종 연금술 도구와 재료
- 신비로운 조명과 분위기

### 🧪 포션 제작
- 실시간 유체 시뮬레이션
- 재료별 고유한 반응
- 화려한 마법 이펙트

## 🤝 기여하기

포션 레시피 추가, 새로운 마법 효과, UI 개선 등의 아이디어가 있으시면 언제든지 이슈나 PR을 보내주세요!

## 📄 라이선스

이 프로젝트는 개인 학습 및 포트폴리오 목적으로 제작되었습니다.

### 사용된 외부 에셋
- AllIn1SpriteShader (Unity Asset Store)
- FluXY (Unity Asset Store)  
- Hovl Studio Effects (Unity Asset Store)
- Witches Cauldron (Unity Asset Store)

## 📞 연락처

**개발자**: myoungjelee  
**GitHub**: [@myoungjelee](https://github.com/myoungjelee)

---

*마법과 기술이 만나는 특별한 VR 체험을 선사합니다* ✨🧪