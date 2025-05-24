[**🇹🇷 TR**](#tr) | [**🇺🇸 EN**](#en)

<a id="en"></a>
## EN

# ZType-Proto

ZType-Proto is a typing-based space shooter prototype where you defend against incoming asteroids and enemy ships by typing the words displayed on them.

---

## Game Overview

Players pilot a spaceship that locks onto and shoots down space objects by typing the associated word. As you destroy more objects, the game speeds up and spawn intervals shorten, increasing the challenge over time.

---

## Core Features

- **Typing Gameplay**: Destroy objects by accurately typing the words shown on them.  
- **Progressive Difficulty**: Movement speed and spawn rate ramp up as you rack up kills.  
- **Enemy Variety**:  
  - Small asteroids with short words  
  - Larger ships with longer words  
- **Visual Effects**: Explosions, camera shake, and dynamic laser trails.  
- **Scoring**: Points awarded proportional to word length.  
- **Health System**: Objects reaching the screen edge damage the player.  
- **Sound Effects**: Distinct SFX for shooting, impacts, and explosions.  
- **Theme Colors**: Switch between color themes for lasers and UI.

---

## Game Mechanics

- **Lock & Shoot**:  
  1. **Lock State**: Type the first letter of a word to target an object.  
  2. **Shoot State**: Complete the word to fire lasers and damage or destroy the target.  
- **HP = Word Length**: Each object’s health equals the number of letters in its word.  
- **Damage & Progression**:  
  - Missing objects reduces player health.  
  - Destroyed‐object count drives dynamic difficulty adjustments.

---

## Technical Architecture

- **State Pattern**: Player input and actions are managed via `LockState` and `ShootState`.  
- **Object Pooling**: Laser and VFX prefabs are pooled for optimal performance.  
- **Event-Driven**: Destruction, locking, and scoring use C# events for loose coupling.  
- **Progression Controller**: Monitors destruction count to adjust speed and spawn rate.

---

## Controls

- **Keyboard**: Type the exact letters of on-screen words to lock and shoot.  
- **Auto-Targeting**: Upon word completion, the next valid object is auto-selected.

---

## Settings

- **Audio**: Master, music, and SFX volume sliders.  
- **Language**: Toggle between English and Turkish word lists.  
- **Vibration**: Enable or disable device vibration feedback.

---

## Development

- **Engine & Language**: Unity (C#)  
- **Key Packages**:  
  - DOTween for smooth animations  
  - TextMeshPro for crisp text rendering  
  - Universal Render Pipeline for enhanced visuals  

---

## Access

Play the game online (WebGL) at:  
https://webbysoftinit.com/games/fatalX

Go ahead and try the game!

---

<a id="tr"></a>
## TR

# ZType-Proto

ZType-Proto, gelen asteroid ve düşman gemileri üzerlerindeki kelimeleri yazarak yok ettiğiniz bir yazım temelli uzay nişancı prototip oyunudur.

---

## Oyun Genel Bakış

Oyuncular bir uzay gemisini kontrol eder; ekranda sağdan kayan nesnelerin üzerindeki kelimeleri yazarak önce kilitlenir, sonra doğru harf vuruşlarında lazer fırlatır. Daha fazla nesne yok ettikçe oyun hızı artar ve yeni nesneler daha kısa aralıklarla spawn olur.

---

## Temel Özellikler

- **Yazım Oynanışı**: Nesneleri üzerlerindeki kelimeleri doğru yazarak yok edin  
- **Artan Zorluk**: Yok edilen nesne sayısına bağlı olarak hız ve spawn oranı artar  
- **Farklı Düşman Türleri**:  
  - Kısa kelimeli küçük asteroidler  
  - Uzun kelimeli büyük gemiler  
- **Görsel Efektler**: Patlamalar, kamera sarsıntısı ve lazer izleri  
- **Skor Sistemi**: Yazılan kelime uzunluğuna göre puan  
- **Can Sistemi**: Kaçırılan nesneler oyuncuya hasar verir  
- **Ses Efektleri**: Ateş, çarpma ve patlama SFX’leri  
- **Tema Renkleri**: Lazer ve UI için farklı renk temaları  

---

## Oynanış Mekaniği

- **Kilitleme ve Ateş**  
  1. **Kilitleme**: Kelimenin ilk harfini yazarak hedef seçimi  
  2. **Ateş**: Kelimeyi tamamlayarak lazerle hasar verme  
- **Can = Kelime Uzunluğu**: Her nesnenin canı, kelime uzunluğu kadar  
- **Hasar & İlerleme**:  
  - Kaçırılan nesneler oyuncuya hasar verir  
  - Yok edilen sayısı dinamik zorluk ayarını tetikler  

---

## Teknik Mimari

- **State Pattern**: `LockState` ve `ShootState` ile oyuncu durumu yönetimi  
- **Nesne Havuzlama**: Lazer ve VFX prefabları performans için havuzlanır  
- **Olay Tabanlı**: Yok etme, kilitleme ve skor işlemleri C# olaylarıyla yürütülür  
- **İlerleme Denetleyici**: `ProgressionController` yok edilen sayısına göre hız ve spawn oranını günceller  

---

## Kontroller

- **Klavye**: Ekrandaki kelimeleri yazarak kilitleme ve ateş  
- **Otomatik Hedefleme**: Kelime tamamlandığında bir sonraki uygun hedef seçilir  

---

## Ayarlar

- **Ses**: Master, müzik ve efekt ses düzeyleri  
- **Dil**: İngilizce ve Türkçe kelime listeleri arasında geçiş  
- **Titreşim**: Cihaz titreşimini açma/kapatma  

---

## Geliştirme

- **Motor & Dil**: Unity (C#)  
- **Kilit Paketler**:  
  - DOTween (animasyonlar)  
  - TextMeshPro (metin render)  
  - Universal Render Pipeline (grafikler)  

---

## Erişim

WebGL üzerinden oyunu şu adresten oynayın:  
https://webbysoftinit.com/games/fatalX

Hadi, oyunu deneyin!
