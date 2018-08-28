using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GooglePlayParser.Models
{
    /// <summary>
    /// Описывает модель приложения из GooglePlay.
    /// </summary>
    public class ApplicationModel
    {
        /// <summary>
        /// Название приложения.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Название пакета приложения.
        /// </summary>
        public string PackageName { get; set; }
        /// <summary>
        /// Иконка приложения.
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// Средняя оценка.
        /// </summary>
        public double Rating { get; set; }
        /// <summary>
        /// Количество оценок.
        /// </summary>
        public int RatingCount { get; set; }
        /// <summary>
        /// Количество установок.
        /// </summary>
        public string InstallCount { get; set; }
        /// <summary>
        /// Скриншоты приложения.
        /// </summary>
        public List<string> Screenshots { get; set; }
        /// <summary>
        /// Описание приложения.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Текст поля "Что нового".
        /// </summary>
        public string WhatsNew { get; set; }
        /// <summary>
        /// Email адрес разработчика.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Стоимость приложения (цена).
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// Цена внутренних покупок.
        /// </summary>
        public string InternalPrice { get; set; }
        /// <summary>
        /// Время последнего обновления записи.
        /// </summary>
        public string UpdateTime { get; set; }

        public bool Verify()
        {
            return !(this.PackageName == null || this.Name == null || this.InternalPrice == null
                || this.Description == null || this.Price == null || this.Screenshots == null
                || this.RatingCount == null || this.Rating == null || this.UpdateTime == null
                || this.InstallCount == null || this.Icon == null || this.Email == null
                || this.WhatsNew == null);
        }
    }
}