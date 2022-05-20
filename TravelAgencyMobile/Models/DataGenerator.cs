using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Android.Text.Style;
using Newtonsoft.Json;
using TravelAgency;

namespace TravelAgencyMobile.Models
{
   public static class DataGenerator
    {
        public static List<Hotel> GenerateHotels() { 
            return _hotels ?? (_hotels = new List<Hotel> { 
                new Hotel { Id = 1, Name = "Hotel 1", City = "Санкт-Петербург", Address = "ул. Пушкина, д. Колотушкина 28", Country = "Россия", Phone = "89" + GenerateNumbersString()},
                new Hotel { Id = 2, Name = "Hotel 2", City = "Москва", Address = "ул. Пушкина, д. Колотушкина 54", Country = "Россия", Phone = "89" + GenerateNumbersString()},
                new Hotel { Id = 3, Name = "Hotel 3", City = "Сочи", Address = "ул. Пушкина, д. Колотушкина 27", Country = "Россия", Phone = "89" + GenerateNumbersString()},
                new Hotel { Id = 4, Name = "Hotel 4", City = "Севастополь", Address = "ул. Пушкина, д. Колотушкина 58", Country = "Россия", Phone = "89" + GenerateNumbersString()},
                new Hotel { Id = 5, Name = "Hotel 5", City = "Уфа", Address = "ул. Пушкина, д. Колотушкина 19", Country = "Россия", Phone = "89" + GenerateNumbersString()},
                new Hotel { Id = 6, Name = "Hotel 6", City = "Челябинск", Address = "ул. Пушкина, д. Колотушкина 24", Country = "Россия", Phone = "89" + GenerateNumbersString()},
                new Hotel { Id = 7, Name = "Hotel 7", City = "Самара", Address = "ул. Пушкина, д. Колотушкина 76", Country = "Россия", Phone = "89" + GenerateNumbersString()},
                new Hotel { Id = 8, Name = "Hotel 8", City = "Тверь", Address = "ул. Пушкина, д. Колотушкина 32А", Country = "Россия", Phone = "89" + GenerateNumbersString()},
                new Hotel { Id = 9, Name = "Hotel 9", City = "Пермь", Address = "ул. Пушкина, д. Колотушкина 21Б", Country = "Россия", Phone = "89" + GenerateNumbersString()},
                new Hotel { Id = 10, Name = "Hotel 10", City = "Волгоград", Address = "ул. Пушкина, д. Колотушкина 1", Country = "Россия", Phone = "89" + GenerateNumbersString()},
            });
        }

        public static event Action VoucherProcessed;

        private static List<Hotel> _hotels;

        private static List<RestType> _restTypes;

        private static List<Voucher> _vouchers;

        public static List<RestType> GenerateRestTypes()
        {
            return _restTypes ?? (_restTypes = new List<RestType>
            {
                new RestType { Id = 1, Name = "Активный отдых", Description = "", Restrictions = ""},
                new RestType { Id = 2, Name = "Детский отдых", Description = "", Restrictions = ""},
                new RestType { Id = 3, Name = "Горнолыжнй тур", Description = "", Restrictions = ""},
                new RestType { Id = 4, Name = "Гастрономический тур", Description = "", Restrictions = ""},
            });
        }

        public static void AddVoucher(Voucher voucher)
        {
            if (_vouchers == null)
            {
                GetVouchers();
            }
            if (_vouchers.Any(v => v == voucher))
            {
                return;
            }

            _vouchers.Add(voucher);
            VoucherProcessed?.Invoke();
        }

        public static void ClearJSON()
        {
            if (!File.Exists(App.FileName))
            {
                return;
            }

            File.Delete(App.FileName);
        }

        public static void RemoveVoucher(Voucher voucher)
        {
            if (!_vouchers.Contains(voucher))
            {
                return;
            }
            _vouchers?.Remove(voucher);
            VoucherProcessed?.Invoke();
        }

        public static List<Voucher> GetVouchers()
        {
            if (_vouchers != null)
            {
                return _vouchers;
            }
            if (!File.Exists(App.FileName))
            {
                _vouchers = new List<Voucher>();
                return _vouchers;
            }

            var json = File.ReadAllText(App.FileName);
            _vouchers = JsonConvert.DeserializeObject<List<Voucher>>(json);
            return _vouchers;
        }

        public static void SerializeVouchers()
        {
            if (_vouchers == null)
            {
                return;
            }

            var json = JsonConvert.SerializeObject(_vouchers);
            File.WriteAllText(App.FileName, json);
        }

        public static string GenerateNumbersString(int length = 9) 
        {
            string resultString = "";
            for (int i = 0; i < length; i++) 
            {
                resultString += new Random().Next(9);
            }


            return resultString;
        } 
    }
}
