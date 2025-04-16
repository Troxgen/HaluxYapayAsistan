# Halux Yapay Asistan

**Halux Yapay Asistan**, C# ile geliştirilen, sesli komutlarla etkileşim sağlayabilen, yapay zeka tabanlı bir asistan uygulamasıdır. Bu proje, kullanıcıların günlük işlemlerini kolaylaştırmak için doğal dil işleme ve sesli yanıt sistemlerini birleştirir. Kullanıcılar, günlük hava durumu, yapılacak işler listesi, alarm kurma ve daha birçok işlevi sesli komutlarla yönetebilirler.

---

## 🚀 Başlangıç

**Halux Yapay Asistan**'ı projede kullanmaya başlamak için aşağıdaki adımları takip edebilirsiniz. Kurulum süreci basit olup, doğru yapılandırıldığında sesli komutlar ve yanıtlarla etkileşim sağlanabilir.

### 🛠️ Gereksinimler

- **C# .NET Core 3.1** veya üzeri
- **Microsoft Visual Studio 2019** veya üzeri
- **NuGet Paketleri**:
  - `System.Speech` (Sesli Komutlar ve Yanıtlar için)
  - `Newtonsoft.Json` (Veri İşleme ve Konfigürasyon)
  - `Microsoft.CognitiveServices.Speech` (İleri düzey doğal dil işleme ve sesli yanıtlar)

---

## 🌟 Özellikler

Halux Yapay Asistan, kullanıcılar için çok çeşitli işlevler sunar:

- **Sesli Komutlar**: Kullanıcılar, doğal dilde sesli komutlar ile asistanla etkileşimde bulunabilirler. Örneğin:
  - "Bugün hava nasıl?"
  - "Yapılacak işlerimi listele."
  - "Alarm kur 10 dakika sonra."
- **Yanıtları Dinleyin**: Asistan, verilen komutlara sesli yanıt verir, böylece kullanıcılar yanıtları kolayca duyabilirler.
- **Zamanlayıcılar ve Alarm**: Kullanıcılar, belirli bir zamanda alarm kurabilir veya hatırlatıcılar ekleyebilir.
- **Çoklu Dil Desteği**: Şu anda **Türkçe** ve **İngilizce** dillerini destekler.
- **Genişletilebilir**: Kullanıcılar yeni komutlar ekleyebilir veya mevcut komutları özelleştirebilir.

---

## ⚙️ Kullanılan Teknolojiler

Halux Yapay Asistan, bir dizi güçlü teknoloji ile geliştirilmiştir:

- **C#** ve **.NET Core**: Uygulamanın temel altyapısı.
- **SpeechRecognition API**: Sesli komutları algılamak için.
- **Microsoft.Speech.Synthesis**: Asistanın sesli yanıtlarını oluşturmak için.
- **Json.NET**: Verilerin işlenmesi ve yapılandırma yönetimi.
- **System.Threading**: Zamanlayıcılar ve asenkron işlemler için.

---

## 🗣️ Windows 10/11'de Ses Tanıma (Speech Recognition) Özelliğini Etkinleştirme

**Halux Yapay Asistan**'ın düzgün çalışabilmesi için Windows 10 veya Windows 11'de **Speech Recognition** özelliğinin etkinleştirilmesi gerekmektedir. Aşağıdaki adımlarla bu özelliği etkinleştirebilir ve kullanabilirsiniz.

### 📋 Adım Adım Kurulum

#### 1. **Windows Ses Tanıma Özelliğini Etkinleştirin**

- **Windows 10 ve 11**'de ses tanıma özelliğini etkinleştirmek için:
   1. **Ayarlar**'ı açın (Windows + I tuşlarına basarak).
   2. **Zaman ve Dil** (Time & Language) bölümüne gidin.
   3. **Konuşma** (Speech) sekmesine tıklayın.
   4. "Sesli komutları kullanmak için Ses Tanıma'yı etkinleştir" seçeneğini etkinleştirin.

#### 2. **Ses Tanıma Eğitimi Yapın**

   - Sesli komutların doğruluğunu artırmak için **Windows ses eğitimi** yapmanız önerilir:
     1. **Ayarlar** > **Konuşma** (Speech) sekmesine gidin.
     2. "Ses Tanıma Eğitimini Başlat" (Train your computer to better understand you) seçeneğine tıklayın.
     3. Ekrandaki talimatları takip ederek ses eğitimi yapın.

#### 3. **Dil ve Bölge Ayarlarını Kontrol Edin**

   - Sesli komutların doğru çalışabilmesi için dil ve bölge ayarlarını kontrol edin:
     1. **Ayarlar** > **Zaman ve Dil** > **Bölge** (Region) sekmesine gidin.
     2. **Dil** menüsünden **İngilizce (Amerika)** veya **İngilizce (Birleşik Krallık)**'ı seçin.

#### 4. **Sesli Komutlar için Ses Tanıma Uygulamasını Başlatın**

   1. Başlat menüsüne gidin ve "Ses Tanıma" (Speech Recognition) yazın.
   2. "Ses Tanıma" uygulamasını başlatın.
   3. Sesli komutları kullanmaya başlamadan önce uygulamanın yapılandırıldığından emin olun.

---

## 🌐 Dış Kaynaklar ve Yardım

Daha fazla bilgi ve kurulum rehberleri için aşağıdaki bağlantılara göz atabilirsiniz:

- [Windows Central: Speech Recognition Setup](https://www.windowscentral.com/how-setup-speech-recognition-windows-10) - Windows 10'da Ses Tanıma kurulumu hakkında ayrıntılı rehber.
- [The Windows Club: Speech Recognition in Windows 10/11](https://www.thewindowsclub.com/how-to-use-speech-recognition-in-windows-10) - Windows 10/11'de ses tanımayı kullanmak için adımlar.
- [MiniTool: Setup and Use Speech Recognition in Windows 10](https://www.minitool.com/news/speech-recognition-windows-10-11.html) - Windows 10 ve 11'de ses tanıma kurulum ve kullanım rehberi.

---

## ⚠️ Notlar

- **Dil Desteği**: Halux Yapay Asistan şu an için **Türkçe**'yi tam anlamıyla desteklememektedir. Sesli komutlar **yalnızca İngilizce** dilinde doğru çalışmaktadır. Bu nedenle, Windows'un dilini **İngilizce** olarak ayarlamanız gerekebilir.
- **Eğitim**: Sesli komutların doğruluğunu artırmak için **Windows'un ses tanıma eğitimini** yapmanız önemlidir.
- **Sesli Yanıtlar**: Bilgisayarınızın sesli yanıt verebilmesi için ses çıkış aygıtlarınızın düzgün çalıştığından emin olun.

---

## 📞 İletişim

Proje hakkında sorularınız veya geri bildirimleriniz için [github.com/Troxgen](https://github.com/Troxgen) üzerinden bana ulaşabilirsiniz.

---

## 📄 Lisans

Bu proje **MIT Lisansı** ile lisanslanmıştır. Detaylı bilgi için [LICENSE](LICENSE) dosyasına göz atabilirsiniz.

---

## 🌟 Projeyi Beğendiniz mi?

Eğer bu projeyi faydalı bulduysanız, lütfen yıldız verin ⭐. Herhangi bir öneri veya hata bildirimi için GitHub Issues üzerinden bana ulaşabilirsiniz.

---
