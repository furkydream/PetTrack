using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetTrackDataAccess.Concretes;
using PetTrackDataAccess.Contexts;
using PetTrackDataAccess.Interfaces;

namespace PetTrackDataAccess.UnitOfWork
{
    // UnitOfWork sınıfı, veritabanı işlemlerini bir bütün olarak yönetmek için tasarlanmıştır.
    // Amacı, birden fazla repository işlemini tek bir "iş birimi" (unit of work) altında toplayıp,
    // hepsini tek bir veritabanı işlemi (transaction) ile gerçekleştirmektir.
    // Bu, veri bütünlüğünü (atomicity) sağlar ve performansı artırır.
    // IUnitOfWork arayüzünü uygular, bu da Service katmanının somut sınıfa değil, arayüze bağımlı olmasını sağlar (Dependency Inversion).
    public class UnitOfWork : IUnitOfWork
    {
        // DbContext, veritabanı ile olan ana iletişim kapısıdır.
        // 'readonly' olarak işaretlenmiştir, yani sadece constructor'da atanabilir.
        private readonly PetDbContext _context;

        // --- Lazy Loading ile Repository'lerin Tanımlanması ---
        // Her bir repository, 'Lazy<T>' sınıfı ile sarmalanmıştır.
        // Lazy<T>'nin amacı:
        // 1. PERFORMANS: Repository nesnesi, ona gerçekten ihtiyaç duyulana kadar (ilk kez çağrılana kadar) oluşturulmaz.
        // 2. VERİMLİLİK: Nesne, yaşam döngüsü boyunca SADECE BİR KEZ oluşturulur.
        // 3. THREAD-SAFETY: Aynı anda birden çok thread'in erişmesi durumunda bile nesnenin sadece bir kez oluşturulacağını garanti eder.
        private readonly Lazy<IAlert> _alertRepo;
        private readonly Lazy<IActivityLog> _activityLogRepo;
        private readonly Lazy<IHealthRecord> _healthRecordRepo;
        private readonly Lazy<IPet> _petRepo;
        private readonly Lazy<IPetOwner> _petOwnerRepo;
        private readonly Lazy<ITrackerDevice> _trackerDeviceRepo;
        private readonly Lazy<IVetAppointment> _vetAppointmentRepo;

        // UnitOfWork sınıfı oluşturulduğunda çalışan metot (Constructor).
        // Dışarıdan (Dependency Injection ile) bir PetDbContext nesnesi alır.
        public UnitOfWork(PetDbContext context)
        {
            _context = context;

            // Lazy<T> nesneleri burada başlatılır.
            // Parantez içindeki '() => new AlertRepo(_context)' ifadesi,
            // "Bu repository'ye ihtiyaç duyulduğunda, bu kodu çalıştırarak onu oluştur" anlamına gelen bir fabrika metodudur.
            // Bu kod, repository'nin ilk çağrıldığı ana kadar ÇALIŞMAZ.
            _alertRepo = new Lazy<IAlert>(() => new AlertRepo(_context));
            _activityLogRepo = new Lazy<IActivityLog>(() => new ActivityLogRepo(_context));
            _healthRecordRepo = new Lazy<IHealthRecord>(() => new HealthRecordRepo(_context));
            _petRepo = new Lazy<IPet>(() => new PetRepo(_context));
            _petOwnerRepo = new Lazy<IPetOwner>(() => new PetOwnerRepo(_context));
            _trackerDeviceRepo = new Lazy<ITrackerDevice>(() => new TrackerDeviceRepo(_context));
            _vetAppointmentRepo = new Lazy<IVetAppointment>(() => new VetAppointmentRepo(_context));
        }

        // --- Repository'lerin Dışarıya Açılması ---
        // Her bir repository, bir property (özellik) olarak dış dünyaya sunulur.
        // '=> _alertRepo.Value;' ifadesi, bu property çağrıldığında Lazy<T> nesnesinin 'Value' özelliğini döndürür.
        // Eğer repository daha önce oluşturulmamışsa, '.Value' çağrısı constructor'da tanımladığımız '() => new AlertRepo(...)' kodunu tetikler,
        // nesneyi oluşturur ve geri döndürür.
        // Eğer zaten oluşturulmuşsa, mevcut nesneyi doğrudan döndürür.
        public IAlert Alert => _alertRepo.Value;
        public IActivityLog Log => _activityLogRepo.Value;
        public IHealthRecord HealthRecord => _healthRecordRepo.Value;
        public IPet Pet => _petRepo.Value;
        public IPetOwner PetOwner => _petOwnerRepo.Value;
        public ITrackerDevice trackerDevice => _trackerDeviceRepo.Value;
        public IVetAppointment VetAppointment => _vetAppointmentRepo.Value;

        // Bu metot, Unit of Work deseninin kalbidir.
        // O ana kadar Add, Update, Delete ile hafızada (Change Tracker) biriktirilen
        // TÜM değişiklikleri tek bir transaction olarak veritabanına gönderir.
        // Eğer işlemlerden herhangi biri başarısız olursa, tüm transaction geri alınır (rollback).
        // Bu, veritabanının tutarlılığını garanti eder.
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // IDisposable arayüzünden gelen bu metot, UnitOfWork nesnesi işini bitirdiğinde
        // yönetilmeyen kaynakları (bu durumda DbContext bağlantısı) serbest bırakmak için kullanılır.
        // 'using' bloğu ile kullanıldığında otomatik olarak çağrılır.
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
